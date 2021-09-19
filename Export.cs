using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI
{
    /**
     * UObject resource type for objects that are contained within this package and can
     * be referenced by other packages.
     */
    public class Export
    {
        public ExportDetails ReferenceData;
        public byte[] Extras;
        public UAsset Asset;

        public Export(ExportDetails reference, UAsset asset, byte[] extras)
        {
            ReferenceData = reference;
            Asset = asset;
            Extras = extras;
        }

        public Export()
        {
            ReferenceData = new ExportDetails();
        }

        public virtual void Read(BinaryReader reader, int nextStarting = 0)
        {

        }

        public virtual void Write(BinaryWriter writer)
        {

        }
    }

    public class NormalExport : Export
    {
        public IList<PropertyData> Data;

        public NormalExport(Export super)
        {
            ReferenceData = super.ReferenceData;
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public NormalExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public NormalExport(IList<PropertyData> data, ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {
            Data = data;
        }

        public override void Read(BinaryReader reader, int nextStarting = 0)
        {
            Data = new List<PropertyData>();
            PropertyData bit;
            while ((bit = MainSerializer.Read(Asset, reader, true)) != null)
            {
                Data.Add(bit);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            for (int j = 0; j < Data.Count; j++)
            {
                PropertyData current = Data[j];
                MainSerializer.Write(current, Asset, writer, true);
            }
            writer.WriteFName(new FName("None"), Asset);
        }
    }

    public class RawExport : Export
    {
        public byte[] Data;

        public RawExport(Export super)
        {
            ReferenceData = super.ReferenceData;
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public RawExport(byte[] data, ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {
            Data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Data);
        }
    }

    public class StringTable : List<FString>
    {
        public string Name;

        public StringTable(string name) : base()
        {
            Name = name;
        }
    }

    public class StringTableExport : NormalExport
    {
        public StringTable Data2;

        public StringTableExport(Export super) : base(super)
        {

        }

        public StringTableExport(StringTable data, ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {
            Data2 = data;
        }

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            reader.ReadInt32();

            Data2 = new StringTable(reader.ReadFString());

            int numEntries = reader.ReadInt32() * 2;
            for (int i = 0; i < numEntries; i++)
            {
                FString x = reader.ReadFStringWithEncoding();
                Data2.Add(x);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);

            writer.Write((int)0);

            writer.WriteFString(Data2.Name);

            writer.Write(Data2.Count / 2);
            int lenData = (Data2.Count / 2) * 2;
            for (int i = 0; i < lenData; i++)
            {
                writer.WriteFString(Data2[i]);
            }
        }
    }

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

    public struct FField
    {
        public FName Name;
        public FName Type;
        public EObjectFlags Flags;
        public byte[] Extras; // Uncertain meaning; please let me know if you have information about the rest of FField's serialization

        public FField(FName name, FName type, EObjectFlags flags, byte[] extras)
        {
            Name = name;
            Type = type;
            Flags = flags;
            Extras = extras;
        }
    }


    public class StructExport : NormalExport
    {
        /**
         * Struct this inherits from, may be null
         */
        public int SuperStruct;

        /**
         * List of child fields
         */
        public int[] Children;

        /**
         * Properties serialized with this struct definition
         */
        public FField[] LoadedProperties;

        /**
         * # of bytecode instructions
         */
        public int ScriptBytecodeSize;

        /**
         * Raw bytecode instructions
         */
        public byte[] ScriptBytecode;

        public StructExport(Export super) : base(super)
        {

        }

        public StructExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);
            reader.ReadInt32();

            SuperStruct = reader.ReadInt32();

            if (true || Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.RemoveUField_Next)
            {
                int numIndexEntries = reader.ReadInt32();
                Children = new int[numIndexEntries];
                for (int i = 0; i < numIndexEntries; i++)
                {
                    Children[i] = reader.ReadInt32();
                }
            }
            else
            {
                // https://github.com/EpicGames/UnrealEngine/blob/master/Engine/Source/Runtime/CoreUObject/Private/UObject/Class.cpp#L1832
                throw new NotImplementedException("StructExport children linked list is unimplemented; please let me know if you see this error message");
            }

            if (Asset.GetCustomVersion<FCoreObjectVersion>() >= FCoreObjectVersion.FProperties)
            {
                int numProps = reader.ReadInt32();
                LoadedProperties = new FField[numProps];
                for (int i = 0; i < numProps; i++)
                {
                    FName fieldType = reader.ReadFName(Asset);
                    FName fieldName = reader.ReadFName(Asset);
                    EObjectFlags flags = (EObjectFlags)reader.ReadUInt32();
                    byte[] strangeExtras = reader.ReadBytes(31);
                    LoadedProperties[i] = new FField(fieldName, fieldType, flags, strangeExtras);
                }
            }
            else
            {
                LoadedProperties = new FField[0];
            }

            ScriptBytecodeSize = reader.ReadInt32(); // # of bytecode instructions
            int ScriptStorageSize = reader.ReadInt32(); // # of bytes in total
            ScriptBytecode = reader.ReadBytes(ScriptStorageSize);
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((int)0);

            writer.Write(SuperStruct);

            if (true || Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.RemoveUField_Next)
            {
                writer.Write(Children.Length);
                for (int i = 0; i < Children.Length; i++)
                {
                    writer.Write(Children[i]);
                }
            }
            else
            {
                // https://github.com/EpicGames/UnrealEngine/blob/master/Engine/Source/Runtime/CoreUObject/Private/UObject/Class.cpp#L1832
                throw new NotImplementedException("StructExport children linked list is unimplemented; please let me know if you see this error message");
            }

            if (Asset.GetCustomVersion<FCoreObjectVersion>() >= FCoreObjectVersion.FProperties)
            {
                writer.Write(LoadedProperties.Length);
                for (int i = 0; i < LoadedProperties.Length; i++)
                {
                    writer.WriteFName(LoadedProperties[i].Type, Asset);
                    writer.WriteFName(LoadedProperties[i].Name, Asset);
                    writer.Write((uint)LoadedProperties[i].Flags);
                    writer.Write(LoadedProperties[i].Extras);
                }
            }

            writer.Write(ScriptBytecodeSize);
            writer.Write(ScriptBytecode.Length);
            writer.Write(ScriptBytecode);
        }
    }

    public class ClassExport : StructExport
    {
        /**
         * Map of all functions by name contained in this class
         */
        public FunctionDataEntry[] FuncMap; // TMap<FName, UFunction*>

        /**
         * Class flags; See EClassFlags for more information
         */
        public EClassFlags ClassFlags;

        /**
         * The required type for the outer of instances of this class
         */
        public int ClassWithin;

        /**
         * Which Name.ini file to load Config variables out of
         */
        public FName ClassConfigName;

        /**
	     * The list of interfaces which this class implements, along with the pointer property that is located at the offset of the interface's vtable.
	     * If the interface class isn't native, the property will be null.
	     */
        public SerializedInterfaceReference[] Interfaces;

        /**
         * This is the blueprint that caused the generation of this class, or null if it is a native compiled-in class
         */
        public int ClassGeneratedBy;

        /**
         * Does this class use deprecated script order?
         */
        public bool bDeprecatedForceScriptOrder;

        /**
         * Used to check if the class was cooked or not
         */
        public bool bCooked;

        /**
         * The class default object; used for delta serialization and object initialization
         */
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

    public struct DataTableEntry
    {
        public StructPropertyData Data;
        public int DuplicateIndex;

        public DataTableEntry(StructPropertyData data, int duplicateIndex)
        {
            Data = data;
            DuplicateIndex = duplicateIndex;
        }
    }


    public class DataTable
    {
        public List<DataTableEntry> Table;

        public DataTable()
        {
            Table = new List<DataTableEntry>();
        }

        public DataTable(List<DataTableEntry> data)
        {
            Table = data;
        }
    }

    public class DataTableExport : NormalExport
    {
        public DataTable Data2;

        public DataTableExport(Export super) : base(super)
        {

        }

        public DataTableExport(DataTable data, ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {
            Data2 = data;
        }

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            // Find an ObjectProperty named RowStruct
            FName decidedStructType = new FName("Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = thisObjData.Value.ObjectName;
                    break;
                }
            }

            reader.ReadInt32();

            Data2 = new DataTable();

            int numEntries = reader.ReadInt32();
            for (int i = 0; i < numEntries; i++)
            {
                FString rowName = Asset.GetNameReference(reader.ReadInt32());
                int duplicateIndex = reader.ReadInt32();
                var nextStruct = new StructPropertyData(new FName(rowName), Asset)
                {
                    StructType = decidedStructType
                };
                nextStruct.Read(reader, false, 0);
                Data2.Table.Add(new DataTableEntry(nextStruct, duplicateIndex));
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);

            // Find an ObjectProperty named RowStruct
            FName decidedStructType = new FName("Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = thisObjData.Value.ObjectName;
                    break;
                }
            }

            writer.Write((int)0);

            writer.Write(Data2.Table.Count);
            for (int i = 0; i < Data2.Table.Count; i++)
            {
                var thisDataTableEntry = Data2.Table[i];
                thisDataTableEntry.Data.StructType = decidedStructType;
                writer.Write((int)Asset.SearchNameReference(thisDataTableEntry.Data.Name.Value));
                writer.Write(thisDataTableEntry.DuplicateIndex);
                thisDataTableEntry.Data.Write(writer, false);
            }
        }
    }

    public class NamespacedString
    {
        public string Namespace;
        public string Value;

        public NamespacedString(string Namespace, string Value)
        {
            this.Namespace = Namespace;
            this.Value = Value;
        }

        public NamespacedString()
        {

        }
    }

    public class LevelExport : NormalExport
    {
        public List<int> IndexData;
        public NamespacedString LevelType;
        public ulong FlagsProbably;
        public List<int> MiscCategoryData;

        public LevelExport(Export super) : base(super)
        {

        }

        public LevelExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            reader.ReadInt32();
            int numIndexEntries = reader.ReadInt32();

            IndexData = new List<int>();
            for (int i = 0; i < numIndexEntries; i++)
            {
                IndexData.Add(reader.ReadInt32());
            }

            var nms = reader.ReadFString();
            reader.ReadInt32(); // null
            var val = reader.ReadFString();
            LevelType = new NamespacedString(nms, val);

            reader.ReadInt64(); // null
            FlagsProbably = reader.ReadUInt64();

            MiscCategoryData = new List<int>();
            while (reader.BaseStream.Position < nextStarting - 1)
            {
                MiscCategoryData.Add(reader.ReadInt32());
            }

            reader.ReadByte();
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);

            writer.Write((int)0);
            writer.Write(IndexData.Count);
            for (int i = 0; i < IndexData.Count; i++)
            {
                writer.Write(IndexData[i]);
            }

            writer.WriteFString(LevelType.Namespace);
            writer.Write((int)0);
            writer.WriteFString(LevelType.Value);

            writer.Write((long)0);
            writer.Write(FlagsProbably);

            for (int i = 0; i < MiscCategoryData.Count; i++)
            {
                writer.Write(MiscCategoryData[i]);
            }

            writer.Write((byte)0);
        }
    }
}
