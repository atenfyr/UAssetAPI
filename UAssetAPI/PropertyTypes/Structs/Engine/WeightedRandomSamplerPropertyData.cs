using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class WeightedRandomSamplerPropertyData : PropertyData<FWeightedRandomSampler>
{

    public WeightedRandomSamplerPropertyData(FName name) : base(name) { }

    public WeightedRandomSamplerPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("WeightedRandomSampler");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FWeightedRandomSampler(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        if (Value == null) Value = new FWeightedRandomSampler();
        return Value.Write(writer);
    }

    public override void FromString(string[] d, UAsset asset)
    {

    }

    public override string ToString()
    {
        string oup = "(";

        oup += "(";
        for (int i = 0; i < Value.Prob.Length; i++)
        {
            oup += Convert.ToString(Value.Prob[i]) + ", ";
        }
        oup = oup.Remove(oup.Length - 2) + ")";

        oup += "(";
        for (int i = 0; i < Value.Alias.Length; i++)
        {
            oup += Convert.ToString(Value.Alias[i]) + ", ";
        }
        oup = oup.Remove(oup.Length - 2) + ")";

        oup += ", " + Value.TotalWeight + ")";

        return oup;
    }
}

public class SkeletalMeshAreaWeightedTriangleSamplerPropertyData : WeightedRandomSamplerPropertyData
{
    public SkeletalMeshAreaWeightedTriangleSamplerPropertyData(FName name) : base(name) { }

    public SkeletalMeshAreaWeightedTriangleSamplerPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SkeletalMeshAreaWeightedTriangleSampler");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}
