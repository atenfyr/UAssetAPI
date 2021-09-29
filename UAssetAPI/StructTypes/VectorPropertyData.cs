using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    /// <summary>
    /// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
    /// </summary>
    public class VectorPropertyData : PropertyData
    {
        /// <summary>Vector's X-component.</summary>
        public float X;

        /// <summary>Vector's Y-component.</summary>
        public float Y;

        /// <summary>Vector's Z-component.</summary>
        public float Z;

        public VectorPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public VectorPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Vector");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
            return sizeof(float) * 3;
        }

        public override void FromString(string[] d)
        {
            if (float.TryParse(d[0], out float res1)) X = res1;
            if (float.TryParse(d[1], out float res2)) Y = res2;
            if (float.TryParse(d[2], out float res3)) Z = res3;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }
    }
}