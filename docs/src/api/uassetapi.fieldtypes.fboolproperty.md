# FBoolProperty

Namespace: UAssetAPI.FieldTypes

```csharp
public class FBoolProperty : FProperty
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FField](./uassetapi.fieldtypes.ffield.md) → [FProperty](./uassetapi.fieldtypes.fproperty.md) → [FBoolProperty](./uassetapi.fieldtypes.fboolproperty.md)

## Fields

### **FieldSize**

Size of the bitfield/bool property. Equal to ElementSize but used to check if the property has been properly initialized (0-8, where 0 means uninitialized).

```csharp
public byte FieldSize;
```

### **ByteOffset**

Offset from the memeber variable to the byte of the property (0-7).

```csharp
public byte ByteOffset;
```

### **ByteMask**

Mask of the byte with the property value.

```csharp
public byte ByteMask;
```

### **FieldMask**

Mask of the field with the property value. Either equal to ByteMask or 255 in case of 'bool' type.

```csharp
public byte FieldMask;
```

### **NativeBool**

```csharp
public bool NativeBool;
```

### **Value**

```csharp
public bool Value;
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

### **FBoolProperty()**

```csharp
public FBoolProperty()
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
