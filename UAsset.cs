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
        public string RequiredString;

        public NameMapOutOfRangeException(string requiredString) : base("Requested string \"" + requiredString + "\" not found in name map")
        {
            RequiredString = requiredString;
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
        public UE4Version GuessedVersion;

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

        public IReadOnlyList<string> GetNameMapIndexList()
        {
            return nameMapIndexList.AsReadOnly();
        }

        public IReadOnlyDictionary<string, int> GetNameMapLookup()
        {
            return nameMapLookup;
        }

        public IReadOnlyDictionary<string, Encoding> GetNameMapEncodingLookup()
        {
            return nameMapEncodingLookup;
        }

        public void ClearNameIndexList()
        {
            nameMapIndexList = new List<string>();
            nameMapLookup = new Dictionary<string, int>();
            nameMapEncodingLookup = new Dictionary<string, Encoding>();
        }

        public void SetNameReference(int index, string value)
        {
            SetNameReference(index, new UString(value, Encoding.ASCII));
        }

        public void SetNameReference(int index, UString value)
        {
            nameMapLookup.Remove(nameMapIndexList[index]);
            nameMapIndexList[index] = value.Value;
            nameMapLookup[value.Value] = index;
            nameMapEncodingLookup[value.Value] = value.Encoding;
        }

        public string GetNameReference(int index)
        {
            if (index < 0) return Convert.ToString(-index);
            if (index > nameMapIndexList.Count) return Convert.ToString(index);
            return nameMapIndexList[index];
        }

        public string GetNameReferenceWithoutZero(int index)
        {
            if (index <= 0) return Convert.ToString(-index);
            if (index > nameMapIndexList.Count) return Convert.ToString(index);
            return nameMapIndexList[index];
        }

        public UString GetNameReferenceWithEncoding(int index)
        {
            string realStr = GetNameReference(index);
            if (nameMapEncodingLookup.ContainsKey(realStr)) return new UString(realStr, nameMapEncodingLookup[realStr]);
            return new UString(realStr, Encoding.ASCII);
        }

        public UString GetNameReferenceWithEncodingWithoutZero(int index)
        {
            string realStr = GetNameReferenceWithoutZero(index);
            if (nameMapEncodingLookup.ContainsKey(realStr)) return new UString(realStr, nameMapEncodingLookup[realStr]);
            return new UString(realStr, Encoding.ASCII);
        }

        public bool NameReferenceContains(string search)
        {
            return nameMapLookup.ContainsKey(search);
        }

        public int SearchNameReference(string search)
        {
            if (NameReferenceContains(search)) return nameMapLookup[search];
            throw new NameMapOutOfRangeException(search);
        }

        public int AddNameReference(string name)
        {
            return AddNameReference(new UString(name, Encoding.ASCII));
        }

        public int AddNameReference(UString name)
        {
            if (nameMapLookup.ContainsKey(name.Value)) return SearchNameReference(name.Value);
            nameMapIndexList.Add(name.Value);
            nameMapLookup.Add(name.Value, nameMapIndexList.Count - 1);
            nameMapEncodingLookup[name.Value] = name.Encoding;
            return nameMapIndexList.Count - 1;
        }

        public string GetImportReference(int index)
        {
            return index < 0 ? GetImportAt(index).Property : Convert.ToString(index);
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
        
        public string GetBGCName()
        {
            var bgcCat = GetBlueprintGeneratedClassExport();
            if (bgcCat == null || bgcCat.ReferenceData == null) return null;

            return GetNameReference(bgcCat.ReferenceData.typeIndex);
        }

        public void GetParentClass(out string parentClassPath, out string parentBGCName)
        {
            parentClassPath = null;
            parentBGCName = null;

            var bgcCat = GetBlueprintGeneratedClassExport();
            if (bgcCat == null) return;

            Import parentClassLink = GetImportAt(bgcCat.BaseClass);
            if (parentClassLink == null) return;
            if (parentClassLink.Linkage >= 0) return;

            parentBGCName = parentClassLink.Property;
            parentClassPath = GetImportAt((int)parentClassLink.Linkage).Property;
        }

        public int SearchForLink(string bbase, string bclass, int link, string property)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (bbase == Imports[i].Base
                    && bclass == Imports[i].Class
                    && link == Imports[i].Linkage
                    && property == Imports[i].Property)
                {
                    return currentPos;
                }

            }

            return 0;
        }

        public int SearchForLink(string bbase, string bclass, string property)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (bbase == Imports[i].Base
                    && bclass == Imports[i].Class
                    && property == Imports[i].Property)
                {
                    return currentPos;
                }

            }

            return 0;
        }

        public int SearchForLink(string property)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (property == Imports[i].Property) return currentPos;
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
        internal int extraGameJump = 0;

        // Do not directly add values to here under any circumstances; use AddStringReference instead
        internal List<string> nameMapIndexList;
        private Dictionary<string, int> nameMapLookup = new Dictionary<string, int>();
        internal Dictionary<string, Encoding> nameMapEncodingLookup = new Dictionary<string, Encoding>();

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

            reader.BaseStream.Seek(20, SeekOrigin.Begin);
            extraGameJump = reader.ReadInt32() * 20;

            reader.BaseStream.Seek(24 + extraGameJump, SeekOrigin.Begin); // 24
            sectionSixOffset = reader.ReadInt32();

            reader.ReadUString(); // Usually "None"
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

            switch (headerSize - extraGameJump)
            {
                case 185:
                    GuessedVersion = UE4Version.VER_GUESSED_V1;
                    break;
                case 193:
                    if (sectionFourOffset == 0)
                    {
                        GuessedVersion = UE4Version.VER_GUESSED_V3;
                    }
                    else
                    {
                        GuessedVersion = UE4Version.VER_GUESSED_V2;
                    }
                    break;
                case 197:
                    GuessedVersion = UE4Version.VER_GUESSED_V2;
                    break;
            }

            // 85, Usually 0
            if (GuessedVersion >= UE4Version.VER_GUESSED_V2) reader.ReadUInt64();
            else if (GuessedVersion >= UE4Version.VER_GUESSED_V3) reader.ReadUInt64();
            else if (GuessedVersion >= UE4Version.VER_GUESSED_V1) reader.ReadUInt32();

            AssetGuid = new Guid(reader.ReadBytes(16));

            Assert(reader.ReadInt32() == 1); // 109
            Assert(reader.ReadInt32() == dataCategoryCount); // 113
            Assert(reader.ReadInt32() == sectionOneStringCount); // 117

            reader.ReadBytes(36); // 36 zeros

            reader.ReadInt64(); // 157, weird 4-byte hash + 4 zeros

            //reader.BaseStream.Seek(165, SeekOrigin.Begin); // 165
            uexpDataOffset = reader.ReadInt32();

            if (GuessedVersion >= UE4Version.VER_GUESSED_V1 && GuessedVersion < UE4Version.VER_GUESSED_V3) Assert(reader.ReadInt32() == (sectionSixOffset - 4));

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
                var str = reader.ReadUStringWithGUIDAndEncoding(out uint guid);
                AddNameReference(str);
            }

            // Section 2
            Imports = new List<Import>(); // base, class, link, connection
            if (sectionTwoOffset > 0)
            {
                reader.BaseStream.Seek(sectionTwoOffset, SeekOrigin.Begin);
                for (int i = 0; i < sectionTwoLinkCount; i++)
                {
                    ulong bbase = reader.ReadUInt64();
                    ulong bclass = reader.ReadUInt64();
                    int link = reader.ReadInt32();
                    ulong property = reader.ReadUInt64();
                    Imports.Add(new Import(this.GetNameReference((int)bbase), this.GetNameReference((int)bclass), link, this.GetNameReference((int)property), UAPUtils.GetImportIndex(i)));
                }
            }

            int gapStart = 0;

            // Section 3
            Exports = new List<Export>(); // connection, connect, category, link, typeIndex, type, length, start, garbage1, garbage2, garbage3
            if (sectionThreeOffset > 0)
            {
                reader.BaseStream.Seek(sectionThreeOffset, SeekOrigin.Begin);
                for (int i = 0; i < dataCategoryCount; i++)
                {
                    int connection = 0, connect = 0, category = 0, link = 0, typeIndex = 0, garbage1 = 0, lengthV = 0, garbage2 = 0, startV = 0;
                    ushort type = 0, garbageNew = 0;

                    connection = reader.ReadInt32();
                    connect = reader.ReadInt32();
                    category = reader.ReadInt32();
                    link = reader.ReadInt32();
                    typeIndex = reader.ReadInt32();

                    if (GuessedVersion >= UE4Version.VER_GUESSED_V2)
                    {
                        garbage1 = reader.ReadInt32();
                    }
                    else if (GuessedVersion >= UE4Version.VER_GUESSED_V3)
                    {
                        garbage1 = reader.ReadInt32();
                    }
                    else
                    {
                        garbage1 = 0;
                    }

                    type = reader.ReadUInt16();
                    garbageNew = reader.ReadUInt16();
                    lengthV = reader.ReadInt32(); // !!!

                    if (GuessedVersion >= UE4Version.VER_GUESSED_V2)
                    {
                        garbage2 = reader.ReadInt32();
                        startV = reader.ReadInt32(); // !!!
                    }
                    else if (GuessedVersion >= UE4Version.VER_GUESSED_V3)
                    {
                        startV = reader.ReadInt32(); // !!!
                        garbage2 = reader.ReadInt32();
                    }
                    else if (GuessedVersion >= UE4Version.VER_GUESSED_V1)
                    {
                        startV = reader.ReadInt32(); // !!!
                        garbage2 = reader.ReadInt32();
                    }

                    int totalByteCount = 104;
                    if (GuessedVersion >= UE4Version.VER_GUESSED_V2) totalByteCount = 104;
                    else if (GuessedVersion >= UE4Version.VER_GUESSED_V3) totalByteCount = 96;
                    else if (GuessedVersion >= UE4Version.VER_GUESSED_V1) totalByteCount = 72;

                    Exports.Add(new Export(new ExportReference(connection, connect, category, link, typeIndex, type, lengthV, startV, garbage1, garbage2, garbageNew, reader.ReadBytes(totalByteCount - (10 * 4))), this, new byte[0]));
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
                    ExportStringReference.Add(reader.ReadUString());
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
                    int firstStart = Exports[0].ReferenceData.startV;
                    while (reader.BaseStream.Position < firstStart)
                    {
                        UExpData.Add(reader.ReadInt32());
                    }
                }

                for (int i = 0; i < Exports.Count; i++)
                {
                    ExportReference refData = Exports[i].ReferenceData;
                    reader.BaseStream.Seek(refData.startV, SeekOrigin.Begin);
                    if (manualSkips != null && manualSkips.Contains(i))
                    {
                        if (forceReads == null || !forceReads.Contains(i))
                        {
                            Exports[i] = new RawExport(Exports[i]);
                            ((RawExport)Exports[i]).Data = reader.ReadBytes(refData.lengthV);
                            continue;
                        }
                    }

                    //Debug.WriteLine(refData.type + " " + GetNameReference(GetImportReference(refData.connection)));
                    try
                    {
                        int nextStarting = (int)reader.BaseStream.Length - 4;
                        if ((Exports.Count - 1) > i) nextStarting = Exports[i + 1].ReferenceData.startV;

                        switch (GetImportReference(refData.connection))
                        {
                            case "BlueprintGeneratedClass":
                            case "WidgetBlueprintGeneratedClass":
                                Exports[i] = new BlueprintGeneratedClassExport(Exports[i]);
                                Exports[i].Read(reader, nextStarting);
                                break;
                            case "Level":
                                Exports[i] = new LevelExport(Exports[i]);
                                Exports[i].Read(reader, nextStarting);
                                break;
                            case "StringTable":
                                Exports[i] = new StringTableExport(Exports[i]);
                                Exports[i].Read(reader, nextStarting);
                                break;
                            case "DataTable":
                                Exports[i] = new DataTableExport(Exports[i]);
                                Exports[i].Read(reader, nextStarting);
                                break;
                            default:
                                Exports[i] = new NormalExport(Exports[i]);
                                Exports[i].Read(reader, nextStarting);
                                break;
                        }

                        int extrasLen = nextStarting - (int)reader.BaseStream.Position;
                        if (extrasLen < 0)
                        {
                            throw new FormatException("Invalid padding at end of export " + (i + 1) + ": " + extrasLen + " bytes");
                        }
                        else
                        {
                            Exports[i].Extras = reader.ReadBytes(extrasLen);
                        }
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        Debug.WriteLine("\nFailed to parse export " + (i + 1) + ": " + ex.ToString());
#endif
                        reader.BaseStream.Seek(refData.startV, SeekOrigin.Begin);
                        Exports[i] = new RawExport(Exports[i]);
                        ((RawExport)Exports[i]).Data = reader.ReadBytes(refData.lengthV);
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

            writer.Seek(24 + this.extraGameJump, SeekOrigin.Begin); // 24
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

            if (this.GuessedVersion >= UE4Version.VER_GUESSED_V2) writer.Seek(8, SeekOrigin.Current);
            else if (this.GuessedVersion >= UE4Version.VER_GUESSED_V3) writer.Seek(8, SeekOrigin.Current);
            else if (this.GuessedVersion >= UE4Version.VER_GUESSED_V1) writer.Seek(4, SeekOrigin.Current);

            writer.Seek(16, SeekOrigin.Current);

            writer.Write((int)1);
            writer.Write(this.dataCategoryCount);
            writer.Write(this.nameMapIndexList.Count); // 117

            // 36 zeros
            // weird 4 byte hash + 4 zeros
            writer.Seek(36, SeekOrigin.Current);
            writer.Seek(8, SeekOrigin.Current);

            writer.Write(this.uexpDataOffset);

            if (this.GuessedVersion >= UE4Version.VER_GUESSED_V1 && this.GuessedVersion < UE4Version.VER_GUESSED_V3) writer.Write(this.sectionSixOffset - 4);

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
                UString ourUString = new UString(this.nameMapIndexList[i], this.nameMapEncodingLookup[this.nameMapIndexList[i]]);
                writer.WriteUString(ourUString);
                writer.Write(CRCGenerator.GenerateHash(ourUString));
            }

            // Section 2
            if (this.Imports.Count > 0)
            {
                this.sectionTwoOffset = (int)writer.BaseStream.Position;
                this.sectionTwoLinkCount = this.Imports.Count;
                int newIndex = 0;
                for (int i = 0; i < this.Imports.Count; i++)
                {
                    writer.Write((ulong)this.SearchNameReference(this.Imports[i].Base));
                    writer.Write((ulong)this.SearchNameReference(this.Imports[i].Class));
                    writer.Write(this.Imports[i].Linkage);
                    writer.Write((ulong)this.SearchNameReference(this.Imports[i].Property));
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
                    ExportReference us = this.Exports[i].ReferenceData;
                    writer.Write(us.connection);
                    writer.Write(us.connect);
                    writer.Write(us.category);
                    writer.Write(us.link);
                    writer.Write(us.typeIndex);
                    writer.Write(us.garbage1);
                    writer.Write(us.type);
                    writer.Write(us.garbageNew);
                    writer.Write(us.lengthV);
                    writer.Write(us.garbage2);
                    writer.Write(us.startV);
                    writer.Write(us.garbage3);
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
                    writer.WriteUString(this.ExportStringReference[i]);
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
            int[] categoryStarts = new int[this.Exports.Count];
            if (WillWriteExportData)
            {
                if (this.Exports.Count > 0)
                {
                    for (int i = 0; i < this.Exports.Count; i++)
                    {
                        categoryStarts[i] = (int)writer.BaseStream.Position;
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
                    ExportReference us = this.Exports[i].ReferenceData;
                    categoryStarts[i] = us.startV + additionalOffset;
                }
            }

            this.fileSize = (int)stre.Length;

            // Rewrite Section 3
            if (this.Exports.Count > 0)
            {
                writer.Seek(this.sectionThreeOffset, SeekOrigin.Begin);
                for (int i = 0; i < this.Exports.Count; i++)
                {
                    ExportReference us = this.Exports[i].ReferenceData;
                    int nextLoc = this.fileSize - 4;
                    if ((this.Exports.Count - 1) > i) nextLoc = categoryStarts[i + 1];

                    us.startV = categoryStarts[i];
                    us.lengthV = nextLoc - categoryStarts[i];

                    writer.Write(us.connection);
                    writer.Write(us.connect);
                    writer.Write(us.category);
                    writer.Write(us.link);
                    writer.Write(us.typeIndex);

                    if (this.GuessedVersion >= UE4Version.VER_GUESSED_V2)
                    {
                        writer.Write(us.garbage1);
                    }
                    else if (this.GuessedVersion >= UE4Version.VER_GUESSED_V3)
                    {
                        writer.Write(us.garbage1);
                    }

                    writer.Write(us.type);
                    writer.Write(us.garbageNew);
                    writer.Write(us.lengthV); // !!!

                    if (this.GuessedVersion >= UE4Version.VER_GUESSED_V2)
                    {
                        writer.Write(us.garbage2);
                        writer.Write(us.startV);
                    }
                    else if (this.GuessedVersion >= UE4Version.VER_GUESSED_V3)
                    {
                        writer.Write(us.startV);
                        writer.Write(us.garbage2);
                    }
                    else if (this.GuessedVersion >= UE4Version.VER_GUESSED_V1)
                    {
                        writer.Write(us.startV);
                        writer.Write(us.garbage2);
                    }

                    writer.Write(us.garbage3);
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
                int breakingOffPoint = this.Exports[0].ReferenceData.startV;
                using (FileStream f = File.Open(output, FileMode.Create, FileAccess.Write))
                {
                    CopySplitUp(newData, f, 0, breakingOffPoint);
                }

                using (FileStream f = File.Open(Path.ChangeExtension(output, "uexp"), FileMode.Create, FileAccess.Write))
                {
                    CopySplitUp(newData, f, breakingOffPoint, (int)(newData.Length - breakingOffPoint));
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

        // manualSkips is an array of category indexes (starting from 1) to skip
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
            Read(PathToReader(this.FilePath), manualSkips, forceReads);
        }

        public UAsset()
        {

        }
    }
}
