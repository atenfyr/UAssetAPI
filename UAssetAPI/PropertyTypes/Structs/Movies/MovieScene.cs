using System;
using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public enum ESectionEvaluationFlags : byte {
    None = 0,
    PreRoll = 1,
    PostRoll = 2,
    ForceKeepState = 4,
    ForceRestoreState = 8,
};

/// <summary>
/// Keyable struct that represents a particular entity within an evaluation template (either a section/template or a track)
/// </summary>
public struct FMovieSceneEvaluationKey
{
    /// <summary> ID of the sequence that the entity is contained within </summary>
    public uint SequenceID;
    /// <summary> ID of the track this key relates to </summary>
    public uint TrackIdentifier;
    /// <summary> Index of the section template within the track this key relates to (or -1 where this key relates to a track) </summary>
    public uint SectionIndex;

    public FMovieSceneEvaluationKey(uint _SequenceID, uint _TrackIdentifier, uint _SectionIndex)
    {
        SequenceID = _SequenceID;
        TrackIdentifier = _TrackIdentifier;
        SectionIndex = _SectionIndex;
    }

    public FMovieSceneEvaluationKey(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            SequenceID = reader.ReadUInt32();
            TrackIdentifier = reader.ReadUInt32();
            SectionIndex = reader.ReadUInt32();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(SequenceID);
        writer.Write(TrackIdentifier);
        writer.Write(SectionIndex);
        return sizeof(uint) * 3;
    }
}

/// <summary>
/// Data that represents a single sub-section
/// </summary>
public struct FMovieSceneSubSectionData
{
    /// <summary> The sub section itself  </summary>
    public FPackageIndex Section;
    /// <summary> The object binding that the sub section belongs to (usually zero) </summary>
    public Guid ObjectBindingId;
    /// <summary> Evaluation flags for the section </summary>
    public ESectionEvaluationFlags Flags;

    public FMovieSceneSubSectionData(FPackageIndex section, Guid objectBindingId, ESectionEvaluationFlags flags)
    {
        Section = section;
        ObjectBindingId = objectBindingId;
        Flags = flags;
    }

    public FMovieSceneSubSectionData(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            Section = new FPackageIndex(reader);
            ObjectBindingId = new Guid(reader.ReadBytes(16));
            Flags = (ESectionEvaluationFlags)reader.ReadByte();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        Section.Write(writer);
        writer.Write(ObjectBindingId.ToByteArray());
        writer.Write((byte)Flags);
        return sizeof(int) + 16 + sizeof(byte);
    }
}

public struct FEntityAndMetaDataIndex
{
    public int EntityIndex;
    public int MetaDataIndex;

    public FEntityAndMetaDataIndex(int entityIndex, int metaDataIndex)
    {
        EntityIndex = entityIndex;
        MetaDataIndex = metaDataIndex;
    }

    public FEntityAndMetaDataIndex(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            EntityIndex = reader.ReadInt32();
            MetaDataIndex = reader.ReadInt32();
        }
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(EntityIndex);
        writer.Write(MetaDataIndex);
    }
}

public struct FMovieSceneSubSequenceTreeEntry
{
    public uint SequenceID;
    public ESectionEvaluationFlags Flags;
    public StructPropertyData RootToSequenceWarpCounter = null;

    public FMovieSceneSubSequenceTreeEntry(uint sequenceID, byte flags, StructPropertyData _struct = null)
    {
        SequenceID = sequenceID;
        Flags = (ESectionEvaluationFlags)flags;
        RootToSequenceWarpCounter = _struct;
    }

    public FMovieSceneSubSequenceTreeEntry(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            SequenceID = reader.ReadUInt32();
            Flags = (ESectionEvaluationFlags)reader.ReadByte();
            if (reader.Asset.GetCustomVersion<FReleaseObjectVersion>() >= FReleaseObjectVersion.AddedSubSequenceEntryWarpCounter ||
                reader.Asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() >= FFortniteMainBranchObjectVersion.AddedSubSequenceEntryWarpCounter)
            {
                var data = new StructPropertyData(FName.DefineDummy(reader.Asset, "RootToSequenceWarpCounter"), FName.DefineDummy(reader.Asset, "MovieSceneWarpCounter"));
                data.Read(reader, false, 1, 0, PropertySerializationContext.StructFallback);
                RootToSequenceWarpCounter = data;
            }
        }
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(SequenceID);
        writer.Write((byte)Flags);
        if (writer.Asset.GetCustomVersion<FReleaseObjectVersion>() >= FReleaseObjectVersion.AddedSubSequenceEntryWarpCounter ||
            writer.Asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() >= FFortniteMainBranchObjectVersion.AddedSubSequenceEntryWarpCounter)
        {
            RootToSequenceWarpCounter?.Write(writer, false, PropertySerializationContext.StructFallback);
        }
    }
}

public struct FMovieSceneSubSectionFieldData(AssetBinaryReader reader)
{
    public TMovieSceneEvaluationTree<FMovieSceneSubSectionData> Field = new(reader, () => new FMovieSceneSubSectionData(reader));

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        Field.Write(writer, entry => entry.Write(writer));

        return (int)(writer.BaseStream.Position - offset);
    }
}

public struct FMovieSceneEvaluationFieldEntityTree(AssetBinaryReader reader)
{
    public TMovieSceneEvaluationTree<FEntityAndMetaDataIndex> SerializedData = new(reader, () => new FEntityAndMetaDataIndex(reader));

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        SerializedData.Write(writer, entry => entry.Write(writer));

        return (int)(writer.BaseStream.Position - offset);
    }
}

public struct FMovieSceneSubSequenceTree(AssetBinaryReader reader)
{
    public TMovieSceneEvaluationTree<FMovieSceneSubSequenceTreeEntry> Data = new(reader, () => new FMovieSceneSubSequenceTreeEntry(reader));

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        Data.Write(writer, entry => entry.Write(writer));

        return (int)(writer.BaseStream.Position - offset);
    }
}

public struct FSectionEvaluationDataTree
{
    public TMovieSceneEvaluationTree<StructPropertyData> Tree;

    public FSectionEvaluationDataTree(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            Tree = new(reader, () => ReadTree(reader));

            static StructPropertyData ReadTree(AssetBinaryReader reader)
            {
                var data = new StructPropertyData(FName.DefineDummy(reader.Asset, "Tree"), FName.DefineDummy(reader.Asset, "SectionEvaluationDataTree"));
                data.Read(reader, false, 1, 0, PropertySerializationContext.StructFallback);
                return data;
            }
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        static void WriteTree(AssetBinaryWriter writer, StructPropertyData data)
        {
            if (data != null)
            {
                data.StructType = FName.DefineDummy(writer.Asset, "SectionEvaluationDataTree");
                data.Write(writer, false, PropertySerializationContext.StructFallback);
            }
        }

        var offset = writer.BaseStream.Position;
        Tree.Write(writer, entry => WriteTree(writer, entry));
        return (int)(writer.BaseStream.Position - offset);
    }
}

public struct FMovieSceneTrackFieldData(AssetBinaryReader reader)
{
    public TMovieSceneEvaluationTree<uint> Field = new (reader, reader.ReadUInt32);

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        Field.Write(writer, writer.Write);

        return (int)(writer.BaseStream.Position - offset);
    }
}