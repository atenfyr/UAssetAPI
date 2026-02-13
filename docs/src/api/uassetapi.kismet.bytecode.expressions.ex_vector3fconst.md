# EX_Vector3fConst

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_Vector3fConst](./uassetapi.kismet.bytecode.eexprtoken.md#ex_vector3fconst) instruction.
 A float vector constant (always 3 floats, regardless of LWC).

```csharp
public class EX_Vector3fConst : UAssetAPI.Kismet.Bytecode.KismetExpression
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [EX_Vector3fConst](./uassetapi.kismet.bytecode.expressions.ex_vector3fconst.md)

## Fields

### **X**

```csharp
public float X;
```

### **Y**

```csharp
public float Y;
```

### **Z**

```csharp
public float Z;
```

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

### **EX_Vector3fConst()**

```csharp
public EX_Vector3fConst()
```

## Methods

### **Read(AssetBinaryReader)**

Reads out the expression from a BinaryReader.

```csharp
public void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from.

### **Write(AssetBinaryWriter)**

Writes the expression to a BinaryWriter.

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

```csharp
public void Visit(UAsset asset, UInt32& offset, Action<KismetExpression, uint> visitor)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

`offset` [UInt32&](https://docs.microsoft.com/en-us/dotnet/api/system.uint32&)<br>

`visitor` [Action&lt;KismetExpression, UInt32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-2)<br>
