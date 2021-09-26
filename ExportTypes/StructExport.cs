using System;
using System.IO;
using UAssetAPI.FieldTypes;

namespace UAssetAPI
{
    public class StructExport : NormalExport
    {
        /// <summary>
        /// Struct this inherits from, may be null
        /// </summary>
        public FPackageIndex SuperStruct;

        /// <summary>
        /// List of child fields
        /// </summary>
        public FPackageIndex[] Children;

        /// <summary>
        /// Properties serialized with this struct definition
        /// </summary>
        public FProperty[] LoadedProperties;

        /// <summary>
        /// Number of bytecode instructions
        /// </summary>
        public int ScriptBytecodeSize;

        /// <summary>
        /// Raw bytecode instructions
        /// </summary>
        public byte[] ScriptBytecode;

        public StructExport(Export super) : base(super)
        {

        }

        public StructExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public StructExport()
        {

        }
        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);
            reader.ReadInt32();

            SuperStruct = new FPackageIndex(reader.ReadInt32());

            if (true || Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.RemoveUField_Next)
            {
                int numIndexEntries = reader.ReadInt32();
                Children = new FPackageIndex[numIndexEntries];
                for (int i = 0; i < numIndexEntries; i++)
                {
                    Children[i] = new FPackageIndex(reader.ReadInt32());
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
                LoadedProperties = new FProperty[numProps];
                for (int i = 0; i < numProps; i++)
                {
                    LoadedProperties[i] = MainSerializer.ReadFProperty(Asset, reader);
                }
            }
            else
            {
                LoadedProperties = new FProperty[0];
            }

            ScriptBytecodeSize = reader.ReadInt32(); // # of bytecode instructions
            int ScriptStorageSize = reader.ReadInt32(); // # of bytes in total
            ScriptBytecode = reader.ReadBytes(ScriptStorageSize);
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((int)0);

            writer.Write(SuperStruct.Index);

            if (true || Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.RemoveUField_Next)
            {
                writer.Write(Children.Length);
                for (int i = 0; i < Children.Length; i++)
                {
                    writer.Write(Children[i].Index);
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
                    MainSerializer.WriteFProperty(LoadedProperties[i], Asset, writer);
                }
            }

            writer.Write(ScriptBytecodeSize);
            writer.Write(ScriptBytecode.Length);
            writer.Write(ScriptBytecode);
        }
    }
}
