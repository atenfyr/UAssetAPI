using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Options that define how to blend when changing view targets in <see cref="ViewTargetBlendParamsPropertyData"/>.
/// </summary>
public enum ViewTargetBlendFunction
{
    /** Camera does a simple linear interpolation. */
    VTBlend_Linear,
    /** Camera has a slight ease in and ease out, but amount of ease cannot be tweaked. */
    VTBlend_Cubic,
    /** Camera immediately accelerates, but smoothly decelerates into the target.  Ease amount controlled by BlendExp. */
    VTBlend_EaseIn,
    /** Camera smoothly accelerates, but does not decelerate into the target.  Ease amount controlled by BlendExp. */
    VTBlend_EaseOut,
    /** Camera smoothly accelerates and decelerates.  Ease amount controlled by BlendExp. */
    VTBlend_EaseInOut,
    VTBlend_MAX,
}

/// <summary>
/// A set of parameters to describe how to transition between view targets.
/// Referred to as FViewTargetTransitionParams in the Unreal Engine.
/// </summary>
public class ViewTargetBlendParamsPropertyData : PropertyData
{
    [JsonProperty]
    public float BlendTime;
    [JsonProperty]
    [JsonConverter(typeof(StringEnumConverter))]
    public ViewTargetBlendFunction BlendFunction;
    [JsonProperty]
    public float BlendExp;
    [JsonProperty]
    public bool bLockOutgoing;

    public ViewTargetBlendParamsPropertyData(FName name) : base(name) { }

    public ViewTargetBlendParamsPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("ViewTargetBlendParams");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        BlendTime = reader.ReadSingle();
        BlendFunction = (ViewTargetBlendFunction)reader.ReadByte();
        BlendExp = reader.ReadSingle();
        bLockOutgoing = reader.ReadInt32() != 0;
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(BlendTime);
        writer.Write((byte)BlendFunction);
        writer.Write(BlendExp);
        writer.Write(bLockOutgoing ? 1 : 0);
        return sizeof(float) * 2 + sizeof(byte) + sizeof(int);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        if (float.TryParse(d[0], out float res1)) BlendTime = res1;
        if (Enum.TryParse(d[1], out ViewTargetBlendFunction res2)) BlendFunction = res2;
        if (float.TryParse(d[2], out float res3)) BlendExp = res3;
        if (bool.TryParse(d[3], out bool res4)) bLockOutgoing = res4;
    }

    public override string ToString()
    {
        string oup = "(";
        oup += BlendTime + ", ";
        oup += BlendFunction + ", ";
        oup += BlendExp + ", ";
        oup += bLockOutgoing + ", ";
        return oup.Remove(oup.Length - 2) + ")";
    }
}
