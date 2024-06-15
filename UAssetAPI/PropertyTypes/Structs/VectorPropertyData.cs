using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
    /// </summary>
    public class VectorPropertyData : PropertyData<FVector>
    {
        public VectorPropertyData(FName name) : base(name)
        {

        }

        public VectorPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Vector");
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
                Value = new FVector(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
            }
            else
            {
                Value = new FVector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
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
                return sizeof(double) * 3;
            }
            else
            {
                writer.Write(Value.XFloat);
                writer.Write(Value.YFloat);
                writer.Write(Value.ZFloat);
                return sizeof(float) * 3;
            }
        }

        public override void FromString(string[] d, UAsset asset)
        {
            double.TryParse(d[0], out double X);
            double.TryParse(d[1], out double Y);
            double.TryParse(d[2], out double Z);
            Value = new FVector(X, Y, Z);
        }

        public override string ToString()
        {
            return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ")";
        }
    }
}