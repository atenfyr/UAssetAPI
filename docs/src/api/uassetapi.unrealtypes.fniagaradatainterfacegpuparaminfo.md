# FNiagaraDataInterfaceGPUParamInfo

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FNiagaraDataInterfaceGPUParamInfo
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FNiagaraDataInterfaceGPUParamInfo](./uassetapi.unrealtypes.fniagaradatainterfacegpuparaminfo.md)

## Fields

### **DataInterfaceHLSLSymbol**

```csharp
public FString DataInterfaceHLSLSymbol;
```

### **DIClassName**

```csharp
public FString DIClassName;
```

### **GeneratedFunctions**

```csharp
public FNiagaraDataInterfaceGeneratedFunction[] GeneratedFunctions;
```

## Constructors

### **FNiagaraDataInterfaceGPUParamInfo()**

```csharp
public FNiagaraDataInterfaceGPUParamInfo()
```

### **FNiagaraDataInterfaceGPUParamInfo(AssetBinaryReader)**

```csharp
public FNiagaraDataInterfaceGPUParamInfo(AssetBinaryReader reader)
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
