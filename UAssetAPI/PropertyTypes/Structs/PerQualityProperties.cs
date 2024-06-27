using System;
using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FPerQualityLevel<T>
{
    public bool bCooked;
    public T Default;
    public Dictionary<int, T> PerQuality;

    public FPerQualityLevel(bool _bCooked, T _default, Dictionary<int, T> perQuality)
    {
        bCooked = _bCooked;
        Default = _default;
        PerQuality = perQuality;
    }

    public FPerQualityLevel(AssetBinaryReader reader, Func<T> valueReader)
    {
        bCooked = reader.ReadBooleanInt();
        Default = valueReader();
        PerQuality = [];
        int numElements = reader.ReadInt32();
        for (int i = 0; i < numElements; i++)
        {
            PerQuality[reader.ReadInt32()] = valueReader();
        }
    }

    public int Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        var offset = writer.BaseStream.Position;
        writer.Write(bCooked ? 1 : 0);
        valueWriter(Default);
        writer.Write(PerQuality.Count);
        foreach (var pair in PerQuality)
        {
            writer.Write(pair.Key);
            valueWriter(pair.Value);
        }
        return (int)(writer.BaseStream.Position - offset);
    }
}

public class PerQualityLevelFloatPropertyData : PropertyData<FPerQualityLevel<float>>
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

        Value = new(reader, reader.ReadSingle);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        return Value.Write(writer, writer.Write);
    }
}

public class PerQualityLevelIntPropertyData : PropertyData<FPerQualityLevel<int>>
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

        Value = new(reader, reader.ReadInt32);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        return Value.Write(writer, writer.Write);
    }
}