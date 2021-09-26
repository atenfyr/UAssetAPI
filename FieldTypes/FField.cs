using System.IO;

namespace UAssetAPI.FieldTypes
{
    /// <summary>Secondary condition to check before considering the replication of a lifetime property.</summary>
    public enum ELifetimeCondition : byte
    {
        /// <summary>This property has no condition, and will send anytime it changes</summary>
        COND_None = 0,
        /// <summary>This property will only attempt to send on the initial bunch</summary>
        COND_InitialOnly = 1,
        /// <summary>This property will only send to the actor's owner</summary>
        COND_OwnerOnly = 2,
        /// <summary>This property send to every connection EXCEPT the owner</summary>
        COND_SkipOwner = 3,
        /// <summary>This property will only send to simulated actors</summary>
        COND_SimulatedOnly = 4,
        /// <summary>This property will only send to autonomous actors</summary>
        COND_AutonomousOnly = 5,
        /// <summary>This property will send to simulated OR bRepPhysics actors</summary>
        COND_SimulatedOrPhysics = 6,
        /// <summary>This property will send on the initial packet, or to the actors owner</summary>
        COND_InitialOrOwner = 7,
        /// <summary>This property has no particular condition, but wants the ability to toggle on/off via SetCustomIsActiveOverride</summary>
        COND_Custom = 8,
        /// <summary>This property will only send to the replay connection, or to the actors owner</summary>
        COND_ReplayOrOwner = 9,
        /// <summary>This property will only send to the replay connection</summary>
        COND_ReplayOnly = 10,
        /// <summary>This property will send to actors only, but not to replay connections</summary>
        COND_SimulatedOnlyNoReplay = 11,
        /// <summary>This property will send to simulated Or bRepPhysics actors, but not to replay connections</summary>
        COND_SimulatedOrPhysicsNoReplay = 12,
        /// <summary>This property will not send to the replay connection</summary>
        COND_SkipReplay = 13,
        /// <summary>This property will never be replicated</summary>
        COND_Never = 15,
        COND_Max = 16
    };

    public class FField
    {
        public FName SerializedType;
        public FName Name;
        public EObjectFlags Flags;

        public virtual void Read(BinaryReader reader, UAsset asset)
        {
            Name = reader.ReadFName(asset);
            Flags = (EObjectFlags)reader.ReadUInt32();
        }

        public virtual void Write(BinaryWriter writer, UAsset asset)
        {
            writer.WriteFName(Name, asset);
            writer.Write((uint)Flags);
        }

        public FField()
        {

        }
    }

    public abstract class FProperty : FField
    {
        public int ArrayDim;
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

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            ArrayDim = reader.ReadInt32();
            ElementSize = reader.ReadInt32();
            PropertyFlags = (EPropertyFlags)reader.ReadUInt64();
            RepIndex = reader.ReadUInt16();
            RepNotifyFunc = reader.ReadFName(asset);
            BlueprintReplicationCondition = (ELifetimeCondition)reader.ReadByte();
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            writer.Write(ArrayDim);
            writer.Write(ElementSize);
            writer.Write((ulong)PropertyFlags);
            writer.Write(RepIndex);
            writer.WriteFName(RepNotifyFunc, asset);
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

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);

            Enum = new FPackageIndex(reader.ReadInt32());
            UnderlyingProp = MainSerializer.ReadFProperty(asset, reader);
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);

            writer.Write(Enum.Index);
            MainSerializer.WriteFProperty(UnderlyingProp, asset, writer);
        }
    }

    public class FArrayProperty : FProperty
    {
        public FProperty Inner;

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            Inner = MainSerializer.ReadFProperty(asset, reader);
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            MainSerializer.WriteFProperty(Inner, asset, writer);
        }
    }

    public class FSetProperty : FProperty
    {
        public FProperty ElementProp;
        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            ElementProp = MainSerializer.ReadFProperty(asset, reader);
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            MainSerializer.WriteFProperty(ElementProp, asset, writer);
        }
    }

    public class FObjectProperty : FProperty
    {
        // UClass*
        public FPackageIndex PropertyClass;

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            PropertyClass = new FPackageIndex(reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            writer.Write(PropertyClass.Index);
        }
    }

    public class FSoftObjectProperty : FObjectProperty
    {

    }

    public class FClassProperty : FObjectProperty
    {
        // UClass*
        public FPackageIndex MetaClass;

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            MetaClass = new FPackageIndex(reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            writer.Write(MetaClass.Index);
        }
    }

    public class FSoftClassProperty : FSoftObjectProperty
    {
        // UClass*
        public FPackageIndex MetaClass;

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            MetaClass = new FPackageIndex(reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            writer.Write(MetaClass.Index);
        }
    }

    public class FDelegateProperty : FProperty
    {
        // UFunction*
        public FPackageIndex SignatureFunction;

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            SignatureFunction = new FPackageIndex(reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
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

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            InterfaceClass = new FPackageIndex(reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            writer.Write(InterfaceClass.Index);
        }
    }

    public class FMapProperty : FProperty
    {
        public FProperty KeyProp;
        public FProperty ValueProp;

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            KeyProp = MainSerializer.ReadFProperty(asset, reader);
            ValueProp = MainSerializer.ReadFProperty(asset, reader);
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            MainSerializer.WriteFProperty(KeyProp, asset, writer);
            MainSerializer.WriteFProperty(ValueProp, asset, writer);
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

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);

            byte BoolSize = (byte)ElementSize;
            FieldSize = reader.ReadByte();
            ByteOffset = reader.ReadByte();
            ByteMask = reader.ReadByte();
            FieldMask = reader.ReadByte();
            NativeBool = reader.ReadBoolean();
            Value = reader.ReadBoolean();
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
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

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            Enum = new FPackageIndex(reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            writer.Write(Enum.Index);
        }
    }

    public class FStructProperty : FProperty
    {
        // UScriptStruct*
        public FPackageIndex Struct;

        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
            Struct = new FPackageIndex(reader.ReadInt32());
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
            writer.Write(Struct.Index);
        }
    }

    public class FNumericProperty : FProperty
    {
        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
        }
    }

    /// <summary>
    /// This is a UAssetAPI-specific property that represents anything that we don't have special serialization for
    /// </summary>
    public class FGenericProperty : FProperty
    {
        public override void Read(BinaryReader reader, UAsset asset)
        {
            base.Read(reader, asset);
        }

        public override void Write(BinaryWriter writer, UAsset asset)
        {
            base.Write(writer, asset);
        }
    }
}
