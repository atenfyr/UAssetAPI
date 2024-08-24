using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 2-D space composed of components (X, Y) with floating/double point precision.
/// </summary>
public class Vector2DPropertyData : PropertyData<FVector2D>
{
    public Vector2DPropertyData(FName name) : base(name) { }

    public Vector2DPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector2D");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FVector2D(reader);
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
        Value = new FVector2D(X, Y);
    }

    public override string ToString()
    {
        return "(" + Value.X + ", " + Value.Y + ")";
    }
}