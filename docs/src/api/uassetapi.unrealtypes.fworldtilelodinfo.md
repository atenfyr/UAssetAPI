# FWorldTileLODInfo

Namespace: UAssetAPI.UnrealTypes

Describes LOD entry in a world tile

```csharp
public class FWorldTileLODInfo
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FWorldTileLODInfo](./uassetapi.unrealtypes.fworldtilelodinfo.md)

## Fields

### **RelativeStreamingDistance**

Relative to LOD0 streaming distance, absolute distance = LOD0 + StreamingDistanceDelta

```csharp
public int RelativeStreamingDistance;
```

### **Reserved0**

Reserved for additional options

```csharp
public float Reserved0;
```

### **Reserved1**

Reserved for additional options

```csharp
public float Reserved1;
```

### **Reserved2**

Reserved for additional options

```csharp
public int Reserved2;
```

### **Reserved3**

Reserved for additional options

```csharp
public int Reserved3;
```

## Constructors

### **FWorldTileLODInfo(Int32, Single, Single, Int32, Int32)**

```csharp
public FWorldTileLODInfo(int relativeStreamingDistance, float reserved0, float reserved1, int reserved2, int reserved3)
```

#### Parameters

`relativeStreamingDistance` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`reserved0` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`reserved1` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`reserved2` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`reserved3` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FWorldTileLODInfo()**

```csharp
public FWorldTileLODInfo()
```

## Methods

### **Read(AssetBinaryReader, UAsset)**

```csharp
public void Read(AssetBinaryReader reader, UAsset asset)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

### **Write(AssetBinaryWriter, UAsset)**

```csharp
public void Write(AssetBinaryWriter writer, UAsset asset)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>
