using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UAssetAPI
{
    public class HeaderOutOfRangeException : FormatException
    {
        public string RequiredString;

        public HeaderOutOfRangeException(string requiredString) : base("Requested string \"" + requiredString + "\" not found in header list")
        {
            RequiredString = requiredString;
        }
    }

    public class AssetReader
    {
        /* Public Methods */
        public string path;
        public bool UseSeparateBulkDataFiles = false;
        public List<int> UExpData;
        public Guid AssetGuid;
        public UE4Version GuessedVersion;

        public IReadOnlyList<string> GetHeaderIndexList()
        {
            return headerIndexList.AsReadOnly();
        }

        public IReadOnlyDictionary<string, int> GetHeaderLookup()
        {
            return headerLookup;
        }

        public void ClearHeaderIndexList()
        {
            headerIndexList = new List<string>();
            headerLookup = new Dictionary<string, int>();
        }

        public void SetHeaderReference(int index, string value)
        {
            headerLookup.Remove(headerIndexList[index]);
            headerIndexList[index] = value;
            headerLookup[value] = index;
        }

        public string GetHeaderReference(int index)
        {
            if (index < 0) return Convert.ToString(-index);
            if (index > headerIndexList.Count) return Convert.ToString(index);
            return headerIndexList[index];
        }

        public string GetHeaderReferenceWithoutZero(int index)
        {
            if (index <= 0) return Convert.ToString(-index);
            if (index > headerIndexList.Count) return Convert.ToString(index);
            return headerIndexList[index];
        }

        public bool HeaderReferenceContains(string search)
        {
            return headerLookup.ContainsKey(search);
        }

        public int SearchHeaderReference(string search)
        {
            if (HeaderReferenceContains(search)) return headerLookup[search];
            throw new HeaderOutOfRangeException(search);
        }

        public int AddHeaderReference(string name)
        {
            if (headerLookup.ContainsKey(name)) return SearchHeaderReference(name);
            headerIndexList.Add(name);
            headerLookup.Add(name, headerIndexList.Count - 1);
            return headerIndexList.Count - 1;
        }

        public int GetLinkReference(int index)
        {
            return (int)(index < 0 ? (long)GetLinkAt(index).Property : -index);
        }

        public Link GetLinkAt(int index)
        {
            int normalIndex = Utils.GetNormalIndex(index);
            if (normalIndex < 0 || normalIndex >= links.Count) return null;
            return links[normalIndex];
        }

        public Link AddLink(string bbase, string bclass, int link, string property)
        {
            Link nuevo = new Link(bbase, bclass, link, property, this, Utils.GetLinkIndex(links.Count));
            links.Add(nuevo);
            return nuevo;
        }

        public int AddLink(Link li)
        {
            li.Index = Utils.GetLinkIndex(links.Count);
            links.Add(li);
            return li.Index;
        }

        public BlueprintGeneratedClassCategory GetBGCCategory()
        {
            foreach (Category cat in categories)
            {
                if (cat is BlueprintGeneratedClassCategory bgcCat) return bgcCat;
            }
            return null;
        }
        
        public string GetBGCName()
        {
            var bgcCat = GetBGCCategory();
            if (bgcCat == null || bgcCat.ReferenceData == null) return null;

            return GetHeaderReference(bgcCat.ReferenceData.typeIndex);
        }

        public void GetParentClass(out string parentClassPath, out string parentBGCName)
        {
            parentClassPath = null;
            parentBGCName = null;

            var bgcCat = GetBGCCategory();
            if (bgcCat == null) return;

            Link parentClassLink = GetLinkAt(bgcCat.BaseClass);
            if (parentClassLink == null) return;
            if (parentClassLink.Linkage >= 0) return;

            parentBGCName = GetHeaderReference((int)parentClassLink.Property);
            parentClassPath = GetHeaderReference((int)GetLinkAt((int)parentClassLink.Linkage).Property);
        }

        public int SearchForLink(ulong bbase, ulong bclass, int link, ulong property)
        {
            int currentPos = 0;
            for (int i = 0; i < links.Count; i++)
            {
                currentPos--;
                if (bbase == links[i].Base
                    && bclass == links[i].Class
                    && link == links[i].Linkage
                    && property == links[i].Property)
                {
                    return currentPos;
                }

            }

            return 0;
        }

        public int SearchForLink(ulong bbase, ulong bclass, ulong property)
        {
            int currentPos = 0;
            for (int i = 0; i < links.Count; i++)
            {
                currentPos--;
                if (bbase == links[i].Base
                    && bclass == links[i].Class
                    && property == links[i].Property)
                {
                    return currentPos;
                }

            }

            return 0;
        }

        public int SearchForLink(ulong property)
        {
            int currentPos = 0;
            for (int i = 0; i < links.Count; i++)
            {
                currentPos--;
                if (property == links[i].Property) return currentPos;
            }

            return 0;
        }

        /* End Public Methods */

        public int headerSize = 0;
        public int sectionSixOffset = 0;
        public int sectionOneStringCount = 0;
        public int dataCategoryCount = 0;
        public int sectionThreeOffset = 0;
        public int sectionTwoLinkCount = 0;
        public int sectionTwoOffset = 0;
        public int sectionFourOffset = 0;
        public int sectionFiveStringCount = 0;
        public int sectionFiveOffset = 0;
        public int uexpDataOffset = 0;
        public int gapBeforeUexp = 0;
        public int fileSize = 0;
        public bool doWeHaveSectionFour = true;
        public bool doWeHaveSectionFive = true;

        public uint extraGameIdentifier = 0;
        public int extraGameJump = 0;

        // Do not directly add values to headerIndexList under any circumstances; use AddHeaderReference instead
        internal List<string> headerIndexList;
        private Dictionary<string, int> headerLookup = new Dictionary<string, int>();
        public List<Link> links; // base, class, link, connection
        public List<int[]> categoryIntReference;
        public List<string> categoryStringReference;
        public List<Category> categories;

        private void Assert(bool v)
        {
            if (!v) throw new FormatException("Failed assertion while reading asset header");
        }

        public static uint UASSET_MAGIC = 2653586369;
        private void ReadHeader(BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            if (reader.ReadUInt32() != UASSET_MAGIC) throw new FormatException("File signature mismatch");

            reader.BaseStream.Seek(8, SeekOrigin.Begin); // 8
            extraGameIdentifier = reader.ReadUInt32();
            if (extraGameIdentifier != 0)
            {
                reader.BaseStream.Seek(20, SeekOrigin.Begin); // 20
                extraGameJump = reader.ReadByte() * 20; // probably not the actual format, but it works for now
            }

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

            ClearHeaderIndexList();
            for (int i = 0; i < sectionOneStringCount; i++)
            {
                var str = reader.ReadUStringWithGUID(out uint guid);
                AddHeaderReference(str);
            }

            // Section 2
            links = new List<Link>(); // base, class, link, connection
            if (sectionTwoOffset > 0)
            {
                reader.BaseStream.Seek(sectionTwoOffset, SeekOrigin.Begin);
                for (int i = 0; i < sectionTwoLinkCount; i++)
                {
                    ulong bbase = reader.ReadUInt64();
                    ulong bclass = reader.ReadUInt64();
                    int link = reader.ReadInt32();
                    ulong property = reader.ReadUInt64();
                    links.Add(new Link(bbase, bclass, link, property, Utils.GetLinkIndex(i)));
                }
            }

            int gapStart = 0;

            // Section 3
            categories = new List<Category>(); // connection, connect, category, link, typeIndex, type, length, start, garbage1, garbage2, garbage3
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

                    categories.Add(new Category(new CategoryReference(connection, connect, category, link, typeIndex, type, lengthV, startV, garbage1, garbage2, garbageNew, reader.ReadBytes(totalByteCount - (10 * 4))), this, new byte[0]));
                }
                gapStart = (int)reader.BaseStream.Position;
            }

            // Section 4
            categoryIntReference = new List<int[]>();
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
                    categoryIntReference.Add(data);
                }
                gapStart = (int)reader.BaseStream.Position;
            }
            else
            {
                doWeHaveSectionFour = false;
            }

            // Section 5
            categoryStringReference = new List<string>();
            if (sectionFiveOffset > 0)
            {
                reader.BaseStream.Seek(sectionFiveOffset, SeekOrigin.Begin);
                for (int i = 0; i < sectionFiveStringCount; i++)
                {
                    categoryStringReference.Add(reader.ReadUString());
                }
                gapStart = (int)reader.BaseStream.Position;
            }
            else
            {
                doWeHaveSectionFive = false;
            }

            // Section 6
            if (sectionSixOffset > 0 && categories.Count > 0)
            {
                if (UseSeparateBulkDataFiles)
                {
                    gapBeforeUexp = uexpDataOffset - gapStart;
                    reader.BaseStream.Seek(uexpDataOffset, SeekOrigin.Begin);
                    UExpData = new List<int>();
                    int firstStart = categories[0].ReferenceData.startV;
                    while (reader.BaseStream.Position < firstStart)
                    {
                        UExpData.Add(reader.ReadInt32());
                    }
                }

                for (int i = 0; i < categories.Count; i++)
                {
                    CategoryReference refData = categories[i].ReferenceData;
                    reader.BaseStream.Seek(refData.startV, SeekOrigin.Begin);
                    if (manualSkips != null && manualSkips.Contains(i))
                    {
                        if (forceReads == null || !forceReads.Contains(i))
                        {
                            categories[i] = new RawCategory(categories[i]);
                            ((RawCategory)categories[i]).Data = reader.ReadBytes(refData.lengthV);
                            continue;
                        }
                    }

                    //Debug.WriteLine(refData.type + " " + GetHeaderReference(GetLinkReference(refData.connection)));
                    try
                    {
                        int nextStarting = (int)reader.BaseStream.Length - 4;
                        if ((categories.Count - 1) > i) nextStarting = categories[i + 1].ReferenceData.startV;

                        switch (GetHeaderReference(GetLinkReference(refData.connection)))
                        {
                            case "BlueprintGeneratedClass":
                            case "WidgetBlueprintGeneratedClass":
                                categories[i] = new BlueprintGeneratedClassCategory(categories[i]);
                                categories[i].Read(reader, nextStarting);
                                break;
                            case "Level":
                                categories[i] = new LevelCategory(categories[i]);
                                categories[i].Read(reader, nextStarting);
                                break;
                            case "StringTable":
                                categories[i] = new StringTableCategory(categories[i]);
                                categories[i].Read(reader, nextStarting);
                                break;
                            case "DataTable":
                                categories[i] = new DataTableCategory(categories[i]);
                                categories[i].Read(reader, nextStarting);
                                break;
                            default:
                                categories[i] = new NormalCategory(categories[i]);
                                categories[i].Read(reader, nextStarting);
                                break;
                        }

                        int extrasLen = nextStarting - (int)reader.BaseStream.Position;
                        if (extrasLen < 0)
                        {
                            throw new FormatException("Invalid padding at end of category " + (i + 1) + ": " + extrasLen + " bytes");
                        }
                        else
                        {
                            categories[i].Extras = reader.ReadBytes(extrasLen);
                        }
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        Debug.WriteLine("\nFailed to parse category " + (i + 1) + ": " + ex.ToString());
#endif
                        reader.BaseStream.Seek(refData.startV, SeekOrigin.Begin);
                        categories[i] = new RawCategory(categories[i]);
                        ((RawCategory)categories[i]).Data = reader.ReadBytes(refData.lengthV);
                    }
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
        public AssetReader(BinaryReader reader, int[] manualSkips = null, int[] forceReads = null)
        {
            Read(reader, manualSkips, forceReads);
        }

        public AssetReader(string path, int[] manualSkips = null, int[] forceReads = null)
        {
            this.path = path;
            Read(PathToReader(this.path), manualSkips, forceReads);
        }

        public AssetReader()
        {

        }
    }
}
