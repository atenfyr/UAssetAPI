using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI
{
    /// <summary>
    /// A regular export, with no special serialization. Serialized as a None-terminated property list.
    /// </summary>
    public class NormalExport : Export
    {
        public List<PropertyData> Data;

        public NormalExport(Export super)
        {
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public NormalExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public NormalExport(List<PropertyData> data, UAsset asset, byte[] extras) : base(asset, extras)
        {
            Data = data;
        }

        public NormalExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting = 0)
        {
            Data = new List<PropertyData>();
            PropertyData bit;
            while ((bit = MainSerializer.Read(Asset, reader, true)) != null)
            {
                Data.Add(bit);
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            for (int j = 0; j < Data.Count; j++)
            {
                PropertyData current = Data[j];
                MainSerializer.Write(current, Asset, writer, true);
            }
            writer.Write(new FName("None"));
        }
    }
}
