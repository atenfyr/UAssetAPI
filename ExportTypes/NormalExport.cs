using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI
{
    /// <summary>
    /// A regular export, with no export-specific custom serialization. Serialized as a None-terminated property list.
    /// </summary>
    public class NormalExport : Export
    {
        public IList<PropertyData> Data;

        public NormalExport(Export super)
        {
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public NormalExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public NormalExport(IList<PropertyData> data, UAsset asset, byte[] extras) : base(asset, extras)
        {
            Data = data;
        }

        public NormalExport()
        {

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
}
