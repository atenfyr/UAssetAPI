# TRangeBound&lt;T&gt;

Namespace: UAssetAPI.UnrealTypes

Template for range bounds.

```csharp
public struct TRangeBound<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [TRangeBound&lt;T&gt;](./uassetapi.unrealtypes.trangebound-1.md)

## Fields

### **Type**

```csharp
public ERangeBoundTypes Type;
```

### **Value**

```csharp
public T Value;
```

## Constructors

### **TRangeBound()**

```csharp
TRangeBound()
```

### **TRangeBound(ERangeBoundTypes, T)**

```csharp
TRangeBound(ERangeBoundTypes type, T value)
```

#### Parameters

`type` [ERangeBoundTypes](./uassetapi.unrealtypes.erangeboundtypes.md)<br>

`value` T<br>

### **TRangeBound(AssetBinaryReader, Func&lt;T&gt;)**

```csharp
TRangeBound(AssetBinaryReader reader, Func<T> valueReader)
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
