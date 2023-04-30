# IOStoreBinaryReader

Namespace: UAssetAPI.IO

```csharp
public class IOStoreBinaryReader : UAssetAPI.UnrealBinaryReader, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BinaryReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader) → [UnrealBinaryReader](./uassetapi.unrealbinaryreader.md) → [IOStoreBinaryReader](./uassetapi.io.iostorebinaryreader.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Fields

### **Asset**

```csharp
public IOStoreContainer Asset;
```

## Properties

### **BaseStream**

```csharp
public Stream BaseStream { get; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Constructors

### **IOStoreBinaryReader(Stream, IOStoreContainer)**

```csharp
public IOStoreBinaryReader(Stream stream, IOStoreContainer asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`asset` [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>

## Methods

### **ReadFName(INameMap)**

```csharp
public FName ReadFName(INameMap nameMap)
```

#### Parameters

`nameMap` [INameMap](./uassetapi.io.inamemap.md)<br>

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>
