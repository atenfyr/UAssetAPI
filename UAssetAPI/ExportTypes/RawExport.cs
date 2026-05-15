using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.ExportTypes
{
    /// <summary>
    /// An export that could not be properly parsed by UAssetAPI, and is instead represented as an array of bytes as a fallback.
    /// </summary>
    public class RawExport : Export
    {
        public byte[] Data;

        public RawExport(Export super)
        {
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public RawExport(byte[] data, UAsset asset, byte[] extras) : base(asset, extras)
        {
            Data = data;
        }

        public RawExport()
        {

        }

        public override void Write(AssetBinaryWriter writer)
        {
            writer.Write(Data);
        }
    }
}
