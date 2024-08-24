using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.StructTypes;

public class SkeletalMeshSamplingRegionBuiltDataPropertyData : PropertyData<FSkeletalMeshSamplingRegionBuiltData>
{
    public SkeletalMeshSamplingRegionBuiltDataPropertyData(FName name) : base(name) { }

    public SkeletalMeshSamplingRegionBuiltDataPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SkeletalMeshSamplingRegionBuiltData");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FSkeletalMeshSamplingRegionBuiltData(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer);
    }
}