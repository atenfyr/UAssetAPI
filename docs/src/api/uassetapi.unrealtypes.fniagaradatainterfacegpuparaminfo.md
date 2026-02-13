# FNiagaraDataInterfaceGPUParamInfo

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FNiagaraDataInterfaceGPUParamInfo : UAssetAPI.PropertyTypes.Objects.IStruct`1[[UAssetAPI.UnrealTypes.FNiagaraDataInterfaceGPUParamInfo]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FNiagaraDataInterfaceGPUParamInfo](./uassetapi.unrealtypes.fniagaradatainterfacegpuparaminfo.md)<br>
Implements [IStruct&lt;FNiagaraDataInterfaceGPUParamInfo&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

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

### **Read(AssetBinaryReader)**

```csharp
public static FNiagaraDataInterfaceGPUParamInfo Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FNiagaraDataInterfaceGPUParamInfo](./uassetapi.unrealtypes.fniagaradatainterfacegpuparaminfo.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FromString(String[], UAsset)**

```csharp
public static FNiagaraDataInterfaceGPUParamInfo FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FNiagaraDataInterfaceGPUParamInfo](./uassetapi.unrealtypes.fniagaradatainterfacegpuparaminfo.md)<br>
