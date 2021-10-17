using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
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

        private static readonly FName CurrentPropertyType = new FName("Vector");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = new FVector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value.X);
            writer.Write(Value.Y);
            writer.Write(Value.Z);
            return sizeof(float) * 3;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            float.TryParse(d[0], out float X);
            float.TryParse(d[1], out float Y);
            float.TryParse(d[2], out float Z);
            Value = new FVector(X, Y, Z);
        }

        public override string ToString()
        {
            return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ")";
        }
    }
}