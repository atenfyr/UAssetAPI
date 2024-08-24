using System;
using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public abstract class TPerPlatformPropertyData<T> : PropertyData<T[]>
{
    public TPerPlatformPropertyData(FName name) : base(name)
    {
        Value = [];
    }

    public TPerPlatformPropertyData()
    {
        Value = [];
    }
}

/// <summary>
/// <see cref="BoolPropertyData"/> (<see cref="bool"/>) property with per-platform overrides.
/// </summary>
public class PerPlatformBoolPropertyData : TPerPlatformPropertyData<bool>
{
    public PerPlatformBoolPropertyData(FName name) : base(name) { }

    public PerPlatformBoolPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("PerPlatformBool");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        int numEntries = reader.ReadInt32();
        Value = new bool[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
            Value[i] = reader.ReadInt32() == 1;
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value.Length);
        for (int i = 0; i < Value.Length; i++)
        {
            writer.Write(Value[i] ? 1 : 0);
        }
        return sizeof(int) + sizeof(int) * Value.Length;
    }

    public override void FromString(string[] d, UAsset asset)
    {
        var valueList = new List<bool>();
        if (bool.TryParse(d[0], out bool res1)) valueList.Add(res1);
        if (bool.TryParse(d[1], out bool res2)) valueList.Add(res2);
        if (bool.TryParse(d[2], out bool res3)) valueList.Add(res3);
        if (bool.TryParse(d[3], out bool res4)) valueList.Add(res4);
        Value = valueList.ToArray();
    }

    public override string ToString()
    {
        string oup = "(";
        for (int i = 0; i < Value.Length; i++)
        {
            oup += Convert.ToString(Value[i]) + ", ";
        }
        return oup.Remove(oup.Length - 2) + ")";
    }
}

/// <summary>
/// <see cref="FloatPropertyData"/> (<see cref="float"/>) property with per-platform overrides.
/// </summary>
public class PerPlatformFloatPropertyData : TPerPlatformPropertyData<float>
{
    public PerPlatformFloatPropertyData(FName name) : base(name) { }

    public PerPlatformFloatPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("PerPlatformFloat");
    public override bool HasCustomStructSerialization { get { return true; } }
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        int numEntries = reader.ReadInt32();
        Value = new float[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
            Value[i] = reader.ReadSingle();
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value.Length);
        for (int i = 0; i < Value.Length; i++)
        {
            writer.Write(Value[i]);
        }
        return sizeof(int) + sizeof(float) * Value.Length;
    }

    public override void FromString(string[] d, UAsset asset)
    {
        var valueList = new List<float>();
        if (float.TryParse(d[0], out float res1)) valueList.Add(res1);
        if (float.TryParse(d[1], out float res2)) valueList.Add(res2);
        if (float.TryParse(d[2], out float res3)) valueList.Add(res3);
        if (float.TryParse(d[3], out float res4)) valueList.Add(res4);
        Value = valueList.ToArray();
    }

    public override string ToString()
    {
        string oup = "(";
        for (int i = 0; i < Value.Length; i++)
        {
            oup += Convert.ToString(Value[i]) + ", ";
        }
        return oup.Remove(oup.Length - 2) + ")";
    }
}

/// <summary>
/// <see cref="IntPropertyData"/> (<see cref="int"/>) property with per-platform overrides.
/// </summary>
public class PerPlatformIntPropertyData : TPerPlatformPropertyData<int>
{
    public PerPlatformIntPropertyData(FName name) : base(name) { }

    public PerPlatformIntPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("PerPlatformInt");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        int numEntries = reader.ReadInt32();
        Value = new int[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
            Value[i] = reader.ReadInt32();
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value.Length);
        for (int i = 0; i < Value.Length; i++)
        {
            writer.Write(Value[i]);
        }
        return sizeof(int) + sizeof(int) * Value.Length;
    }

    public override void FromString(string[] d, UAsset asset)
    {
        var valueList = new List<int>();
        if (int.TryParse(d[0], out int res1)) valueList.Add(res1);
        if (int.TryParse(d[1], out int res2)) valueList.Add(res2);
        if (int.TryParse(d[2], out int res3)) valueList.Add(res3);
        if (int.TryParse(d[3], out int res4)) valueList.Add(res4);
        Value = valueList.ToArray();
    }

    public override string ToString()
    {
        string oup = "(";
        for (int i = 0; i < Value.Length; i++)
        {
            oup += Convert.ToString(Value[i]) + ", ";
        }
        return oup.Remove(oup.Length - 2) + ")";
    }
}

/// <summary>
/// <see cref="PerPlatformFrameRatePropertyData"/> (<see cref="FFrameRate"/>) property with per-platform overrides.
/// </summary>
public class PerPlatformFrameRatePropertyData : TPerPlatformPropertyData<FFrameRate>
{
    public PerPlatformFrameRatePropertyData(FName name) : base(name) { }

    public PerPlatformFrameRatePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("PerPlatformFrameRate");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        int numEntries = reader.ReadInt32();
        Value = new FFrameRate[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
            Value[i] = new FFrameRate(reader.ReadInt32(), reader.ReadInt32());
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value.Length);
        for (int i = 0; i < Value.Length; i++)
        {
            writer.Write(Value[i].Numerator);
            writer.Write(Value[i].Denominator);
        }
        return sizeof(int) + sizeof(int) * 2 * Value.Length;
    }

    public override void FromString(string[] d, UAsset asset)
    {
        var valueList = new List<FFrameRate>();
        if (FFrameRate.TryParse(d[0], out FFrameRate res1)) valueList.Add(res1);
        if (FFrameRate.TryParse(d[1], out FFrameRate res2)) valueList.Add(res2);
        if (FFrameRate.TryParse(d[2], out FFrameRate res3)) valueList.Add(res3);
        if (FFrameRate.TryParse(d[3], out FFrameRate res4)) valueList.Add(res4);
        Value = valueList.ToArray();
    }

    public override string ToString()
    {
        string oup = "(";
        for (int i = 0; i < Value.Length; i++)
        {
            oup += Convert.ToString(Value[i]) + ", ";
        }
        return oup.Remove(oup.Length - 2) + ")";
    }
}