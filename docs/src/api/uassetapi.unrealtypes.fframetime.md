# FFrameTime

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FFrameTime
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FFrameTime](./uassetapi.unrealtypes.fframetime.md)

## Fields

### **FrameNumber**

```csharp
public FFrameNumber FrameNumber;
```

### **SubFrame**

```csharp
public float SubFrame;
```

## Constructors

### **FFrameTime()**

```csharp
FFrameTime()
```

### **FFrameTime(FFrameNumber, Single)**

```csharp
FFrameTime(FFrameNumber frameNumber, float subFrame)
```

#### Parameters

`frameNumber` [FFrameNumber](./uassetapi.unrealtypes.fframenumber.md)<br>

`subFrame` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FFrameTime(AssetBinaryReader)**

```csharp
FFrameTime(AssetBinaryReader reader)
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
