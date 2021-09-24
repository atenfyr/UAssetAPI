using System;
using System.IO;
using UAssetAPI.FieldTypes;

namespace UAssetAPI
{
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
        public FProperty[] LoadedProperties;

        /**
         * Number of bytecode instructions
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
                    MainSerializer.WriteFProperty(LoadedProperties[i], Asset, writer);
                }
            }

            writer.Write(ScriptBytecodeSize);
            writer.Write(ScriptBytecode.Length);
            writer.Write(ScriptBytecode);
        }
    }
}
