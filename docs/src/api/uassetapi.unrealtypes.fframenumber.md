# FFrameNumber

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FFrameNumber
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FFrameNumber](./uassetapi.unrealtypes.fframenumber.md)<br>
Implements [IStruct&lt;FFrameNumber&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **Value**

```csharp
public int Value;
```

## Constructors

### **FFrameNumber()**

```csharp
FFrameNumber()
```

### **FFrameNumber(Int32)**

```csharp
FFrameNumber(int value)
```

#### Parameters

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FFrameNumber(AssetBinaryReader)**

```csharp
FFrameNumber(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FFrameNumber Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FFrameNumber](./uassetapi.unrealtypes.fframenumber.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FromString(String[], UAsset)**

```csharp
FFrameNumber FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FFrameNumber](./uassetapi.unrealtypes.fframenumber.md)<br>
