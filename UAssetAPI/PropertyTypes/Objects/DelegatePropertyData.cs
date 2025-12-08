using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes a function bound to an Object.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class FDelegate
{
    /// <summary>
    /// References the main actor export
    /// </summary>
    [JsonProperty]
    public FPackageIndex Object;
    /// <summary>
    /// The name of the delegate
    /// </summary>
    [JsonProperty]
    public FName Delegate;

    public FDelegate(FPackageIndex _object, FName @delegate)
    {
        Object = _object;
        Delegate = @delegate;
    }

    public FDelegate() { }

    public FDelegate(AssetBinaryReader reader)
    {
        Object = new FPackageIndex(reader);
        Delegate = reader.ReadFName();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.XFERPTR(Object);
        int size = sizeof(int);
        writer.Write(Delegate);
        size += 8;
        return size;
    }
}

/// <summary>
/// Describes a function bound to an Object.
/// </summary>
public class DelegatePropertyData : PropertyData<FDelegate>
{
    public DelegatePropertyData(FName name) : base(name) { }

    public DelegatePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("DelegateProperty");
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FDelegate(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        Value ??= new FDelegate(FPackageIndex.FromRawIndex(0), null);
        return Value.Write(writer);
    }

    public override string ToString()
    {
        return null;
    }

    public override void FromString(string[] d, UAsset asset)
    {

    }

    protected override void HandleCloned(PropertyData res)
    {
        DelegatePropertyData cloningProperty = (DelegatePropertyData)res;

        cloningProperty.Value = new FDelegate(this.Value.Object, this.Value.Delegate);
    }
}