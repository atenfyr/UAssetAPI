using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAssetAPI
{
    /// <summary>
    /// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
    /// </summary>
    public struct FVector
    {
        /// <summary>Vector's X-component.</summary>
        [JsonProperty]
        public float X;

        /// <summary>Vector's Y-component.</summary>
        [JsonProperty]
        public float Y;

        /// <summary>Vector's Z-component.</summary>
        [JsonProperty]
        public float Z;

        public FVector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
