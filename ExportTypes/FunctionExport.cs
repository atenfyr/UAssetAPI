using System.IO;

namespace UAssetAPI
{
    public class FunctionExport : StructExport
    {
        public FunctionExport(Export super) : base(super)
        {
            ReferenceData = super.ReferenceData;
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public FunctionExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            // TODO
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            
            // TODO
        }
    }
}
