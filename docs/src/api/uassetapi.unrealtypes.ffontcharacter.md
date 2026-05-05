# FFontCharacter

Namespace: UAssetAPI.UnrealTypes

This struct is serialized using native serialization so any changes to it require a package version bump.

```csharp
public struct FFontCharacter
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FFontCharacter](./uassetapi.unrealtypes.ffontcharacter.md)

## Fields

### **StartU**

```csharp
public int StartU;
```

### **StartV**

```csharp
public int StartV;
```

### **USize**

```csharp
public int USize;
```

### **VSize**

```csharp
public int VSize;
```

### **TextureIndex**

```csharp
public byte TextureIndex;
```

### **VerticalOffset**

```csharp
public int VerticalOffset;
```

## Constructors

### **FFontCharacter(AssetBinaryReader)**

```csharp
FFontCharacter(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
