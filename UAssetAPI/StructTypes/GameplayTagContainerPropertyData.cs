using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class GameplayTagContainerPropertyData : PropertyData<NamePropertyData[]>
    {
        public GameplayTagContainerPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public GameplayTagContainerPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("GameplayTagContainer");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            int numEntries = reader.ReadInt32();
            Value = new NamePropertyData[numEntries];
            for (int i = 0; i < numEntries; i++)
            {
                Value[i] = new NamePropertyData(new FName("TagName"), Asset);
                Value[i].Read(reader, false, sizeof(int) * 2);
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value.Length);
            int totalSize = sizeof(int);
            for (int i = 0; i < Value.Length; i++)
            {
                totalSize += Value[i].Write(writer, false);
            }
            return totalSize;
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Convert.ToString(Value[i]) + ", ";
            }
            return oup.Remove(oup.Length - 2) + ")";
        }

        protected override void HandleCloned(PropertyData res)
        {
            GameplayTagContainerPropertyData cloningProperty = (GameplayTagContainerPropertyData)res;

            NamePropertyData[] newData = new NamePropertyData[this.Value.Length];
            for (int i = 0; i < this.Value.Length; i++)
            {
                newData[i] = (NamePropertyData)this.Value[i].Clone();
            }
            cloningProperty.Value = newData;
        }
    }
}