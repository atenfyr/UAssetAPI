# FWorldTileInfo

Namespace: UAssetAPI.UnrealTypes

Tile information used by WorldComposition.
 Defines properties necessary for tile positioning in the world. Stored with package summary

```csharp
public class FWorldTileInfo
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FWorldTileInfo](./uassetapi.unrealtypes.fworldtileinfo.md)

## Fields

### **Position**

Tile position in the world relative to parent

```csharp
public Int32[] Position;
```

### **AbsolutePosition**

Absolute tile position in the world. Calculated in runtime

```csharp
public Int32[] AbsolutePosition;
```

### **Bounds**

Tile bounding box

```csharp
public BoxPropertyData Bounds;
```

### **Layer**

Tile assigned layer

```csharp
public FWorldTileLayer Layer;
```

### **bHideInTileView**

Whether to hide sub-level tile in tile view

```csharp
public bool bHideInTileView;
```

### **ParentTilePackageName**

Parent tile package name

```csharp
public FString ParentTilePackageName;
```

### **LODList**

LOD information

```csharp
public FWorldTileLODInfo[] LODList;
```

### **ZOrder**

Sorting order

```csharp
public int ZOrder;
```

## Constructors

### **FWorldTileInfo(Int32[], Int32[], BoxPropertyData, FWorldTileLayer, Boolean, FString, FWorldTileLODInfo[], Int32)**

```csharp
public FWorldTileInfo(Int32[] position, Int32[] absolutePosition, BoxPropertyData bounds, FWorldTileLayer layer, bool bHideInTileView, FString parentTilePackageName, FWorldTileLODInfo[] lODList, int zOrder)
```

#### Parameters

`position` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`absolutePosition` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`bounds` [BoxPropertyData](./uassetapi.propertytypes.structs.boxpropertydata.md)<br>

`layer` [FWorldTileLayer](./uassetapi.unrealtypes.fworldtilelayer.md)<br>

`bHideInTileView` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`parentTilePackageName` [FString](./uassetapi.unrealtypes.fstring.md)<br>

`lODList` [FWorldTileLODInfo[]](./uassetapi.unrealtypes.fworldtilelodinfo.md)<br>

`zOrder` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FWorldTileInfo()**

```csharp
public FWorldTileInfo()
```

## Methods

### **Read(AssetBinaryReader, UAsset)**

```csharp
public void Read(AssetBinaryReader reader, UAsset asset)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

### **ResolveAncestries(UAsset, AncestryInfo)**

```csharp
public void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

`ancestrySoFar` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>

### **Write(AssetBinaryWriter, UAsset)**

```csharp
public void Write(AssetBinaryWriter writer, UAsset asset)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>
