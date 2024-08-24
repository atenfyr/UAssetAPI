using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class FLevelSequenceLegacyObjectReference
{
    public Guid ObjectId;
    public FString ObjectPath;

    public FLevelSequenceLegacyObjectReference(Guid objectId, FString objectPath)
    {
        ObjectId = objectId;
        ObjectPath = objectPath;
    }
    
    public FLevelSequenceLegacyObjectReference(AssetBinaryReader reader)
    {
        ObjectId = new Guid(reader.ReadBytes(16));
        ObjectPath = reader.ReadFString();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(ObjectId.ToByteArray());
        var size = 16;
        size += writer.Write(ObjectPath);
        return size;
    }
}

public class LevelSequenceObjectReferenceMapPropertyData : PropertyData<TMap<Guid, FLevelSequenceLegacyObjectReference>>
{
    public LevelSequenceObjectReferenceMapPropertyData(FName name) : base(name) { }

    public LevelSequenceObjectReferenceMapPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("LevelSequenceObjectReferenceMap");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
    

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        var num = reader.ReadInt32();
        Value = new TMap<Guid, FLevelSequenceLegacyObjectReference>();
        for (int i = 0; i < num; i++)
        {
            Value[new Guid(reader.ReadBytes(16))] = new FLevelSequenceLegacyObjectReference(reader);
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        var offset = writer.BaseStream.Position;

        if (Value == null) Value = [];
        writer.Write(Value.Count);

        foreach (var pair in Value)
        {
            writer.Write(pair.Key.ToByteArray());
            pair.Value.Write(writer);
        }

        return (int)(writer.BaseStream.Position - offset);
    }
}