using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public abstract class TBoxPropertyData<T> : PropertyData<TBox<T>>
{
    public TBoxPropertyData(FName name) : base(name) { }

    public TBoxPropertyData() { }
}

public class BoxPropertyData : TBoxPropertyData<FVector>
{
    public BoxPropertyData(FName name) : base(name) { }

    public BoxPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Box");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new(reader, () => new FVector(reader));
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer, entry => entry.Write(writer));
    }
}

public class Box2fPropertyData : TBoxPropertyData<FVector2f>
{
    public Box2fPropertyData(FName name) : base(name) { }

    public Box2fPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Box2f");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new(reader, () => new FVector2f(reader));
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer, entry => entry.Write(writer));
    }
}

public class Box2DPropertyData : TBoxPropertyData<FVector2D>
{
    public Box2DPropertyData(FName name) : base(name) { }

    public Box2DPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Box2D");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new TBox<FVector2D>(reader, () => new FVector2D(reader));
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer, entry => entry.Write(writer));
    }
}