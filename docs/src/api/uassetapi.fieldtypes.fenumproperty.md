# FEnumProperty

Namespace: UAssetAPI.FieldTypes

```csharp
public class FEnumProperty : FProperty
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FField](./uassetapi.fieldtypes.ffield.md) → [FProperty](./uassetapi.fieldtypes.fproperty.md) → [FEnumProperty](./uassetapi.fieldtypes.fenumproperty.md)

## Fields

### **Enum**

A pointer to the UEnum represented by this property

```csharp
public FPackageIndex Enum;
```

### **UnderlyingProp**

The FNumericProperty which represents the underlying type of the enum

```csharp
public FProperty UnderlyingProp;
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

### **FEnumProperty()**

```csharp
public FEnumProperty()
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
