# FNumberFormattingOptions

Namespace: UAssetAPI.PropertyTypes.Objects

```csharp
public class FNumberFormattingOptions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FNumberFormattingOptions](./uassetapi.propertytypes.objects.fnumberformattingoptions.md)

## Fields

### **AlwaysSign**

```csharp
public bool AlwaysSign;
```

### **UseGrouping**

```csharp
public bool UseGrouping;
```

### **RoundingMode**

```csharp
public ERoundingMode RoundingMode;
```

### **MinimumIntegralDigits**

```csharp
public int MinimumIntegralDigits;
```

### **MaximumIntegralDigits**

```csharp
public int MaximumIntegralDigits;
```

### **MinimumFractionalDigits**

```csharp
public int MinimumFractionalDigits;
```

### **MaximumFractionalDigits**

```csharp
public int MaximumFractionalDigits;
```

## Constructors

### **FNumberFormattingOptions()**

```csharp
public FNumberFormattingOptions()
```

### **FNumberFormattingOptions(AssetBinaryReader)**

```csharp
public FNumberFormattingOptions(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
