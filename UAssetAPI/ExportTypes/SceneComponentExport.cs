using System.Reflection.PortableExecutable;
using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes
{
    public struct FBoxSphereBounds
    {
        public FVector Origin;
        public FVector BoxExtent;
        public double SphereRadius;
    }

    public class SceneComponentExport : ActorComponentExport
    {
        public bool bComputeBoundsOnceForGame;
        public bool bComputedBoundsOnceForGame;
        public bool bIsCooked;
        public FBoxSphereBounds Bounds;

        public SceneComponentExport(Export super) : base(super)
        {

        }

        public SceneComponentExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public SceneComponentExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);
            bool bComputeBounds = ((this[FName.DefineDummy(reader.Asset, "bComputeBoundsOnceForGame")] as BoolPropertyData)?.Value ?? false) || ((this[FName.DefineDummy(reader.Asset, "bComputedBoundsOnceForGame")] as BoolPropertyData)?.Value ?? false);
            if (bComputeBounds && reader.Asset.GetCustomVersion<FUE5SpecialProjectStreamObjectVersion>() >= FUE5SpecialProjectStreamObjectVersion.SerializeSceneComponentStaticBounds)
            {
                bIsCooked = reader.ReadBooleanInt();
                if (bIsCooked)
                {
                    FVector Origin = new FVector(reader);
                    FVector BoxExtent = new FVector(reader);
                    double SphereRadius = reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES ? reader.ReadDouble() : reader.ReadSingle();
                    Bounds = new FBoxSphereBounds() { Origin = Origin, BoxExtent = BoxExtent, SphereRadius = SphereRadius };
                }
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            bool bComputeBounds = ((this[FName.DefineDummy(writer.Asset, "bComputeBoundsOnceForGame")] as BoolPropertyData)?.Value ?? false) || ((this[FName.DefineDummy(writer.Asset, "bComputedBoundsOnceForGame")] as BoolPropertyData)?.Value ?? false);
            if (bComputeBounds && writer.Asset.GetCustomVersion<FUE5SpecialProjectStreamObjectVersion>() >= FUE5SpecialProjectStreamObjectVersion.SerializeSceneComponentStaticBounds)
            {
                writer.WriteBooleanInt(bIsCooked);
                if (bIsCooked)
                {
                    Bounds.Origin.Write(writer);
                    Bounds.BoxExtent.Write(writer);
                    if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
                    {
                        writer.Write((double)Bounds.SphereRadius);
                    }
                    else
                    {
                        writer.Write((float)Bounds.SphereRadius);
                    }
                }
            }
        }
    }
}
