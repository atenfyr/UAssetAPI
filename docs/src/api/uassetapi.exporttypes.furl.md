# FURL

Namespace: UAssetAPI.ExportTypes

URL structure.

```csharp
public struct FURL
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FURL](./uassetapi.exporttypes.furl.md)

## Fields

### **Protocol**

```csharp
public FString Protocol;
```

### **Host**

```csharp
public FString Host;
```

### **Port**

```csharp
public int Port;
```

### **Valid**

```csharp
public int Valid;
```

### **Map**

```csharp
public FString Map;
```

### **Op**

```csharp
public List<FString> Op;
```

### **Portal**

```csharp
public FString Portal;
```

## Constructors

### **FURL(AssetBinaryReader)**

```csharp
FURL(AssetBinaryReader reader)
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
