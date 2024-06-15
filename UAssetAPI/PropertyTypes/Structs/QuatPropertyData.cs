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

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
            {
                Value = new FQuat(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
            }
            else
            {
                Value = new FQuat(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
            {
                writer.Write(Value.X);
                writer.Write(Value.Y);
                writer.Write(Value.Z);
                writer.Write(Value.W);
                return sizeof(double) * 4;
            }
            else
            {
                writer.Write(Value.XFloat);
                writer.Write(Value.YFloat);
                writer.Write(Value.ZFloat);
                writer.Write(Value.WFloat);
                return sizeof(float) * 4;
            }
        }

        public override void FromString(string[] d, UAsset asset)
        {
            double.TryParse(d[0], out double X);
            double.TryParse(d[1], out double Y);
            double.TryParse(d[2], out double Z);
            double.TryParse(d[3], out double W);
            Value = new FQuat(X, Y, Z, W);
        }

        public override string ToString()
        {
            return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ", " + Value.W + ")";
        }
    }
}