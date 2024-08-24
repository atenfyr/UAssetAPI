using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Long range attachment tether pathfinding based on Dijkstra's algorithm.
/// </summary>
public class ClothTetherDataPropertyData : StructPropertyData
{
    public (int, int, float)[][] Tethers;

    public ClothTetherDataPropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public ClothTetherDataPropertyData(FName name) : base(name) { }
    public ClothTetherDataPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("ClothTetherData");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
    

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        StructType = FName.DefineDummy(reader.Asset, CurrentPropertyType);
        base.Read(reader, false, 1, 0, PropertySerializationContext.StructFallback);

        int numElements = reader.ReadInt32();
        Tethers = new (int, int, float)[numElements][];
        for (int i = 0; i < numElements; i++)
        {
            int numInnerElements = reader.ReadInt32();
            Tethers[i] = new (int, int, float)[numInnerElements];
            for (int j = 0; j < numInnerElements; j++)
            {
                Tethers[i][j] = (reader.ReadInt32(), reader.ReadInt32(), reader.ReadSingle());
            }
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        StructType = FName.DefineDummy(writer.Asset, CurrentPropertyType);
        int totalSize = base.Write(writer, includeHeader, PropertySerializationContext.StructFallback);

        if (Tethers == null) Tethers = [];
        writer.Write(Tethers.Length);
        totalSize += sizeof(int);
        for (int i = 0; i < Tethers.Length; i++)
        {
            writer.Write(Tethers[i].Length);
            totalSize += sizeof(int);
            for (int j = 0; j < Tethers[i].Length; j++)
            {
                writer.Write(Tethers[i][j].Item1);
                writer.Write(Tethers[i][j].Item2);
                writer.Write(Tethers[i][j].Item3);
                totalSize += sizeof(int) * 2 + sizeof(float);
            }
        }

        return totalSize;
    }
}