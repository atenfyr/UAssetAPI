using Newtonsoft.Json;
using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A rectangular 2D Box.
/// </summary>
public struct Box2f : ICloneable
{
    public Vector2f Min;
    public Vector2f Max;
    public bool IsValid;

    public Box2f(Vector2f min, Vector2f max, bool isValid)
    {
        Min = min;
        Max = max;
        IsValid = isValid;
    }

    public Box2f(AssetBinaryReader reader)
    {
        Min = new Vector2f(reader);
        Max = new Vector2f(reader);
        IsValid = reader.ReadBoolean();
    }

    public int Write(AssetBinaryWriter writer)
    {
        int totalSize = 0;
        totalSize += Min.Write(writer);
        totalSize += Max.Write(writer);
        writer.Write(IsValid);
        return totalSize + sizeof(bool);
    }

    public object Clone() => new Box2f(Min, Max, IsValid);
}

public class Box2fPropertyData : PropertyData<Box2f>
{
    [JsonProperty]
    public Vector2f Min;
    [JsonProperty]
    public Vector2f Max;
    [JsonProperty]
    public bool IsValid;

    public Box2fPropertyData(FName name) : base(name) { }

    public Box2fPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Box2f");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new Box2f(reader);
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