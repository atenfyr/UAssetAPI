# FFormatArgumentData

Namespace: UAssetAPI.PropertyTypes.Objects

```csharp
public class FFormatArgumentData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FFormatArgumentData](./uassetapi.propertytypes.objects.fformatargumentdata.md)

## Fields

### **ArgumentName**

```csharp
public FString ArgumentName;
```

### **ArgumentValue**

```csharp
public FFormatArgumentValue ArgumentValue;
```

## Constructors

### **FFormatArgumentData()**

```csharp
public FFormatArgumentData()
```

### **FFormatArgumentData(FString, FFormatArgumentValue)**

```csharp
public FFormatArgumentData(FString name, FFormatArgumentValue value)
```

#### Parameters

`name` [FString](./uassetapi.unrealtypes.fstring.md)<br>

`value` [FFormatArgumentValue](./uassetapi.propertytypes.objects.fformatargumentvalue.md)<br>

### **FFormatArgumentData(AssetBinaryReader)**

```csharp
public FFormatArgumentData(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

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
