# FIoFileEntry

Namespace: UAssetAPI.IO

```csharp
public class FIoFileEntry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FIoFileEntry](./uassetapi.io.fiofileentry.md)

## Fields

### **ParentContainer**

```csharp
public IOStoreContainer ParentContainer;
```

### **NextFileEntry**

```csharp
public uint NextFileEntry;
```

### **UserData**

```csharp
public uint UserData;
```

## Properties

### **Name**

```csharp
public FString Name { get; set; }
```

#### Property Value

[FString](./uassetapi.unrealtypes.fstring.md)<br>

## Constructors

### **FIoFileEntry(IOStoreContainer, UInt32, UInt32, UInt32)**

```csharp
public FIoFileEntry(IOStoreContainer padre, uint nameIndex, uint nextFileEntryIndex, uint userDataIndex)
```

#### Parameters

`padre` [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>

`nameIndex` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`nextFileEntryIndex` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`userDataIndex` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
