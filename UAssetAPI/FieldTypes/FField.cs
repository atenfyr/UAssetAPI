using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.FieldTypes
{
    /// <summary>
    /// Base class of reflection data objects.
    /// </summary>
    public class FField
    {
        public FName SerializedType;
        public FName Name;
        public EObjectFlags Flags;
        public TMap<FName, FString> MetaDataMap;

        public virtual void Read(AssetBinaryReader reader)
        {
            Name = reader.ReadFName();
            Flags = (EObjectFlags)reader.ReadUInt32();

            if (!reader.Asset.IsFilterEditorOnly && !reader.Asset.PackageFlags.HasFlag(EPackageFlags.PKG_Cooked))
            {
                bool bHasMetaData = reader.ReadBooleanInt();
                if (bHasMetaData)
                {
                    MetaDataMap = new TMap<FName, FString>();
                    int leng = reader.ReadInt32();
                    for (int i = 0; i < leng; i++)
                    {
                        FName key = reader.ReadFName();
                        FString val = reader.ReadFString();
                        MetaDataMap.Add(key, val);
                    }
                }
            }
        }

        public virtual void Write(AssetBinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write((uint)Flags);

            if (!writer.Asset.IsFilterEditorOnly && !writer.Asset.PackageFlags.HasFlag(EPackageFlags.PKG_Cooked))
            {
                writer.Write(MetaDataMap is not null ? 1 : 0); // int32
                if (MetaDataMap is not null && MetaDataMap.Count > 0)
                {
                    writer.Write(MetaDataMap.Count); // int32
                    for (int i = 0; i < MetaDataMap.Count; i++)
                    {
                        var pair = MetaDataMap.GetItem(i);
                        writer.Write(pair.Key);
                        writer.Write(pair.Value);
                    }
                }

            }
        }

        public FField()
        {

        }
    }

    /// <summary>
    /// An UnrealScript variable.
    /// </summary>
    public abstract class FProperty : FField
    {
        public EArrayDim ArrayDim;
        public int ElementSize;
        public EPropertyFlags PropertyFlags;
        public ushort RepIndex;
        public FName RepNotifyFunc;
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

        [JsonIgnore]
        public IDictionary<string, EPropertyType> UsmapPropertyTypeOverrides = new Dictionary<string, EPropertyType>()
        {
            { "MulticastInlineDelegateProperty", EPropertyType.MulticastDelegateProperty },
            { "ClassProperty", EPropertyType.ObjectProperty },
            { "SoftClassProperty", EPropertyType.SoftObjectProperty }
        };

        public EPropertyType GetUsmapPropertyType()
        {
            EPropertyType res = EPropertyType.Unknown;
            if (UsmapPropertyTypeOverrides.TryGetValue(SerializedType.Value.Value, out res)) return res;
            if (Enum.TryParse(SerializedType.Value.Value, out res)) return res;
            return res;
        }

        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);
            ArrayDim = (EArrayDim)reader.ReadInt32();
            ElementSize = reader.ReadInt32();
            PropertyFlags = (EPropertyFlags)reader.ReadUInt64();
            RepIndex = reader.ReadUInt16();
            RepNotifyFunc = reader.ReadFName();
            BlueprintReplicationCondition = (ELifetimeCondition)reader.ReadByte();
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((int)ArrayDim);
            writer.Write(ElementSize);
            writer.Write((ulong)PropertyFlags);
            writer.Write(RepIndex);
            writer.Write(RepNotifyFunc);
            writer.Write((byte)BlueprintReplicationCondition);
        }

        public FProperty()
        {

        }
    }

    public class FEnumProperty : FProperty
    {
        ///<summary>A pointer to the UEnum represented by this property</summary>
        public FPackageIndex Enum;
        ///<summary>The FNumericProperty which represents the underlying type of the enum</summary>
        public FProperty UnderlyingProp;

        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);

            Enum = new FPackageIndex(reader.ReadInt32());
            UnderlyingProp = MainSerializer.ReadFProperty(reader);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);

            writer.Write(Enum.Index);
            MainSerializer.WriteFProperty(UnderlyingProp, writer);
        }
    }

    public class FArrayProperty : FProperty
    {
        public FProperty Inner;

        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);
            Inner = MainSerializer.ReadFProperty(reader);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            MainSerializer.WriteFProperty(Inner, writer);
        }
    }

    public class FSetProperty : FProperty
    {
        public FProperty ElementProp;

        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);
            ElementProp = MainSerializer.ReadFProperty(reader);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            MainSerializer.WriteFProperty(ElementProp, writer);
        }
    }

    public class FObjectProperty : FProperty
    {
        // UClass*
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

    public class FSoftObjectProperty : FObjectProperty
    {

    }

    public class FWeakObjectProperty : FObjectProperty
    {

    }

    public class FClassProperty : FObjectProperty
    {
        // UClass*
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

    public class FSoftClassProperty : FObjectProperty
    {
        // UClass*
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

    public class FDelegateProperty : FProperty
    {
        // UFunction*
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

    public class FMulticastDelegateProperty : FDelegateProperty
    {

    }

    public class FMulticastInlineDelegateProperty : FMulticastDelegateProperty
    {

    }

    public class FInterfaceProperty : FProperty
    {
        // UFunction*
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

    public class FMapProperty : FProperty
    {
        public FProperty KeyProp;
        public FProperty ValueProp;

        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);
            KeyProp = MainSerializer.ReadFProperty(reader);
            ValueProp = MainSerializer.ReadFProperty(reader);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            MainSerializer.WriteFProperty(KeyProp, writer);
            MainSerializer.WriteFProperty(ValueProp, writer);
        }
    }

    public class FBoolProperty : FProperty
    {
        /// <summary>Size of the bitfield/bool property. Equal to ElementSize but used to check if the property has been properly initialized (0-8, where 0 means uninitialized).</summary>
        public byte FieldSize;
        /// <summary>Offset from the memeber variable to the byte of the property (0-7).</summary>
        public byte ByteOffset;
        /// <summary>Mask of the byte with the property value.</summary>
        public byte ByteMask;
        /// <summary>Mask of the field with the property value. Either equal to ByteMask or 255 in case of 'bool' type.</summary>
        public byte FieldMask;

        public bool NativeBool;
        public bool Value;

        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);

            byte BoolSize = (byte)ElementSize;
            FieldSize = reader.ReadByte();
            ByteOffset = reader.ReadByte();
            ByteMask = reader.ReadByte();
            FieldMask = reader.ReadByte();
            NativeBool = reader.ReadBoolean();
            Value = reader.ReadBoolean();
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(FieldSize);
            writer.Write(ByteOffset);
            writer.Write(ByteMask);
            writer.Write(FieldMask);
            writer.Write(NativeBool);
            writer.Write(Value);
        }
    }

    public class FByteProperty : FProperty
    {
        /// <summary>A pointer to the UEnum represented by this property</summary>
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

    public class FStructProperty : FProperty
    {
        // UScriptStruct*
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

    public class FNumericProperty : FProperty
    {
        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
        }
    }

    /// <summary>
    /// This is a UAssetAPI-specific property that represents anything that we don't have special serialization for
    /// </summary>
    public class FGenericProperty : FProperty
    {
        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
        }
    }

    public class FOptionalProperty : FProperty
    {
        public FProperty ValueProperty;

        public override void Read(AssetBinaryReader reader)
        {
            base.Read(reader);
            ValueProperty = MainSerializer.ReadFProperty(reader);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            MainSerializer.WriteFProperty(ValueProperty, writer);
        }
    }
}
