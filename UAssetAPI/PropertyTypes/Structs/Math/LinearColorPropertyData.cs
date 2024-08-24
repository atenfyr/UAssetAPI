using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class LinearColorPropertyData : PropertyData<FLinearColor> // R, G, B, A
{
    public LinearColorPropertyData(FName name) : base(name) { }

    public LinearColorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("LinearColor");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FLinearColor(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override void FromString(string[] d, UAsset asset)
    {
        if (!float.TryParse(d[0], out float colorR)) return;
        if (!float.TryParse(d[1], out float colorG)) return;
        if (!float.TryParse(d[2], out float colorB)) return;
        if (!float.TryParse(d[3], out float colorA)) return;
        Value = new FLinearColor(colorR, colorG, colorB, colorA);
    }
}