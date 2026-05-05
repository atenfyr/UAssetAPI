# FTimecode

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FTimecode
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FTimecode](./uassetapi.unrealtypes.ftimecode.md)

## Fields

### **Hours**

```csharp
public int Hours;
```

### **Minutes**

```csharp
public int Minutes;
```

### **Seconds**

```csharp
public int Seconds;
```

### **Frames**

```csharp
public int Frames;
```

### **bDropFrameFormat**

```csharp
public bool bDropFrameFormat;
```

## Constructors

### **FTimecode()**

```csharp
FTimecode()
```

### **FTimecode(Int32, Int32, Int32, Int32, Boolean)**

```csharp
FTimecode(int hours, int minutes, int seconds, int frames, bool bDropFrameFormat)
```

#### Parameters

`hours` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`minutes` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`seconds` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`frames` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`bDropFrameFormat` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **FTimecode(AssetBinaryReader)**

```csharp
FTimecode(AssetBinaryReader reader)
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
