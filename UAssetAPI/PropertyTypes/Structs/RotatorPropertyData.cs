using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
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

        private static readonly FString CurrentPropertyType = new FString("Rotator");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
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
            float.TryParse(d[0], out float Roll);
            float.TryParse(d[1], out float Pitch);
            float.TryParse(d[2], out float Yaw);
            Value = new FRotator(Pitch, Yaw, Roll);
        }

        public override string ToString()
        {
            return "(" + Value.Roll + ", " + Value.Pitch + ", " + Value.Yaw + ")";
        }
    }
}