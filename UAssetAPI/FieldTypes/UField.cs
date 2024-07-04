using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;
using UAssetAPI.CustomVersions;
using UAssetAPI.Unversioned;

namespace UAssetAPI.FieldTypes;

/// <summary>
/// Base class of reflection data objects.
/// </summary>
public class UField
{
    /// <summary>
    /// Next Field in the linked list. Removed entirely in the custom version FFrameworkObjectVersion::RemoveUField_Next in favor of a regular array
    /// </summary>
    [DisplayIndexOrder(0)]
    public FPackageIndex Next;

    public virtual void Read(AssetBinaryReader reader)
    {
        if (reader.Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.RemoveUField_Next)
        {
            Next = new FPackageIndex(reader.ReadInt32());
        }
    }

    public virtual void Write(AssetBinaryWriter writer)
    {
        if (writer.Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.RemoveUField_Next)
        {
            writer.Write(Next.Index);
        }
    }

    public UField() { }
}

/// <summary>
/// An UnrealScript variable.
/// </summary>
public abstract class UProperty : UField
{
    [DisplayIndexOrder(1)]
    public EArrayDim ArrayDim;
    [DisplayIndexOrder(2)]
    public int ElementSize;
    [DisplayIndexOrder(3)]
    public EPropertyFlags PropertyFlags;
    [DisplayIndexOrder(4)]
    public FName RepNotifyFunc;
    [DisplayIndexOrder(5)]
    public ELifetimeCondition BlueprintReplicationCondition;

    public object RawValue;

    public void SetObject(object value)
    {
        RawValue = value;
    }

    public T GetObject<T>()
    {
        return (T)RawValue;
    }

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        ArrayDim = (EArrayDim)reader.ReadInt32();
        PropertyFlags = (EPropertyFlags)reader.ReadUInt64();
        RepNotifyFunc = reader.ReadFName();

        if (reader.Asset.GetCustomVersion<FReleaseObjectVersion>() >= FReleaseObjectVersion.PropertiesSerializeRepCondition)
        {
            BlueprintReplicationCondition = (ELifetimeCondition)reader.ReadByte();
        }
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write((int)ArrayDim);
        writer.Write((ulong)PropertyFlags);
        writer.Write(RepNotifyFunc);

        if (writer.Asset.GetCustomVersion<FReleaseObjectVersion>() >= FReleaseObjectVersion.PropertiesSerializeRepCondition)
        {
            writer.Write((byte)BlueprintReplicationCondition);
        }
    }

    public UProperty() { }

    public EPropertyType GetUsmapPropertyType()
    {
        return this switch
        {
            UEnumProperty => EPropertyType.EnumProperty,
            UByteProperty => EPropertyType.ByteProperty,
            UBoolProperty => EPropertyType.BoolProperty,
            UInt8Property => EPropertyType.Int8Property,
            UInt16Property => EPropertyType.Int16Property,
            UIntProperty => EPropertyType.IntProperty,
            UInt64Property => EPropertyType.Int64Property,
            UUInt16Property => EPropertyType.UInt16Property,
            UUInt32Property => EPropertyType.UInt32Property,
            UUInt64Property => EPropertyType.UInt64Property,
            UFloatProperty => EPropertyType.FloatProperty,
            UDoubleProperty => EPropertyType.DoubleProperty,

            UAssetClassProperty => EPropertyType.SoftObjectProperty,
            USoftClassProperty => EPropertyType.SoftObjectProperty,
            UClassProperty => EPropertyType.ObjectProperty,
            UAssetObjectProperty => EPropertyType.AssetObjectProperty,
            UWeakObjectProperty => EPropertyType.WeakObjectProperty,
            ULazyObjectProperty => EPropertyType.LazyObjectProperty,
            USoftObjectProperty => EPropertyType.SoftObjectProperty,
            UObjectProperty => EPropertyType.ObjectProperty,

            UNameProperty => EPropertyType.NameProperty,
            UStrProperty => EPropertyType.StrProperty,
            UTextProperty => EPropertyType.TextProperty,

            UInterfaceProperty => EPropertyType.InterfaceProperty,

            UMulticastDelegateProperty => EPropertyType.MulticastDelegateProperty,
            UDelegateProperty => EPropertyType.DelegateProperty,

            UMapProperty => EPropertyType.MapProperty,
            USetProperty => EPropertyType.SetProperty,
            UArrayProperty => EPropertyType.ArrayProperty,
            UStructProperty => EPropertyType.StructProperty,

            _ => EPropertyType.Unknown,
        };
    }
}

