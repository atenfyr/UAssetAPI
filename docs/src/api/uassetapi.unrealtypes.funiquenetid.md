# FUniqueNetId

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FUniqueNetId
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FUniqueNetId](./uassetapi.unrealtypes.funiquenetid.md)

## Fields

### **Type**

```csharp
public FName Type;
```

### **Contents**

```csharp
public FString Contents;
```

## Constructors

### **FUniqueNetId(FName, FString)**

```csharp
public FUniqueNetId(FName type, FString contents)
```

#### Parameters

`type` [FName](./uassetapi.unrealtypes.fname.md)<br>

`contents` [FString](./uassetapi.unrealtypes.fstring.md)<br>

### **FUniqueNetId(AssetBinaryReader)**

```csharp
public FUniqueNetId(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
