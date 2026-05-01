# FMetaData

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FMetaData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMetaData](./uassetapi.unrealtypes.fmetadata.md)

## Fields

### **ObjectMetaDataMap**

Mapping between an object, and its key-&gt;value meta-data pairs.

```csharp
public TMap<FSoftObjectPath, TMap<FName, FString>> ObjectMetaDataMap;
```

### **RootMetaDataMap**

Root-level (not associated with a particular object) key-&gt;value meta-data pairs.
 Meta-data associated with the package itself should be stored here.

```csharp
public TMap<FName, FString> RootMetaDataMap;
```

## Constructors

### **FMetaData()**

```csharp
public FMetaData()
```

### **FMetaData(AssetBinaryReader)**

```csharp
public FMetaData(AssetBinaryReader Ar)
```

#### Parameters

`Ar` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter Ar)
```

#### Parameters

`Ar` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
