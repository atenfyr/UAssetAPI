# EX_PopExecutionFlowIfNot

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_PopExecutionFlowIfNot](./uassetapi.kismet.bytecode.eexprtoken.md#ex_popexecutionflowifnot) instruction.
 Conditional equivalent of the [EExprToken.EX_PopExecutionFlow](./uassetapi.kismet.bytecode.eexprtoken.md#ex_popexecutionflow) expression.

```csharp
public class EX_PopExecutionFlowIfNot : UAssetAPI.Kismet.Bytecode.KismetExpression
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [EX_PopExecutionFlowIfNot](./uassetapi.kismet.bytecode.expressions.ex_popexecutionflowifnot.md)

## Fields

### **BooleanExpression**

Expression to evaluate to determine whether or not a pop should be performed.

```csharp
public KismetExpression BooleanExpression;
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

### **EX_PopExecutionFlowIfNot()**

```csharp
public EX_PopExecutionFlowIfNot()
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
