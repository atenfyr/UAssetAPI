using Newtonsoft.Json;
using UAssetAPI.JSON;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A vector in 2-D space composed of components (X, Y) with floating point precision.
    /// </summary>
    public class Vector2DPropertyData : PropertyData
    {
        private float? _x1;
        private double _x2;
        private float? _y1;
        private double _y2;

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

        public Vector2DPropertyData(FName name) : base(name)
        {

        }

        public Vector2DPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Vector2D");
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
                _x2 = reader.ReadDouble();
                _y2 = reader.ReadDouble();
            }
            else
            {
                _x1 = reader.ReadSingle();
                _y1 = reader.ReadSingle();
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
                writer.Write(X);
                writer.Write(Y);
                return sizeof(double) * 2;
            }
            else
            {
                writer.Write(XFloat);
                writer.Write(YFloat);
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

    public class DeprecateSlateVector2DPropertyData : Vector2DPropertyData
    {
        public DeprecateSlateVector2DPropertyData(FName name) : base(name)
        {

        }

        public DeprecateSlateVector2DPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("DeprecateSlateVector2D");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            base.Read(reader, includeHeader, leng1, leng2, serializationContext);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            return base.Write(writer, includeHeader, serializationContext);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            base.FromString(d, asset);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}