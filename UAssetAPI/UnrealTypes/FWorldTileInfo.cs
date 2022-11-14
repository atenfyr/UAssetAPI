using System.IO;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// World layer information for tile tagging
    /// </summary>
    public class FWorldTileLayer
    {
        /// <summary>Human readable name for this layer</summary>
        public FString Name;

        /// <summary>Reserved for additional options</summary>
        public int Reserved0;
        /// <summary>Reserved for additional options</summary>
        public IntPointPropertyData Reserved1;

        /// <summary>Distance starting from where tiles belonging to this layer will be streamed in</summary>
        public int StreamingDistance;
        public bool DistanceStreamingEnabled;

        public void Read(AssetBinaryReader reader, UAsset asset)
        {
            Name = reader.ReadFString();
            Reserved0 = reader.ReadInt32();
            Reserved1 = new IntPointPropertyData(FName.DefineDummy(asset, "Reserved1"));
            Reserved1.Read(reader, reader.Asset.GetParentClassExportName(), false, 0, 0);

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO_UPDATED)
            {
                StreamingDistance = reader.ReadInt32();
            }

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LAYER_ENABLE_DISTANCE_STREAMING)
            {
                DistanceStreamingEnabled = reader.ReadInt32() == 1;
            }
        }

        public void Write(AssetBinaryWriter writer, UAsset asset)
        {
            writer.Write(this.Name);
            writer.Write(Reserved0);
            Reserved1.Write(writer, false);

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO_UPDATED)
            {
                writer.Write(StreamingDistance);
            }

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LAYER_ENABLE_DISTANCE_STREAMING)
            {
                writer.Write(DistanceStreamingEnabled ? 1 : 0);
            }
        }

        public FWorldTileLayer(FString name, int reserved0, IntPointPropertyData reserved1, int streamingDistance, bool distanceStreamingEnabled)
        {
            Name = name;
            Reserved0 = reserved0;
            Reserved1 = reserved1;
            StreamingDistance = streamingDistance;
            DistanceStreamingEnabled = distanceStreamingEnabled;
        }

        public FWorldTileLayer()
        {

        }
    }

    /// <summary>
    /// Describes LOD entry in a world tile
    /// </summary>
    public class FWorldTileLODInfo
    {
        /// <summary>Relative to LOD0 streaming distance, absolute distance = LOD0 + StreamingDistanceDelta</summary>
        public int RelativeStreamingDistance;

        /// <summary>Reserved for additional options</summary>
        public float Reserved0;
        /// <summary>Reserved for additional options</summary>
        public float Reserved1;
        /// <summary>Reserved for additional options</summary>
        public int Reserved2;
        /// <summary>Reserved for additional options</summary>
        public int Reserved3;

        public FWorldTileLODInfo(int relativeStreamingDistance, float reserved0, float reserved1, int reserved2, int reserved3)
        {
            RelativeStreamingDistance = relativeStreamingDistance;
            Reserved0 = reserved0;
            Reserved1 = reserved1;
            Reserved2 = reserved2;
            Reserved3 = reserved3;
        }

        public void Read(AssetBinaryReader reader, UAsset asset)
        {
            RelativeStreamingDistance = reader.ReadInt32();
            Reserved0 = reader.ReadSingle();
            Reserved1 = reader.ReadSingle();
            Reserved2 = reader.ReadInt32();
            Reserved3 = reader.ReadInt32();
        }

        public void Write(AssetBinaryWriter writer, UAsset asset)
        {
            writer.Write(RelativeStreamingDistance);
            writer.Write(Reserved0);
            writer.Write(Reserved1);
            writer.Write(Reserved2);
            writer.Write(Reserved3);
        }

        public FWorldTileLODInfo()
        {

        }
    }

    /// <summary>
    /// Tile information used by WorldComposition.
    /// Defines properties necessary for tile positioning in the world. Stored with package summary
    /// </summary>
    public class FWorldTileInfo
    {
        /// <summary>Tile position in the world relative to parent</summary>
        public int[] Position; // FIntVector
        /// <summary>Absolute tile position in the world. Calculated in runtime</summary>
        public int[] AbsolutePosition; // FIntVector
        /// <summary>Tile bounding box</summary>
        public BoxPropertyData Bounds;
        /// <summary>Tile assigned layer</summary>
        public FWorldTileLayer Layer;
        /// <summary>Whether to hide sub-level tile in tile view</summary>
        public bool bHideInTileView;
        /// <summary>Parent tile package name</summary>
        public FString ParentTilePackageName;
        /// <summary>LOD information</summary>
        public FWorldTileLODInfo[] LODList;
        /// <summary>Sorting order</summary>
        public int ZOrder;

        public void Read(AssetBinaryReader reader, UAsset asset)
        {
            Position = new int[3];
            AbsolutePosition = new int[3];

            if (asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() < FFortniteMainBranchObjectVersion.WorldCompositionTile3DOffset)
            {
                Position[0] = reader.ReadInt32();
                Position[1] = reader.ReadInt32();
                Position[2] = 0;
            }
            else
            {
                Position[0] = reader.ReadInt32();
                Position[1] = reader.ReadInt32();
                Position[2] = reader.ReadInt32();
            }
            Bounds = new BoxPropertyData(FName.DefineDummy(asset, "Bounds"));
            Bounds.Read(reader, reader.Asset.GetParentClassExportName(), false, 0, 0);
            Layer = new FWorldTileLayer();
            Layer.Read(reader, asset);

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO_UPDATED)
            {
                bHideInTileView = reader.ReadInt32() == 1;
                ParentTilePackageName = reader.ReadFString();
            }

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO_LOD_LIST)
            {
                int numEntries = reader.ReadInt32();
                LODList = new FWorldTileLODInfo[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    LODList[i] = new FWorldTileLODInfo();
                    LODList[i].Read(reader, asset);
                }
            }

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO_ZORDER)
            {
                ZOrder = reader.ReadInt32();
            }

