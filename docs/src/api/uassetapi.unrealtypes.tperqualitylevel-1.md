# TPerQualityLevel&lt;T&gt;

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct TPerQualityLevel<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [TPerQualityLevel&lt;T&gt;](./uassetapi.unrealtypes.tperqualitylevel-1.md)

## Fields

### **bCooked**

```csharp
public bool bCooked;
```

### **Default**

```csharp
public T Default;
```

### **PerQuality**

```csharp
public Dictionary<int, T> PerQuality;
```

## Constructors

### **TPerQualityLevel(Boolean, T, Dictionary&lt;Int32, T&gt;)**

```csharp
TPerQualityLevel(bool _bCooked, T _default, Dictionary<int, T> perQuality)
```

#### Parameters

`_bCooked` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`_default` T<br>

`perQuality` Dictionary&lt;Int32, T&gt;<br>

### **TPerQualityLevel(AssetBinaryReader, Func&lt;T&gt;)**

```csharp
TPerQualityLevel(AssetBinaryReader reader, Func<T> valueReader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`valueReader` Func&lt;T&gt;<br>

## Methods

### **Write(AssetBinaryWriter, Action&lt;T&gt;)**

```csharp
int Write(AssetBinaryWriter writer, Action<T> valueWriter)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`valueWriter` Action&lt;T&gt;<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
