using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class SkeletalMeshSamplingLODBuiltDataPropertyData : PropertyData<SkeletalMeshAreaWeightedTriangleSamplerPropertyData>
{
    public SkeletalMeshSamplingLODBuiltDataPropertyData(FName name) : base(name) { }

    public SkeletalMeshSamplingLODBuiltDataPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SkeletalMeshSamplingLODBuiltData");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new SkeletalMeshAreaWeightedTriangleSamplerPropertyData(FName.DefineDummy(reader.Asset, "AreaWeightedTriangleSampler"));
        Value.Ancestry.Initialize(Ancestry, Name);
        Value.Read(reader, false, 0);
    }

    public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
    {
        var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
        ancestryNew.SetAsParent(Name);

        if (Value == null) Value = new SkeletalMeshAreaWeightedTriangleSamplerPropertyData();
        Value.ResolveAncestries(asset, ancestryNew);
        base.ResolveAncestries(asset, ancestrySoFar);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        if (Value == null) Value = new SkeletalMeshAreaWeightedTriangleSamplerPropertyData();
        return Value.Write(writer, false);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}