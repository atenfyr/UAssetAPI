using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    /// <summary>
    /// Floating point quaternion that can represent a rotation about an axis in 3-D space.
    /// The X, Y, Z, W components also double as the Axis/Angle format.
    /// </summary>
    public class QuatPropertyData : PropertyData
    {
        /// <summary>The quaternion's X-component.</summary>
        public float X;

        /// <summary>The quaternion's Y-component.</summary>
        public float Y;

        /// <summary>The quaternion's Z-component.</summary>
        public float Z;

        /// <summary>The quaternion's W-component.</summary>
        public float W;

        public QuatPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public QuatPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Quat");
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
            W = reader.ReadSingle();
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
            writer.Write(W);
            return sizeof(float) * 4;
        }

        public override void FromString(string[] d)
        {
            if (float.TryParse(d[0], out float res1)) X = res1;
            if (float.TryParse(d[1], out float res2)) Y = res2;
            if (float.TryParse(d[2], out float res3)) Z = res3;
            if (float.TryParse(d[3], out float res4)) W = res4;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ", " + W + ")";
        }
    }
}