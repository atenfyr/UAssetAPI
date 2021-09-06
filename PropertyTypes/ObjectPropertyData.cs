using System;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class ObjectPropertyData : PropertyData<Import>
    {
        public int CurrentIndex = 0;

        public ObjectPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("ObjectProperty");
        }

        public ObjectPropertyData()
        {
            Type = new FName("ObjectProperty");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            SetCurrentIndex(reader.ReadInt32());
        }

        public void SetCurrentIndex(int newIndex)
        {
            CurrentIndex = newIndex;
            if (CurrentIndex < 0 && UAPUtils.GetNormalIndex(CurrentIndex) >= 0)
            {
                Value = Asset.Imports[UAPUtils.GetNormalIndex(CurrentIndex)]; // link reference
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

            if (Value != null) CurrentIndex = Value.Index;
            writer.Write(CurrentIndex);
            return 4;
        }

        public override string ToString()
        {
            if (CurrentIndex > 0) return Convert.ToString(CurrentIndex);
            if (Value == null) return "null";
            return Value.ObjectName.Value.Value;
        }

        public override void FromString(string[] d)
        {
            if (int.TryParse(d[0], out int res))
            {
                CurrentIndex = res;
                return;
            }

            for (int i = 0; i < Asset.Imports.Count; i++)
            {
                if (Asset.Imports[i].ObjectName.Equals(d[0]))
                {
                    Value = Asset.Imports[i];
                    return;
                }
            }
        }
    }
}