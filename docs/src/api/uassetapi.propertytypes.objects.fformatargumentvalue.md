# FFormatArgumentValue

Namespace: UAssetAPI.PropertyTypes.Objects

```csharp
public class FFormatArgumentValue
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FFormatArgumentValue](./uassetapi.propertytypes.objects.fformatargumentvalue.md)

## Fields

### **Type**

```csharp
public EFormatArgumentType Type;
```

### **Value**

```csharp
public object Value;
```

## Constructors

### **FFormatArgumentValue()**

```csharp
public FFormatArgumentValue()
```

### **FFormatArgumentValue(EFormatArgumentType, Object)**

```csharp
public FFormatArgumentValue(EFormatArgumentType type, object value)
```

#### Parameters

`type` [EFormatArgumentType](./uassetapi.unrealtypes.engineenums.eformatargumenttype.md)<br>

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **FFormatArgumentValue(AssetBinaryReader, Boolean)**

```csharp
public FFormatArgumentValue(AssetBinaryReader reader, bool isArgumentData)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`isArgumentData` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Methods

### **Write(AssetBinaryWriter, Boolean)**

```csharp
public int Write(AssetBinaryWriter writer, bool isArgumentData)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`isArgumentData` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