public class UEnumProperty : UProperty
{
    ///<summary>A pointer to the UEnum represented by this property</summary>
    [DisplayIndexOrder(6)]
    public FPackageIndex Enum;
    ///<summary>The FNumericProperty which represents the underlying type of the enum</summary>
    [DisplayIndexOrder(7)]
    public FPackageIndex UnderlyingProp;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);

        Enum = new FPackageIndex(reader.ReadInt32());
        UnderlyingProp = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);

        writer.Write(Enum.Index);
        writer.Write(UnderlyingProp.Index);
    }
}

public class UArrayProperty : UProperty
{
    [DisplayIndexOrder(6)]
    public FPackageIndex Inner;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        Inner = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(Inner.Index);
    }
}

public class USetProperty : UProperty
{
    [DisplayIndexOrder(6)]
    public FPackageIndex ElementProp;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        ElementProp = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(ElementProp.Index);
    }
}

public class UObjectProperty : UProperty
{
    // UClass*
    [DisplayIndexOrder(6)]
    public FPackageIndex PropertyClass;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        PropertyClass = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(PropertyClass.Index);
    }
}

public class UWeakObjectProperty : UObjectProperty { }

public class USoftObjectProperty : UObjectProperty { }

public class ULazyObjectProperty : UObjectProperty { }

public class UAssetObjectProperty : UObjectProperty { }

public class UClassProperty : UObjectProperty
{
    // UClass*
    [DisplayIndexOrder(7)]
    public FPackageIndex MetaClass;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        MetaClass = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(MetaClass.Index);
    }
}

public class UAssetClassProperty : UObjectProperty
{
    // UClass*
    [DisplayIndexOrder(7)]
    public FPackageIndex MetaClass;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        MetaClass = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(MetaClass.Index);
    }
}

public class USoftClassProperty : UObjectProperty
{
    // UClass*
    [DisplayIndexOrder(7)]
    public FPackageIndex MetaClass;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        MetaClass = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(MetaClass.Index);
    }
}

public class UDelegateProperty : UProperty
{
    // UFunction*
    [DisplayIndexOrder(6)]
    public FPackageIndex SignatureFunction;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        SignatureFunction = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(SignatureFunction.Index);
    }
}

public class UMulticastDelegateProperty : UDelegateProperty { }

public class UMulticastInlineDelegateProperty : UMulticastDelegateProperty { }

public class UMulticastSparseDelegateProperty : UMulticastDelegateProperty { }

public class UInterfaceProperty : UProperty
{
    // UFunction*
    [DisplayIndexOrder(6)]
    public FPackageIndex InterfaceClass;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        InterfaceClass = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(InterfaceClass.Index);
    }
}

public class UMapProperty : UProperty
{
    [DisplayIndexOrder(6)]
    public FPackageIndex KeyProp;
    [DisplayIndexOrder(7)]
    public FPackageIndex ValueProp;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        KeyProp = new FPackageIndex(reader.ReadInt32());
        ValueProp = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(KeyProp.Index);
        writer.Write(ValueProp.Index);
    }
}

public class UBoolProperty : UProperty
{
    [DisplayIndexOrder(6)]
    public bool NativeBool;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);

        ElementSize = reader.ReadByte();
        NativeBool = reader.ReadBoolean();
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write((byte)ElementSize);
        writer.Write(NativeBool);
    }
}

public class UByteProperty : UProperty
{
    /// <summary>A pointer to the UEnum represented by this property</summary>
    [DisplayIndexOrder(6)]
    public FPackageIndex Enum;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        Enum = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(Enum.Index);
    }
}

public class UStructProperty : UProperty
{
    // UScriptStruct*
    [DisplayIndexOrder(6)]
    public FPackageIndex Struct;

    public override void Read(AssetBinaryReader reader)
    {
        base.Read(reader);
        Struct = new FPackageIndex(reader.ReadInt32());
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);
        writer.Write(Struct.Index);
    }
}

public class UNameProperty : UProperty { }

public class UStrProperty : UProperty { }

public class UTextProperty : UProperty { }

public class UNumericProperty : UProperty { }

public class UDoubleProperty : UNumericProperty { }

public class UFloatProperty : UNumericProperty { }

public class UIntProperty : UNumericProperty { }

public class UInt8Property : UNumericProperty { }

public class UInt16Property : UNumericProperty { }

public class UInt64Property : UNumericProperty { }

public class UUInt16Property : UNumericProperty { }

public class UUInt32Property : UNumericProperty { }

public class UUInt64Property : UNumericProperty { }

/// <summary>
/// This is a UAssetAPI-specific property that represents anything that we don't have special serialization for
/// </summary>
public class UGenericProperty : UProperty { }
