using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 3-D space composed of components (X, Y, Z) with floating/double point precision.
/// </summary>
public class VectorPropertyData : PropertyData<FVector>
{
    public VectorPropertyData(FName name) : base(name) { }

    public VectorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FVector(reader);
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
        Value = new FVector(X, Y, Z);
    }

    public override string ToString()
    {
        return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ")";
    }
}