# EX_LetWeakObjPtr

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_LetWeakObjPtr](./uassetapi.kismet.bytecode.eexprtoken.md#ex_letweakobjptr) instruction.

```csharp
public class EX_LetWeakObjPtr : EX_LetBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [EX_LetBase](./uassetapi.kismet.bytecode.expressions.ex_letbase.md) → [EX_LetWeakObjPtr](./uassetapi.kismet.bytecode.expressions.ex_letweakobjptr.md)

## Fields

### **VariableExpression**

Variable expression.

```csharp
public KismetExpression VariableExpression;
```

### **AssignmentExpression**

Assignment expression.

```csharp
public KismetExpression AssignmentExpression;
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

### **EX_LetWeakObjPtr()**

```csharp
public EX_LetWeakObjPtr()
```
