using Newtonsoft.Json;

namespace UAssetAPI
{
    /// <summary>
    /// Floating point quaternion that can represent a rotation about an axis in 3-D space.
    /// The X, Y, Z, W components also double as the Axis/Angle format.
    /// </summary>
    public struct FQuat
    {
        /// <summary>The quaternion's X-component.</summary>
        [JsonProperty]
        public float X;

        /// <summary>The quaternion's Y-component.</summary>
        [JsonProperty]
        public float Y;

        /// <summary>The quaternion's Z-component.</summary>
        [JsonProperty]
        public float Z;

        /// <summary>The quaternion's W-component.</summary>
        [JsonProperty]
        public float W;

        public FQuat(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
