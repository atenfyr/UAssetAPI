# UField

Namespace: UAssetAPI.FieldTypes

Base class of reflection data objects.

```csharp
public class UField
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UField](./uassetapi.fieldtypes.ufield.md)

## Fields

### **Next**

Next Field in the linked list. Removed entirely in the custom version FFrameworkObjectVersion::RemoveUField_Next in favor of a regular array

```csharp
public FPackageIndex Next;
```

## Constructors

### **UField()**

```csharp
public UField()
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
