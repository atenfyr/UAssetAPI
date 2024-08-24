using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneGenerationLedgerPropertyData : PropertyData
{
    /** Map of track identifiers to number of references within th template (generally 1, maybe >1 for shared tracks) */
    TMap<StructPropertyData, int> TrackReferenceCounts;

    /** Map of track signature to array of track identifiers that it created */
    TMap<Guid, StructPropertyData> TrackSignatureToTrackIdentifier;

    public MovieSceneGenerationLedgerPropertyData(FName name) : base(name) { }
    public MovieSceneGenerationLedgerPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneGenerationLedger");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        var NumReferenceCounts = reader.ReadInt32();
        TrackReferenceCounts = new TMap<StructPropertyData, int>();

        for (int i = 0; i < NumReferenceCounts; i++)
        {
            var identifier = new StructPropertyData(FName.DefineDummy(reader.Asset, "MovieSceneTrackIdentifier"), FName.DefineDummy(reader.Asset, "Generic"));
            identifier.Ancestry.Initialize(Ancestry, Name);
            identifier.Read(reader, false, 1, 0, PropertySerializationContext.StructFallback);
            TrackReferenceCounts.Add(identifier, reader.ReadInt32());
        }
        var SignatureToTrackIDs = reader.ReadInt32();
        TrackSignatureToTrackIdentifier = new TMap<Guid, StructPropertyData>();
        for (int i = 0; i < SignatureToTrackIDs; i++)
        {
            var guid = new Guid(reader.ReadBytes(16));
            var counts = reader.ReadInt32();
            if (counts != 1) throw new FormatException("Invalid TrackSignatureToTrackIdentifier count");
            var identifier = new StructPropertyData(FName.DefineDummy(reader.Asset, "MovieSceneTrackIdentifier"), FName.DefineDummy(reader.Asset, "Generic"));
            identifier.Ancestry.Initialize(Ancestry, Name);
            identifier.Read(reader, false, 1, 0, PropertySerializationContext.StructFallback);
            TrackSignatureToTrackIdentifier.Add(guid, identifier);
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        var offset = writer.BaseStream.Position;

        writer.Write(TrackReferenceCounts.Count);
        foreach (var kvp in TrackReferenceCounts)
        {
            kvp.Key.Write(writer, false, PropertySerializationContext.StructFallback);
            writer.Write(kvp.Value);
        }

        writer.Write(TrackSignatureToTrackIdentifier.Count);
        foreach (var kvp in TrackSignatureToTrackIdentifier)
        {
            writer.Write(kvp.Key.ToByteArray());
            writer.Write(1);
            kvp.Value.Write(writer, false, PropertySerializationContext.StructFallback);
        }

        return (int)(writer.BaseStream.Position - offset);
    }
}