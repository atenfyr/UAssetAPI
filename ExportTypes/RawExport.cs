using System.IO;

namespace UAssetAPI
{
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
}
