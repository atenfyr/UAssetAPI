using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace UAssetAPI
{
    public class NameMapOutOfRangeException : FormatException
    {
        public FString RequiredName;

        public NameMapOutOfRangeException(FString requiredName) : base("Requested name \"" + requiredName + "\" not found in name map")
        {
            RequiredName = requiredName;
        }
    }

    public class UAsset
    {
        /* Public Methods */
        public string FilePath;
        public bool WillWriteExportData = true;
        public bool WillStoreOriginalCopyInMemory = false;
        public bool UseSeparateBulkDataFiles = false;
        public byte[] OriginalCopy;
        public List<int> UExpData;
        public Guid AssetGuid;
        public UE4Version EngineVersion;

        public bool VerifyParsing()
        {
            MemoryStream f = this.PathToStream(FilePath);
            f.Seek(0, SeekOrigin.Begin);
            MemoryStream newDataStream = WriteData(new BinaryReader(f));
            f.Seek(0, SeekOrigin.Begin);

            if (f.Length != newDataStream.Length) return false;

            const int CHUNK_SIZE = 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            byte[] buffer2 = new byte[CHUNK_SIZE];
            int lastRead1;
            while ((lastRead1 = f.Read(buffer, 0, buffer.Length)) > 0)
            {
                int lastRead2 = newDataStream.Read(buffer2, 0, buffer2.Length);
                if (lastRead1 != lastRead2) return false;
                if (!buffer.SequenceEqual(buffer2)) return false;
            }

            return true;
        }

        public IReadOnlyList<FString> GetNameMapIndexList()
        {
            return nameMapIndexList.AsReadOnly();
        }

        public void ClearNameIndexList()
        {
            nameMapIndexList = new List<FString>();
            nameMapLookup = new Dictionary<int, int>();
        }

        public void SetNameReference(int index, FString value)
        {
            nameMapIndexList[index] = value;
            nameMapLookup[value.GetHashCode()] = index;
        }

        public FString GetNameReference(int index)
        {
            if (index < 0) return new FString(Convert.ToString(-index));
            if (index > nameMapIndexList.Count) return new FString(Convert.ToString(index));
            return nameMapIndexList[index];
        }

        public FString GetNameReferenceWithoutZero(int index)
        {
            if (index <= 0) return new FString(Convert.ToString(-index));
            if (index > nameMapIndexList.Count) return new FString(Convert.ToString(index));
            return nameMapIndexList[index];
        }

        public bool NameReferenceContains(FString search)
        {
            return nameMapLookup.ContainsKey(search.GetHashCode());
        }

        public int SearchNameReference(FString search)
        {
            if (NameReferenceContains(search)) return nameMapLookup[search.GetHashCode()];
            throw new NameMapOutOfRangeException(search);
        }

        public int AddNameReference(FString name)
        {
            if (nameMapLookup.ContainsKey(name.GetHashCode())) return SearchNameReference(name);
            nameMapIndexList.Add(name);
            nameMapLookup.Add(name.GetHashCode(), nameMapIndexList.Count - 1);
            return nameMapIndexList.Count - 1;
        }

        public FName GetImportObjectName(int index)
        {
            return index < 0 ? GetImportAt(index).ObjectName : new FName(Convert.ToString(index));
        }

        public Import GetImportAt(int index)
        {
            int normalIndex = UAPUtils.GetNormalIndex(index);
            if (normalIndex < 0 || normalIndex >= Imports.Count) return null;
            return Imports[normalIndex];
        }

        public Import AddImport(string bbase, string bclass, int link, string property)
        {
            Import nuevo = new Import(bbase, bclass, link, property, UAPUtils.GetImportIndex(Imports.Count));
            Imports.Add(nuevo);
            return nuevo;
        }

        public int AddLink(Import li)
        {
            li.Index = UAPUtils.GetImportIndex(Imports.Count);
            Imports.Add(li);
            return li.Index;
        }

        public BlueprintGeneratedClassExport GetBlueprintGeneratedClassExport()
        {
            foreach (Export cat in Exports)
            {
                if (cat is BlueprintGeneratedClassExport bgcCat) return bgcCat;
            }
            return null;
        }
        
        public FName GetBGCName()
        {
            var bgcCat = GetBlueprintGeneratedClassExport();
            if (bgcCat == null || bgcCat.ReferenceData == null) return null;

            return bgcCat.ReferenceData.ObjectName;
        }

        public void GetParentClass(out FName parentClassPath, out FName parentBGCName)
        {
            parentClassPath = null;
            parentBGCName = null;

            var bgcCat = GetBlueprintGeneratedClassExport();
            if (bgcCat == null) return;

            Import parentClassLink = GetImportAt(bgcCat.BaseClass);
            if (parentClassLink == null) return;
            if (parentClassLink.OuterIndex >= 0) return;

            parentBGCName = parentClassLink.ObjectName;
            parentClassPath = GetImportAt((int)parentClassLink.OuterIndex).ObjectName;
        }

        public int SearchForLink(FName classPackage, FName className, int outerIndex, FName objectName)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (classPackage == Imports[i].ClassPackage
                    && className == Imports[i].ClassName
                    && outerIndex == Imports[i].OuterIndex
                    && objectName == Imports[i].ObjectName)
                {
                    return currentPos;
                }

            }

            return 0;
        }

        public int SearchForLink(FName classPackage, FName className, FName objectName)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (classPackage == Imports[i].ClassPackage
                    && className == Imports[i].ClassName
                    && objectName == Imports[i].ObjectName)
                {
                    return currentPos;
                }

            }

            return 0;
        }

        public int SearchForLink(FName objectName)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (objectName == Imports[i].ObjectName) return currentPos;
            }

            return 0;
        }

        /* End Public Methods */

        internal int headerSize = 0;
        internal int sectionSixOffset = 0;
        internal int sectionOneStringCount = 0;
        internal int dataCategoryCount = 0;
        internal int sectionThreeOffset = 0;
        internal int sectionTwoLinkCount = 0;
        internal int sectionTwoOffset = 0;
        internal int sectionFourOffset = 0;
        internal int sectionFiveStringCount = 0;
        internal int sectionFiveOffset = 0;
        internal int uexpDataOffset = 0;
        internal int gapBeforeUexp = 0;
        internal int fileSize = 0;
        internal bool doWeHaveSectionFour = true;
        internal bool doWeHaveSectionFive = true;

        // Do not directly add values to here under any circumstances; use AddNameReference instead
        internal List<FString> nameMapIndexList;
        private Dictionary<int, int> nameMapLookup = new Dictionary<int, int>();

        public List<CustomVersion> CustomVersionContainer;
        public List<Import> Imports; // base, class, link, connection
        public List<int[]> ExportIntReference;
        public List<string> ExportStringReference;
        public List<Export> Exports;

        private void Assert(bool v)
        {
            if (!v) throw new FormatException("Failed assertion while reading asset header");
        }

        public static uint UASSET_MAGIC = 2653586369;
        private void ReadHeader(BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            if (reader.ReadUInt32() != UASSET_MAGIC) throw new FormatException("File signature mismatch");

            // Custom versions container
            reader.BaseStream.Seek(20, SeekOrigin.Begin);
            CustomVersionContainer = new List<CustomVersion>();

            int numCustomVersions = reader.ReadInt32();
            for (int i = 0; i < numCustomVersions; i++)
            {
                var customVersionID = new Guid(reader.ReadBytes(16));
                var customVersionNumber = reader.ReadInt32();
                CustomVersionContainer.Add(new CustomVersion(customVersionID, customVersionNumber));
            }

            sectionSixOffset = reader.ReadInt32(); // 24

            reader.ReadFString(); // Usually "None"
            reader.ReadSingle(); // Usually 0

            //reader.BaseStream.Seek(41, SeekOrigin.Begin); // 41
            sectionOneStringCount = reader.ReadInt32();

            headerSize = reader.ReadInt32();

            reader.ReadInt64(); // uncertain, always 0

            //reader.BaseStream.Seek(57, SeekOrigin.Begin); // 57
            dataCategoryCount = reader.ReadInt32();
            sectionThreeOffset = reader.ReadInt32(); // 61
            sectionTwoLinkCount = reader.ReadInt32(); // 65
            sectionTwoOffset = reader.ReadInt32(); // 69 (haha funny)
            sectionFourOffset = reader.ReadInt32(); // 73
            sectionFiveStringCount = reader.ReadInt32(); // 77
            sectionFiveOffset = reader.ReadInt32(); // 81

            switch (headerSize - (numCustomVersions * (16 + sizeof(int))))
            {
                case 185:
                    EngineVersion = UE4Version.VER_GUESSED_V1;
                    break;
                case 193:
                    if (sectionFourOffset == 0)
                    {
                        EngineVersion = UE4Version.VER_GUESSED_V3;
                    }
                    else
                    {
                        EngineVersion = UE4Version.VER_GUESSED_V2;
                    }
                    break;
                case 197:
                    EngineVersion = UE4Version.VER_GUESSED_V2;
                    break;
            }

            // 85, Usually 0
            if (EngineVersion >= UE4Version.VER_GUESSED_V2) reader.ReadUInt64();
            else if (EngineVersion >= UE4Version.VER_GUESSED_V3) reader.ReadUInt64();
            else if (EngineVersion >= UE4Version.VER_GUESSED_V1) reader.ReadUInt32();

            AssetGuid = new Guid(reader.ReadBytes(16));

            Assert(reader.ReadInt32() == 1); // 109
            Assert(reader.ReadInt32() == dataCategoryCount); // 113
            Assert(reader.ReadInt32() == sectionOneStringCount); // 117

            reader.ReadBytes(36); // 36 zeros

            reader.ReadInt64(); // 157, weird 4-byte hash + 4 zeros

            //reader.BaseStream.Seek(165, SeekOrigin.Begin); // 165
            uexpDataOffset = reader.ReadInt32();

            if (EngineVersion >= UE4Version.VER_GUESSED_V1 && EngineVersion < UE4Version.VER_GUESSED_V3) Assert(reader.ReadInt32() == (sectionSixOffset - 4));

            //reader.BaseStream.Seek(169, SeekOrigin.Begin); // 169
            fileSize = reader.ReadInt32() + 4;

            reader.ReadBytes(12); // 12 zeros
        }

        public void Read(BinaryReader reader, int[] manualSkips = null, int[] forceReads = null)
        {
            // Header
            ReadHeader(reader);

            // Section 1
            reader.BaseStream.Seek(headerSize, SeekOrigin.Begin);

            ClearNameIndexList();
            for (int i = 0; i < sectionOneStringCount; i++)
            {
                var str = reader.ReadFStringWithGUIDAndEncoding(out uint guid);
                AddNameReference(str);
            }

            // Section 2
            Imports = new List<Import>(); // base, class, link, connection
            if (sectionTwoOffset > 0)
            {
                reader.BaseStream.Seek(sectionTwoOffset, SeekOrigin.Begin);
                for (int i = 0; i < sectionTwoLinkCount; i++)
                {
                    Imports.Add(new Import(reader.ReadFName(this), reader.ReadFName(this), reader.ReadInt32(), reader.ReadFName(this), UAPUtils.GetImportIndex(i)));
                }
            }

            int gapStart = 0;

            // Section 3
            Exports = new List<Export>();
            if (sectionThreeOffset > 0)
            {
                reader.BaseStream.Seek(sectionThreeOffset, SeekOrigin.Begin);
                for (int i = 0; i < dataCategoryCount; i++)
                {
                    var newRef = new ExportDetails();
                    newRef.ClassIndex = reader.ReadInt32();
                    newRef.SuperIndex = reader.ReadInt32();
                    if (EngineVersion >= UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
                    {
                        newRef.TemplateIndex = reader.ReadInt32();
                    }
                    newRef.OuterIndex = reader.ReadInt32();
                    newRef.ObjectName = reader.ReadFName(this);
                    newRef.ObjectFlags = (EObjectFlags)reader.ReadUInt32();
                    if (EngineVersion < UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
                    {
                        newRef.SerialSize = reader.ReadInt32();
                        newRef.SerialOffset = reader.ReadInt32();
                    }
                    else
                    {
                        newRef.SerialSize = reader.ReadInt64();
                        newRef.SerialOffset = reader.ReadInt64();
                    }
                    newRef.bForcedExport = reader.ReadInt32() == 1;
                    newRef.bNotForClient = reader.ReadInt32() == 1;
                    newRef.bNotForServer = reader.ReadInt32() == 1;
                    newRef.PackageGuid = new Guid(reader.ReadBytes(16));
                    newRef.PackageFlags = reader.ReadUInt32();
                    if (EngineVersion >= UE4Version.VER_UE4_LOAD_FOR_EDITOR_GAME)
                    {
                        newRef.bNotAlwaysLoadedForEditorGame = reader.ReadInt32() == 1;
                    }
                    if (EngineVersion >= UE4Version.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                    {
                        newRef.bIsAsset = reader.ReadInt32() == 1;
                    }
                    if (EngineVersion >= UE4Version.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
                    {
                        newRef.FirstExportDependency = reader.ReadInt32();
                        newRef.SerializationBeforeSerializationDependencies = reader.ReadInt32();
                        newRef.CreateBeforeSerializationDependencies = reader.ReadInt32();
                        newRef.SerializationBeforeCreateDependencies = reader.ReadInt32();
                        newRef.CreateBeforeCreateDependencies = reader.ReadInt32();
                    }

                    Exports.Add(new Export(newRef, this, new byte[0]));
                }
                gapStart = (int)reader.BaseStream.Position;
            }

            // Section 4
            ExportIntReference = new List<int[]>();
            if (sectionFourOffset > 0)
            {
                reader.BaseStream.Seek(sectionFourOffset, SeekOrigin.Begin);
                for (int i = 0; i < dataCategoryCount; i++)
                {
                    int size = reader.ReadInt32();
                    int[] data = new int[size];
                    for (int j = 0; j < size; j++)
                    {
                        data[j] = reader.ReadInt32();
                    }
                    ExportIntReference.Add(data);
                }
                gapStart = (int)reader.BaseStream.Position;
            }
            else
            {
                doWeHaveSectionFour = false;
            }

            // Section 5
            ExportStringReference = new List<string>();
            if (sectionFiveOffset > 0)
            {
                reader.BaseStream.Seek(sectionFiveOffset, SeekOrigin.Begin);
                for (int i = 0; i < sectionFiveStringCount; i++)
                {
                    ExportStringReference.Add(reader.ReadFString());
                }
                gapStart = (int)reader.BaseStream.Position;
            }
            else
            {
                doWeHaveSectionFive = false;
            }

            // Section 6
            if (sectionSixOffset > 0 && Exports.Count > 0)
            {
                if (UseSeparateBulkDataFiles)
                {
                    gapBeforeUexp = uexpDataOffset - gapStart;
                    reader.BaseStream.Seek(uexpDataOffset, SeekOrigin.Begin);
                    UExpData = new List<int>();
                    long firstStart = Exports[0].ReferenceData.SerialOffset;
                    while (reader.BaseStream.Position < firstStart)
                    {
                        UExpData.Add(reader.ReadInt32());
                    }
                }

                for (int i = 0; i < Exports.Count; i++)
                {
                    ExportDetails refData = Exports[i].ReferenceData;
                    reader.BaseStream.Seek(refData.SerialOffset, SeekOrigin.Begin);
                    if (manualSkips != null && manualSkips.Contains(i))
                    {
                        if (forceReads == null || !forceReads.Contains(i))
                        {
                            Exports[i] = new RawExport(Exports[i]);
                            ((RawExport)Exports[i]).Data = reader.ReadBytes((int)refData.SerialSize);
                            continue;
                        }
                    }

                    //Debug.WriteLine(refData.type + " " + GetNameReference(GetImportObjectName(refData.connection)));
                    try
                    {
                        long nextStarting = reader.BaseStream.Length - 4;
                        if ((Exports.Count - 1) > i) nextStarting = Exports[i + 1].ReferenceData.SerialOffset;

                        switch (GetImportObjectName(refData.ClassIndex).Value.Value)
                        {
                            case "BlueprintGeneratedClass":
                            case "WidgetBlueprintGeneratedClass":
                                Exports[i] = new BlueprintGeneratedClassExport(Exports[i]);
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                            case "Level":
                                Exports[i] = new LevelExport(Exports[i]);
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                            case "StringTable":
                                Exports[i] = new StringTableExport(Exports[i]);
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                            case "DataTable":
                                Exports[i] = new DataTableExport(Exports[i]);
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                            default:
                                Exports[i] = new NormalExport(Exports[i]);
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                        }

                        long extrasLen = nextStarting - reader.BaseStream.Position;
                        if (extrasLen < 0)
                        {
                            throw new FormatException("Invalid padding at end of export " + (i + 1) + ": " + extrasLen + " bytes");
                        }
                        else
                        {
                            Exports[i].Extras = reader.ReadBytes((int)extrasLen);
                        }
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        Debug.WriteLine("\nFailed to parse export " + (i + 1) + ": " + ex.ToString());
#endif
                        reader.BaseStream.Seek(refData.SerialOffset, SeekOrigin.Begin);
                        Exports[i] = new RawExport(Exports[i]);
                        ((RawExport)Exports[i]).Data = reader.ReadBytes((int)refData.SerialSize);
                    }
                }
            }
        }

        private byte[] MakeHeader(BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var stre = new MemoryStream(reader.ReadBytes(this.headerSize));
            BinaryWriter writer = new BinaryWriter(stre);

            writer.Write(UAsset.UASSET_MAGIC);

            writer.Seek(24, SeekOrigin.Begin); // 24
            for (int i = 0; i < CustomVersionContainer.Count; i++)
            {
                writer.Write(CustomVersionContainer[i].Key.ToByteArray());
                writer.Write(CustomVersionContainer[i].Version);
            }
            writer.Write(this.sectionSixOffset);

            writer.Seek(13, SeekOrigin.Current); // 41
            writer.Write(this.sectionOneStringCount);

            writer.Seek(12, SeekOrigin.Current); // 57
            writer.Write(this.dataCategoryCount);
            writer.Write(this.sectionThreeOffset); // 61
            writer.Write(this.sectionTwoLinkCount); // 65
            writer.Write(this.sectionTwoOffset); // 69 (haha funny)
            writer.Write(this.sectionFourOffset); // 73
            writer.Write(this.sectionFiveStringCount); // 77
            writer.Write(this.sectionFiveOffset); // 81

            // 8 or 4 bytes 0
            // 16-byte GUID
            // 4 bytes for the 1

            if (this.EngineVersion >= UE4Version.VER_GUESSED_V2) writer.Seek(8, SeekOrigin.Current);
            else if (this.EngineVersion >= UE4Version.VER_GUESSED_V3) writer.Seek(8, SeekOrigin.Current);
            else if (this.EngineVersion >= UE4Version.VER_GUESSED_V1) writer.Seek(4, SeekOrigin.Current);

            writer.Seek(16, SeekOrigin.Current);

            writer.Write((int)1);
            writer.Write(this.dataCategoryCount);
            writer.Write(this.nameMapIndexList.Count); // 117

            // 36 zeros
            // weird 4 byte hash + 4 zeros
            writer.Seek(36, SeekOrigin.Current);
            writer.Seek(8, SeekOrigin.Current);

            writer.Write(this.uexpDataOffset);

            if (this.EngineVersion >= UE4Version.VER_GUESSED_V1 && this.EngineVersion < UE4Version.VER_GUESSED_V3) writer.Write(this.sectionSixOffset - 4);

            writer.Write(this.fileSize - 4);

            return stre.ToArray();
        }

        public MemoryStream WriteData(BinaryReader reader)
        {
            var stre = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stre);

            // Header
            writer.Seek(0, SeekOrigin.Begin);
            writer.Write(MakeHeader(reader));

            // Section 1
            writer.Seek(this.headerSize, SeekOrigin.Begin);
            this.sectionOneStringCount = this.nameMapIndexList.Count;
            for (int i = 0; i < this.nameMapIndexList.Count; i++)
            {
                writer.WriteFString(nameMapIndexList[i]);
                writer.Write(CRCGenerator.GenerateHash(nameMapIndexList[i]));
            }

            // Section 2
            if (this.Imports.Count > 0)
            {
                this.sectionTwoOffset = (int)writer.BaseStream.Position;
                this.sectionTwoLinkCount = this.Imports.Count;
                int newIndex = 0;
                for (int i = 0; i < this.Imports.Count; i++)
                {
                    //Debug.WriteLine("l " + writer.BaseStream.Position);
                    writer.WriteFName(this.Imports[i].ClassPackage, this);
                    writer.WriteFName(this.Imports[i].ClassName, this);
                    writer.Write(this.Imports[i].OuterIndex);
                    writer.WriteFName(this.Imports[i].ObjectName, this);
                    this.Imports[i].Index = --newIndex;
                }
            }
            else
            {
                this.sectionTwoOffset = 0;
            }

            // Section 3
            if (this.Exports.Count > 0)
            {
                this.sectionThreeOffset = (int)writer.BaseStream.Position;
                this.dataCategoryCount = this.Exports.Count;
                for (int i = 0; i < this.Exports.Count; i++)
                {
                    ExportDetails us = this.Exports[i].ReferenceData;
                    //Debug.WriteLine("d " + writer.BaseStream.Position);
                    writer.Write(us.ClassIndex);
                    writer.Write(us.SuperIndex);
                    if (EngineVersion >= UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
                    {
                        writer.Write(us.TemplateIndex);
                    }
                    writer.Write(us.OuterIndex);
                    writer.WriteFName(us.ObjectName, this);
                    writer.Write((uint)us.ObjectFlags);
                    if (EngineVersion < UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
                    {
                        writer.Write((int)us.SerialSize);
                        writer.Write((int)us.SerialOffset);
                    }
                    else
                    {
                        writer.Write(us.SerialSize);
                        writer.Write(us.SerialOffset);
                    }
                    writer.Write(us.bForcedExport ? 1 : 0);
                    writer.Write(us.bNotForClient ? 1 : 0);
                    writer.Write(us.bNotForServer ? 1 : 0);
                    writer.Write(us.PackageGuid.ToByteArray());
                    writer.Write(us.PackageFlags);
                    if (EngineVersion >= UE4Version.VER_UE4_LOAD_FOR_EDITOR_GAME)
                    {
                        writer.Write(us.bNotAlwaysLoadedForEditorGame ? 1 : 0);
                    }
                    if (EngineVersion >= UE4Version.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                    {
                        writer.Write(us.bIsAsset ? 1 : 0);
                    }
                    if (EngineVersion >= UE4Version.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
                    {
                        writer.Write(us.FirstExportDependency);
                        writer.Write(us.SerializationBeforeSerializationDependencies);
                        writer.Write(us.CreateBeforeSerializationDependencies);
                        writer.Write(us.SerializationBeforeCreateDependencies);
                        writer.Write(us.CreateBeforeCreateDependencies);
                    }
                }
            }
            else
            {
                this.sectionThreeOffset = 0;
            }

            // Section 4
            if (this.doWeHaveSectionFour)
            {
                this.sectionFourOffset = (int)writer.BaseStream.Position;
                for (int i = 0; i < this.Exports.Count; i++)
                {
                    if (i >= this.ExportIntReference.Count) this.ExportIntReference.Add(new int[0]);

                    int[] currentData = this.ExportIntReference[i];
                    writer.Write(currentData.Length);
                    for (int j = 0; j < currentData.Length; j++)
                    {
                        writer.Write(currentData[j]);
                    }
                }
            }
            else
            {
                this.sectionFourOffset = 0;
            }

            // Section 5
            if (this.doWeHaveSectionFive)
            {
                this.sectionFiveOffset = (int)writer.BaseStream.Position;
                this.sectionFiveStringCount = this.ExportStringReference.Count;
                for (int i = 0; i < this.ExportStringReference.Count; i++)
                {
                    writer.WriteFString(this.ExportStringReference[i]);
                }
            }
            else
            {
                this.sectionFiveOffset = 0;
            }

            // Uexp Data
            if (this.UseSeparateBulkDataFiles)
            {
                writer.Write(new byte[this.gapBeforeUexp]);
                this.uexpDataOffset = (int)stre.Position;
                foreach (int part in this.UExpData)
                {
                    writer.Write(part);
                }
            }
            else
            {
                writer.Write((int)0);
            }

            // Section 6
            int oldOffset = this.sectionSixOffset;
            this.sectionSixOffset = (int)writer.BaseStream.Position;
            long[] categoryStarts = new long[this.Exports.Count];
            if (WillWriteExportData)
            {
                if (this.Exports.Count > 0)
                {
                    for (int i = 0; i < this.Exports.Count; i++)
                    {
                        categoryStarts[i] = writer.BaseStream.Position;
                        Export us = this.Exports[i];
                        us.Write(writer);
                        writer.Write(us.Extras);
                    }
                }
                writer.Write(new byte[] { 0xC1, 0x83, 0x2A, 0x9E });
            }
            else // Old behavior
            {
                reader.BaseStream.Seek(oldOffset, SeekOrigin.Begin);
                writer.Write(reader.ReadBytes((int)reader.BaseStream.Length - oldOffset));

                int additionalOffset = this.sectionSixOffset - oldOffset;
                for (int i = 0; i < this.Exports.Count; i++)
                {
                    ExportDetails us = this.Exports[i].ReferenceData;
                    categoryStarts[i] = us.SerialOffset + additionalOffset;
                }
            }

            this.fileSize = (int)stre.Length;

            // Rewrite Section 3
            if (this.Exports.Count > 0)
            {
                writer.Seek(this.sectionThreeOffset, SeekOrigin.Begin);
                for (int i = 0; i < this.Exports.Count; i++)
                {
                    ExportDetails us = this.Exports[i].ReferenceData;
                    long nextLoc = this.fileSize - 4;
                    if ((this.Exports.Count - 1) > i) nextLoc = categoryStarts[i + 1];

                    us.SerialOffset = categoryStarts[i];
                    us.SerialSize = nextLoc - categoryStarts[i];

                    writer.Write(us.ClassIndex);
                    writer.Write(us.SuperIndex);
                    if (EngineVersion >= UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
                    {
                        writer.Write(us.TemplateIndex);
                    }
                    writer.Write(us.OuterIndex);
                    writer.WriteFName(us.ObjectName, this);
                    writer.Write((uint)us.ObjectFlags);
                    if (EngineVersion < UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
                    {
                        writer.Write((int)us.SerialSize);
                        writer.Write((int)us.SerialOffset);
                    }
                    else
                    {
                        writer.Write(us.SerialSize);
                        writer.Write(us.SerialOffset);
                    }
                    writer.Write(us.bForcedExport ? 1 : 0);
                    writer.Write(us.bNotForClient ? 1 : 0);
                    writer.Write(us.bNotForServer ? 1 : 0);
                    writer.Write(us.PackageGuid.ToByteArray());
                    writer.Write(us.PackageFlags);
                    if (EngineVersion >= UE4Version.VER_UE4_LOAD_FOR_EDITOR_GAME)
                    {
                        writer.Write(us.bNotAlwaysLoadedForEditorGame ? 1 : 0);
                    }
                    if (EngineVersion >= UE4Version.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                    {
                        writer.Write(us.bIsAsset ? 1 : 0);
                    }
                    if (EngineVersion >= UE4Version.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
                    {
                        writer.Write(us.FirstExportDependency);
                        writer.Write(us.SerializationBeforeSerializationDependencies);
                        writer.Write(us.CreateBeforeSerializationDependencies);
                        writer.Write(us.SerializationBeforeCreateDependencies);
                        writer.Write(us.CreateBeforeCreateDependencies);
                    }
                }
            }

            // Rewrite Header
            writer.Seek(0, SeekOrigin.Begin);
            writer.Write(MakeHeader(reader));

            writer.Seek(0, SeekOrigin.Begin);
            return stre;
        }

        private static void CopySplitUp(Stream input, Stream output, int start, int leng)
        {
            input.Seek(start, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            byte[] buffer = new byte[32768];
            int read;
            while (leng > 0 && (read = input.Read(buffer, 0, Math.Min(buffer.Length, leng))) > 0)
            {
                output.Write(buffer, 0, read);
                leng -= read;
            }
        }

        public void Write(string output)
        {
            MemoryStream newData;
            if (WillStoreOriginalCopyInMemory)
            {
                newData = WriteData(new BinaryReader(new MemoryStream(OriginalCopy)));
            }
            else
            {
                using (FileStream f = File.Open(FilePath, FileMode.Open, FileAccess.Read))
                {
                    f.Seek(0, SeekOrigin.Begin);
                    newData = WriteData(new BinaryReader(f));
                }
            }

            if (this.UseSeparateBulkDataFiles && this.Exports.Count > 0)
            {
                long breakingOffPoint = this.Exports[0].ReferenceData.SerialOffset;
                using (FileStream f = File.Open(output, FileMode.Create, FileAccess.Write))
                {
                    CopySplitUp(newData, f, 0, (int)breakingOffPoint);
                }

                using (FileStream f = File.Open(Path.ChangeExtension(output, "uexp"), FileMode.Create, FileAccess.Write))
                {
                    CopySplitUp(newData, f, (int)breakingOffPoint, (int)(newData.Length - breakingOffPoint));
                }
            }
            else
            {
                using (FileStream f = File.Open(output, FileMode.Create, FileAccess.Write))
                {
                    newData.CopyTo(f);
                }
            }

        }

        public MemoryStream PathToStream(string p)
        {
            using (FileStream origStream = File.Open(p, FileMode.Open))
            {
                MemoryStream completeStream = new MemoryStream();
                origStream.CopyTo(completeStream);

                UseSeparateBulkDataFiles = false;
                try
                {
                    var targetFile = Path.ChangeExtension(p, "uexp");
                    if (File.Exists(targetFile))
                    {
                        using (FileStream newStream = File.Open(targetFile, FileMode.Open))
                        {
                            completeStream.Seek(0, SeekOrigin.End);
                            newStream.CopyTo(completeStream);
                            UseSeparateBulkDataFiles = true;
                        }
                    }
                }
                catch (FileNotFoundException) { }

                completeStream.Seek(0, SeekOrigin.Begin);
                return completeStream;
            }
        }

        public BinaryReader PathToReader(string p)
        {
            return new BinaryReader(PathToStream(p));
        }

        // If willStoreOriginalCopyInMemory is true when calling this constructor then you must set OriginalCopy yourself
        public UAsset(BinaryReader reader, bool willStoreOriginalCopyInMemory = false, bool willWriteExportData = true, int[] manualSkips = null, int[] forceReads = null)
        {
            WillStoreOriginalCopyInMemory = willStoreOriginalCopyInMemory;
            WillWriteExportData = willWriteExportData;
            Read(reader, manualSkips, forceReads);
        }

        public UAsset(string path, bool willStoreOriginalCopyInMemory = false, bool willWriteExportData = true, int[] manualSkips = null, int[] forceReads = null)
        {
            this.FilePath = path;
            WillStoreOriginalCopyInMemory = willStoreOriginalCopyInMemory;
            WillWriteExportData = willWriteExportData;

            var ourReader = PathToReader(path);
            Read(ourReader, manualSkips, forceReads);

            if (WillStoreOriginalCopyInMemory)
            {
                ourReader.BaseStream.Seek(0, SeekOrigin.Begin);
                OriginalCopy = ourReader.ReadBytes((int)ourReader.BaseStream.Length);
            }
        }

        public UAsset()
        {

        }
    }
}
