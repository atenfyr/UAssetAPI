# FEntry

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FEntry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FEntry](./uassetapi.propertytypes.structs.fentry.md)

## Fields

### **StartIndex**

The index into Items of the first item

```csharp
public int StartIndex;
```

### **Size**

The number of currently valid items

```csharp
public int Size;
```

### **Capacity**

The total capacity of allowed items before reallocating

```csharp
public int Capacity;
```

## Constructors

### **FEntry(Int32, Int32, Int32)**

```csharp
FEntry(int startIndex, int size, int capacity)
```

#### Parameters

`startIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`size` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`capacity` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FEntry(AssetBinaryReader)**

```csharp
FEntry(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
