using System.IO;

namespace UAssetAPI
{
    /// <summary>
    /// UObject resource type for objects that are contained within this package and can be referenced by other packages.
    /// </summary>
    public class Export
    {
        public ExportDetails ReferenceData;
        public byte[] Extras;
        public UAsset Asset;

        public Export(ExportDetails reference, UAsset asset, byte[] extras)
        {
            ReferenceData = reference;
            Asset = asset;
            Extras = extras;
        }

        public Export()
        {
            ReferenceData = new ExportDetails();
        }

        public virtual void Read(BinaryReader reader, int nextStarting = 0)
        {

        }

        public virtual void Write(BinaryWriter writer)
        {

        }
    }
}
