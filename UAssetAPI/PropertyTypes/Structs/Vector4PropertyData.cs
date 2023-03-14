using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A 4D homogeneous vector, 4x1 FLOATs, 16-byte aligned.
    /// </summary>
    public class Vector4PropertyData : PropertyData
    {
        /// <summary>Vector's X-component.</summary>
        [JsonProperty]
        public double X;

        /// <summary>Vector's Y-component.</summary>
        [JsonProperty]
        public double Y;

        /// <summary>Vector's Z-component.</summary>
        [JsonProperty]
        public double Z;

        /// <summary>Vector's W-component.</summary>
        [JsonProperty]
        public double W;

        public Vector4PropertyData(FName name) : base(name)
        {

        }

        public Vector4PropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Vector4");
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
                X = reader.ReadDouble();
                Y = reader.ReadDouble();
                Z = reader.ReadDouble();
                W = reader.ReadDouble();
            }
            else
            {
                X = reader.ReadSingle();
                Y = reader.ReadSingle();
                Z = reader.ReadSingle();
                W = reader.ReadSingle();
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
                writer.Write((double)X);
                writer.Write((double)Y);
                writer.Write((double)Z);
                writer.Write((double)W);
                return sizeof(double) * 4;
            }
            else
            {
                writer.Write((float)X);
                writer.Write((float)Y);
                writer.Write((float)Z);
                writer.Write((float)W);
                return sizeof(float) * 4;
            }
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (double.TryParse(d[0], out double res1)) X = res1;
            if (double.TryParse(d[1], out double res2)) Y = res2;
            if (double.TryParse(d[2], out double res3)) Z = res3;
            if (double.TryParse(d[3], out double res4)) W = res4;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ", " + W + ")";
        }
    }
}