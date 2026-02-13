# FPropertyTypeName

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FPropertyTypeName
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FPropertyTypeName](./uassetapi.unrealtypes.fpropertytypename.md)

## Fields

### **Nodes**

```csharp
public List<FPropertyTypeNameNode> Nodes;
```

### **ShouldSerializeNodes**

```csharp
public bool ShouldSerializeNodes;
```

## Constructors

### **FPropertyTypeName(List&lt;FPropertyTypeNameNode&gt;, Boolean)**

```csharp
public FPropertyTypeName(List<FPropertyTypeNameNode> list, bool shouldSerialize)
```

#### Parameters

`list` [List&lt;FPropertyTypeNameNode&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

`shouldSerialize` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **FPropertyTypeName(AssetBinaryReader)**

```csharp
public FPropertyTypeName(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

### **GetName()**

```csharp
public FName GetName()
```

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **GetParameter(Int32)**

```csharp
public FPropertyTypeName GetParameter(int paramIndex)
```

#### Parameters

`paramIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[FPropertyTypeName](./uassetapi.unrealtypes.fpropertytypename.md)<br>
