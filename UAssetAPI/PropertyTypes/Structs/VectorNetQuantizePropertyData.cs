using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class VectorNetQuantizePropertyData : VectorPropertyData
{
    public VectorNetQuantizePropertyData(FName name) : base(name) { }

    public VectorNetQuantizePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector_NetQuantize");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class VectorNetQuantizeNormalPropertyData : VectorPropertyData
{
    public VectorNetQuantizeNormalPropertyData(FName name) : base(name) { }

    public VectorNetQuantizeNormalPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector_NetQuantizeNormal");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class VectorNetQuantize10PropertyData : VectorPropertyData
{
    public VectorNetQuantize10PropertyData(FName name) : base(name) { }

    public VectorNetQuantize10PropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector_NetQuantize10");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class VectorNetQuantize100PropertyData : VectorPropertyData
{
    public VectorNetQuantize100PropertyData(FName name) : base(name) { }

    public VectorNetQuantize100PropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector_NetQuantize100");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}