using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;
using UAssetAPI.JSON;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A 4D homogeneous vector, 4x1 FLOATs, 16-byte aligned.
    /// </summary>
    public class Vector4PropertyData : PropertyData
    {
        private float? _x1;
        private double _x2;
        private float? _y1;
        private double _y2;
        private float? _z1;
        private double _z2;
        private float? _w1;
        private double _w2;

        /// <summary>The vector's X-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public double X
        {
            get
            {
                return _x1 == null ? _x2 : (double)_x1;
            }
            set
            {
                _x1 = null;
                _x2 = value;
            }
        }

        [JsonIgnore]
        public float XFloat => _x1 == null ? (float)_x2 : (float)_x1;

        /// <summary>The vector's Y-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public double Y
        {
            get
            {
                return _y1 == null ? _y2 : (double)_y1;
            }
            set
            {
                _y1 = null;
                _y2 = value;
            }
        }

        [JsonIgnore]
        public float YFloat => _y1 == null ? (float)_y2 : (float)_y1;

        /// <summary>The vector's Z-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public double Z
        {
            get
            {
                return _z1 == null ? _z2 : (double)_z1;
            }
            set
            {
                _z1 = null;
                _z2 = value;
            }
        }

        [JsonIgnore]
        public float ZFloat => _z1 == null ? (float)_z2 : (float)_z1;

        /// <summary>The vector's W-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public double W
        {
            get
            {
                return _w1 == null ? _w2 : (double)_w1;
            }
            set
            {
                _w1 = null;
                _w2 = value;
            }
        }

        [JsonIgnore]
        public float WFloat => _w1 == null ? (float)_w2 : (float)_w1;

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
                _x2 = reader.ReadDouble();
                _y2 = reader.ReadDouble();
                _z2 = reader.ReadDouble();
                _w2 = reader.ReadDouble();
            }
            else
            {
                _x1 = reader.ReadSingle();
                _y1 = reader.ReadSingle();
                _z1 = reader.ReadSingle();
                _w1 = reader.ReadSingle();
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
                writer.Write(X);
                writer.Write(Y);
                writer.Write(Z);
                writer.Write(W);
                return sizeof(double) * 4;
            }
            else
            {
                writer.Write(XFloat);
                writer.Write(YFloat);
                writer.Write(ZFloat);
                writer.Write(WFloat);
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