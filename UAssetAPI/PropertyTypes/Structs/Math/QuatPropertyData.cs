using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Floating point quaternion that can represent a rotation about an axis in 3-D space.
/// The X, Y, Z, W components also double as the Axis/Angle format.
/// </summary>
public class QuatPropertyData : PropertyData<FQuat>
{
    public QuatPropertyData(FName name) : base(name) { }

    public QuatPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Quat");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FQuat(reader);
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
        Value = new FQuat(X, Y, Z, W);
    }

    public override string ToString()
    {
        return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ", " + Value.W + ")";
    }
}