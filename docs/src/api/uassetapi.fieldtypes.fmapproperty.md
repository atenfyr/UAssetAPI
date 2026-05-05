# FMapProperty

Namespace: UAssetAPI.FieldTypes

```csharp
public class FMapProperty : FProperty
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FField](./uassetapi.fieldtypes.ffield.md) → [FProperty](./uassetapi.fieldtypes.fproperty.md) → [FMapProperty](./uassetapi.fieldtypes.fmapproperty.md)

## Fields

### **KeyProp**

```csharp
public FProperty KeyProp;
```

### **ValueProp**

```csharp
public FProperty ValueProp;
```

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

### **FMapProperty()**

```csharp
public FMapProperty()
```

## Methods

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
