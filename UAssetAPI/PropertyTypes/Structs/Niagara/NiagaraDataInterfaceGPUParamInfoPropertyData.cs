using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class NiagaraDataInterfaceGPUParamInfoPropertyData : PropertyData<FNiagaraDataInterfaceGPUParamInfo>
{
    public NiagaraDataInterfaceGPUParamInfoPropertyData(FName name) : base(name) { }

    public NiagaraDataInterfaceGPUParamInfoPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NiagaraDataInterfaceGPUParamInfo");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FNiagaraDataInterfaceGPUParamInfo(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        if (Value == null) Value = new FNiagaraDataInterfaceGPUParamInfo();
        return Value.Write(writer);
    }
}