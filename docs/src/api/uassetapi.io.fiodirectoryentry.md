# FIoDirectoryEntry

Namespace: UAssetAPI.IO

```csharp
public class FIoDirectoryEntry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FIoDirectoryEntry](./uassetapi.io.fiodirectoryentry.md)

## Fields

### **ParentContainer**

```csharp
public IOStoreContainer ParentContainer;
```

### **FirstChildEntry**

```csharp
public uint FirstChildEntry;
```

### **NextSiblingEntry**

```csharp
public uint NextSiblingEntry;
```

### **FirstFileEntry**

```csharp
public uint FirstFileEntry;
```

## Properties

### **Name**

```csharp
public FString Name { get; set; }
```

#### Property Value

[FString](./uassetapi.unrealtypes.fstring.md)<br>

## Constructors

### **FIoDirectoryEntry(IOStoreContainer, UInt32, UInt32, UInt32, UInt32)**

```csharp
public FIoDirectoryEntry(IOStoreContainer padre, uint nameIndex, uint firstChildEntryIndex, uint nextSiblingEntryIndex, uint firstFileEntryIndex)
```

#### Parameters

`padre` [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>

`nameIndex` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`firstChildEntryIndex` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`nextSiblingEntryIndex` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`firstFileEntryIndex` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
