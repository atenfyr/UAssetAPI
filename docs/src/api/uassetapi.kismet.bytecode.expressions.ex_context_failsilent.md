# EX_Context_FailSilent

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_Context_FailSilent](./uassetapi.kismet.bytecode.eexprtoken.md#ex_context_failsilent) instruction.

```csharp
public class EX_Context_FailSilent : EX_Context
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [EX_Context](./uassetapi.kismet.bytecode.expressions.ex_context.md) → [EX_Context_FailSilent](./uassetapi.kismet.bytecode.expressions.ex_context_failsilent.md)

## Fields

### **ObjectExpression**

Object expression.

```csharp
public KismetExpression ObjectExpression;
```

### **Offset**

Code offset for NULL expressions.

```csharp
public uint Offset;
```

### **PropertyType**

Old property type.

```csharp
public byte PropertyType;
```

### **RValuePointer**

Property corresponding to the r-value data, in case the l-value needs to be mem-zero'd. FField*

```csharp
public KismetPropertyPointer RValuePointer;
```

### **ContextExpression**

Context expression.

```csharp
public KismetExpression ContextExpression;
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

### **EX_Context_FailSilent()**

```csharp
public EX_Context_FailSilent()
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
