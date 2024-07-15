using UAssetAPI.UnrealTypes;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A structure for holding mesh-to-mesh triangle influences to skin one mesh to another (similar to a wrap deformer)
/// </summary>
public class FMeshToMeshVertData
{
    /// <summary>
    /// Barycentric coords and distance along normal for the position of the final vert
    /// </summary>
    public Vector4fPropertyData PositionBaryCoordsAndDist;

    /// <summary>
    /// Barycentric coords and distance along normal for the location of the unit normal endpoint.
    /// Actual normal = ResolvedNormalPosition - ResolvedPosition
    /// </summary>
    public Vector4fPropertyData NormalBaryCoordsAndDist;

    /// <summary>
    /// Barycentric coords and distance along normal for the location of the unit Tangent endpoint.
    /// Actual normal = ResolvedNormalPosition - ResolvedPosition
    /// </summary>
    public Vector4fPropertyData TangentBaryCoordsAndDist;

    /// <summary>
    /// Contains the 3 indices for verts in the source mesh forming a triangle, the last element
    /// is a flag to decide how the skinning works, 0xffff uses no simulation, and just normal
    /// skinning, anything else uses the source mesh and the above skin data to get the final position
    /// </summary>
    public ushort[] SourceMeshVertIndices;

    /// <summary>
    /// For weighted averaging of multiple triangle influences
    /// </summary>
    public float Weight = 0.0f;

    /// <summary>
    /// Dummy for alignment
    /// </summary>
    public uint Padding;

    public void Read(AssetBinaryReader reader)
    {
        PositionBaryCoordsAndDist = new Vector4fPropertyData(FName.DefineDummy(reader.Asset, "PositionBaryCoordsAndDist"));
        PositionBaryCoordsAndDist.Offset = reader.BaseStream.Position;
        PositionBaryCoordsAndDist.Read(reader, false, 0);

        NormalBaryCoordsAndDist = new Vector4fPropertyData(FName.DefineDummy(reader.Asset, "NormalBaryCoordsAndDist"));
        NormalBaryCoordsAndDist.Offset = reader.BaseStream.Position;
        NormalBaryCoordsAndDist.Read(reader, false, 0);

        TangentBaryCoordsAndDist = new Vector4fPropertyData(FName.DefineDummy(reader.Asset, "TangentBaryCoordsAndDist"));
        TangentBaryCoordsAndDist.Offset = reader.BaseStream.Position;
        TangentBaryCoordsAndDist.Read(reader, false, 0);

        SourceMeshVertIndices = new ushort[4];
        for (int i = 0; i < 4; i++)
        {
            SourceMeshVertIndices[i] = reader.ReadUInt16();
        }

        Weight = reader.ReadSingle();

        Padding = reader.ReadUInt32();
    }

    public int Write(AssetBinaryWriter writer)
    {
        int res = 0;
        res += PositionBaryCoordsAndDist.Write(writer, false);
        res += NormalBaryCoordsAndDist.Write(writer, false);
        res += TangentBaryCoordsAndDist.Write(writer, false);

        for (int i = 0; i < 4; i++)
        {
            writer.Write(SourceMeshVertIndices.Length > i ? SourceMeshVertIndices[i] : (ushort)0); res += sizeof(ushort);
        }

        writer.Write(Weight); res += sizeof(float);
        writer.Write(Padding); res += sizeof(uint);

        return res;
    }

    public FMeshToMeshVertData(AssetBinaryReader reader)
    {
        Read(reader);
    }

    public FMeshToMeshVertData(Vector4fPropertyData positionBaryCoordsAndDist, Vector4fPropertyData normalBaryCoordsAndDist, Vector4fPropertyData tangentBaryCoordsAndDist, ushort[] sourceMeshVertIndices, float weight, uint padding)
    {
        PositionBaryCoordsAndDist = positionBaryCoordsAndDist;
        NormalBaryCoordsAndDist = normalBaryCoordsAndDist;
        TangentBaryCoordsAndDist = tangentBaryCoordsAndDist;
        SourceMeshVertIndices = sourceMeshVertIndices;
        Weight = weight;
        Padding = padding;
    }

    public FMeshToMeshVertData() { }
}

/// <summary>
/// Common Cloth LOD representation for all clothing assets.
/// </summary>
public class ClothLODDataPropertyData : StructPropertyData
{
    /// <summary>
    /// Skinning data for transitioning from a higher detail LOD to this one
    /// </summary>
    public FMeshToMeshVertData[] TransitionUpSkinData;

    /// <summary>
    /// Skinning data for transitioning from a lower detail LOD to this one
    /// </summary>
    public FMeshToMeshVertData[] TransitionDownSkinData;

    public ClothLODDataPropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public ClothLODDataPropertyData(FName name) : base(name) { }
    public ClothLODDataPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("ClothLODData");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        StructType = FName.DefineDummy(reader.Asset, PropertyType);
        base.Read(reader, includeHeader, 1, leng2, PropertySerializationContext.StructFallback);

        int sizeUpData = reader.ReadInt32();
        TransitionUpSkinData = new FMeshToMeshVertData[sizeUpData];
        for (int i = 0; i < sizeUpData; i++)
        {
            TransitionUpSkinData[i] = new FMeshToMeshVertData(reader);
        }

        int sizeDownData = reader.ReadInt32();
        TransitionDownSkinData = new FMeshToMeshVertData[sizeDownData];
        for (int i = 0; i < sizeDownData; i++)
        {
            TransitionDownSkinData[i] = new FMeshToMeshVertData(reader);
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        StructType = FName.DefineDummy(writer.Asset, PropertyType);
        int res = base.Write(writer, includeHeader, PropertySerializationContext.StructFallback);

        if (TransitionUpSkinData == null) TransitionUpSkinData = [];
        writer.Write(TransitionUpSkinData.Length); res += sizeof(int);
        for (int i = 0; i < TransitionUpSkinData.Length; i++)
        {
            res += TransitionUpSkinData[i].Write(writer);
        }

        if (TransitionDownSkinData == null) TransitionDownSkinData = [];
        writer.Write(TransitionDownSkinData.Length); res += sizeof(int);
        for (int i = 0; i < TransitionDownSkinData.Length; i++)
        {
            res += TransitionDownSkinData[i].Write(writer);
        }

        return res;
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void FromString(string[] d, UAsset asset)
    {
        base.FromString(d, asset);
    }
}

public class ClothLODDataCommonPropertyData : ClothLODDataPropertyData
{
    public ClothLODDataCommonPropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public ClothLODDataCommonPropertyData(FName name) : base(name) { }
    public ClothLODDataCommonPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("ClothLODDataCommon");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}