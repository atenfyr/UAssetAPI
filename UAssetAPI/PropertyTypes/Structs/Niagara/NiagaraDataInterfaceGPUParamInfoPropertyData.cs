using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class NiagaraDataInterfaceGPUParamInfoPropertyData : BasePropertyData<FNiagaraDataInterfaceGPUParamInfo>
{
    public NiagaraDataInterfaceGPUParamInfoPropertyData(FName name) : base(name) { }

    public NiagaraDataInterfaceGPUParamInfoPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NiagaraDataInterfaceGPUParamInfo");
    public override FString PropertyType => CurrentPropertyType;
}