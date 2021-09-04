using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class GameplayTagContainerPropertyData : PropertyData<NamePropertyData[]>
    {
        public GameplayTagContainerPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "GameplayTagContainer";
        }

        public GameplayTagContainerPropertyData()
        {
            Type = "GameplayTagContainer";
        }

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
                Value[i] = new NamePropertyData("TagName", Asset);
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
    }
}