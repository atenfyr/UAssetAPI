using System;
using System.Collections.Generic;

namespace UAssetAPI.UnrealTypes;

public struct TPerQualityLevel<T>
{
    public bool bCooked;
    public T Default;
    public Dictionary<int, T> PerQuality;

    public TPerQualityLevel(bool _bCooked, T _default, Dictionary<int, T> perQuality)
    {
        bCooked = _bCooked;
        Default = _default;
        PerQuality = perQuality;
    }

    public TPerQualityLevel(AssetBinaryReader reader, Func<T> valueReader)
    {
        bCooked = reader.ReadBooleanInt();
        Default = valueReader();
        PerQuality = [];
        int numElements = reader.ReadInt32();
        for (int i = 0; i < numElements; i++)
        {
            PerQuality[reader.ReadInt32()] = valueReader();
        }
    }

    public int Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        var offset = writer.BaseStream.Position;
        writer.Write(bCooked ? 1 : 0);
        valueWriter(Default);
        writer.Write(PerQuality?.Count ?? 0);
        if (PerQuality != null)
        {
            foreach (var pair in PerQuality)
            {
                writer.Write(pair.Key);
                valueWriter(pair.Value);
            }
        }
        return (int)(writer.BaseStream.Position - offset);
    }
}
