# EX_DefaultVariable

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

A single Kismet bytecode instruction, corresponding to the [EExprToken.EX_DefaultVariable](./uassetapi.kismet.bytecode.eexprtoken.md#ex_defaultvariable) instruction.

```csharp
public class EX_DefaultVariable : EX_VariableBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [EX_VariableBase](./uassetapi.kismet.bytecode.expressions.ex_variablebase.md) → [EX_DefaultVariable](./uassetapi.kismet.bytecode.expressions.ex_defaultvariable.md)

## Fields

### **Variable**

A pointer to the variable in question.

```csharp
public KismetPropertyPointer Variable;
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

### **EX_DefaultVariable()**

```csharp
public EX_DefaultVariable()
```
