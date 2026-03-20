using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes
{
    public class AssetImportDataExport : NormalExport
    {
        public FString Json;

        public AssetImportDataExport(Export super) : base(super)
        {

        }

        public AssetImportDataExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public AssetImportDataExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            Json = reader.ReadFString();

            base.Read(reader, nextStarting);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            writer.Write(Json);

            base.Write(writer);
        }
    }
}
