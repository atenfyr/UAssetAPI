using System;

namespace UAssetAPI.ExportTypes
{
    /// <summary>
    /// An export whose UObject class owns a native custom serialization format
    /// that UAssetAPI intentionally preserves as opaque bytes.
    /// </summary>
    public class CustomSerializedExport : RawExport
    {
        public override void Read(AssetBinaryReader reader, int nextStarting = 0)
        {
            Data = reader.ReadBytes(checked((int)SerialSize));
        }
    }
}
