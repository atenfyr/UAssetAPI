using System;
using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;

namespace UAssetAPI.UnrealTypes;

public class UniversalObjectLocatorFragmentPropertyData : StructPropertyData
{
    private static Dictionary<string, string> FragmentTypeRegistry = new()
    {
        { "actor", "DirectPathObjectLocator" },
        { "animinst", "AnimInstanceLocatorFragment" },
        { "subobj", "SubObjectLocator" },
        { "ls_lazy_obj_ptr", "LegacyLazyObjectPtrFragment" },
    };

    public FName FragmentTypeID;

    public UniversalObjectLocatorFragmentPropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public UniversalObjectLocatorFragmentPropertyData(FName name) : base(name) { }
    public UniversalObjectLocatorFragmentPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("UniversalObjectLocatorFragment");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        FragmentTypeID = reader.ReadFName();
        if (FragmentTypeID.Value.Value == "None") return;

        if (FragmentTypeRegistry.TryGetValue(FragmentTypeID.Value.Value, out var structType))
        {
            StructType = FName.DefineDummy(reader.Asset, structType);
        }
        else
        {
            throw new FormatException($"Unknown FragmentTypeID : {FragmentTypeID}");
        }

        base.Read(reader, includeHeader, 1, leng2, PropertySerializationContext.StructFallback);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        writer.Write(FragmentTypeID);
        int res = 8;
        if (FragmentTypeID.Value.Value == "None") return res;
        res += base.Write(writer, includeHeader, PropertySerializationContext.StructFallback);
        return res;
    }
}