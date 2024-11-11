using System.Collections.Generic;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;
using System;
using UAssetAPI.CustomVersions;
using System.Reflection.PortableExecutable;

namespace UAssetAPI.ExportTypes
{
    public class MetaDataExport : NormalExport
    {
        public List<(int, Dictionary<FName, FString>)> ObjectMetaData;
        public Dictionary<FName, FString> RootMetaData;

        public MetaDataExport(Export super) : base(super)
        {

        }

        public MetaDataExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public MetaDataExport()
        {

        }
        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            {
                ObjectMetaData = [];
                var objectMetaDataMapCount = reader.ReadInt32();
                for (var i = 0; i < objectMetaDataMapCount; i++)
                {
                    var import = reader.ReadInt32();
                    var metaDataCount = reader.ReadInt32();
                    var metaData = new Dictionary<FName, FString>();
                    for (var j = 0; j < metaDataCount; j++)
                    {
                        var key = reader.ReadFName();
                        var value = reader.ReadFString();
                        metaData.Add(key, value);
                    }
                    ObjectMetaData.Add((import, metaData));
                }
            }

            if (reader.Asset.GetCustomVersion<FEditorObjectVersion>() >= FEditorObjectVersion.RootMetaDataSupport)
            {
                RootMetaData = [];
                var rootMetaDataMapCount = reader.ReadInt32();
                for (var i = 0; i < rootMetaDataMapCount; i++)
                {
                    var key = reader.ReadFName();
                    var value = reader.ReadFString();
                    RootMetaData.Add(key, value);
                }
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);

            writer.Write(ObjectMetaData.Count);
            foreach ((var import, var metaData) in ObjectMetaData)
            {
                writer.Write(import);
                writer.Write(metaData.Count);
                foreach ((var key, var value) in metaData)
                {
                    writer.Write(key);
                    writer.Write(value);
                }
            }

            if (writer.Asset.GetCustomVersion<FEditorObjectVersion>() >= FEditorObjectVersion.RootMetaDataSupport)
            {
                writer.Write(RootMetaData.Count);
                foreach ((var key, var value) in RootMetaData)
                {
                    writer.Write(key);
                    writer.Write(value);
                }
            }
        }
    }
}
