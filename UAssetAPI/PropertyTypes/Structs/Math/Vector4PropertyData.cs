using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 4-D space composed of components (X, Y, Z, W) with floating/double point precision.
/// </summary>
public class Vector4PropertyData : PropertyData<FVector4>
{
    public Vector4PropertyData(FName name) : base(name) { }

    public Vector4PropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector4");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FVector4(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        double.TryParse(d[0], out double X);
        double.TryParse(d[1], out double Y);
        double.TryParse(d[2], out double Z);
        double.TryParse(d[3], out double W);
        Value = new FVector4(X, Y, Z, W);
    }

    public override string ToString()
    {
        return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ", " + Value.W + ")";
    }
}