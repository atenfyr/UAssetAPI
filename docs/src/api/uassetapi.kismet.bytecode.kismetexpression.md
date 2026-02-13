# KismetExpression

Namespace: UAssetAPI.Kismet.Bytecode

A Kismet bytecode instruction.

```csharp
public class KismetExpression
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md)

## Fields

### **Tag**

An optional tag which can be set on any expression in memory. This is for the user only, and has no bearing in the API itself.

```csharp
public object Tag;
```

### **RawValue**

```csharp
public object RawValue;
```

## Properties

### **Token**

The token of this expression.

```csharp
public EExprToken Token { get; }
```

#### Property Value

[EExprToken](./uassetapi.kismet.bytecode.eexprtoken.md)<br>

### **Inst**

The token of this instruction expressed as a string.

```csharp
public string Inst { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **KismetExpression()**

```csharp
public KismetExpression()
```

## Methods

### **SetObject(Object)**

```csharp
public void SetObject(object value)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **GetObject&lt;T&gt;()**

```csharp
public T GetObject<T>()
```

#### Type Parameters

`T`<br>

#### Returns

T<br>

### **Read(AssetBinaryReader)**

Reads out an expression from a BinaryReader.

```csharp
public void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from.

### **Write(AssetBinaryWriter)**

Writes an expression to a BinaryWriter.

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
The BinaryWriter to write from.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The iCode offset of the data that was written.

### **Visit(UAsset, UInt32&, Action&lt;KismetExpression, UInt32&gt;)**

Visits this expression and all child expressions, calling the visitor function for each with the in-memory offset.
 Note: The offset is the in-memory offset, not the serialization offset.

```csharp
public void Visit(UAsset asset, UInt32& offset, Action<KismetExpression, uint> visitor)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>
The asset containing this expression.

`offset` [UInt32&](https://docs.microsoft.com/en-us/dotnet/api/system.uint32&)<br>
Reference to the current in-memory offset, which is incremented as expressions are visited.

`visitor` [Action&lt;KismetExpression, UInt32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-2)<br>
The visitor function to call for each expression with the expression and its offset.

### **GetSize(UAsset)**

Gets the in-memory size of this expression and all child expressions.

```csharp
public uint GetSize(UAsset asset)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>
The asset containing this expression.

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
The size in bytes of this expression.
