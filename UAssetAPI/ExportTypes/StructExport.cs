using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.CustomVersions;
using UAssetAPI.FieldTypes;
using UAssetAPI.Kismet.Bytecode;
using UAssetAPI.UnrealTypes;

#if DEBUGVERBOSE
using System.Diagnostics;
#endif

namespace UAssetAPI.ExportTypes
{
    /// <summary>
    /// Base export for all UObject types that contain fields.
    /// </summary>
    public class StructExport : FieldExport
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
        /// The bytecode instructions contained within this struct.
        /// </summary>
        public KismetExpression[] ScriptBytecode;

        /// <summary>
        /// Bytecode size in total in deserialized memory. Filled out in lieu of <see cref="ScriptBytecode"/> if an error occurs during bytecode parsing.
        /// </summary>
        public int ScriptBytecodeSize;

        /// <summary>
        /// Raw binary bytecode data. Filled out in lieu of <see cref="ScriptBytecode"/> if an error occurs during bytecode parsing.
        /// </summary>
        public byte[] ScriptBytecodeRaw;

        public StructExport(Export super) : base(super)
        {

        }

        public StructExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public StructExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            SuperStruct = new FPackageIndex(reader.ReadInt32());

            if (Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.RemoveUField_Next)
            {
                var firstChild = new FPackageIndex(reader.ReadInt32());
                Children = firstChild.IsNull() ? Array.Empty<FPackageIndex>() : new[] { firstChild };
            }
            else
            {
                int numIndexEntries = reader.ReadInt32();
                Children = new FPackageIndex[numIndexEntries];
                for (int i = 0; i < numIndexEntries; i++)
                {
                    Children[i] = new FPackageIndex(reader.ReadInt32());
                }
            }

            if (Asset.GetCustomVersion<FCoreObjectVersion>() >= FCoreObjectVersion.FProperties)
            {
                int numProps = reader.ReadInt32();
                LoadedProperties = new FProperty[numProps];
                for (int i = 0; i < numProps; i++)
                {
                    LoadedProperties[i] = MainSerializer.ReadFProperty(reader);
                }
            }
            else
            {
                LoadedProperties = [];
            }

            ScriptBytecodeSize = reader.ReadInt32(); // # of bytes in total in deserialized memory
            int scriptStorageSize = reader.ReadInt32(); // # of bytes in total
            long startedReading = reader.BaseStream.Position;

            bool willParseRaw = true;
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                if (!Asset.CustomSerializationFlags.HasFlag(CustomSerializationFlags.SkipParsingBytecode))
                {
                    var tempCode = new List<Kismet.Bytecode.KismetExpression>();
                    while ((reader.BaseStream.Position - startedReading) < scriptStorageSize)
                    {
                        tempCode.Add(ExpressionSerializer.ReadExpression(reader));
                    }
                    ScriptBytecode = tempCode.ToArray();
                    willParseRaw = false;
                }
            }
            catch (Exception ex)
            {
#if DEBUGVERBOSE
                Debug.WriteLine(ex.StackTrace);
#endif
            }
#pragma warning restore CS0168 // Variable is declared but never used

            if (willParseRaw)
            {
                reader.BaseStream.Seek(startedReading, SeekOrigin.Begin);
                ScriptBytecode = null;
                ScriptBytecodeRaw = reader.ReadBytes(scriptStorageSize);
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);

            writer.Write(SuperStruct.Index);

            if (Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.RemoveUField_Next)
            {
                if (Children.Length == 0) 
                {
                    writer.Write(0);
                }
                else
                {
                    writer.Write(Children[0].Index);
                }
            }
            else
            {
                writer.Write(Children.Length);
                for (int i = 0; i < Children.Length; i++)
                {
                    writer.Write(Children[i].Index);
                }
            }

            if (Asset.GetCustomVersion<FCoreObjectVersion>() >= FCoreObjectVersion.FProperties)
            {
                writer.Write(LoadedProperties.Length);
                for (int i = 0; i < LoadedProperties.Length; i++)
                {
                    MainSerializer.WriteFProperty(LoadedProperties[i], writer);
                }
            }

            if (ScriptBytecode == null)
            {
                writer.Write(ScriptBytecodeSize);
                writer.Write(ScriptBytecodeRaw.Length);
                writer.Write(ScriptBytecodeRaw);
            }
            else
            {
                long lengthOffset1 = writer.BaseStream.Position;
                writer.Write((int)0); // total iCode offset; to be filled out after serialization
                long lengthOffset2 = writer.BaseStream.Position;
                writer.Write((int)0); // size on disk; to be filled out after serialization

                int totalICodeOffset = 0;
                long startMetric = writer.BaseStream.Position;
                for (int i = 0; i < ScriptBytecode.Length; i++)
                {
                    totalICodeOffset += ExpressionSerializer.WriteExpression(ScriptBytecode[i], writer);
                }
                long endMetric = writer.BaseStream.Position;

                // Write out total size in bytes
                long totalLength = endMetric - startMetric;
                long here = writer.BaseStream.Position;
                writer.Seek((int)lengthOffset1, SeekOrigin.Begin);
                writer.Write(totalICodeOffset);
                writer.Seek((int)lengthOffset2, SeekOrigin.Begin);
                writer.Write((int)totalLength);
                writer.Seek((int)here, SeekOrigin.Begin);
            }
        }
    }
}
