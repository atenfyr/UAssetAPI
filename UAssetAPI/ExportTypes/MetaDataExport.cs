using Newtonsoft.Json;
using System.Collections.Generic;
using UAssetAPI.CustomVersions;
using UAssetAPI.JSON;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes
{
    public struct ObjectMetaDataEntry
    {
        public int Import;
        [JsonConverter(typeof(TMapJsonConverter<FName, FString>))]
        public TMap<FName, FString> MetaData;
        
        public ObjectMetaDataEntry(int import, TMap<FName, FString> metaData)
        {
            Import = import;
            MetaData = metaData;
        }
    }

    public class MetaDataExport : NormalExport
    {
        public List<ObjectMetaDataEntry> ObjectMetaData;
        [JsonConverter(typeof(TMapJsonConverter<FName, FString>))]
        public TMap<FName, FString> RootMetaData;

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
                    var metaData = new TMap<FName, FString>();
                    for (var j = 0; j < metaDataCount; j++)
                    {
                        var key = reader.ReadFName();
                        var value = reader.ReadFString();
                        metaData.Add(key, value);
                    }
                    ObjectMetaData.Add(new ObjectMetaDataEntry(import, metaData));
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
            foreach (var entry in ObjectMetaData)
            {
                writer.Write(entry.Import);
                writer.Write(entry.MetaData.Count);
                foreach (var entry2 in entry.MetaData)
                {
                    writer.Write(entry2.Key);
                    writer.Write(entry2.Value);
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
