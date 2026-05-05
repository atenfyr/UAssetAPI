# ExpressionSerializer

Namespace: UAssetAPI.Kismet.Bytecode

```csharp
public static class ExpressionSerializer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ExpressionSerializer](./uassetapi.kismet.bytecode.expressionserializer.md)

## Methods

### **ReadExpression(AssetBinaryReader)**

```csharp
public static KismetExpression ReadExpression(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md)<br>

### **WriteExpression(KismetExpression, AssetBinaryWriter)**

```csharp
public static int WriteExpression(KismetExpression expr, AssetBinaryWriter writer)
```

#### Parameters

`expr` [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md)<br>

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
