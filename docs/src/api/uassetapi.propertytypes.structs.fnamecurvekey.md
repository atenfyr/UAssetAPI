# FNameCurveKey

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FNameCurveKey
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FNameCurveKey](./uassetapi.propertytypes.structs.fnamecurvekey.md)<br>
Implements [IStruct&lt;FNameCurveKey&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **Time**

```csharp
public float Time;
```

### **Value**

```csharp
public FName Value;
```

## Constructors

### **FNameCurveKey(AssetBinaryReader)**

```csharp
FNameCurveKey(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FNameCurveKey Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FNameCurveKey](./uassetapi.propertytypes.structs.fnamecurvekey.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
