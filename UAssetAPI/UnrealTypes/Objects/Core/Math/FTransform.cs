namespace UAssetAPI.UnrealTypes;

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
public struct FTransform
{
    /// <summary>
    /// Rotation of this transformation, as a quaternion
    /// </summary>
    public FQuat Rotation;

    /// <summary>
    /// Translation of this transformation, as a vector.
    /// </summary>
    public FVector Translation;

    /// <summary>
    /// 3D scale (always applied in local space) as a vector.
    /// </summary>
    public FVector Scale3D;

    public FTransform(FQuat rotation, FVector translation, FVector scale3D)
    {
        Rotation = rotation;
        Translation = translation;
        Scale3D = scale3D;
    }

    public FTransform(AssetBinaryReader reader)
    {
        Rotation = new FQuat(reader);
        Translation = new FVector(reader);
        Scale3D = new FVector(reader);
    }

    public int Write(AssetBinaryWriter writer)
    {
        int size = 0;
        size += Rotation.Write(writer);
        size += Translation.Write(writer);
        size += Scale3D.Write(writer);
        return size;
    }
}
