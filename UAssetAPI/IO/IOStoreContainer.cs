using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
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
                if (NameIndex < 0) return null;
                return ParentContainer.directoryStringMapIndexList[NameIndex];
            }
            set
            {
                NameIndex = value == null ? -1 : ParentContainer.AddNameReference(value);
            }
        }
        public FString FirstChildEntry
        {
            get
            {
                if (FirstChildEntryIndex < 0) return null;
                return ParentContainer.directoryStringMapIndexList[FirstChildEntryIndex];
            }
            set
            {
                FirstChildEntryIndex = value == null ? -1 : ParentContainer.AddNameReference(value);
            }
        }
        public FString NextSiblingEntry
        {
            get
            {
                if (NextSiblingEntryIndex < 0) return null;
                return ParentContainer.directoryStringMapIndexList[NextSiblingEntryIndex];
            }
            set
            {
                NextSiblingEntryIndex = value == null ? -1 : ParentContainer.AddNameReference(value);
            }
        }
        public FString FirstFileEntry
        {
            get
            {
                if (FirstFileEntryIndex < 0) return null;
                return ParentContainer.directoryStringMapIndexList[FirstFileEntryIndex];
            }
            set
            {
                FirstFileEntryIndex = value == null ? -1 : ParentContainer.AddNameReference(value);
            }
        }

        private int NameIndex;
        private int FirstChildEntryIndex;
        private int NextSiblingEntryIndex;
        private int FirstFileEntryIndex;

        public FIoDirectoryEntry(IOStoreContainer padre, int nameIndex, int firstChildEntryIndex, int nextSiblingEntryIndex, int firstFileEntryIndex)
        {
            ParentContainer = padre;
            NameIndex = nameIndex;
            FirstChildEntryIndex = firstChildEntryIndex;
            NextSiblingEntryIndex = nextSiblingEntryIndex;
            FirstFileEntryIndex = firstFileEntryIndex;
        }
    }

    public class FIoFileEntry
    {
        public IOStoreContainer ParentContainer;
        public FString Name
        {
            get
            {
                if (NameIndex < 0) return null;
                return ParentContainer.directoryStringMapIndexList[NameIndex];
            }
            set
            {
                NameIndex = value == null ? -1 : ParentContainer.AddNameReference(value);
            }
        }
        public FString NextFileEntry
        {
            get
            {
                if (NextFileEntryIndex < 0) return null;
                return ParentContainer.directoryStringMapIndexList[NextFileEntryIndex];
            }
            set
            {
                NextFileEntryIndex = value == null ? -1 : ParentContainer.AddNameReference(value);
            }
        }
        public FString UserData
        {
            get
            {
                if (UserDataIndex < 0) return null;
                return ParentContainer.directoryStringMapIndexList[UserDataIndex];
            }
            set
            {
                UserDataIndex = value == null ? -1 : ParentContainer.AddNameReference(value);
            }
        }

        private int NameIndex;
        private int NextFileEntryIndex;
        private int UserDataIndex;

        public FIoFileEntry(IOStoreContainer padre, int nameIndex, int nextFileEntryIndex, int userDataIndex)
        {
            ParentContainer = padre;
            NameIndex = nameIndex;
            NextFileEntryIndex = nextFileEntryIndex;
            UserDataIndex = userDataIndex;
        }
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
    public class IOStoreContainer
    {
        public EIoStoreTocVersion TocVersion;
        public List<EIoCompressionMethod> CompressionMethods;

        public byte _reserved0;
        public ushort _reserved1;
        public uint PartitionCount;
        public ulong ContainerId;
        public Guid EncryptionKeyGuid;
        public EIoContainerFlags ContainerFlags;
        public byte _reserved3;
        public ushort _reserved4;
        public uint _reserved7;
        public ulong[] _reserved8;

        public byte[] TocSignature;
        public byte[] BlockSignature;
        public List<byte[]> ChunkHashes;

        public FString MountPoint;
        public List<FIoDirectoryEntry> DirectoryEntries;
        public List<FIoFileEntry> FileEntries;
        public List<EIoStoreTocEntryMetaFlags> MetaData;

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

        public static readonly byte[] TOC_MAGIC = Encoding.ASCII.GetBytes("-==--==--==--==-");
        public void ReadToc(IOStoreBinaryReader reader)
        {
            if (!reader.ReadBytes(TOC_MAGIC.Length).SequenceEqual(TOC_MAGIC)) throw new FormatException("Invalid TOC magic");

            TocVersion = (EIoStoreTocVersion)reader.ReadByte();
            _reserved0 = reader.ReadByte();
            _reserved1 = reader.ReadUInt16();
            uint TocHeaderSize = reader.ReadUInt32();
            uint TocEntryCount = reader.ReadUInt32();
            uint TocCompressedBlockEntryCount = reader.ReadUInt32();
            uint TocCompressedBlockEntrySize = reader.ReadUInt32();
            uint CompressionMethodNameCount = reader.ReadUInt32();
            uint CompressionMethodNameLength = reader.ReadUInt32();
            uint CompressionBlockSize = reader.ReadUInt32();
            uint DirectoryIndexSize = reader.ReadUInt32();
            PartitionCount = reader.ReadUInt32();
            ContainerId = reader.ReadUInt64();
            EncryptionKeyGuid = new Guid(reader.ReadBytes(16));
            ContainerFlags = (EIoContainerFlags)reader.ReadByte();
            _reserved3 = reader.ReadByte();
            _reserved4 = reader.ReadUInt16();
            uint TocChunkPerfectHashSeedsCount = reader.ReadUInt32();
            ulong PartitionSize = reader.ReadUInt64();
            uint TocChunksWithoutPerfectHashCount = reader.ReadUInt32();
            _reserved7 = reader.ReadUInt32();
            _reserved8 = new ulong[5];
            for (int i = 0; i < _reserved8.Length; i++) _reserved8[i] = reader.ReadUInt64();
            reader.BaseStream.Position = UAPUtils.AlignPadding(reader.BaseStream.Position, 4);

            if (TocVersion < EIoStoreTocVersion.PartitionSize)
            {
                PartitionCount = 1;
                PartitionSize = uint.MaxValue;
            }

            FIoChunkId[] chunkIds = new FIoChunkId[TocEntryCount];
            for (int i = 0; i < TocEntryCount; i++) chunkIds[i] = FIoChunkId.Read(reader);

            FIoOffsetAndLength[] chunkOffsetsAndLengths = new FIoOffsetAndLength[TocEntryCount];
            for (int i = 0; i < TocEntryCount; i++) chunkOffsetsAndLengths[i] = FIoOffsetAndLength.Read(reader);

            TocChunkPerfectHashSeedsCount = TocVersion >= EIoStoreTocVersion.PerfectHash ? TocChunkPerfectHashSeedsCount : 0u;
            TocChunksWithoutPerfectHashCount = TocVersion >= EIoStoreTocVersion.PerfectHashWithOverflow ? TocChunksWithoutPerfectHashCount : 0u;

            int[] ChunkPerfectHashSeeds = new int[TocChunkPerfectHashSeedsCount];
            for (int i = 0; i < TocChunkPerfectHashSeedsCount; i++) ChunkPerfectHashSeeds[i] = reader.ReadInt32();
            int[] ChunkIndicesWithoutPerfectHash = new int[TocChunksWithoutPerfectHashCount];
            for (int i = 0; i < TocChunksWithoutPerfectHashCount; i++) ChunkIndicesWithoutPerfectHash[i] = reader.ReadInt32();

            FIoStoreTocCompressedBlockEntry[] CompressionBlocks = new FIoStoreTocCompressedBlockEntry[TocCompressedBlockEntryCount];
            for (int i = 0; i < TocCompressedBlockEntryCount; i++) CompressionBlocks[i] = FIoStoreTocCompressedBlockEntry.Read(reader);

            CompressionMethods = new List<EIoCompressionMethod>();
            CompressionMethods.Add(EIoCompressionMethod.None); // 0 = none
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

            if (TocVersion >= EIoStoreTocVersion.DirectoryIndex && ContainerFlags.HasFlag(EIoContainerFlags.Indexed) && DirectoryIndexSize > 0)
            {
                // todo: sometimes this stuff is encrypted, provide option for AES key
                MountPoint = reader.ReadFString();

                int numDiec = reader.ReadInt32();
                DirectoryEntries = new List<FIoDirectoryEntry>(numDiec);
                for (int i = 0; i < numDiec; i++) DirectoryEntries.Add(new FIoDirectoryEntry(this, reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()));

                int numFiec = reader.ReadInt32();
                FileEntries = new List<FIoFileEntry>(numFiec);
                for (int i = 0; i < numFiec; i++) FileEntries.Add(new FIoFileEntry(this, reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()));

                int numStrs = reader.ReadInt32();
                ClearNameIndexList();
                for (int i = 0; i < numStrs; i++) AddNameReference(reader.ReadFString(), true);
            }

            // meta data
            MetaData = new List<EIoStoreTocEntryMetaFlags>();
            for (int i = 0; i < TocEntryCount; i++)
            {
                var actualHash = reader.ReadBytes(20);
                var padding = reader.ReadBytes(32 - 20);
                var flags = (EIoStoreTocEntryMetaFlags)reader.ReadByte();
                MetaData.Add(flags);
            }
        }

        public MemoryStream WriteToc()
        {
            var stre = new MemoryStream();
            IOStoreBinaryWriter writer = new IOStoreBinaryWriter(stre, this);

            return stre;
        }

        public void ReadCas(IOStoreBinaryReader reader)
        {

        }

        public MemoryStream WriteCas()
        {
            var stre = new MemoryStream();
            IOStoreBinaryWriter writer = new IOStoreBinaryWriter(stre, this);

            return stre;
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

        /// <summary>
        /// Serializes and writes an asset to disk from memory.
        /// </summary>
        /// <param name="outputPath1">The path on disk to write the .utoc to.</param>
        /// <param name="outputPath2">The path on disk to write the .ucas to.</param>
        public void Write(string outputPath1, string outputPath2 = null)
        {
            if (outputPath2 == null) outputPath2 = Path.ChangeExtension(outputPath1, ".ucas");

            MemoryStream newData = WriteToc();
            using (FileStream f = File.Open(outputPath1, FileMode.Create, FileAccess.Write))
            {
                newData.CopyTo(f);
            }
            MemoryStream newData2 = WriteCas();
            using (FileStream f = File.Open(outputPath2, FileMode.Create, FileAccess.Write))
            {
                newData2.CopyTo(f);
            }
        }

        /// <summary>
        /// Reads an io store container from disk and initializes a new instance of the <see cref="IOStoreContainer"/> class to store its data in memory.
        /// </summary>
        /// <param name="path1">The path of the .utoc file on disk that this instance will read from.</param>
        /// <param name="path2">The path of the .ucas file on disk that this instance will read from.</param>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public IOStoreContainer(string path1, string path2 = null)
        {
            if (path2 == null) path2 = Path.ChangeExtension(path1, ".ucas");

            ReadToc(PathToReader(path1));
            ReadCas(PathToReader(path2));
        }
    }
}
