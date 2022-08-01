using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// Transform composed of Scale, Rotation (as a quaternion), and Translation.
    /// Transforms can be used to convert from one space to another, for example by transforming
    /// positions and directions from local space to world space.
    /// 
    /// Transformation of position vectors is applied in the order:  Scale -> Rotate -> Translate.
    /// Transformation of direction vectors is applied in the order: Scale -> Rotate.
    /// 
    /// Order matters when composing transforms: C = A * B will yield a transform C that logically
    /// first applies A then B to any subsequent transformation. Note that this is the opposite order of quaternion (FQuat) multiplication.
    /// 
    /// Example: LocalToWorld = (DeltaRotation * LocalToWorld) will change rotation in local space by DeltaRotation.
    /// Example: LocalToWorld = (LocalToWorld * DeltaRotation) will change rotation in world space by DeltaRotation.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public struct FTransform
    {
        /// <summary>
        /// Rotation of this transformation, as a quaternion
        /// </summary>
        [JsonProperty]
        public FQuat Rotation;

        /// <summary>
        /// Translation of this transformation, as a vector.
        /// </summary>
        [JsonProperty]
        public FVector Translation;

        /// <summary>
        /// 3D scale (always applied in local space) as a vector.
        /// </summary>
        [JsonProperty]
        public FVector Scale3D;

        public FTransform(FQuat rotation, FVector translation, FVector scale3D)
        {
            Rotation = rotation;
            Translation = translation;
            Scale3D = scale3D;
        }
    }
}
