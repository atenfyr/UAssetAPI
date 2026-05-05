# FQualifiedFrameTime

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FQualifiedFrameTime
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FQualifiedFrameTime](./uassetapi.unrealtypes.fqualifiedframetime.md)

## Fields

### **Time**

```csharp
public FFrameTime Time;
```

### **Rate**

```csharp
public FFrameRate Rate;
```

## Constructors

### **FQualifiedFrameTime()**

```csharp
FQualifiedFrameTime()
```

### **FQualifiedFrameTime(FFrameTime, FFrameRate)**

```csharp
FQualifiedFrameTime(FFrameTime time, FFrameRate rate)
```

#### Parameters

`time` [FFrameTime](./uassetapi.unrealtypes.fframetime.md)<br>

`rate` [FFrameRate](./uassetapi.unrealtypes.fframerate.md)<br>

### **FQualifiedFrameTime(AssetBinaryReader)**

```csharp
FQualifiedFrameTime(AssetBinaryReader reader)
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
