using System.IO;
using System.Text;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes an <see cref="FString"/>.
    /// </summary>
    public class StrPropertyData : PropertyData<FString>
    {
        public StrPropertyData(FName name) : base(name)
        {

        }

        public StrPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("StrProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadFString();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write(Value);
            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void FromString(string[] d, UAsset asset)
        {
            var encoding = Encoding.ASCII;
            if (d.Length >= 5) encoding = (d[4].Equals("utf-16") ? Encoding.Unicode : Encoding.ASCII);
            Value = FString.FromString(d[0], encoding);
        }
    }
}