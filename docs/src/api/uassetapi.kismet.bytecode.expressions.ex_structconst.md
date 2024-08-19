# EX_StructConst

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_StructConst](./uassetapi.kismet.bytecode.eexprtoken.md#ex_structconst) instruction.

```csharp
public class EX_StructConst : UAssetAPI.Kismet.Bytecode.KismetExpression`1[[UAssetAPI.Kismet.Bytecode.KismetExpression[]]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [KismetExpression&lt;KismetExpression[]&gt;](./uassetapi.kismet.bytecode.kismetexpression-1.md) → [EX_StructConst](./uassetapi.kismet.bytecode.expressions.ex_structconst.md)

## Fields

### **Struct**

Pointer to the UScriptStruct in question.

```csharp
public FPackageIndex Struct;
```

### **StructSize**

The size of the struct that this constant represents in memory in bytes.

```csharp
public int StructSize;
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

### **Value**

The value of this expression if it is a constant.

```csharp
public KismetExpression[] Value { get; set; }
```

#### Property Value

[KismetExpression[]](./uassetapi.kismet.bytecode.kismetexpression.md)<br>

### **Inst**

The token of this instruction expressed as a string.

```csharp
public string Inst { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **EX_StructConst()**

```csharp
public EX_StructConst()
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
