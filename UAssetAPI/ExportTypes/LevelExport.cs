using System.Collections.Generic;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.ExportTypes
{
    public class NamespacedString
    {
        public FString Namespace;
        public FString Value;

        public NamespacedString(FString Namespace, FString Value)
        {
            this.Namespace = Namespace;
            this.Value = Value;
        }

        public NamespacedString()
        {

        }
    }

    public class LevelExport : NormalExport
    {
        public List<int> IndexData;
        public NamespacedString LevelType;
        public ulong FlagsProbably;
        public List<int> MiscCategoryData;

        public LevelExport(Export super) : base(super)
        {

        }

        public LevelExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public LevelExport()
        {

        }
        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            reader.ReadInt32();
            int numIndexEntries = reader.ReadInt32();

            IndexData = new List<int>();
            for (int i = 0; i < numIndexEntries; i++)
            {
                IndexData.Add(reader.ReadInt32());
            }

            var nms = reader.ReadFString();
            reader.ReadInt32(); // null
            var val = reader.ReadFString();
            LevelType = new NamespacedString(nms, val);

            reader.ReadInt64(); // null
            FlagsProbably = reader.ReadUInt64();

            MiscCategoryData = new List<int>();
            while (reader.BaseStream.Position < nextStarting - 1)
            {
                MiscCategoryData.Add(reader.ReadInt32());
            }

            reader.ReadByte();
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);

            writer.Write((int)0);
            writer.Write(IndexData.Count);
            for (int i = 0; i < IndexData.Count; i++)
            {
                writer.Write(IndexData[i]);
            }

            writer.Write(LevelType.Namespace);
            writer.Write((int)0);
            writer.Write(LevelType.Value);

            writer.Write((long)0);
            writer.Write(FlagsProbably);

            for (int i = 0; i < MiscCategoryData.Count; i++)
            {
                writer.Write(MiscCategoryData[i]);
            }

            writer.Write((byte)0);
        }
    }
}
