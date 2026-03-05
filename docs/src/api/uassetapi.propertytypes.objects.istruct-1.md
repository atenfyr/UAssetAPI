# IStruct&lt;T&gt;

Namespace: UAssetAPI.PropertyTypes.Objects

```csharp
public interface IStruct<T>
```

#### Type Parameters

`T`<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
T Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

T<br>

### **FromString(String[], UAsset)**

```csharp
T FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

T<br>

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
