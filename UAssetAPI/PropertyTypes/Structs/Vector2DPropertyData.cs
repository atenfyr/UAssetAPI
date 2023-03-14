using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A vector in 2-D space composed of components (X, Y) with floating point precision.
    /// </summary>
    public class Vector2DPropertyData : PropertyData
    {
        /// <summary>Vector's X-component.</summary>
        [JsonProperty]
        public double X;

        /// <summary>Vector's Y-component.</summary>
        [JsonProperty]
        public double Y;

        public Vector2DPropertyData(FName name) : base(name)
        {

        }

        public Vector2DPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Vector2D");
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
            }
            else
            {
                X = reader.ReadSingle();
                Y = reader.ReadSingle();
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
                return sizeof(double) * 2;
            }
            else
            {
                writer.Write((float)X);
                writer.Write((float)Y);
                return sizeof(float) * 2;
            }
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (double.TryParse(d[0], out double res1)) X = res1;
            if (double.TryParse(d[1], out double res2)) Y = res2;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }
}