#pragma warning disable CS0162 // Unreachable code detected
            if (false) // Ar.GetPortFlags() & PPF_DuplicateForPIE
            {
                AbsolutePosition[0] = reader.ReadInt32();
                AbsolutePosition[1] = reader.ReadInt32();
                AbsolutePosition[2] = reader.ReadInt32();
            }
#pragma warning restore CS0162 // Unreachable code detected
        }

        public void Write(AssetBinaryWriter writer, UAsset asset)
        {
            if (asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() < FFortniteMainBranchObjectVersion.WorldCompositionTile3DOffset)
            {
                writer.Write(Position[0]);
                writer.Write(Position[1]);
            }
            else
            {
                writer.Write(Position[0]);
                writer.Write(Position[1]);
                writer.Write(Position[2]);
            }
            Bounds.Write(writer, false);
            Layer.Write(writer, asset);

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO_UPDATED)
            {
                writer.Write(bHideInTileView ? 1 : 0);
                writer.Write(ParentTilePackageName);
            }

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO_LOD_LIST)
            {
                writer.Write(LODList.Length);
                for (int i = 0; i < LODList.Length; i++)
                {
                    LODList[i].Write(writer, asset);
                }
            }

            if (asset.ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO_ZORDER)
            {
                writer.Write(ZOrder);
            }

#pragma warning disable CS0162 // Unreachable code detected
            if (false) // Ar.GetPortFlags() & PPF_DuplicateForPIE
            {
                writer.Write(AbsolutePosition[0]);
                writer.Write(AbsolutePosition[1]);
                writer.Write(AbsolutePosition[2]);
            }
#pragma warning restore CS0162 // Unreachable code detected
        }

        public FWorldTileInfo(int[] position, int[] absolutePosition, BoxPropertyData bounds, FWorldTileLayer layer, bool bHideInTileView, FString parentTilePackageName, FWorldTileLODInfo[] lODList, int zOrder)
        {
            Position = position;
            AbsolutePosition = absolutePosition;
            Bounds = bounds;
            Layer = layer;
            this.bHideInTileView = bHideInTileView;
            ParentTilePackageName = parentTilePackageName;
            LODList = lODList;
            ZOrder = zOrder;
        }

        public FWorldTileInfo()
        {

        }
    }
}
