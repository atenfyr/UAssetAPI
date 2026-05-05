# FEngineVersion

Namespace: UAssetAPI

Holds basic Unreal version numbers.

```csharp
public struct FEngineVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FEngineVersion](./uassetapi.fengineversion.md)

## Fields

### **Major**

Major version number.

```csharp
public ushort Major;
```

### **Minor**

Minor version number.

```csharp
public ushort Minor;
```

### **Patch**

Patch version number.

```csharp
public ushort Patch;
```

### **Changelist**

Changelist number. This is used by the engine to arbitrate when Major/Minor/Patch version numbers match.

```csharp
public uint Changelist;
```

### **Branch**

Branch name.

```csharp
public FString Branch;
```

## Constructors

### **FEngineVersion(UnrealBinaryReader)**

```csharp
FEngineVersion(UnrealBinaryReader reader)
```

#### Parameters

`reader` [UnrealBinaryReader](./uassetapi.unrealbinaryreader.md)<br>

### **FEngineVersion(UInt16, UInt16, UInt16, UInt32, FString)**

```csharp
FEngineVersion(ushort major, ushort minor, ushort patch, uint changelist, FString branch)
```

#### Parameters

`major` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`minor` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`patch` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`changelist` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`branch` [FString](./uassetapi.unrealtypes.fstring.md)<br>

## Methods

### **Write(UnrealBinaryWriter)**

```csharp
void Write(UnrealBinaryWriter writer)
```

#### Parameters

`writer` [UnrealBinaryWriter](./uassetapi.unrealbinarywriter.md)<br>
