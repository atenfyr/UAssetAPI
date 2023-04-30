# UEnum

Namespace: UAssetAPI.ExportTypes

Reflection data for an enumeration.

```csharp
public class UEnum
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UEnum](./uassetapi.exporttypes.uenum.md)

## Fields

### **Names**

List of pairs of all enum names and values.

```csharp
public List<Tuple<FName, long>> Names;
```

### **CppForm**

How the enum was originally defined.

```csharp
public ECppForm CppForm;
```

## Constructors

### **UEnum()**

```csharp
public UEnum()
```

## Methods

### **Read(AssetBinaryReader, UnrealPackage)**

```csharp
public void Read(AssetBinaryReader reader, UnrealPackage asset)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

### **Write(AssetBinaryWriter, UnrealPackage)**

```csharp
public void Write(AssetBinaryWriter writer, UnrealPackage asset)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>
