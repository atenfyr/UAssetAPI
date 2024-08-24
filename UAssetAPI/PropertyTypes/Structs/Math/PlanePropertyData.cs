using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A plane in 3-D space stores the coeffecients as Xx+Yy+Zz=W.
/// </summary>
public class PlanePropertyData : PropertyData<FPlane>
{
    public PlanePropertyData(FName name) : base(name) { }

    public PlanePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Plane");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FPlane(reader);
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
        Value = new FPlane(X, Y, Z, W);
    }

    public override string ToString()
    {
        return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ", " + Value.W + ")";
    }
}