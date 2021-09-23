using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class ObjectPropertyData : PropertyData<Import>
    {
        public int CurrentIndex = 0;

        public ObjectPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public ObjectPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("ObjectProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

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
            int normalIndex = UAPUtils.GetNormalIndex(CurrentIndex);
            if (CurrentIndex < 0 && normalIndex >= 0 && normalIndex < Asset.Imports.Count)
            {
                Value = Asset.Imports[normalIndex];
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
            return sizeof(int);
        }

        public override string ToString()
        {
            if (CurrentIndex > 0) return Convert.ToString(CurrentIndex);
            if (Value == null) return "null";
            return Value.ObjectName.ToString();
        }

        public override void FromString(string[] d)
        {
            if (int.TryParse(d[0], out int res))
            {
                SetCurrentIndex(res);
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