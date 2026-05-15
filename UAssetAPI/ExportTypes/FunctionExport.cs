using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.ExportTypes
{
    /// <summary>
    /// Export data for a blueprint function.
    /// </summary>
    public class FunctionExport : StructExport
    {
        public EFunctionFlags FunctionFlags;
        public FunctionExport(Export super) : base(super)
        {
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public FunctionExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public FunctionExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);
            FunctionFlags = (EFunctionFlags)reader.ReadUInt32();
            // TODO
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((uint)FunctionFlags);
            // TODO
        }
    }
}
