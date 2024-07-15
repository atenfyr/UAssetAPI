using System;

namespace UAssetAPI.UnrealTypes;

public class FWeightedRandomSampler : ICloneable
{
    public float[] Prob;
    public int[] Alias;
    public float TotalWeight;

    public FWeightedRandomSampler()
    {
        Prob = Array.Empty<float>();
        Alias = Array.Empty<int>();
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
        var num = reader.ReadInt32();
        Prob = new float[num];
        for (int i = 0; i < num; i++)
        {
            Prob[i] = reader.ReadSingle();
        }

        num = reader.ReadInt32();
        Alias = new int[num];
        for (int i = 0; i < num; i++)
        {
            Alias[i] = reader.ReadInt32();
        }

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
}

/// <summary>
/// Allows area weighted sampling of triangles on a skeletal mesh.
/// </summary>
public class FSkeletalMeshAreaWeightedTriangleSampler : FWeightedRandomSampler
{
    public FSkeletalMeshAreaWeightedTriangleSampler(AssetBinaryReader reader) : base(reader) { }

    public FSkeletalMeshAreaWeightedTriangleSampler() { }
}