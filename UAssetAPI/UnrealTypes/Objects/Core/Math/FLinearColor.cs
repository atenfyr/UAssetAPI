using Newtonsoft.Json;
using System;
using System.Drawing;

namespace UAssetAPI.UnrealTypes;

public static class LinearHelpers
{
    public static Color Convert(FLinearColor color)
    {
        float FloatR = UAPUtils.Clamp(color.R, 0.0f, 1.0f);
        float FloatG = UAPUtils.Clamp(color.G, 0.0f, 1.0f);
        float FloatB = UAPUtils.Clamp(color.B, 0.0f, 1.0f);
        float FloatA = UAPUtils.Clamp(color.A, 0.0f, 1.0f);

        FloatR = (float)(FloatR <= 0.0031308f ? FloatR * 12.92f : Math.Pow(FloatR, 1.0f / 2.4f) * 1.055f - 0.055f);
        FloatG = (float)(FloatG <= 0.0031308f ? FloatG * 12.92f : Math.Pow(FloatG, 1.0f / 2.4f) * 1.055f - 0.055f);
        FloatB = (float)(FloatB <= 0.0031308f ? FloatB * 12.92f : Math.Pow(FloatB, 1.0f / 2.4f) * 1.055f - 0.055f);

        return Color.FromArgb((byte)Math.Floor(FloatA * 255.999f), (byte)Math.Floor(FloatR * 255.999f), (byte)Math.Floor(FloatG * 255.999f), (byte)Math.Floor(FloatB * 255.999f));
    }
}

/// <summary>
/// A linear, 32-bit/component floating point RGBA color.
/// </summary>
public struct FLinearColor : ICloneable
{
    [JsonProperty]
    public float R;
    [JsonProperty]
    public float G;
    [JsonProperty]
    public float B;
    [JsonProperty]
    public float A;

    public FLinearColor(float R, float G, float B, float A)
    {
        this.R = R;
        this.G = G;
        this.B = B;
        this.A = A;
    }

    public object Clone() => new FLinearColor(this.R, this.G, this.B, this.A);

    public FLinearColor(AssetBinaryReader reader)
    {
        R = reader.ReadSingle();
        G = reader.ReadSingle();
        B = reader.ReadSingle();
        A = reader.ReadSingle();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(R);
        writer.Write(G);
        writer.Write(B);
        writer.Write(A);
        return sizeof(float) * 4;
    }
}