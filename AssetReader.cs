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
            return links[Utils.GetNormalIndex(index)];
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

        public int headerSize;
        public int sectionSixOffset;
        public int sectionOneStringCount;
        public int dataCategoryCount;
        public int sectionThreeOffset;
        public int sectionTwoLinkCount;
        public int sectionTwoOffset;
        public int sectionFourOffset;
        public int sectionFiveStringCount;
        public int sectionFiveOffset;
        public int uexpDataOffset;
        public int gapBeforeUexp;
        public int fileSize;

        // Do not directly add values to headerIndexList under any circumstances; use AddHeaderReference instead
        internal List<string> headerIndexList;
        private Dictionary<string, int> headerLookup = new Dictionary<string, int>();
        public List<Link> links; // base, class, link, connection
        public List<int[]> categoryIntReference;
        public List<string> categoryStringReference;
        public List<Category> categories;

        private static uint UASSET_MAGIC = 2653586369;
        private void ReadHeader(BinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            if (reader.ReadUInt32() != UASSET_MAGIC) throw new FormatException("File signature mismatch");

            reader.BaseStream.Seek(24, SeekOrigin.Begin); // 24
            sectionSixOffset = reader.ReadInt32();

            reader.ReadUString(); // Usually "None"
            reader.ReadSingle(); // Usually 0

            reader.BaseStream.Seek(41, SeekOrigin.Begin); // 41
            sectionOneStringCount = reader.ReadInt32();

            headerSize = reader.ReadInt32();

            reader.ReadInt64(); // uncertain, always 0

            reader.BaseStream.Seek(57, SeekOrigin.Begin); // 57
            dataCategoryCount = reader.ReadInt32();
            sectionThreeOffset = reader.ReadInt32(); // 61
            sectionTwoLinkCount = reader.ReadInt32(); // 65
            sectionTwoOffset = reader.ReadInt32(); // 69 (haha funny)
            sectionFourOffset = reader.ReadInt32(); // 73
            sectionFiveStringCount = reader.ReadInt32(); // 77
            sectionFiveOffset = reader.ReadInt32(); // 81

            switch (headerSize)
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

            reader.ReadUInt64(); // 85, Usually 0
            AssetGuid = new Guid(reader.ReadBytes(16));

            Debug.Assert(reader.ReadInt32() == 1); // 109
            Debug.Assert(reader.ReadInt32() == dataCategoryCount); // 113
            Debug.Assert(reader.ReadInt32() == sectionOneStringCount); // 117

            reader.ReadBytes(36); // 36 zeros

            // 157, weird 4-byte hash + 4 zeros

            reader.BaseStream.Seek(165, SeekOrigin.Begin); // 165
            uexpDataOffset = reader.ReadInt32();

            reader.BaseStream.Seek(169, SeekOrigin.Begin); // 169
            fileSize = reader.ReadInt32() + 4;

            reader.ReadBytes(12); // 12 zeros
        }

        public void Read(BinaryReader reader, int[] manualSkips = null, int[] forceReads = null)
        {
            // Header
            ReadHeader(reader);

            // Section 1
            reader.BaseStream.Position += 8;

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
                    garbage1 = reader.ReadInt32();
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
                        garbage2 = reader.ReadInt32();
                        startV = reader.ReadInt32(); // !!!
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
                try
                {
                    using (FileStream newStream = File.Open(Path.ChangeExtension(p, "uexp"), FileMode.Open))
                    {
                        completeStream.Seek(0, SeekOrigin.End);
                        newStream.CopyTo(completeStream);
                        UseSeparateBulkDataFiles = true;
                    }
                }
                catch (FileNotFoundException)
                {
                    UseSeparateBulkDataFiles = false;
                }

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
