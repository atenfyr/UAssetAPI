using System;
using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class VectorNetQuantizePropertyData : StructPropertyData
{
    public VectorNetQuantizePropertyData(FName name, FName forcedType) : base(name, forcedType)
    {
        Value.Add(new VectorPropertyData(name)); //backup for isZero
    }

    public VectorNetQuantizePropertyData(FName name) : base(name)
    {
        Value.Add(new VectorPropertyData(name));
    }

    public VectorNetQuantizePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector_NetQuantize");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        Value = [];
        //either this or 5.0+ or unversioned
        if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            var data = new VectorPropertyData(Name);
            data.Read(reader, includeHeader, leng1, leng2, serializationContext);
            Value.Add(data);
        }
        else
        {
            StructType = FName.DefineDummy(reader.Asset, PropertyType);
            base.Read(reader, includeHeader, 1, leng2, PropertySerializationContext.StructFallback);
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            if (Value == null)
            {
                Value = new List<PropertyData>();
                Value.Add(new VectorPropertyData(Name));
            }

            if (Value.Count == 1 && Value[0] is VectorPropertyData vector)
            {
                return Value[0].Write(writer, includeHeader, serializationContext);
            }
            throw new FormatException($"{PropertyType} must have a VectorPropertyData child");
        }
        else
        {
            StructType = FName.DefineDummy(writer.Asset, PropertyType);
            return base.Write(writer, includeHeader, PropertySerializationContext.StructFallback);
        }
    }
}

public class VectorNetQuantizeNormalPropertyData : VectorNetQuantizePropertyData
{
    public VectorNetQuantizeNormalPropertyData(FName name) : base(name) { }

    public VectorNetQuantizeNormalPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector_NetQuantizeNormal");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class VectorNetQuantize10PropertyData : VectorNetQuantizePropertyData
{
    public VectorNetQuantize10PropertyData(FName name) : base(name) { }

    public VectorNetQuantize10PropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector_NetQuantize10");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class VectorNetQuantize100PropertyData : VectorNetQuantizePropertyData
{
    public VectorNetQuantize100PropertyData(FName name) : base(name) { }

    public VectorNetQuantize100PropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector_NetQuantize100");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}