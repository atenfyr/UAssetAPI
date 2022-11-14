using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class IntPointPropertyData : PropertyData<int[]> // X, Y
    {
        public IntPointPropertyData(FName name) : base(name)
        {

        }

        public IntPointPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("IntPoint");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new int[2];
            for (int i = 0; i < 2; i++)
            {
                Value[i] = reader.ReadInt32();
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            for (int i = 0; i < 2; i++)
            {
                writer.Write(Value[i]);
            }
            return sizeof(int) * 2;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = new int[2];
            if (int.TryParse(d[0], out int res1)) Value[0] = res1;
            if (int.TryParse(d[1], out int res2)) Value[1] = res2;
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