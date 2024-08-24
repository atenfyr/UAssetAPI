using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Implements a container for rotation information.
/// All rotation values are stored in degrees.
/// </summary>
public class RotatorPropertyData : PropertyData<FRotator>
{        
    public RotatorPropertyData(FName name) : base(name) { }

    public RotatorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Rotator");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FRotator(reader);
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
        double.TryParse(d[0], out double Roll);
        double.TryParse(d[1], out double Pitch);
        double.TryParse(d[2], out double Yaw);
        Value = new FRotator(Pitch, Yaw, Roll);
    }

    public override string ToString()
    {
        return "(" + Value.Roll + ", " + Value.Pitch + ", " + Value.Yaw + ")";
    }
}