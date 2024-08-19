# EX_TransformConst

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_TransformConst](./uassetapi.kismet.bytecode.eexprtoken.md#ex_transformconst) instruction.

```csharp
public class EX_TransformConst : UAssetAPI.Kismet.Bytecode.KismetExpression`1[[UAssetAPI.UnrealTypes.FTransform]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [KismetExpression&lt;FTransform&gt;](./uassetapi.kismet.bytecode.kismetexpression-1.md) → [EX_TransformConst](./uassetapi.kismet.bytecode.expressions.ex_transformconst.md)

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

### **Value**

The value of this expression if it is a constant.

```csharp
public FTransform Value { get; set; }
```

#### Property Value

[FTransform](./uassetapi.unrealtypes.ftransform.md)<br>

### **Inst**

The token of this instruction expressed as a string.

```csharp
public string Inst { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **EX_TransformConst()**

```csharp
public EX_TransformConst()
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
