# FFontData

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FFontData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FFontData](./uassetapi.unrealtypes.ffontdata.md)

## Fields

### **LocalFontFaceAsset**

```csharp
public FPackageIndex LocalFontFaceAsset;
```

### **FontFilename**

```csharp
public FString FontFilename;
```

### **Hinting**

```csharp
public EFontHinting Hinting;
```

### **LoadingPolicy**

```csharp
public EFontLoadingPolicy LoadingPolicy;
```

### **SubFaceIndex**

```csharp
public int SubFaceIndex;
```

### **bIsCooked**

```csharp
public bool bIsCooked;
```

## Constructors

### **FFontData()**

```csharp
public FFontData()
```

### **FFontData(AssetBinaryReader)**

```csharp
public FFontData(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
