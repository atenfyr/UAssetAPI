# KismetExpression&lt;T&gt;

Namespace: UAssetAPI.Kismet.Bytecode

```csharp
public abstract class KismetExpression<T> : KismetExpression
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md) → [KismetExpression&lt;T&gt;](./uassetapi.kismet.bytecode.kismetexpression-1.md)

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

### **Value**

The value of this expression if it is a constant.

```csharp
public T Value { get; set; }
```

#### Property Value

T<br>

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
