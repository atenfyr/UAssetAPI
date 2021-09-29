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
    public class RotatorPropertyData : PropertyData
    {
        /// <summary>Rotation around the right axis (around Y axis), Looking up and down (0=Straight Ahead, +Up, -Down)</summary>
        [JsonProperty]
        public float Pitch;

        /// <summary>Rotation around the up axis (around Z axis), Running in circles 0=East, +North, -South.</summary>
        [JsonProperty]
        public float Yaw;

        /// <summary>Rotation around the forward axis (around X axis), Tilting your head, 0=Straight, +Clockwise, -CCW.</summary>
        [JsonProperty]
        public float Roll;
        
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
                reader.ReadByte();
            }

            Pitch = reader.ReadSingle();
            Yaw = reader.ReadSingle();
            Roll = reader.ReadSingle();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Pitch);
            writer.Write(Yaw);
            writer.Write(Roll);
            return sizeof(float) * 3;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (float.TryParse(d[0], out float res1)) Pitch = res1;
            if (float.TryParse(d[1], out float res2)) Yaw = res2;
            if (float.TryParse(d[2], out float res3)) Roll = res3;
        }

        public override string ToString()
        {
            return "(" + Pitch + ", " + Yaw + ", " + Roll + ")";

        }
    }
}