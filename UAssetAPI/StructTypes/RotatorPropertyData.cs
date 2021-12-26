using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    /// <summary>
    /// Implements a container for rotation information.
    /// All rotation values are stored in degrees.
    /// </summary>
    public class RotatorPropertyData : PropertyData<FRotator>
    {        
        public RotatorPropertyData(FName name) : base(name)
        {

        }

        public RotatorPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Rotator");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FRotator(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.Pitch);
            writer.Write(Value.Yaw);
            writer.Write(Value.Roll);
            return sizeof(float) * 3;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            float.TryParse(d[0], out float Pitch);
            float.TryParse(d[1], out float Yaw);
            float.TryParse(d[2], out float Roll);
            Value = new FRotator(Pitch, Yaw, Roll);
        }

        public override string ToString()
        {
            return "(" + Value.Pitch + ", " + Value.Yaw + ", " + Value.Roll + ")";

        }
    }
}