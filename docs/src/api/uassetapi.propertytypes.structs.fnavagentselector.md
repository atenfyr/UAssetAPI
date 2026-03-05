# FNavAgentSelector

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FNavAgentSelector
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FNavAgentSelector](./uassetapi.propertytypes.structs.fnavagentselector.md)<br>
Implements [IStruct&lt;FNavAgentSelector&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **PackedBits**

```csharp
public uint PackedBits;
```

## Constructors

### **FNavAgentSelector(UInt32)**

```csharp
FNavAgentSelector(uint packedBits)
```

#### Parameters

`packedBits` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FNavAgentSelector(AssetBinaryReader)**

```csharp
FNavAgentSelector(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FNavAgentSelector Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FNavAgentSelector](./uassetapi.propertytypes.structs.fnavagentselector.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FromString(String[], UAsset)**

```csharp
FNavAgentSelector FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FNavAgentSelector](./uassetapi.propertytypes.structs.fnavagentselector.md)<br>
