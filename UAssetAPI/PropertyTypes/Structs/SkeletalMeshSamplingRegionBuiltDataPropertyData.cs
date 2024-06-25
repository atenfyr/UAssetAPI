using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.StructTypes;

public class FWeightedRandomSampler
{
    public float[] Prob;
    public int[] Alias;
    public float TotalWeight;

    public FWeightedRandomSampler() { }

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

    public void Write(AssetBinaryWriter writer)
    {
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
    }
}

public class FSkeletalMeshAreaWeightedTriangleSampler : FWeightedRandomSampler
{
    public FSkeletalMeshAreaWeightedTriangleSampler(AssetBinaryReader reader) : base(reader) { }
    
    public FSkeletalMeshAreaWeightedTriangleSampler() { }
}

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
        
        //if (reader.Asset.GetCustomVersion<FNiagaraObjectVersion>() >= FNiagaraObjectVersion.SkeletalMeshVertexSampling)
        if (reader.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_21)
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

        if (writer.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_21)
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
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new FSkeletalMeshSamplingRegionBuiltData(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        return Value.Write(writer);
    }
}