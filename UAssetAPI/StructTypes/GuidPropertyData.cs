using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    /// <summary>
    /// Describes a 128-bit <see cref="Guid"/>.
    /// </summary>
    public class GuidPropertyData : PropertyData<Guid>
    {
        public GuidPropertyData(FName name) : base(name)
        {

        }

        public GuidPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Guid");
        public override bool HasCustomStructSerialization { get { return true; } } 
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new Guid(reader.ReadBytes(16));
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.ToByteArray());
            return 16;
        }

        public override string ToString()
        {
            return Value.ConvertToString();
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = d[0].ConvertToGUID();
        }

        protected override void HandleCloned(PropertyData res)
        {
            GuidPropertyData cloningProperty = (GuidPropertyData)res;

            cloningProperty.Value = new Guid(Value.ToByteArray());
        }
    }
}