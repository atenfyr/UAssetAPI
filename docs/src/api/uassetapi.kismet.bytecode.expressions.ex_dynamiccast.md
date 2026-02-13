# EX_DynamicCast

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_DynamicCast](./uassetapi.kismet.bytecode.eexprtoken.md#ex_dynamiccast) instruction.

```csharp
public class EX_DynamicCast : EX_CastBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [EX_CastBase](./uassetapi.kismet.bytecode.expressions.ex_castbase.md) → [EX_DynamicCast](./uassetapi.kismet.bytecode.expressions.ex_dynamiccast.md)

## Fields

### **ClassPtr**

The interface class to convert to.

```csharp
public FPackageIndex ClassPtr;
```

### **Target**

The target of this expression.

```csharp
public KismetExpression Target;
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

### **EX_DynamicCast()**

```csharp
public EX_DynamicCast()
```
