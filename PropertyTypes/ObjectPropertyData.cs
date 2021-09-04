using System;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class ObjectPropertyData : PropertyData<Import>
    {
        public int LinkValue = 0;

        public ObjectPropertyData(string name, UAsset asset) : base(name, asset)
        {
            Type = "ObjectProperty";
        }

        public ObjectPropertyData()
        {
            Type = "ObjectProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            SetLinkValue(reader.ReadInt32());
        }

        public void SetLinkValue(int newLinkValue)
        {
            LinkValue = newLinkValue;
            if (LinkValue < 0 && UAPUtils.GetNormalIndex(LinkValue) >= 0)
            {
                Value = Asset.Imports[UAPUtils.GetNormalIndex(LinkValue)]; // link reference
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
            return Value.Property;
        }

        public override void FromString(string[] d)
        {
            if (int.TryParse(d[0], out int res))
            {
                LinkValue = res;
                return;
            }

            for (int i = 0; i < Asset.Imports.Count; i++)
            {
                if (Asset.Imports[i].Property.Equals(d[0]))
                {
                    Value = Asset.Imports[i];
                    return;
                }
            }
        }
    }
}