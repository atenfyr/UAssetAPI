using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class GameplayTagContainerPropertyData : PropertyData<FName[]>
{
    public GameplayTagContainerPropertyData(FName name) : base(name)
    {
        Value = [];
    }

    public GameplayTagContainerPropertyData()
    {
        Value = [];
    }

    private static readonly FString CurrentPropertyType = new FString("GameplayTagContainer");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        int numEntries = reader.ReadInt32();
        Value = new FName[numEntries];
        for (int i = 0; i < numEntries; i++)
        {
            Value[i] = reader.ReadFName();
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        if (Value == null) Value = [];
        writer.Write(Value.Length);
        int totalSize = sizeof(int);
        for (int i = 0; i < Value.Length; i++)
        {
            writer.Write(Value[i]);
            totalSize += sizeof(int) * 2;
        }
        return totalSize;
    }

    public override string ToString()
    {
        string oup = "(";
        for (int i = 0; i < Value.Length; i++)
        {
            oup += Convert.ToString(Value[i]) + ", ";
        }
        return oup.Remove(oup.Length - 2) + ")";
    }

    protected override void HandleCloned(PropertyData res)
    {
        GameplayTagContainerPropertyData cloningProperty = (GameplayTagContainerPropertyData)res;

        if (this.Value != null)
        {
            FName[] newData = new FName[this.Value.Length];
            for (int i = 0; i < this.Value.Length; i++)
            {
                newData[i] = (FName)this.Value[i].Clone();
            }
            cloningProperty.Value = newData;
        }
        else
        {
            cloningProperty.Value = null;
        }
    }
}