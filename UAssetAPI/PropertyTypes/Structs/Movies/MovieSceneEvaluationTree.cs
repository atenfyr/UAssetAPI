using System;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FEntry
{
    /// <summary>
    /// The index into Items of the first item
    /// </summary>
    public int StartIndex;
    /// <summary>
    /// The number of currently valid items
    /// </summary>
    public int Size;
    /// <summary>
    /// The total capacity of allowed items before reallocating
    /// </summary>
    public int Capacity;

    public FEntry(int startIndex, int size, int capacity)
    {
        StartIndex = startIndex;
        Size = size;
        Capacity = capacity;
    }

    public FEntry(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            StartIndex = reader.ReadInt32();
            Size = reader.ReadInt32();
            Capacity = reader.ReadInt32();
        }
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(StartIndex);
        writer.Write(Size);
        writer.Write(Capacity);
    }
}

public struct TEvaluationTreeEntryContainer<T>
{
    /// <summary>
    /// List of allocated entries for each allocated entry. Should only ever grow, never shrink. Shrinking would cause previously established handles to become invalid. */
    /// </summary>
    public FEntry[] Entries;
    /// <summary>
    /// Linear array of allocated entry contents. Once allocated, indices are never invalidated until Compact is called. Entries needing more capacity are re-allocated on the end of the array.
    /// </summary>
    public T[] Items;

    public TEvaluationTreeEntryContainer(FEntry[] entries, T[] items)
    {
        Entries = entries;
        Items = items;
    }

    public TEvaluationTreeEntryContainer(AssetBinaryReader reader, Func<T> valueReader)
    {
        if (reader != null)
        {
            var entriesamount = reader.ReadInt32();
            Entries = new FEntry[entriesamount];
            for (int i = 0; i < entriesamount; i++)
            {
                Entries[i] = new FEntry(reader);
            }

            int itemsamount = reader.ReadInt32();
            Items = new T[itemsamount];
            for (int i = 0; i < itemsamount; i++)
            {
                Items[i] = valueReader();
            }
        }
    }

    public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        writer.Write(Entries.Length);
        for (int i = 0; i < Entries.Length; i++)
        {
            Entries[i].Write(writer);
        }

        writer.Write(Items.Length);
        for (int i = 0; i < Items.Length; i++)
        {
            valueWriter(Items[i]);
        }
    }
}

public class FMovieSceneEvaluationTree(AssetBinaryReader reader)
{
    /// <summary> 
    /// This tree's root node
    /// </summary>
    public FMovieSceneEvaluationTreeNode RootNode = new FMovieSceneEvaluationTreeNode(reader);
    /// <summary>
    /// Segmented array of all child nodes within this tree (in no particular order)
    /// </summary>
    public TEvaluationTreeEntryContainer<FMovieSceneEvaluationTreeNode> ChildNodes = new(reader, () => new FMovieSceneEvaluationTreeNode(reader));

    public void Write(AssetBinaryWriter writer)
    {
        RootNode.Write(writer);
        ChildNodes.Write(writer, node => node.Write(writer));
    }
}

public class TMovieSceneEvaluationTree<T> : FMovieSceneEvaluationTree
{
    /// <summary>
    /// Tree data container that corresponds to FMovieSceneEvaluationTreeNode::DataID
    /// </summary>
    public TEvaluationTreeEntryContainer<T> Data;

    public TMovieSceneEvaluationTree(AssetBinaryReader reader, Func<T> valueReader) : base(reader)
    {
        if (reader != null) Data = new(reader, valueReader);
    }

    public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        base.Write(writer);
        Data.Write(writer, valueWriter);
    }
}