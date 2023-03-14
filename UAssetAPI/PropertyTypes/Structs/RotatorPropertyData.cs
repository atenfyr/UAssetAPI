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

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
            {
                Value = new FRotator(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
            }
            else
            {
                Value = new FRotator(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
            {
                writer.Write(Value.Pitch);
                writer.Write(Value.Yaw);
                writer.Write(Value.Roll);
                return sizeof(double) * 3;
            }
            else
            {
                writer.Write(Value.PitchFloat);
                writer.Write(Value.YawFloat);
                writer.Write(Value.RollFloat);
                return sizeof(float) * 3;
            }
        }

        public override void FromString(string[] d, UAsset asset)
        {
            double.TryParse(d[0], out double Roll);
            double.TryParse(d[1], out double Pitch);
            double.TryParse(d[2], out double Yaw);
            Value = new FRotator(Pitch, Yaw, Roll);
        }

        public override string ToString()
        {
            return "(" + Value.Roll + ", " + Value.Pitch + ", " + Value.Yaw + ")";
        }
    }
}