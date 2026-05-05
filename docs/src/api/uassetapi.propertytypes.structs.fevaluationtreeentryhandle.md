# FEvaluationTreeEntryHandle

Namespace: UAssetAPI.PropertyTypes.Structs

A structure that uniquely identifies an entry within a TEvaluationTreeEntryContaine

```csharp
public struct FEvaluationTreeEntryHandle
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FEvaluationTreeEntryHandle](./uassetapi.propertytypes.structs.fevaluationtreeentryhandle.md)

## Fields

### **EntryIndex**

Specifies an index into TEvaluationTreeEntryContainer::Entries

```csharp
public int EntryIndex;
```

## Constructors

### **FEvaluationTreeEntryHandle(Int32)**

```csharp
FEvaluationTreeEntryHandle(int _EntryIndex)
```

#### Parameters

`_EntryIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FEvaluationTreeEntryHandle(AssetBinaryReader)**

```csharp
FEvaluationTreeEntryHandle(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
