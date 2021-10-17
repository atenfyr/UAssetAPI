using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAssetAPI
{
    /// <summary>
    /// Implements a container for rotation information.
    /// All rotation values are stored in degrees.
    /// </summary>
    public struct FRotator
    {
        /// <summary>Rotation around the right axis (around Y axis), Looking up and down (0=Straight Ahead, +Up, -Down)</summary>
        [JsonProperty]
        public float Pitch;

        /// <summary>Rotation around the up axis (around Z axis), Running in circles 0=East, +North, -South.</summary>
        [JsonProperty]
        public float Yaw;

        /// <summary>Rotation around the forward axis (around X axis), Tilting your head, 0=Straight, +Clockwise, -CCW.</summary>
        [JsonProperty]
        public float Roll;

        public FRotator(float pitch, float yaw, float roll)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }
    }
}
