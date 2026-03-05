# FStringCurveKey

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FStringCurveKey
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FStringCurveKey](./uassetapi.propertytypes.structs.fstringcurvekey.md)<br>
Implements [IStruct&lt;FStringCurveKey&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **Time**

```csharp
public float Time;
```

### **Value**

```csharp
public FString Value;
```

## Constructors

### **FStringCurveKey(Single, FString)**

```csharp
FStringCurveKey(float time, FString value)
```

#### Parameters

`time` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`value` [FString](./uassetapi.unrealtypes.fstring.md)<br>

### **FStringCurveKey(AssetBinaryReader)**

```csharp
FStringCurveKey(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FStringCurveKey Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FStringCurveKey](./uassetapi.propertytypes.structs.fstringcurvekey.md)<br>

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
FStringCurveKey FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FStringCurveKey](./uassetapi.propertytypes.structs.fstringcurvekey.md)<br>
