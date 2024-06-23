using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FTwoVectors
{
    public FVector V1;
    public FVector V2;

    public FTwoVectors(FVector v1, FVector v2)
    {
        V1 = v1;
        V2 = v2;
    }

    public FTwoVectors(AssetBinaryReader reader)
    {
        V1 = new FVector(reader);
        V2 = new FVector(reader);
    }

    public int Write(AssetBinaryWriter writer)
    {
        var size = V1.Write(writer);
        size += V2.Write(writer);
        return size;
    }
}

public class TwoVectorsPropertyData : PropertyData<FTwoVectors>
{
    public TwoVectorsPropertyData(FName name) : base(name) { }

    public TwoVectorsPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("TwoVectors");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new FTwoVectors(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        return Value.Write(writer);
    }
}