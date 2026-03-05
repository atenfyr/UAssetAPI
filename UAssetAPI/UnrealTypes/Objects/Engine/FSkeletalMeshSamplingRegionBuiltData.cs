using System;
using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// Built data for sampling a single region of a skeletal mesh
/// </summary>
public class FSkeletalMeshSamplingRegionBuiltData : IStruct<FSkeletalMeshSamplingRegionBuiltData>
{
    /** Triangles included in this region. */
    public int[] TriangleIndices;

    /** Vertices included in this region. */
    public int[] Vertices;

    /** Bones included in this region. */
    public int[] BoneIndices;

    /** Provides random area weighted sampling of the TriangleIndices array. */
    FSkeletalMeshAreaWeightedTriangleSampler AreaWeightedSampler;

    public FSkeletalMeshSamplingRegionBuiltData()
    {
        TriangleIndices = [];
        Vertices = [];
        BoneIndices = [];
        AreaWeightedSampler = new FSkeletalMeshAreaWeightedTriangleSampler();
    }

    public FSkeletalMeshSamplingRegionBuiltData(AssetBinaryReader reader)
    {
        TriangleIndices = reader.ReadArray(reader.ReadInt32);
        BoneIndices = reader.ReadArray(reader.ReadInt32);

        AreaWeightedSampler = new FSkeletalMeshAreaWeightedTriangleSampler(reader);

        if (reader.Asset.GetCustomVersion<FNiagaraObjectVersion>() >= FNiagaraObjectVersion.SkeletalMeshVertexSampling)
        {
            Vertices = reader.ReadArray(reader.ReadInt32);
        }
    }

    public static FSkeletalMeshSamplingRegionBuiltData Read(AssetBinaryReader reader) => new FSkeletalMeshSamplingRegionBuiltData(reader);

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        if (TriangleIndices == null) TriangleIndices = Array.Empty<int>();
        writer.Write(TriangleIndices.Length);
        foreach (var t in TriangleIndices)
        {
            writer.Write(t);
        }

        if (BoneIndices == null) BoneIndices = Array.Empty<int>();
        writer.Write(BoneIndices.Length);
        foreach (var b in BoneIndices)
        {
            writer.Write(b);
        }

        if (AreaWeightedSampler == null) AreaWeightedSampler = new FSkeletalMeshAreaWeightedTriangleSampler();
        AreaWeightedSampler.Write(writer);

        if (writer.Asset.GetCustomVersion<FNiagaraObjectVersion>() >= FNiagaraObjectVersion.SkeletalMeshVertexSampling)
        {
            if (Vertices == null) Vertices = Array.Empty<int>();
            writer.Write(Vertices.Length);
            foreach (var v in Vertices)
            {
                writer.Write(v);
            }
        }

        return (int)(writer.BaseStream.Position - offset);
    }

    public static FSkeletalMeshSamplingRegionBuiltData FromString(string[] d, UAsset asset)
    {
        throw new NotImplementedException();
    }
}