# SaveGame

Namespace: UAssetAPI.Unversioned

Represents an Unreal save game file. Parsing is only implemented for engine and custom version data.

```csharp
public class SaveGame
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SaveGame](./uassetapi.unversioned.savegame.md)

## Fields

### **FilePath**

The path of the file on disk.

```csharp
public string FilePath;
```

### **SaveGameFileVersion**

```csharp
public ESaveGameFileVersion SaveGameFileVersion;
```

### **ObjectVersion**

```csharp
public ObjectVersion ObjectVersion;
```

### **ObjectVersionUE5**

```csharp
public ObjectVersionUE5 ObjectVersionUE5;
```

### **EngineVersion**

```csharp
public FEngineVersion EngineVersion;
```

### **CustomVersionSerializationFormat**

```csharp
public ECustomVersionSerializationFormat CustomVersionSerializationFormat;
```

### **CustomVersionContainer**

All the custom versions stored in the archive.

```csharp
public List<CustomVersion> CustomVersionContainer;
```

### **SAVE_MAGIC**

```csharp
public static Byte[] SAVE_MAGIC;
```

## Constructors

### **SaveGame(String)**

Reads a save game from disk and initializes a new instance of the [SaveGame](./uassetapi.unversioned.savegame.md) class to store its data in memory.



Parsing is only implemented for engine and custom version data.

```csharp
public SaveGame(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path of the .sav file on disk that this instance will read from.

#### Exceptions

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **SaveGame()**

Initializes a new instance of the [SaveGame](./uassetapi.unversioned.savegame.md) class. This instance will store no file data and does not represent any file in particular until the [SaveGame.Read(UnrealBinaryReader)](./uassetapi.unversioned.savegame.md#readunrealbinaryreader) method is manually called.

```csharp
public SaveGame()
```

## Methods

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
public UnrealBinaryReader PathToReader(string p)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

#### Returns

[UnrealBinaryReader](./uassetapi.unrealbinaryreader.md)<br>
A new BinaryReader that stores the binary data of the input file.

### **Read(UnrealBinaryReader)**

Reads a save game from disk.



Parsing is only implemented for engine and custom version data.

```csharp
public void Read(UnrealBinaryReader reader)
```

#### Parameters

`reader` [UnrealBinaryReader](./uassetapi.unrealbinaryreader.md)<br>
The binary reader to use.

### **PatchUsmap(String)**

Patches a .usmap file to contain the versioning info within this save file.

```csharp
public void PatchUsmap(string usmapPath)
```

#### Parameters

`usmapPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the .usmap file to patch.
