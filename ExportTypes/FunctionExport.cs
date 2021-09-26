using System.IO;

namespace UAssetAPI
{
    public class FunctionExport : StructExport
    {
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
