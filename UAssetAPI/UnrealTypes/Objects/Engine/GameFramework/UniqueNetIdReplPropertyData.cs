using System.Text;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

public class FUniqueNetId
{
    public FName Type;
    public FString Contents;

    public FUniqueNetId(FName type, FString contents)
    {
        Type = type;
        Contents = contents;
    }

    public FUniqueNetId(AssetBinaryReader reader)
    {
        if (reader.ReadInt32() <= 0) return;
        if (reader.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_20)
            Type = reader.ReadFName();
        Contents = reader.ReadFString();
    }

    public int Write(AssetBinaryWriter writer)
    {
        if (Type is null && Contents is null)
        {
            writer.Write(0);
            return sizeof(int);
        }

        var size = sizeof(int);
        if (writer.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_20)
        {
            size += sizeof(int) * 2;
        }

        if (Contents != null)
        {
            size += Contents.Encoding is UnicodeEncoding ? (Contents.Value.Length + 1) * 2 : (Contents.Value.Length + 1);
        }

        //not sure about this alignment, maybe need this only in old versions
        writer.Write(size + 3 & ~3);

        if (writer.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_20)
            writer.Write(Type);
        writer.Write(Contents);

        return size+sizeof(int);
    }
}

public class UniqueNetIdReplPropertyData : PropertyData<FUniqueNetId>
{
    public UniqueNetIdReplPropertyData(FName name) : base(name) { } 

    public UniqueNetIdReplPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("UniqueNetIdRepl");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FUniqueNetId(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        if (Value is null)
        {
            writer.Write(0);
            return sizeof(int);
        }
      
        return Value.Write(writer);
    }
}
