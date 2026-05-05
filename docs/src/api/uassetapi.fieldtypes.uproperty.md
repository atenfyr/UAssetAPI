# UProperty

Namespace: UAssetAPI.FieldTypes

An UnrealScript variable.

```csharp
public abstract class UProperty : UField
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UField](./uassetapi.fieldtypes.ufield.md) → [UProperty](./uassetapi.fieldtypes.uproperty.md)

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

### **Next**

Next Field in the linked list. Removed entirely in the custom version FFrameworkObjectVersion::RemoveUField_Next in favor of a regular array

```csharp
public FPackageIndex Next;
```

## Constructors

### **UProperty()**

```csharp
public UProperty()
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

### **GetUsmapPropertyType()**

```csharp
public EPropertyType GetUsmapPropertyType()
```

#### Returns

[EPropertyType](./uassetapi.unversioned.epropertytype.md)<br>
