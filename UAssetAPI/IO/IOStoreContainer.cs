using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.IO
{
    /// <summary>
    /// IO store container format version
    /// </summary>
    public enum EIoStoreTocVersion : byte
    {
        Invalid = 0,
        Initial,
        DirectoryIndex,
        PartitionSize,
        PerfectHash,
        PerfectHashWithOverflow,
        LatestPlusOne,
        Latest = LatestPlusOne - 1
    }

    [Flags]
    public enum EIoContainerFlags : byte
    {
        None = 0,
        Compressed = 1,
        Encrypted = 2,
        Signed = 4,
        Indexed = 8,
    }

    /// <summary>
    /// EIoChunkType in UE4
    /// </summary>
    public enum EIoChunkType4 : byte
    {
        Invalid,
	    InstallManifest,
	    ExportBundleData,
	    BulkData,
	    OptionalBulkData,
	    MemoryMappedBulkData,
	    LoaderGlobalMeta,
	    LoaderInitialLoadMeta,
	    LoaderGlobalNames,
	    LoaderGlobalNameHashes,
	    ContainerHeader
    }

    /// <summary>
    /// EIoChunkType in UE5
    /// </summary>
    public enum EIoChunkType5 : byte
    {
        Invalid = 0,
        ExportBundleData = 1,
        BulkData = 2,
        OptionalBulkData = 3,
        MemoryMappedBulkData = 4,
        ScriptObjects = 5,
        ContainerHeader = 6,
        ExternalFile = 7,
        ShaderCodeLibrary = 8,
        ShaderCode = 9,
        PackageStoreEntry = 10,
        DerivedData = 11,
        EditorDerivedData = 12,

        MAX
    }

    /// <summary>
    /// Identifier to a chunk of data.
    /// </summary>
    public struct FIoChunkId
    {
        public ulong ChunkId;
        public ushort ChunkIndex;
        public byte ChunkType;
        public EIoChunkType4 ChunkType4 => (EIoChunkType4)ChunkType;
        public EIoChunkType5 ChunkType5 => (EIoChunkType5)ChunkType;

        public static FIoChunkId Read(IOStoreBinaryReader reader)
        {
            var res = new FIoChunkId();
            res.ChunkId = reader.ReadUInt64();
            var temp = reader.ReadUInt16();
            reader.ReadByte(); // garbage data
            res.ChunkType = reader.ReadByte();
            res.ChunkIndex = (ushort)((temp & 0xFF) << 8 | (temp & 0xFF00) >> 8); // switch first 8 bits and last 8 bits
            return res;
        }

        public static byte[] Pack(ulong v1, ushort v2_, byte v3)
        {
            var v2 = (ushort)((v2_ & 0xFF) << 8 | (v2_ & 0xFF00) >> 8);

            byte[] res = new byte[12];
            BitConverter.GetBytes(v1).CopyTo(res, 0);
            BitConverter.GetBytes(v2).CopyTo(res, 8);
            res[10] = 0;
            res[11] = v3;
            return res;
        }

        public byte[] Pack()
        {
            return FIoChunkId.Pack(ChunkId, ChunkIndex, ChunkType);
        }

        public static int Write(IOStoreBinaryWriter writer, ulong v1, ushort v2, byte v3)
        {
            byte[] res = Pack(v1, v2, v3);
            writer.Write(res);
            return res.Length;
        }

        public int Write(IOStoreBinaryWriter writer)
        {
            return FIoChunkId.Write(writer, ChunkId, ChunkIndex, ChunkType);
        }

        public FIoChunkId(ulong chunkId, ushort chunkIndex, byte chunkType)
        {
            ChunkId = chunkId;
            ChunkIndex = chunkIndex;
            ChunkType = chunkType;
        }

        public FIoChunkId(ulong chunkId, ushort chunkIndex, EIoChunkType4 chunkType)
        {
            ChunkId = chunkId;
            ChunkIndex = chunkIndex;
            ChunkType = (byte)chunkType;
        }

        public FIoChunkId(ulong chunkId, ushort chunkIndex, EIoChunkType5 chunkType)
        {
            ChunkId = chunkId;
            ChunkIndex = chunkIndex;
            ChunkType = (byte)chunkType;
        }
    }

    public struct FIoOffsetAndLength
    {
        public ulong Offset;
        public ulong Length;

        public static FIoOffsetAndLength Read(IOStoreBinaryReader reader)
        {
            var res = new FIoOffsetAndLength();
            var packed = reader.ReadBytes(10);
            res.Offset = packed[4] | ((ulong)packed[3] << 8) | ((ulong)packed[2] << 16) | ((ulong)packed[1] << 24) | ((ulong)packed[0] << 32); // someone made the active decision to put this into the engine
            res.Length = packed[9] | ((ulong)packed[8] << 8) | ((ulong)packed[7] << 16) | ((ulong)packed[6] << 24) | ((ulong)packed[5] << 32);
            return res;
        }

        public static int Write(IOStoreBinaryWriter writer, ulong v1, ulong v2)
        {
            byte[] res = new byte[10];
            res[0] = (byte)(v1 >> 32);
            res[1] = (byte)(v1 >> 24);
            res[2] = (byte)(v1 >> 16);
            res[3] = (byte)(v1 >> 8);
            res[4] = (byte)v1;

            res[5] = (byte)(v2 >> 32);
            res[6] = (byte)(v2 >> 24);
            res[7] = (byte)(v2 >> 16);
            res[8] = (byte)(v2 >> 8);
            res[9] = (byte)v2;

            writer.Write(res);
            return res.Length;
        }

        public int Write(IOStoreBinaryWriter writer)
        {
            return FIoOffsetAndLength.Write(writer, Offset, Length);
        }
    }

    /// <summary>
    /// Compression block entry.
    /// </summary>
    public struct FIoStoreTocCompressedBlockEntry
    {
        public readonly static int OffsetBits = 40;
        public readonly static ulong OffsetMask = (1ul << OffsetBits) - 1ul;
	    public readonly static int SizeBits = 24;
        public readonly static uint SizeMask = (1u << SizeBits) - 1u;
        public readonly static int SizeShift = 8;

        public ulong Offset;
        public uint CompressedSize;
        public uint UncompressedSize;
        public byte CompressionMethodIndex;

        // God has cursed me for my hubris
        public static FIoStoreTocCompressedBlockEntry Read(IOStoreBinaryReader reader)
        {
            var res = new FIoStoreTocCompressedBlockEntry();
            var packed1 = reader.ReadUInt64();
            var packed2 = reader.ReadUInt32();
            res.Offset = (ulong)(packed1 & OffsetMask);
            res.CompressedSize = (uint)(packed1 >> OffsetBits);
            res.UncompressedSize = (uint)(packed2 & SizeMask);
            res.CompressionMethodIndex = (byte)(packed2 >> SizeBits);
            return res;
        }

        public static int Write(IOStoreBinaryWriter writer, ulong v1, uint v2, uint v3, byte v4)
        {
            ulong packed1 = (v1 & OffsetMask) | ((ulong)v2 << OffsetBits);
            uint packed2 = (v3 & SizeMask) | ((uint)v4 << SizeBits);
            writer.Write(packed1);
            writer.Write(packed2);
            return sizeof(ulong) + sizeof(uint);
        }

        public int Write(IOStoreBinaryWriter writer)
        {
            return FIoStoreTocCompressedBlockEntry.Write(writer, Offset, CompressedSize, UncompressedSize, CompressionMethodIndex);
        }
    }

    public class FIoDirectoryEntry
    {
        public IOStoreContainer ParentContainer;
        public FString Name
        {
            get
            {
                if (NameIndex == uint.MaxValue) return null;
                return ParentContainer.directoryStringMapIndexList[(int)NameIndex];
            }
            set
            {
                NameIndex = value == null ? uint.MaxValue : (uint)ParentContainer.AddNameReference(value);
            }
        }

        private uint NameIndex;
        public uint FirstChildEntry;
        public uint NextSiblingEntry;
        public uint FirstFileEntry;

        public FIoDirectoryEntry(IOStoreContainer padre, uint nameIndex, uint firstChildEntryIndex, uint nextSiblingEntryIndex, uint firstFileEntryIndex)
        {
            ParentContainer = padre;
            NameIndex = nameIndex;
            FirstChildEntry = firstChildEntryIndex;
            NextSiblingEntry = nextSiblingEntryIndex;
            FirstFileEntry = firstFileEntryIndex;
        }
    }

    public class FIoFileEntry
    {
        public IOStoreContainer ParentContainer;
        public FString Name
        {
            get
            {
                if (NameIndex == uint.MaxValue) return null;
                return ParentContainer.directoryStringMapIndexList[(int)NameIndex];
            }
            set
            {
                NameIndex = value == null ? uint.MaxValue : (uint)ParentContainer.AddNameReference(value);
            }
        }

        private uint NameIndex;
        public uint NextFileEntry;
        public uint UserData;

        public FIoFileEntry(IOStoreContainer padre, uint nameIndex, uint nextFileEntryIndex, uint userDataIndex)
        {
            ParentContainer = padre;
            NameIndex = nameIndex;
            NextFileEntry = nextFileEntryIndex;
            UserData = userDataIndex;
        }
    }

    public struct FIoStoreMetaData
    {
        public byte[] SHA1Hash;
        public EIoStoreTocEntryMetaFlags Flags;
    }

    [Flags]
    public enum EIoStoreTocEntryMetaFlags : byte
    {
        None,
        Compressed = 1,
        MemoryMapped = 2
    }

    /// <summary>
    /// Enum of known compression methods. Serialized as a string, but stored as an enum in UAssetAPI for convenience.
    /// </summary>
    public enum EIoCompressionMethod
    {
        None = 0,
        Zlib,
        Gzip,
        Custom,
        Oodle,
        LZ4,
        Unknown
    }

    /// <summary>
    /// Represents an IO store container (utoc/ucas).
    /// </summary>
    public class IOStoreContainer : IDisposable
    {
        /// <summary>
        /// The path of the .utoc file on disk.
        /// </summary>
        [JsonIgnore]
        public string FilePathTOC = null;

        public bool HasReadToc = false;
        public EIoStoreTocVersion TocVersion;
        public FIoChunkId[] ChunkIds;
        public Dictionary<FIoChunkId, FIoOffsetAndLength> ChunkMap;
        public List<EIoCompressionMethod> CompressionMethods;
        public List<FIoStoreTocCompressedBlockEntry> CompressionBlocks;
        public Dictionary<string, FIoChunkId> Files;

        public byte _reserved0;
        public ushort _reserved1;
        public uint CompressionBlockSize;
        public uint PartitionCount;
        public ulong ContainerId;
        public Guid EncryptionKeyGuid;
        public EIoContainerFlags ContainerFlags;
        public byte _reserved3;
        public ushort _reserved4;
        public ulong PartitionSize;
        public uint _reserved7;
        public ulong[] _reserved8;

        public byte[] TocSignature;
        public byte[] BlockSignature;
        public List<byte[]> ChunkHashes;

        public FString MountPoint;
        public List<FIoDirectoryEntry> DirectoryEntries;
        public List<FIoFileEntry> FileEntries;
        public List<FIoStoreMetaData> MetaData;

        internal List<FString> directoryStringMapIndexList;
        internal Dictionary<string, int> directoryStringMapLookup;

        internal void ClearNameIndexList()
        {
            directoryStringMapIndexList = new List<FString>();
            directoryStringMapLookup = new Dictionary<string, int>();
        }

        internal void FixNameMapLookupIfNeeded()
        {
            if (directoryStringMapIndexList.Count > 0 && directoryStringMapLookup.Count == 0)
            {
                for (int i = 0; i < directoryStringMapIndexList.Count; i++)
                {
                    directoryStringMapLookup[directoryStringMapIndexList[i].Value] = i;
                }
            }
        }

        internal bool ContainsNameReference(FString search)
        {
            FixNameMapLookupIfNeeded();
            return directoryStringMapLookup.ContainsKey(search.Value);
        }

        internal int SearchNameReference(FString search)
        {
            //FixNameMapLookupIfNeeded();
            if (ContainsNameReference(search)) return directoryStringMapLookup[search.Value];
            throw new NameMapOutOfRangeException(search);
        }

        internal int AddNameReference(FString name, bool forceAddDuplicates = false)
        {
            FixNameMapLookupIfNeeded();

            if (!forceAddDuplicates)
            {
                if (name?.Value == null) throw new ArgumentException("Cannot add a null FString to the name map");
                if (name.Value == string.Empty) throw new ArgumentException("Cannot add an empty FString to the name map");
                if (ContainsNameReference(name)) return SearchNameReference(name);
            }

            directoryStringMapIndexList.Add(name);
            directoryStringMapLookup[name.Value] = directoryStringMapIndexList.Count - 1;
            return directoryStringMapIndexList.Count - 1;
        }

        private readonly List<IOStoreBinaryReader> PartitionStreams = new List<IOStoreBinaryReader>();
        /// <summary>
        /// Opens a file stream for each partition in this container.
        /// This must be called before reading any CAS blocks.
        /// </summary>
        public void BeginRead()
        {
            if (FilePathTOC == null) throw new InvalidOperationException("FilePathTOC must be defined before beginning to read");
            if (!HasReadToc) ReadToc(PathToReader(FilePathTOC));
            if (PartitionStreams.Count > 0) return; // already ready

            GetPartitionInfo(out int numPartitions, out _);
            for (int i = 0; i < numPartitions; i++)
            {
                PartitionStreams.Add(new IOStoreBinaryReader(File.Open(GetPartitionFileName(FilePathTOC, i), FileMode.Open, FileAccess.Read), this));
            }
        }

        /// <summary>
        /// Closes all partition file streams.
        /// This must be called once reading CAS blocks has been finished.
        /// </summary>
        public void EndRead()
        {
            if (PartitionStreams == null) return;
            for (int i = 0; i < PartitionStreams.Count; i++) PartitionStreams[i].Dispose();
            PartitionStreams.Clear();
        }

        public void Dispose()
        {
            EndRead();
        }

        /// <summary>
        /// Returns a list of the path of every in this container.
        /// </summary>
        /// <returns>A list of the path of every in this container.</returns>
        public string[] GetAllFiles()
        {
            string[] res = new string[Files.Count];
            int i = 0;
            foreach (KeyValuePair<string, FIoChunkId> entry in Files) res[i++] = entry.Key;
            return res;
        }

        /// <summary>
        /// Extracts every file in this container to disk. This operation may take a while.
        /// </summary>
        /// <param name="outPath">The directory to extract to.</param>
        /// <returns>The number of files that were successfully extracted.</returns>
        public int Extract(string outPath)
        {
            int n = 0;
            Directory.CreateDirectory(outPath);
            foreach (KeyValuePair<string, FIoChunkId> entry in Files)
            {
                Directory.CreateDirectory(Path.Combine(outPath, Path.GetDirectoryName(entry.Key)));
                byte[] f = ReadChunk(entry.Value);
                File.WriteAllBytes(Path.Combine(outPath, entry.Key), f);
                n++;
            }
            return n;
        }

        /// <summary>
        /// Reads out a specific file within this container.
        /// </summary>
        /// <param name="path">The path to the file in question.</param>
        /// <returns>The raw data of the file.</returns>
        public byte[] ReadFile(string path)
        {
            if (!Files.ContainsKey(path)) return new byte[0];
            return ReadChunk(Files[path]);
        }

        public bool DoesChunkExist(FIoChunkId chunkId) => ChunkMap.ContainsKey(chunkId);

        /// <summary>
        /// Reads out a specific chunk.
        /// </summary>
        /// <param name="chunkId">The ID of the chunk to read.</param>
        /// <returns>The raw data of the chunk in question.</returns>
        public byte[] ReadChunk(FIoChunkId chunkId)
        {
            if (chunkId.ChunkType == 0 || !DoesChunkExist(chunkId)) return new byte[0];
            return ReadRaw((long)ChunkMap[chunkId].Offset, (long)ChunkMap[chunkId].Length);
        }

        /// <summary>
        /// Reads out any segment of CAS data.
        /// </summary>
        /// <param name="offset">The offset of the chunk to read.</param>
        /// <param name="length">The length of the chunk to read.</param>
        /// <returns>The raw data that was read.</returns>
        public byte[] ReadRaw(long offset, long length)
        {
            if (PartitionStreams == null || PartitionStreams.Count == 0) throw new InvalidOperationException("BeginRead must be called before reading CAS data");

            var res = new byte[length];
            int resDoneSoFar = 0;
            int firstBlock = (int)(offset / CompressionBlockSize);
            int lastBlock = (int)((UAPUtils.AlignPadding(offset + length, (int)CompressionBlockSize) - 1) / CompressionBlockSize);
            long startingOffset = offset % CompressionBlockSize;
            for (int i = firstBlock; i <= lastBlock; i++)
            {
                FIoStoreTocCompressedBlockEntry ourBlock = CompressionBlocks[i];
                EIoCompressionMethod compressionType = ourBlock.CompressionMethodIndex == 0 ? EIoCompressionMethod.None : CompressionMethods[ourBlock.CompressionMethodIndex - 1];

                int partitionIdx = (int)(ourBlock.Offset / PartitionSize);
                long offsetWithinPartition = (long)(ourBlock.Offset - ((ulong)partitionIdx * PartitionSize));
                if (partitionIdx > PartitionStreams.Count) throw new FormatException("Attempt to reference a partition that doesn't exist");
                IOStoreBinaryReader chosenReader = PartitionStreams[partitionIdx];

                chosenReader.BaseStream.Position = offsetWithinPartition;
                byte[] compressedBuffer = chosenReader.ReadBytes(UAPUtils.AlignPadding((int)ourBlock.CompressedSize, 16));
                // TODO: decrypt compressed buffer if necessary

                // decompress
                byte[] uncompressedBuffer = new byte[ourBlock.UncompressedSize];
                switch (compressionType)
                {
                    case EIoCompressionMethod.None:
                        uncompressedBuffer = compressedBuffer;
                        break;
                    case EIoCompressionMethod.Oodle:
                        uncompressedBuffer = Oodle.Decompress(compressedBuffer, compressedBuffer.Length, uncompressedBuffer.Length);
                        break;
                    default:
                        throw new NotImplementedException("Unimplemented compression method " + compressionType);
                }

                int amountToAdd = Math.Min(res.Length - resDoneSoFar, uncompressedBuffer.Length);
                Buffer.BlockCopy(uncompressedBuffer, (int)startingOffset, res, resDoneSoFar, amountToAdd);
                startingOffset = 0;
                resDoneSoFar += amountToAdd;
            }

            return res;
        }

        public static readonly byte[] TOC_MAGIC = Encoding.ASCII.GetBytes("-==--==--==--==-");
        public void ReadToc(IOStoreBinaryReader reader)
        {
            if (!reader.ReadBytes(TOC_MAGIC.Length).SequenceEqual(TOC_MAGIC)) throw new FormatException("Invalid TOC magic");

            TocVersion = (EIoStoreTocVersion)reader.ReadByte();
            if (TocVersion < EIoStoreTocVersion.Initial || TocVersion > EIoStoreTocVersion.Latest) throw new FormatException("Invalid TOC version " + TocVersion);
            _reserved0 = reader.ReadByte();
            _reserved1 = reader.ReadUInt16();
            uint TocHeaderSize = reader.ReadUInt32();
            uint TocEntryCount = reader.ReadUInt32();
            uint TocCompressedBlockEntryCount = reader.ReadUInt32();
            uint TocCompressedBlockEntrySize = reader.ReadUInt32();
            uint CompressionMethodNameCount = reader.ReadUInt32();
            uint CompressionMethodNameLength = reader.ReadUInt32();
            CompressionBlockSize = reader.ReadUInt32();
            uint DirectoryIndexSize = reader.ReadUInt32();
            PartitionCount = reader.ReadUInt32();
            ContainerId = reader.ReadUInt64();
            EncryptionKeyGuid = new Guid(reader.ReadBytes(16));
            ContainerFlags = (EIoContainerFlags)reader.ReadByte();
            _reserved3 = reader.ReadByte();
            _reserved4 = reader.ReadUInt16();
            uint TocChunkPerfectHashSeedsCount = reader.ReadUInt32();
            PartitionSize = reader.ReadUInt64();
            uint TocChunksWithoutPerfectHashCount = reader.ReadUInt32();
            _reserved7 = reader.ReadUInt32();
            _reserved8 = new ulong[5];
            for (int i = 0; i < _reserved8.Length; i++) _reserved8[i] = reader.ReadUInt64();
            reader.BaseStream.Position = UAPUtils.AlignPadding(reader.BaseStream.Position, 4);

            // TocHeaderSize == reader.BaseStream.Position

            ChunkMap = new Dictionary<FIoChunkId, FIoOffsetAndLength>();
            ChunkIds = new FIoChunkId[TocEntryCount];
            for (int i = 0; i < TocEntryCount; i++) ChunkIds[i] = FIoChunkId.Read(reader);

            FIoOffsetAndLength[] chunkOffsetsAndLengths = new FIoOffsetAndLength[TocEntryCount];
            for (int i = 0; i < TocEntryCount; i++)
            {
                chunkOffsetsAndLengths[i] = FIoOffsetAndLength.Read(reader);
                ChunkMap[ChunkIds[i]] = chunkOffsetsAndLengths[i];
            }

            TocChunkPerfectHashSeedsCount = TocVersion >= EIoStoreTocVersion.PerfectHash ? TocChunkPerfectHashSeedsCount : 0u;
            TocChunksWithoutPerfectHashCount = TocVersion >= EIoStoreTocVersion.PerfectHashWithOverflow ? TocChunksWithoutPerfectHashCount : 0u;

            int[] ChunkPerfectHashSeeds = new int[TocChunkPerfectHashSeedsCount];
            for (int i = 0; i < TocChunkPerfectHashSeedsCount; i++) ChunkPerfectHashSeeds[i] = reader.ReadInt32();
            int[] ChunkIndicesWithoutPerfectHash = new int[TocChunksWithoutPerfectHashCount];
            for (int i = 0; i < TocChunksWithoutPerfectHashCount; i++)
            {
                ChunkIndicesWithoutPerfectHash[i] = reader.ReadInt32();
                ChunkMap[ChunkIds[ChunkIndicesWithoutPerfectHash[i]]] = chunkOffsetsAndLengths[ChunkIndicesWithoutPerfectHash[i]];
            }

            CompressionBlocks = new List<FIoStoreTocCompressedBlockEntry>((int)TocCompressedBlockEntryCount);
            for (int i = 0; i < TocCompressedBlockEntryCount; i++) CompressionBlocks.Add(FIoStoreTocCompressedBlockEntry.Read(reader));

            CompressionMethods = new List<EIoCompressionMethod>();
            for (int i = 0; i < CompressionMethodNameCount; i++)
            {
                byte[] methodBytes = reader.ReadBytes((int)CompressionMethodNameLength);
                int lastGoodIdx = methodBytes.Length - 1;
                while (methodBytes[lastGoodIdx] == 0) lastGoodIdx--;
                byte[] methodBytesTrimmed = new byte[lastGoodIdx + 1];
                Array.Copy(methodBytes, methodBytesTrimmed, lastGoodIdx + 1);

                string methodString = Encoding.ASCII.GetString(methodBytesTrimmed);
                CompressionMethods.Add(Enum.TryParse(methodString, out EIoCompressionMethod method) ? method : EIoCompressionMethod.Unknown);
            }

            // signatures
            if (ContainerFlags.HasFlag(EIoContainerFlags.Signed))
            {
                // i haven't seen this in practice, so im not 100% sure how these hashes are actually determined; we just store it for now
                int hashSize = reader.ReadInt32();
                TocSignature = reader.ReadBytes(hashSize);
                BlockSignature = reader.ReadBytes(hashSize);
                for (int i = 0; i < TocCompressedBlockEntryCount; i++)
                {
                    ChunkHashes.Add(reader.ReadBytes(20));
                }
            }

            Files = new Dictionary<string, FIoChunkId>();
            if (TocVersion >= EIoStoreTocVersion.DirectoryIndex && ContainerFlags.HasFlag(EIoContainerFlags.Indexed) && DirectoryIndexSize > 0)
            {
                // todo: sometimes this stuff is encrypted, provide option for AES key
                MountPoint = reader.ReadFString();

                int numDiec = reader.ReadInt32();
                DirectoryEntries = new List<FIoDirectoryEntry>(numDiec);
                for (int i = 0; i < numDiec; i++) DirectoryEntries.Add(new FIoDirectoryEntry(this, reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32()));

                int numFiec = reader.ReadInt32();
                FileEntries = new List<FIoFileEntry>(numFiec);
                for (int i = 0; i < numFiec; i++) FileEntries.Add(new FIoFileEntry(this, reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32()));

                int numStrs = reader.ReadInt32();
                ClearNameIndexList();
                for (int i = 0; i < numStrs; i++) AddNameReference(reader.ReadFString(), true);

                ParseDirectory(string.Empty, 0u);
            }

            // meta data
            MetaData = new List<FIoStoreMetaData>();
            for (int i = 0; i < TocEntryCount; i++)
            {
                var res = new FIoStoreMetaData();
                res.SHA1Hash = reader.ReadBytes(20);
                reader.ReadBytes(32 - 20); // padding
                res.Flags = (EIoStoreTocEntryMetaFlags)reader.ReadByte();
                MetaData.Add(res);
            }

            HasReadToc = true;
        }

        private void ParseDirectory(string parentPath, uint idx)
        {
            while (idx != uint.MaxValue)
            {
                FIoDirectoryEntry dirEntry = DirectoryEntries[(int)idx];
                string thisPath = dirEntry.Name == null ? parentPath : ((string.IsNullOrWhiteSpace(parentPath) ? "" : (parentPath + "/")) + dirEntry.Name.Value);

                // first parse files
                uint subFile = dirEntry.FirstFileEntry;
                while (subFile != uint.MaxValue)
                {
                    FIoFileEntry fileEntry = FileEntries[(int)subFile];
                    Files[thisPath + "/" + fileEntry.Name.Value] = ChunkIds[fileEntry.UserData];
                    subFile = fileEntry.NextFileEntry;
                }

                ParseDirectory(thisPath, dirEntry.FirstChildEntry);
                idx = dirEntry.NextSiblingEntry;
            }
        }

        /// <summary>
        /// Creates a MemoryStream from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new MemoryStream that stores the binary data of the input file.</returns>
        public MemoryStream PathToStream(string p)
        {
            using (FileStream origStream = File.Open(p, FileMode.Open))
            {
                MemoryStream completeStream = new MemoryStream();
                origStream.CopyTo(completeStream);

                completeStream.Seek(0, SeekOrigin.Begin);
                return completeStream;
            }
        }

        /// <summary>
        /// Creates a BinaryReader from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new BinaryReader that stores the binary data of the input file.</returns>
        public IOStoreBinaryReader PathToReader(string p)
        {
            return new IOStoreBinaryReader(PathToStream(p), this);
        }

        private void GetPartitionInfo(out int numPartitions, out ulong partitionSize)
        {
            numPartitions = (TocVersion < EIoStoreTocVersion.PartitionSize || PartitionCount == 0) ? 1 : (int)PartitionCount;
            partitionSize = TocVersion < EIoStoreTocVersion.PartitionSize ? uint.MaxValue : PartitionSize;
        }

        private string GetPartitionFileName(string tocPath, int num)
        {
            if (num == 0) return Path.ChangeExtension(tocPath, ".ucas");
            return Path.ChangeExtension(Path.Combine(Path.GetDirectoryName(tocPath), Path.GetFileNameWithoutExtension(tocPath) + "_s" + num), ".ucas");
        }

        /*public MemoryStream WriteToc()
        {
            var stre = new MemoryStream();
            IOStoreBinaryWriter writer = new IOStoreBinaryWriter(stre, this);

            return stre;
        }

        /// <summary>
        /// Writes a specific partition to disk.
        /// </summary>
        /// <param name="stre">A file stream to the partition in question.</param>
        /// <param name="partitionNum">The index of this partition.</param>
        /// <param name="partitionSize">The maximum size of this partition. This will be automatically increased if required.</param>
        public void WriteCas(Stream stre, int partitionNum, ulong partitionSize)
        {
            IOStoreBinaryWriter writer = new IOStoreBinaryWriter(stre, this);
        }

        /// <summary>
        /// Serializes and writes a container to disk from memory.
        /// </summary>
        /// <param name="outputPath">The path on disk to write the .utoc file to. Respective .ucas files will be located in the same directory.</param>
        public void Write(string outputPath)
        {
            GetPartitionInfo(out int numPartitions, out ulong partitionSize);

            for (int i = 0; i < numPartitions; i++)
            {
                using (FileStream f = File.Open(GetPartitionFileName(outputPath, i), FileMode.Open, FileAccess.Write))
                {
                    WriteCas(f, i, partitionSize);
                }
            }

            MemoryStream newData = WriteToc();
            using (FileStream f = File.Open(outputPath, FileMode.Create, FileAccess.Write))
            {
                newData.CopyTo(f);
            }
        }*/

        /// <summary>
        /// Reads an io store container from disk and initializes a new instance of the <see cref="IOStoreContainer"/> class to store its data in memory.
        /// </summary>
        /// <param name="tocPath">The path of the .utoc file on disk that this instance will read from. Respective .ucas files must be located in the same directory.</param>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public IOStoreContainer(string tocPath)
        {
            FilePathTOC = tocPath;
            ReadToc(PathToReader(FilePathTOC));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IOStoreContainer"/> class. This instance will store no container data and does not represent any container in particular until the <see cref="ReadToc"/> method is manually called.
        /// </summary>
        public IOStoreContainer()
        {

        }
    }
}
