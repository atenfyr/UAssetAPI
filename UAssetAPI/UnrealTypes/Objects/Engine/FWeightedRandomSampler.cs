using System;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

public class FWeightedRandomSampler : ICloneable, IStruct<FWeightedRandomSampler>
{
    public float[] Prob;
    public int[] Alias;
    public float TotalWeight;

    public FWeightedRandomSampler()
    {
        Prob = [];
        Alias = [];
        TotalWeight = 0;
    }

    public FWeightedRandomSampler(float[] prob, int[] alias, float totalWeight)
    {
        Prob = prob;
        Alias = alias;
        TotalWeight = totalWeight;
    }

    public FWeightedRandomSampler(AssetBinaryReader reader)
    {
        Prob = reader.ReadArray(reader.ReadSingle);
        Alias = reader.ReadArray(reader.ReadInt32);
        TotalWeight = reader.ReadSingle();
    }

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        writer.Write(Prob.Length);
        foreach (var p in Prob)
        {
            writer.Write(p);
        }

        writer.Write(Alias.Length);
        foreach (var a in Alias)
        {
            writer.Write(a);
        }

        writer.Write(TotalWeight);

        return (int)(writer.BaseStream.Position - offset);
    }

    public object Clone() => new FWeightedRandomSampler((float[])Prob.Clone(), (int[])Alias.Clone(), TotalWeight);

    public static FWeightedRandomSampler Read(AssetBinaryReader reader) => new FWeightedRandomSampler(reader);

    public override string ToString()
    {
        string oup = "(";

        oup += "(";
        for (int i = 0; i < Prob.Length; i++)
        {
            oup += Convert.ToString(Prob[i]) + ", ";
        }
        oup = oup.Remove(oup.Length - 2) + ")";

        oup += "(";
        for (int i = 0; i < Alias.Length; i++)
        {
            oup += Convert.ToString(Alias[i]) + ", ";
        }
        oup = oup.Remove(oup.Length - 2) + ")";

        oup += ", " + TotalWeight + ")";

        return oup;
    }

    public static FWeightedRandomSampler FromString(string[] d, UAsset asset)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Allows area weighted sampling of triangles on a skeletal mesh.
/// </summary>
public class FSkeletalMeshAreaWeightedTriangleSampler : FWeightedRandomSampler
{
    public FSkeletalMeshAreaWeightedTriangleSampler(AssetBinaryReader reader) : base(reader) { }

    public FSkeletalMeshAreaWeightedTriangleSampler() { }
}