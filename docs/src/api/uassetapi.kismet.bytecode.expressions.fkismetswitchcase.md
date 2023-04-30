# FKismetSwitchCase

Namespace: UAssetAPI.Kismet.Bytecode.Expressions

Represents a case in a Kismet bytecode switch statement.

```csharp
public struct FKismetSwitchCase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FKismetSwitchCase](./uassetapi.kismet.bytecode.expressions.fkismetswitchcase.md)

## Fields

### **CaseIndexValueTerm**

The index value term of this case.

```csharp
public KismetExpression CaseIndexValueTerm;
```

### **NextOffset**

Code offset to the next case.

```csharp
public uint NextOffset;
```

### **CaseTerm**

The main case term.

```csharp
public KismetExpression CaseTerm;
```

## Constructors

### **FKismetSwitchCase(KismetExpression, UInt32, KismetExpression)**

```csharp
FKismetSwitchCase(KismetExpression caseIndexValueTerm, uint nextOffset, KismetExpression caseTerm)
```

#### Parameters

`caseIndexValueTerm` [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md)<br>

`nextOffset` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`caseTerm` [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md)<br>
