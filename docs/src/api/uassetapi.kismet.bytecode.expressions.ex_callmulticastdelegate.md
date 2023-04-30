# EX_CallMulticastDelegate

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_CallMulticastDelegate](./uassetapi.kismet.bytecode.eexprtoken.md#ex_callmulticastdelegate) instruction.

```csharp
public class EX_CallMulticastDelegate : EX_FinalFunction
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [EX_FinalFunction](./uassetapi.kismet.bytecode.expressions.ex_finalfunction.md) → [EX_CallMulticastDelegate](./uassetapi.kismet.bytecode.expressions.ex_callmulticastdelegate.md)

## Fields

### **Delegate**

Delegate property.

```csharp
public KismetExpression Delegate;
```

### **StackNode**

Stack node.

```csharp
public FPackageIndex StackNode;
```

### **Parameters**

List of parameters for this function.

```csharp
public KismetExpression[] Parameters;
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

### **EX_CallMulticastDelegate()**

```csharp
public EX_CallMulticastDelegate()
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
