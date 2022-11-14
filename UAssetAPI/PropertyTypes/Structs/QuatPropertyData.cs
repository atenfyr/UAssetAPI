using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// Floating point quaternion that can represent a rotation about an axis in 3-D space.
    /// The X, Y, Z, W components also double as the Axis/Angle format.
    /// </summary>
    public class QuatPropertyData : PropertyData<FQuat>
    {
        public QuatPropertyData(FName name) : base(name)
        {

        }

        public QuatPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Quat");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FQuat(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.X);
            writer.Write(Value.Y);
            writer.Write(Value.Z);
            writer.Write(Value.W);
            return sizeof(float) * 4;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            float.TryParse(d[0], out float X);
            float.TryParse(d[1], out float Y);
            float.TryParse(d[2], out float Z);
            float.TryParse(d[3], out float W);
            Value = new FQuat(X, Y, Z, W);
        }

        public override string ToString()
        {
            return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ", " + Value.W + ")";
        }
    }
}