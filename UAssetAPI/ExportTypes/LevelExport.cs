using System.Collections.Generic;
using UAssetAPI.CustomVersions;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes;

/// <summary>
/// URL structure.
/// </summary>
public struct FURL
{
    // Protocol, i.e. "unreal" or "http".
    public FString Protocol;
    // Optional hostname, i.e. "204.157.115.40" or "unreal.epicgames.com", blank if local.
    public FString Host;
    // Optional host port.
    public int Port;
    public int Valid;
    // Map name, i.e. "SkyCity", default is "Entry".
    public FString Map;
    // Options.
    public List<FString> Op;
    // Portal to enter through, default is "".
    public FString Portal;

    public FURL(AssetBinaryReader reader)
    {
        Protocol = reader.ReadFString();
        Host = reader.ReadFString();
        Map = reader.ReadFString();
        Portal = reader.ReadFString();
        var len = reader.ReadInt32();
        Op = new List<FString>(len);
        for (int i = 0; i < len; i++)
        {
            Op.Add(reader.ReadFString());
        }
        Port = reader.ReadInt32();
        Valid = reader.ReadInt32();
    }

    public int Write(AssetBinaryWriter writer){

        var offset = writer.BaseStream.Position;
        writer.Write(Protocol);
        writer.Write(Host);
        writer.Write(Map);
        writer.Write(Portal);
        writer.Write(Op.Count);
        for (int i = 0; i < Op.Count; i++)
        {
            writer.Write(Op[i]);
        }
        writer.Write(Port);
        writer.Write(Valid);
        return (int)(writer.BaseStream.Position-offset);
    }
}

public class LevelExport : NormalExport
{
    // Owner of TTransArray<AActor> Actors
    public FPackageIndex Owner;
    public List<FPackageIndex> Actors;
    public FURL URL;
    public FPackageIndex Model;
    public List<FPackageIndex> ModelComponents;
    public FPackageIndex LevelScriptActor;
    public FPackageIndex NavListStart;
    public FPackageIndex NavListEnd;
    //public FPrecomputedVisibilityHandler PrecomputedVisibilityHandler;
    //public FPrecomputedVolumeDistanceField PrecomputedVolumeDistanceField;

    public LevelExport(Export super) : base(super) { }

    public LevelExport(UAsset asset, byte[] extras) : base(asset, extras) { }

    public LevelExport(){ } 

    public override void Read(AssetBinaryReader reader, int nextStarting)
    {
        base.Read(reader, nextStarting);

        if (reader.Asset.GetCustomVersion<FReleaseObjectVersion>() < FReleaseObjectVersion.LevelTransArrayConvertedToTArray)
            Owner = new FPackageIndex(reader);

        int numIndexEntries = reader.ReadInt32();

        Actors = new List<FPackageIndex>(numIndexEntries);
        for (int i = 0; i < numIndexEntries; i++) {
            Actors.Add(new FPackageIndex(reader));
        }
        
        URL = new FURL(reader);

        Model = new FPackageIndex(reader);
        int numModelEntries = reader.ReadInt32();

        ModelComponents = new List<FPackageIndex>(numModelEntries);
        for (int i = 0; i < numModelEntries; i++)
        {
            ModelComponents.Add(new FPackageIndex(reader));
        }

        LevelScriptActor = new FPackageIndex(reader);
        NavListStart = new FPackageIndex(reader);
        NavListEnd = new FPackageIndex(reader);
        
        // TODO: Implement the rest of the properties
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);

        if (writer.Asset.GetCustomVersion<FReleaseObjectVersion>() < FReleaseObjectVersion.LevelTransArrayConvertedToTArray)
            Owner.Write(writer);

        writer.Write(Actors.Count);
        for (int i = 0; i < Actors.Count; i++)
        {
            Actors[i].Write(writer);
        }

        URL.Write(writer);

        Model.Write(writer);
        writer.Write(ModelComponents.Count);
        for (int i = 0; i < ModelComponents.Count; i++)
        {
            ModelComponents[i].Write(writer);
        }

        LevelScriptActor.Write(writer);
        NavListStart.Write(writer);
        NavListEnd.Write(writer);
    }
}
