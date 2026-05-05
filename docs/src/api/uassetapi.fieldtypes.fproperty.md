# FProperty

Namespace: UAssetAPI.FieldTypes

An UnrealScript variable.

```csharp
public abstract class FProperty : FField
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FField](./uassetapi.fieldtypes.ffield.md) → [FProperty](./uassetapi.fieldtypes.fproperty.md)

## Fields

### **ArrayDim**

```csharp
public EArrayDim ArrayDim;
```

### **ElementSize**

```csharp
public int ElementSize;
```

### **PropertyFlags**

```csharp
public EPropertyFlags PropertyFlags;
```

### **RepIndex**

```csharp
public ushort RepIndex;
```

### **RepNotifyFunc**

```csharp
public FName RepNotifyFunc;
```

### **BlueprintReplicationCondition**

```csharp
public ELifetimeCondition BlueprintReplicationCondition;
```

### **RawValue**

```csharp
public object RawValue;
```

### **UsmapPropertyTypeOverrides**

```csharp
public IDictionary<string, EPropertyType> UsmapPropertyTypeOverrides;
```

### **SerializedType**

```csharp
public FName SerializedType;
```

### **Name**

```csharp
public FName Name;
```

### **Flags**

```csharp
public EObjectFlags Flags;
```

### **MetaDataMap**

```csharp
public TMap<FName, FString> MetaDataMap;
```

## Constructors

### **FProperty()**

```csharp
public FProperty()
```

## Methods

### **SetObject(Object)**

```csharp
public void SetObject(object value)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **GetObject&lt;T&gt;()**

```csharp
public T GetObject<T>()
```

#### Type Parameters

`T`<br>

#### Returns

T<br>

### **GetUsmapPropertyType()**

```csharp
public EPropertyType GetUsmapPropertyType()
```

#### Returns

[EPropertyType](./uassetapi.unversioned.epropertytype.md)<br>

### **Read(AssetBinaryReader)**

```csharp
public void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
