# TRange&lt;T&gt;

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct TRange<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [TRange&lt;T&gt;](./uassetapi.unrealtypes.trange-1.md)

## Fields

### **LowerBound**

```csharp
public TRangeBound<T> LowerBound;
```

### **UpperBound**

```csharp
public TRangeBound<T> UpperBound;
```

## Constructors

### **TRange(TRangeBound&lt;T&gt;, TRangeBound&lt;T&gt;)**

```csharp
TRange(TRangeBound<T> lowerBound, TRangeBound<T> upperBound)
```

#### Parameters

`lowerBound` TRangeBound&lt;T&gt;<br>

`upperBound` TRangeBound&lt;T&gt;<br>

### **TRange(AssetBinaryReader, Func&lt;T&gt;)**

```csharp
TRange(AssetBinaryReader reader, Func<T> valueReader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`valueReader` Func&lt;T&gt;<br>

## Methods

### **Write(AssetBinaryWriter, Action&lt;T&gt;)**

```csharp
void Write(AssetBinaryWriter writer, Action<T> valueWriter)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`valueWriter` Action&lt;T&gt;<br>
