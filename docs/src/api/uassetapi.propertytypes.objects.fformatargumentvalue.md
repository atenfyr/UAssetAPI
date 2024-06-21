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

## Methods

### **Read(AssetBinaryReader)**

```csharp
public void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
