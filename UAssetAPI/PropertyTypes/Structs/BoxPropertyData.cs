using Newtonsoft.Json;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class BoxPropertyData : PropertyData<FVector[]> // Min, Max, IsValid
    {
        [JsonProperty]
        public bool IsValid;

        public BoxPropertyData(FName name) : base(name)
        {

        }

        public BoxPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Box");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FVector[2];
            for (int i = 0; i < 2; i++)
            {
                Value[i] = new FVector(reader);
            }

            IsValid = reader.ReadBoolean();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            if (Value == null)
            {
                Value = new FVector[2];
                Value[0] = new FVector(0, 0, 0);
                Value[1] = new FVector(0, 0, 0);
            }

            int totalSize = 0;
            for (int i = 0; i < 2; i++)
            {
                totalSize += Value[i].Write(writer);
            }
            writer.Write(IsValid);
            return totalSize + sizeof(bool);
        }

        public override void FromString(string[] d, UAsset asset)
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

            FVector[] newData = new FVector[this.Value.Length];
            for (int i = 0; i < this.Value.Length; i++)
            {
                newData[i] = this.Value[i]; // struct, don't worry about cloning
            }
            cloningProperty.Value = newData;
        }
    }
}