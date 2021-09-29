using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    /// <summary>
    /// A 4D homogeneous vector, 4x1 FLOATs, 16-byte aligned.
    /// </summary>
    public class Vector4PropertyData : PropertyData
    {
        /// <summary>Vector's X-component.</summary>
        public float X;

        /// <summary>Vector's Y-component.</summary>
        public float Y;

        /// <summary>Vector's Z-component.</summary>
        public float Z;
        
        /// <summary>Vector's W-component.</summary>
        public float W;

        public Vector4PropertyData(FName name) : base(name)
        {

        }

        public Vector4PropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Vector4");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
            Z = reader.ReadSingle();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
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

        public override void FromString(string[] d, UAsset asset)
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