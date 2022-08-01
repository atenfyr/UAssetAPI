using Newtonsoft.Json;
using UAssetAPI.JSON;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
    /// </summary>
    public class FVector
    {
        /// <summary>Vector's X-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public float X;

        /// <summary>Vector's Y-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public float Y;

        /// <summary>Vector's Z-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public float Z;

        public FVector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public FVector()
        {

        }
    }
}
