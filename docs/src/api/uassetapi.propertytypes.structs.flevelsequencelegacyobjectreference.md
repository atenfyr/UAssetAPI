# FLevelSequenceLegacyObjectReference

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public class FLevelSequenceLegacyObjectReference
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FLevelSequenceLegacyObjectReference](./uassetapi.propertytypes.structs.flevelsequencelegacyobjectreference.md)

## Fields

### **ObjectId**

```csharp
public Guid ObjectId;
```

### **ObjectPath**

```csharp
public FString ObjectPath;
```

## Constructors

### **FLevelSequenceLegacyObjectReference(Guid, FString)**

```csharp
public FLevelSequenceLegacyObjectReference(Guid objectId, FString objectPath)
```

#### Parameters

`objectId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`objectPath` [FString](./uassetapi.unrealtypes.fstring.md)<br>

### **FLevelSequenceLegacyObjectReference(AssetBinaryReader)**

```csharp
public FLevelSequenceLegacyObjectReference(AssetBinaryReader reader)
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
