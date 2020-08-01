using System;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class ObjectPropertyData : PropertyData<Link>
    {
        public int LinkValue = 0;

        public ObjectPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "ObjectProperty";
        }

        public ObjectPropertyData()
        {
            Type = "ObjectProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            LinkValue = reader.ReadInt32();
            if (LinkValue < 0 && Utils.GetNormalIndex(LinkValue) >= 0)
            {
                Value = Asset.links[Utils.GetNormalIndex(LinkValue)]; // link reference
            }
            else
            {
                Value = null;
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            if (Value != null) LinkValue = Value.Index;
            writer.Write(LinkValue);
            return 4;
        }

        public override string ToString()
        {
            if (LinkValue > 0) return Convert.ToString(LinkValue);
            if (Value == null) return "null";
            return Asset.GetHeaderReference((int)Value.Property);
        }

        public override void FromString(string[] d)
        {
            if (int.TryParse(d[0], out int res))
            {
                LinkValue = res;
                return;
            }

            for (int i = 0; i < Asset.links.Count; i++)
            {
                if (Asset.GetHeaderReference((int)Asset.links[i].Property).Equals(d[0]))
                {
                    Value = Asset.links[i];
                    return;
                }
            }
        }
    }
}