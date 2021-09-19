using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.StructTypes;

namespace UAssetAPI
{
    /**
     * World layer information for tile tagging
     */
    public class FWorldTileLayer
    {
        /** Human readable name for this layer */
        public FString Name;

        /** Reserved for additional options */
        public int Reserved0;
        public IntPointPropertyData Reserved1;

        /** Distance starting from where tiles belonging to this layer will be streamed in */
        public int StreamingDistance;
        public bool DistanceStreamingEnabled;

        public void Read(BinaryReader reader, UAsset asset)
        {
            Name = reader.ReadFStringWithEncoding();
            Reserved0 = reader.ReadInt32();
            Reserved1 = new IntPointPropertyData(new FName(string.Empty), asset);
            Reserved1.Read(reader, false, 0, 0);

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LEVEL_INFO_UPDATED)
            {
                StreamingDistance = reader.ReadInt32();
            }

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LAYER_ENABLE_DISTANCE_STREAMING)
            {
                DistanceStreamingEnabled = reader.ReadInt32() == 1;
            }
        }

        public void Write(BinaryWriter writer, UAsset asset)
        {
            writer.WriteFString(this.Name);
            writer.Write(Reserved0);
            Reserved1.Asset = asset;
            Reserved1.Write(writer, false);

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LEVEL_INFO_UPDATED)
            {
                writer.Write(StreamingDistance);
            }

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LAYER_ENABLE_DISTANCE_STREAMING)
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

    /** 
     * Describes LOD entry in a world tile 
     */
    public class FWorldTileLODInfo
    {
        /** Relative to LOD0 streaming distance, absolute distance = LOD0 + StreamingDistanceDelta */
        public int RelativeStreamingDistance;

        /** Reserved for additional options */
        public float Reserved0;
        public float Reserved1;
        public int Reserved2;
        public int Reserved3;

        public FWorldTileLODInfo(int relativeStreamingDistance, float reserved0, float reserved1, int reserved2, int reserved3)
        {
            RelativeStreamingDistance = relativeStreamingDistance;
            Reserved0 = reserved0;
            Reserved1 = reserved1;
            Reserved2 = reserved2;
            Reserved3 = reserved3;
        }

        public void Read(BinaryReader reader, UAsset asset)
        {
            RelativeStreamingDistance = reader.ReadInt32();
            Reserved0 = reader.ReadSingle();
            Reserved1 = reader.ReadSingle();
            Reserved2 = reader.ReadInt32();
            Reserved3 = reader.ReadInt32();
        }

        public void Write(BinaryWriter writer, UAsset asset)
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

    /** 
     * Tile information used by WorldComposition. 
     * Defines properties necessary for tile positioning in the world. Stored with package summary 
     */
    public class FWorldTileInfo
    {
        /** Tile position in the world relative to parent */
        public int[] Position; // FIntVector
        /** Absolute tile position in the world. Calculated in runtime */
        public int[] AbsolutePosition; // FIntVector
        /** Tile bounding box  */
        public BoxPropertyData Bounds;
        /** Tile assigned layer  */
        public FWorldTileLayer Layer;
        /** Whether to hide sub-level tile in tile view*/
        public bool bHideInTileView;
        /** Parent tile package name */
        public FString ParentTilePackageName;
        /** LOD information */
        public FWorldTileLODInfo[] LODList;
        /** Sorting order */
        public int ZOrder;

        public void Read(BinaryReader reader, UAsset asset)
        {
            Position = new int[3];
            AbsolutePosition = new int[3];

            // Needs further testing
            if (true || asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() < FFortniteMainBranchObjectVersion.WorldCompositionTile3DOffset)
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
            Bounds = new BoxPropertyData(new FName(), asset);
            Bounds.Read(reader, false, 0, 0);
            Layer = new FWorldTileLayer();
            Layer.Read(reader, asset);

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LEVEL_INFO_UPDATED)
            {
                bHideInTileView = reader.ReadInt32() == 1;
                ParentTilePackageName = reader.ReadFStringWithEncoding();
            }

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LEVEL_INFO_LOD_LIST)
            {
                int numEntries = reader.ReadInt32();
                LODList = new FWorldTileLODInfo[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    LODList[i] = new FWorldTileLODInfo();
                    LODList[i].Read(reader, asset);
                }
            }

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LEVEL_INFO_ZORDER)
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

        public void Write(BinaryWriter writer, UAsset asset)
        {
            // Needs further testing
            if (true || asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() < FFortniteMainBranchObjectVersion.WorldCompositionTile3DOffset)
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
            Bounds.Asset = asset;
            Bounds.Write(writer, false);
            Layer.Write(writer, asset);

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LEVEL_INFO_UPDATED)
            {
                writer.Write(bHideInTileView ? 1 : 0);
                writer.WriteFString(ParentTilePackageName);
            }

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LEVEL_INFO_LOD_LIST)
            {
                writer.Write(LODList.Length);
                for (int i = 0; i < LODList.Length; i++)
                {
                    LODList[i].Write(writer, asset);
                }
            }

            if (asset.EngineVersion >= UE4Version.VER_UE4_WORLD_LEVEL_INFO_ZORDER)
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
