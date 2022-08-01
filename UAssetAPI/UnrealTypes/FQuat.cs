using Newtonsoft.Json;
using UAssetAPI.JSON;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// Floating point quaternion that can represent a rotation about an axis in 3-D space.
    /// The X, Y, Z, W components also double as the Axis/Angle format.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FQuat
    {
        /// <summary>The quaternion's X-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public float X;

        /// <summary>The quaternion's Y-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public float Y;

        /// <summary>The quaternion's Z-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public float Z;

        /// <summary>The quaternion's W-component.</summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public float W;

        public FQuat(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public FQuat()
        {

        }
    }
}
