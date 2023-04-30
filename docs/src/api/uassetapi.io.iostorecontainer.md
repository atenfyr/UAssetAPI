# IOStoreContainer

Namespace: UAssetAPI.IO

Represents an IO store container (utoc/ucas).

```csharp
public class IOStoreContainer : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Fields

### **FilePathTOC**

The path of the .utoc file on disk.

```csharp
public string FilePathTOC;
```

### **HasReadToc**

```csharp
public bool HasReadToc;
```

### **TocVersion**

```csharp
public EIoStoreTocVersion TocVersion;
```

### **ChunkIds**

```csharp
public FIoChunkId[] ChunkIds;
```

### **ChunkMap**

```csharp
public Dictionary<FIoChunkId, FIoOffsetAndLength> ChunkMap;
```

### **CompressionMethods**

```csharp
public List<EIoCompressionMethod> CompressionMethods;
```

### **CompressionBlocks**

```csharp
public List<FIoStoreTocCompressedBlockEntry> CompressionBlocks;
```

### **Files**

```csharp
public Dictionary<string, FIoChunkId> Files;
```

### **_reserved0**

```csharp
public byte _reserved0;
```

### **_reserved1**

```csharp
public ushort _reserved1;
```

### **CompressionBlockSize**

```csharp
public uint CompressionBlockSize;
```

### **PartitionCount**

```csharp
public uint PartitionCount;
```

### **ContainerId**

```csharp
public ulong ContainerId;
```

### **EncryptionKeyGuid**

```csharp
public Guid EncryptionKeyGuid;
```

### **ContainerFlags**

```csharp
public EIoContainerFlags ContainerFlags;
```

### **_reserved3**

```csharp
public byte _reserved3;
```

### **_reserved4**

```csharp
public ushort _reserved4;
```

### **PartitionSize**

```csharp
public ulong PartitionSize;
```

### **_reserved7**

```csharp
public uint _reserved7;
```

### **_reserved8**

```csharp
public UInt64[] _reserved8;
```

### **TocSignature**

```csharp
public Byte[] TocSignature;
```

### **BlockSignature**

```csharp
public Byte[] BlockSignature;
```

### **ChunkHashes**

```csharp
public List<Byte[]> ChunkHashes;
```

### **MountPoint**

```csharp
public FString MountPoint;
```

### **DirectoryEntries**

```csharp
public List<FIoDirectoryEntry> DirectoryEntries;
```

### **FileEntries**

```csharp
public List<FIoFileEntry> FileEntries;
```

### **MetaData**

```csharp
public List<FIoStoreMetaData> MetaData;
```

### **TOC_MAGIC**

```csharp
public static Byte[] TOC_MAGIC;
```

## Constructors

### **IOStoreContainer(String)**

Reads an io store container from disk and initializes a new instance of the [IOStoreContainer](./uassetapi.io.iostorecontainer.md) class to store its data in memory.

```csharp
public IOStoreContainer(string tocPath)
```

#### Parameters

`tocPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path of the .utoc file on disk that this instance will read from. Respective .ucas files must be located in the same directory.

#### Exceptions

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **IOStoreContainer()**

Initializes a new instance of the [IOStoreContainer](./uassetapi.io.iostorecontainer.md) class. This instance will store no container data and does not represent any container in particular until the [IOStoreContainer.ReadToc(IOStoreBinaryReader)](./uassetapi.io.iostorecontainer.md#readtociostorebinaryreader) method is manually called.

```csharp
public IOStoreContainer()
```

## Methods

### **ClearNameIndexList()**

```csharp
internal void ClearNameIndexList()
```

### **FixNameMapLookupIfNeeded()**

```csharp
internal void FixNameMapLookupIfNeeded()
```

### **ContainsNameReference(FString)**

```csharp
internal bool ContainsNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **SearchNameReference(FString)**

```csharp
internal int SearchNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AddNameReference(FString, Boolean)**

```csharp
internal int AddNameReference(FString name, bool forceAddDuplicates)
```

#### Parameters

`name` [FString](./uassetapi.unrealtypes.fstring.md)<br>

`forceAddDuplicates` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **BeginRead()**

Opens a file stream for each partition in this container.
 This must be called before reading any CAS blocks.

```csharp
public void BeginRead()
```

### **EndRead()**

Closes all partition file streams.
 This must be called once reading CAS blocks has been finished.

```csharp
public void EndRead()
```

### **Dispose()**

```csharp
public void Dispose()
```

### **GetAllFiles()**

Returns a list of the path of every in this container.

```csharp
public String[] GetAllFiles()
```

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A list of the path of every in this container.

### **Extract(String)**

Extracts every file in this container to disk. This operation may take a while.

```csharp
public int Extract(string outPath)
```

#### Parameters

`outPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory to extract to.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The number of files that were successfully extracted.

### **ReadFile(String)**

Reads out a specific file within this container.

```csharp
public Byte[] ReadFile(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file in question.

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The raw data of the file.

### **DoesChunkExist(FIoChunkId)**

```csharp
public bool DoesChunkExist(FIoChunkId chunkId)
```

#### Parameters

`chunkId` [FIoChunkId](./uassetapi.io.fiochunkid.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ReadChunk(FIoChunkId)**

Reads out a specific chunk.

```csharp
public Byte[] ReadChunk(FIoChunkId chunkId)
```

#### Parameters

`chunkId` [FIoChunkId](./uassetapi.io.fiochunkid.md)<br>
The ID of the chunk to read.

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The raw data of the chunk in question.

### **ReadRaw(Int64, Int64)**

Reads out any segment of CAS data.

```csharp
public Byte[] ReadRaw(long offset, long length)
```

#### Parameters

`offset` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
The offset of the chunk to read.

`length` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
The length of the chunk to read.

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
The raw data that was read.

### **ReadToc(IOStoreBinaryReader)**

```csharp
public void ReadToc(IOStoreBinaryReader reader)
```

#### Parameters

`reader` [IOStoreBinaryReader](./uassetapi.io.iostorebinaryreader.md)<br>

### **PathToStream(String)**

Creates a MemoryStream from an asset path.

```csharp
public MemoryStream PathToStream(string p)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

#### Returns

[MemoryStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream)<br>
A new MemoryStream that stores the binary data of the input file.

### **PathToReader(String)**

Creates a BinaryReader from an asset path.

```csharp
public IOStoreBinaryReader PathToReader(string p)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

#### Returns

[IOStoreBinaryReader](./uassetapi.io.iostorebinaryreader.md)<br>
A new BinaryReader that stores the binary data of the input file.
