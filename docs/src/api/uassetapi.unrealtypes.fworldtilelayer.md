# FWorldTileLayer

Namespace: UAssetAPI.UnrealTypes

World layer information for tile tagging

```csharp
public class FWorldTileLayer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FWorldTileLayer](./uassetapi.unrealtypes.fworldtilelayer.md)

## Fields

### **Name**

Human readable name for this layer

```csharp
public FString Name;
```

### **Reserved0**

Reserved for additional options

```csharp
public int Reserved0;
```

### **Reserved1**

Reserved for additional options

```csharp
public IntPointPropertyData Reserved1;
```

### **StreamingDistance**

Distance starting from where tiles belonging to this layer will be streamed in

```csharp
public int StreamingDistance;
```

### **DistanceStreamingEnabled**

```csharp
public bool DistanceStreamingEnabled;
```

## Constructors

### **FWorldTileLayer(FString, Int32, IntPointPropertyData, Int32, Boolean)**

```csharp
public FWorldTileLayer(FString name, int reserved0, IntPointPropertyData reserved1, int streamingDistance, bool distanceStreamingEnabled)
```

#### Parameters

`name` [FString](./uassetapi.unrealtypes.fstring.md)<br>

`reserved0` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`reserved1` [IntPointPropertyData](./uassetapi.propertytypes.structs.intpointpropertydata.md)<br>

`streamingDistance` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`distanceStreamingEnabled` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **FWorldTileLayer()**

```csharp
public FWorldTileLayer()
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
