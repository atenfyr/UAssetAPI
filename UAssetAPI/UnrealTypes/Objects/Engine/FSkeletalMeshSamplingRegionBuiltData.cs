using UAssetAPI.CustomVersions;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// Built data for sampling a single region of a skeletal mesh
/// </summary>
public class FSkeletalMeshSamplingRegionBuiltData
{
    /** Triangles included in this region. */
    public int[] TriangleIndices;

    /** Vertices included in this region. */
    public int[] Vertices;

    /** Bones included in this region. */
    public int[] BoneIndices;

    /** Provides random area weighted sampling of the TriangleIndices array. */
    FSkeletalMeshAreaWeightedTriangleSampler AreaWeightedSampler;

    public FSkeletalMeshSamplingRegionBuiltData(AssetBinaryReader reader)
    {
        var num = reader.ReadInt32();
        TriangleIndices = new int[num];
        for (int i = 0; i < num; i++)
        {
            TriangleIndices[i] = reader.ReadInt32();
        }

        num = reader.ReadInt32();
        BoneIndices = new int[num];
        for (int i = 0; i < num; i++)
        {
            BoneIndices[i] = reader.ReadInt32();
        }

        AreaWeightedSampler = new FSkeletalMeshAreaWeightedTriangleSampler(reader);

        if (reader.Asset.GetCustomVersion<FNiagaraObjectVersion>() >= FNiagaraObjectVersion.SkeletalMeshVertexSampling)
        {
            num = reader.ReadInt32();
            Vertices = new int[num];
            for (int i = 0; i < num; i++)
            {
                Vertices[i] = reader.ReadInt32();
            }
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        writer.Write(TriangleIndices.Length);
        foreach (var t in TriangleIndices)
        {
            writer.Write(t);
        }

        writer.Write(BoneIndices.Length);
        foreach (var b in BoneIndices)
        {
            writer.Write(b);
        }
        AreaWeightedSampler.Write(writer);

        if (writer.Asset.GetCustomVersion<FNiagaraObjectVersion>() >= FNiagaraObjectVersion.SkeletalMeshVertexSampling)
        {
            writer.Write(Vertices.Length);
            foreach (var v in Vertices)
            {
                writer.Write(v);
            }
        }

        return (int)(writer.BaseStream.Position - offset);
    }
}