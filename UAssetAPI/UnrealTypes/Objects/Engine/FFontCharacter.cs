namespace UAssetAPI.UnrealTypes;

/// <summary>
/// This struct is serialized using native serialization so any changes to it require a package version bump.
/// </summary>
public struct FFontCharacter
{
    public int StartU;
    public int StartV;
    public int USize;
    public int VSize;
    public byte TextureIndex;
    public int VerticalOffset;

    public FFontCharacter(AssetBinaryReader reader)
    {
        StartU = reader.ReadInt32();
        StartV = reader.ReadInt32();
        USize = reader.ReadInt32();
        VSize = reader.ReadInt32();
        TextureIndex = reader.ReadByte();
        VerticalOffset = reader.ReadInt32();
    }

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;
        writer.Write(StartU);
        writer.Write(StartV);
        writer.Write(USize);
        writer.Write(VSize);
        writer.Write(TextureIndex);
        writer.Write(VerticalOffset);
        return (int)(writer.BaseStream.Position - offset);
    }
}
