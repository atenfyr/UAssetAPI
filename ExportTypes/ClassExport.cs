using System.IO;

namespace UAssetAPI
{
    public class FunctionData
    {

    }

    public class FunctionCategory : Export
    {

    }

    // Used in ClassExport
    public struct FunctionDataEntry
    {
        public FName Name;
        public int Category;

        public FunctionDataEntry(FName name, int category)
        {
            Name = name;
            Category = category;
        }

        public override string ToString()
        {
            return "(" + Name.ToString() + ", " + Category + ")";
        }
    }

    public struct SerializedInterfaceReference
    {
        public int Class;
        public int PointerOffset;
        public bool bImplementedByK2;

        public SerializedInterfaceReference(int @class, int pointerOffset, bool bImplementedByK2)
        {
            Class = @class;
            PointerOffset = pointerOffset;
            this.bImplementedByK2 = bImplementedByK2;
        }
    }

    public class ClassExport : StructExport
    {
        /// <summary>
        /// Map of all functions by name contained in this class
        /// </summary>
        public FunctionDataEntry[] FuncMap; // TMap<FName, UFunction*>

        /// <summary>
        /// Class flags; See <see cref="EClassFlags"/> for more information
        /// </summary>
        public EClassFlags ClassFlags;

        /// <summary>
        /// The required type for the outer of instances of this class
        /// </summary>
        public int ClassWithin;

        /// <summary>
        /// Which Name.ini file to load Config variables out of
        /// </summary>
        public FName ClassConfigName;

        /// <summary>
        /// The list of interfaces which this class implements, along with the pointer property that is located at the offset of the interface's vtable.
        /// If the interface class isn't native, the property will be null.
        /// </summary>
        public SerializedInterfaceReference[] Interfaces;

        /// <summary>
        /// This is the blueprint that caused the generation of this class, or null if it is a native compiled-in class
        /// </summary>
        public int ClassGeneratedBy;

        /// <summary>
        /// Does this class use deprecated script order?
        /// </summary>
        public bool bDeprecatedForceScriptOrder;

        /// <summary>
        /// Used to check if the class was cooked or not
        /// </summary>
        public bool bCooked;

        /// <summary>
        /// The class default object; used for delta serialization and object initialization
        /// </summary>
        public int ClassDefaultObject;

        public ClassExport(Export super) : base(super)
        {

        }

        public ClassExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            int numFuncIndexEntries = reader.ReadInt32();
            FuncMap = new FunctionDataEntry[numFuncIndexEntries];
            for (int i = 0; i < numFuncIndexEntries; i++)
            {
                FName functionName = reader.ReadFName(Asset);
                int functionCategory = reader.ReadInt32();

                FuncMap[i] = new FunctionDataEntry(functionName, functionCategory);
            }

            ClassFlags = (EClassFlags)reader.ReadUInt32();

            if (Asset.EngineVersion < UE4Version.VER_UE4_CLASS_NOTPLACEABLE_ADDED)
            {
                ClassFlags ^= EClassFlags.CLASS_NotPlaceable;
            }

            ClassWithin = reader.ReadInt32();
            ClassConfigName = reader.ReadFName(Asset);
            Asset.AddNameReference(ClassConfigName.Value);

            int numInterfaces = 0;
            long interfacesStart = 0;
            if (Asset.EngineVersion < UE4Version.VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING)
            {
                interfacesStart = reader.BaseStream.Position;
                numInterfaces = reader.ReadInt32();
                reader.BaseStream.Seek(interfacesStart + sizeof(int) + numInterfaces * (sizeof(int) * 3), SeekOrigin.Begin);
            }

            // Linking procedure here; I don't think anything is really serialized during this
            ClassGeneratedBy = reader.ReadInt32();

            long currentOffset = reader.BaseStream.Position;
            if (Asset.EngineVersion < UE4Version.VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING)
            {
                reader.BaseStream.Seek(interfacesStart, SeekOrigin.Begin);
            }
            numInterfaces = reader.ReadInt32();
            Interfaces = new SerializedInterfaceReference[numInterfaces];
            for (int i = 0; i < numInterfaces; i++)
            {
                Interfaces[i] = new SerializedInterfaceReference(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32() == 1);
            }
            if (Asset.EngineVersion < UE4Version.VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING)
            {
                reader.BaseStream.Seek(currentOffset, SeekOrigin.Begin);
            }

            bDeprecatedForceScriptOrder = reader.ReadInt32() == 1;

            reader.ReadInt64(); // None

            if (Asset.EngineVersion >= UE4Version.VER_UE4_ADD_COOKED_TO_UCLASS)
            {
                bCooked = reader.ReadInt32() == 1;
            }

            ClassDefaultObject = reader.ReadInt32();

            // CDO serialization usually comes after this export has finished serializing
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);

            writer.Write(FuncMap.Length);
            for (int i = 0; i < FuncMap.Length; i++)
            {
                writer.WriteFName(FuncMap[i].Name, Asset);
                writer.Write((int)FuncMap[i].Category);
            }

            EClassFlags serializingClassFlags = ClassFlags;
            if (Asset.EngineVersion < UE4Version.VER_UE4_CLASS_NOTPLACEABLE_ADDED)
            {
                serializingClassFlags ^= EClassFlags.CLASS_NotPlaceable;
            }
            writer.Write((uint)serializingClassFlags);

            writer.Write(ClassWithin);
            writer.WriteFName(ClassConfigName, Asset);

            if (Asset.EngineVersion < UE4Version.VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING)
            {
                SerializeInterfaces(writer);
            }

            // Linking procedure here; I don't think anything is really serialized during this
            writer.Write(ClassGeneratedBy);

            if (Asset.EngineVersion >= UE4Version.VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING)
            {
                SerializeInterfaces(writer);
            }

            writer.Write(bDeprecatedForceScriptOrder ? 1 : 0);

            writer.WriteFName(new FName("None"), Asset);

            if (Asset.EngineVersion >= UE4Version.VER_UE4_ADD_COOKED_TO_UCLASS)
            {
                writer.Write(bCooked ? 1 : 0);
            }

            writer.Write(ClassDefaultObject);
        }

        private void SerializeInterfaces(BinaryWriter writer)
        {
            writer.Write(Interfaces.Length);
            for (int i = 0; i < Interfaces.Length; i++)
            {
                writer.Write(Interfaces[i].Class);
                writer.Write(Interfaces[i].PointerOffset);
                writer.Write(Interfaces[i].bImplementedByK2 ? 1 : 0);
            }
        }
    }
}
