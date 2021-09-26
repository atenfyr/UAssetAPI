using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class BoxPropertyData : PropertyData<VectorPropertyData[]> // Min, Max, IsValid
    {
        public bool IsValid;

        public BoxPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public BoxPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Box");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = new VectorPropertyData[2];
            for (int i = 0; i < 2; i++)
            {
                var next = new VectorPropertyData(Name, Asset);
                next.Read(reader, false, 0);
                Value[i] = next;
            }

            IsValid = reader.ReadBoolean();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int totalSize = 0;
            for (int i = 0; i < 2; i++)
            {
                totalSize += Value[i].Write(writer, includeHeader);
            }
            writer.Write(IsValid);
            return totalSize + sizeof(bool);
        }

        public override void FromString(string[] d)
        {
            IsValid = d[0].Equals("1") || d[0].ToLower().Equals("true");
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Value[i] + ", ";
            }
            return oup.Remove(oup.Length - 2) + ")";
        }

        protected override void HandleCloned(PropertyData res)
        {
            BoxPropertyData cloningProperty = (BoxPropertyData)res;

            VectorPropertyData[] newData = new VectorPropertyData[this.Value.Length];
            for (int i = 0; i < this.Value.Length; i++)
            {
                newData[i] = (VectorPropertyData)this.Value[i].Clone();
            }
            cloningProperty.Value = newData;
        }
    }
}