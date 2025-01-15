using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A structure that uniquely identifies an entry within a TEvaluationTreeEntryContaine
/// </summary>
public struct FEvaluationTreeEntryHandle
{
    /// <summary>
    /// Specifies an index into TEvaluationTreeEntryContainer::Entries
    /// </summary>
    public int EntryIndex;

    public FEvaluationTreeEntryHandle(int _EntryIndex)
    {
        EntryIndex = _EntryIndex;
    }

    public FEvaluationTreeEntryHandle(AssetBinaryReader reader)
    {
        EntryIndex = reader.ReadInt32();
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(EntryIndex);
    }
}

/// <summary>
/// A handle to a node in an FMovieSceneEvaluationTree, defined in terms of an entry handle (corrsponding to FMovieSceneEvaluationTree::ChildNodes), and its child index
/// </summary>
public struct FMovieSceneEvaluationTreeNodeHandle
{
    /// <summary>
    /// Entry handle for the parent's children in FMovieSceneEvaluationTree::ChildNodes
    /// </summary>
    public FEvaluationTreeEntryHandle ChildrenHandle;
    /// <summary>
    /// The index of this child within its parent's children
    /// </summary>
    public int Index;

    public FMovieSceneEvaluationTreeNodeHandle(int _ChildrenHandle, int _Index)
    {
        ChildrenHandle.EntryIndex = _ChildrenHandle;
        Index = _Index;
    }

    public FMovieSceneEvaluationTreeNodeHandle(AssetBinaryReader reader)
    {
        ChildrenHandle = new FEvaluationTreeEntryHandle(reader.ReadInt32());
        Index = reader.ReadInt32();
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(ChildrenHandle.EntryIndex);
        writer.Write(Index);
    }
}

public class FMovieSceneEvaluationTreeNode
{
    /// <summary>
    /// The time-range that this node represents
    /// </summary>
    public TRange<FFrameNumber> Range;
    public FMovieSceneEvaluationTreeNodeHandle Parent;
    /// <summary>
    /// Identifier for the child node entries associated with this node (FMovieSceneEvaluationTree::ChildNodes)
    /// </summary>
    public FEvaluationTreeEntryHandle ChildrenID;
    /// <summary>
    /// Identifier for externally stored data entries associated with this node
    /// </summary>
    public FEvaluationTreeEntryHandle DataID;

    public FMovieSceneEvaluationTreeNode(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            Range = new(reader, () => new FFrameNumber(reader));
            Parent = new FMovieSceneEvaluationTreeNodeHandle(reader.ReadInt32(), reader.ReadInt32());
            ChildrenID = new FEvaluationTreeEntryHandle(reader.ReadInt32());
            DataID = new FEvaluationTreeEntryHandle(reader.ReadInt32());
        }
    }

    public void Write(AssetBinaryWriter writer)
    {
        Range.Write(writer, frame => frame.Write(writer));
        writer.Write(Parent.ChildrenHandle.EntryIndex);
        writer.Write(Parent.Index);
        writer.Write(ChildrenID.EntryIndex);
        writer.Write(DataID.EntryIndex);
    }
}