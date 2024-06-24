using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FPerQualityLevelFloat
{
    public bool bCooked;
    public float Default;
    public Dictionary<int, float> PerQuality;

    public FPerQualityLevelFloat(bool bCooked, float Default, Dictionary<int, float> PerQuality)
    {
        this.bCooked = bCooked;
        this.Default = Default;
        this.PerQuality = PerQuality;
    }

    public FPerQualityLevelFloat(AssetBinaryReader reader)
    {
        bCooked = reader.ReadInt32() != 0;
        Default = reader.ReadSingle();
        PerQuality = [];
        int numElements = reader.ReadInt32();
        for (int i = 0; i < numElements; i++)
        {
            PerQuality[reader.ReadInt32()] = reader.ReadSingle();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;
        writer.Write(bCooked ? 1 : 0);
        writer.Write(Default);
        writer.Write(PerQuality.Count);
        foreach (var pair in PerQuality)
        {
            writer.Write(pair.Key);
            writer.Write(pair.Value);
        }
        return (int)(writer.BaseStream.Position - offset);
    }
}

public class PerQualityLevelFloatPropertyData : PropertyData<FPerQualityLevelFloat>
{
    public PerQualityLevelFloatPropertyData(FName name) : base(name) { }

    public PerQualityLevelFloatPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("PerQualityLevelFloat");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new FPerQualityLevelFloat(reader);
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

public struct FPerQualityLevelInt
{
    public bool bCooked;
    public int Default;
    public Dictionary<int, int> PerQuality;

    public FPerQualityLevelInt(bool bCooked, int Default, Dictionary<int, int> PerQuality)
    {
        this.bCooked = bCooked;
        this.Default = Default;
        this.PerQuality = PerQuality;
    }

    public FPerQualityLevelInt(AssetBinaryReader reader)
    {
        bCooked = reader.ReadInt32() != 0;
        Default = reader.ReadInt32();
        PerQuality = [];
        int numElements = reader.ReadInt32();
        for (int i = 0; i < numElements; i++)
        {
            PerQuality[reader.ReadInt32()] = reader.ReadInt32();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;
        writer.Write(bCooked ? 1 : 0);
        writer.Write(Default);
        writer.Write(PerQuality.Count);
        foreach (var pair in PerQuality)
        {
            writer.Write(pair.Key);
            writer.Write(pair.Value);
        }
        return (int)(writer.BaseStream.Position - offset);
    }
}

public class PerQualityLevelIntPropertyData : PropertyData<FPerQualityLevelInt>
{
    public PerQualityLevelIntPropertyData(FName name) : base(name) { }

    public PerQualityLevelIntPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("PerQualityLevelInt");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new FPerQualityLevelInt(reader);
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