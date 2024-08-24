using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using UAssetAPI.CustomVersions;
using UAssetAPI.UnrealTypes;
using UAssetAPI.UnrealTypes.EngineEnums;

namespace UAssetAPI.PropertyTypes.Objects;

[Flags]
public enum ETextFlag
{
    Transient = 1 << 0,
    CultureInvariant = 1 << 1,
    ConvertedProperty = 1 << 2,
    Immutable = 1 << 3,
    InitializedFromString = 1 << 4
}

public enum ETransformType : byte
{
    ToLower = 0,
    ToUpper,
}

public class FNumberFormattingOptions
{
    public bool AlwaysSign;
    public bool UseGrouping;
    public ERoundingMode RoundingMode;
    public int MinimumIntegralDigits;
    public int MaximumIntegralDigits;
    public int MinimumFractionalDigits;
    public int MaximumFractionalDigits;

    public FNumberFormattingOptions()
    {
        AlwaysSign = false;
        UseGrouping = true;
        RoundingMode = ERoundingMode.HalfToEven;
        MinimumIntegralDigits = 1;
        MaximumIntegralDigits = 316;
        MinimumFractionalDigits = 0;
        MaximumFractionalDigits = 3;
    }

    public FNumberFormattingOptions(AssetBinaryReader reader)
    {
        if (reader.Asset.GetCustomVersion<FEditorObjectVersion>() >= FEditorObjectVersion.AddedAlwaysSignNumberFormattingOption)
            AlwaysSign = reader.ReadBooleanInt();
        UseGrouping = reader.ReadBooleanInt();
        RoundingMode = (ERoundingMode)reader.ReadByte();
        MinimumIntegralDigits = reader.ReadInt32();
        MaximumIntegralDigits = reader.ReadInt32();
        MinimumFractionalDigits = reader.ReadInt32();
        MaximumFractionalDigits = reader.ReadInt32();
    }

    public void Write(AssetBinaryWriter writer)
    {
        if (writer.Asset.GetCustomVersion<FEditorObjectVersion>() >= FEditorObjectVersion.AddedAlwaysSignNumberFormattingOption)
            writer.Write(AlwaysSign ? 1 : 0);
        writer.Write(UseGrouping ? 1 : 0);
        writer.Write((byte)RoundingMode);
        writer.Write(MinimumIntegralDigits);
        writer.Write(MaximumIntegralDigits);
        writer.Write(MinimumFractionalDigits);
        writer.Write(MaximumFractionalDigits);
    }
}


public class FFormatArgumentValue
{
    public EFormatArgumentType Type;
    public object Value;

    public FFormatArgumentValue() { }

    public FFormatArgumentValue(EFormatArgumentType type, object value)
    {
        Type = type;
        Value = value;
    }

    public FFormatArgumentValue(AssetBinaryReader reader, bool isArgumentData = false)
    {
        Type = (EFormatArgumentType)reader.ReadByte();
        switch (Type)
        {
            case EFormatArgumentType.Int:
                Value = isArgumentData && reader.Asset.GetCustomVersion<FUE5ReleaseStreamObjectVersion>() < FUE5ReleaseStreamObjectVersion.TextFormatArgumentData64bitSupport ? reader.ReadInt32() : reader.ReadInt64();
                break;
            case EFormatArgumentType.UInt:
                Value = reader.ReadUInt64();
                break;
            case EFormatArgumentType.Double:
                Value = reader.ReadDouble();
                break;
            case EFormatArgumentType.Float:
                Value = reader.ReadSingle();
                break;
            case EFormatArgumentType.Text:
                var val = new TextPropertyData(FName.DefineDummy(reader.Asset, "Value"));
                val.Read(reader, false, 1, 0, PropertySerializationContext.Normal);
                Value = val;
                break;
            default:
                throw new NotImplementedException("EFormatArgumentType type " + Type.ToString() + " is not implemented for reading");
        }
    }

    public int Write(AssetBinaryWriter writer, bool isArgumentData = false)
    {
        int sz = 0;
        writer.Write((byte)Type); sz += sizeof(byte);
        switch (Type)
        {
            case EFormatArgumentType.Int:
                if (isArgumentData && writer.Asset.GetCustomVersion<FUE5ReleaseStreamObjectVersion>() < FUE5ReleaseStreamObjectVersion.TextFormatArgumentData64bitSupport)
                {
                    writer.Write((int)(long)Value);
                    sz += sizeof(int);
                }
                else
                {
                    writer.Write((long)Value);
                    sz += sizeof(long);
                }
                break;
            case EFormatArgumentType.UInt:
                writer.Write((ulong)Value);
                sz += sizeof(ulong);
                break;
            case EFormatArgumentType.Double:
                writer.Write((double)Value);
                sz += sizeof(double);
                break;
            case EFormatArgumentType.Float:
                writer.Write((float)Value);
                sz += sizeof(float);
                break;
            case EFormatArgumentType.Text:
                int here = (int)writer.BaseStream.Position;
                var val = (TextPropertyData)Value;
                val.Write(writer, false);
                sz += (int)writer.BaseStream.Position - here;
                break;
            default:
                throw new NotImplementedException("EFormatArgumentType type " + Type.ToString() + " is not implemented for writing");
        }

        return sz;
    }
}

public class FFormatArgumentData
{
    public FString ArgumentName;
    public FFormatArgumentValue ArgumentValue;

    public FFormatArgumentData() { }

    public FFormatArgumentData(FString name, FFormatArgumentValue value)
    {
        ArgumentName = name;
        ArgumentValue = value;
    }

    public FFormatArgumentData(AssetBinaryReader reader)
    {
        Read(reader);
    }

    public void Read(AssetBinaryReader reader)
    {
        ArgumentName = reader.ReadFString();
        ArgumentValue = new FFormatArgumentValue(reader, true);
    }

    public int Write(AssetBinaryWriter writer)
    {
        int sz = writer.Write(ArgumentName);
        sz += ArgumentValue.Write(writer, true);
        return sz;
    }
}

/// <summary>
/// Describes an FText.
/// </summary>
public class TextPropertyData : PropertyData<FString>
{
    /// <summary>Flags with various information on what sort of FText this is</summary>
    [JsonProperty]
    public ETextFlag Flags = 0;
    /// <summary>The HistoryType of this FText.</summary>
    [JsonProperty]
    [JsonConverter(typeof(StringEnumConverter))]
    public TextHistoryType HistoryType = TextHistoryType.Base;
    /// <summary>The string table ID being referenced, if applicable</summary>
    [JsonProperty]
    public FName TableId = null;
    /// <summary>A namespace to use when parsing texts that use LOCTEXT</summary>
    [JsonProperty]
    public FString Namespace = null;
    /// <summary>The source string for this FText. In the Unreal Engine, this is also known as SourceString.</summary>
    [JsonProperty]
    public FString CultureInvariantString = null;

    // OrderedFormat
    [JsonProperty]
    public TextPropertyData SourceFmt;
    [JsonProperty]
    public FFormatArgumentValue[] Arguments;
    //ArgumentFormat
    [JsonProperty]
    public FFormatArgumentData[] ArgumentsData;
    //Transform
    [JsonProperty]
    public ETransformType TransformType;
    //AsNumber
    [JsonProperty]
    FFormatArgumentValue SourceValue;
    [JsonProperty]
    FNumberFormattingOptions FormatOptions;
    [JsonProperty]
    FString TargetCulture;


    public bool ShouldSerializeTableId()
    {
        return HistoryType == TextHistoryType.StringTableEntry;
    }

    public TextPropertyData(FName name) : base(name) { }

    public TextPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("TextProperty");
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        if (reader.Asset.ObjectVersion < ObjectVersion.VER_UE4_FTEXT_HISTORY)
        {
            CultureInvariantString = reader.ReadFString();
            if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT)
            {
                Namespace = reader.ReadFString();
                Value = reader.ReadFString();
            }
            else
            {
                Namespace = null;
                Value = reader.ReadFString();
            }
        }

        Flags = (ETextFlag)reader.ReadUInt32();

        if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_FTEXT_HISTORY)
        {
            HistoryType = (TextHistoryType)reader.ReadSByte();

            switch (HistoryType)
            {
                case TextHistoryType.None:
                    Value = null;
                    if (reader.Asset.GetCustomVersion<FEditorObjectVersion>() >= FEditorObjectVersion.CultureInvariantTextSerializationKeyStability)
                    {
                        bool bHasCultureInvariantString = reader.ReadInt32() == 1;
                        if (bHasCultureInvariantString)
                        {
                            CultureInvariantString = reader.ReadFString();
                        }
                    }
                    break;
                case TextHistoryType.Base:
                    Namespace = reader.ReadFString();
                    Value = reader.ReadFString(); // Key
                    CultureInvariantString = reader.ReadFString(); // SourceString
                    break;
                case TextHistoryType.StringTableEntry:
                    TableId = reader.ReadFName();
                    Value = reader.ReadFString();
                    break;
                case TextHistoryType.RawText:
                    Value = reader.ReadFString();
                    break;
                case TextHistoryType.OrderedFormat:
                    SourceFmt = new TextPropertyData(FName.DefineDummy(reader.Asset, "SourceFmt"));
                    SourceFmt.Read(reader, false, 1, 0, serializationContext);
                    int ArgumentsSize = reader.ReadInt32();
                    Arguments = new FFormatArgumentValue[ArgumentsSize];
                    for (int i = 0; i < ArgumentsSize; i++)
                    {
                        Arguments[i] = new FFormatArgumentValue(reader);
                    }
                    break;
                case TextHistoryType.ArgumentFormat:
                    SourceFmt = new TextPropertyData(FName.DefineDummy(reader.Asset, "SourceFmt"));
                    SourceFmt.Read(reader, false, 1, 0, serializationContext);
                    ArgumentsSize = reader.ReadInt32();
                    ArgumentsData = new FFormatArgumentData[ArgumentsSize];
                    for (int i = 0; i < ArgumentsSize; i++)
                    {
                        ArgumentsData[i] = new FFormatArgumentData(reader);
                    }
                    break;
                case TextHistoryType.Transform:
                    SourceFmt = new TextPropertyData(FName.DefineDummy(reader.Asset, "SourceFmt"));
                    SourceFmt.Read(reader, false, 1, 0, serializationContext);
                    TransformType = (ETransformType)reader.ReadByte();
                    break;
                case TextHistoryType.AsNumber:
                    SourceValue = new FFormatArgumentValue(reader);
                    if (reader.ReadBooleanInt())
                    {
                        FormatOptions = new FNumberFormattingOptions(reader);
                    }
                    TargetCulture = reader.ReadFString();
                    break;
                default:
                    throw new NotImplementedException("Unimplemented reader for " + HistoryType.ToString() + " @ " + reader.BaseStream.Position);
            }
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        var here = writer.BaseStream.Position;

        if (writer.Asset.ObjectVersion < ObjectVersion.VER_UE4_FTEXT_HISTORY)
        {
            writer.Write(CultureInvariantString);
            if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT)
            {
                writer.Write(Namespace);
                writer.Write(Value);
            }
            else
            {
                writer.Write(Value);
            }
        }

        writer.Write((uint)Flags);

        if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_FTEXT_HISTORY)
        {
            writer.Write((sbyte)HistoryType);

            switch (HistoryType)
            {
                case TextHistoryType.None:
                    if (writer.Asset.GetCustomVersion<FEditorObjectVersion>() >= FEditorObjectVersion.CultureInvariantTextSerializationKeyStability)
                    {
                        if (CultureInvariantString == null || string.IsNullOrEmpty(CultureInvariantString.Value))
                        {
                            writer.Write(0);
                        }
                        else
                        {
                            writer.Write(1);
                            writer.Write(CultureInvariantString);
                        }
                    }
                    break;
                case TextHistoryType.Base:
                    writer.Write(Namespace);
                    writer.Write(Value);
                    writer.Write(CultureInvariantString);
                    break;
                case TextHistoryType.StringTableEntry:
                    writer.Write(TableId);
                    writer.Write(Value);
                    break;
                case TextHistoryType.RawText:
                    writer.Write(Value);
                    break;
                case TextHistoryType.OrderedFormat:
                    SourceFmt.Write(writer, false, serializationContext);
                    writer.Write(Arguments.Length);
                    for (int i = 0; i < Arguments.Length; i++)
                    {
                        Arguments[i].Write(writer);
                    }
                    break;
                case TextHistoryType.ArgumentFormat:
                    SourceFmt.Write(writer, false, serializationContext);
                    writer.Write(ArgumentsData.Length);
                    for (int i = 0; i < ArgumentsData.Length; i++)
                    {
                        ArgumentsData[i].Write(writer);
                    }
                    break;
                case TextHistoryType.Transform:
                    SourceFmt.Write(writer, false, serializationContext);
                    writer.Write((byte)TransformType);
                    break;
                case TextHistoryType.AsNumber:
                    SourceValue.Write(writer);
                    if (FormatOptions != null)
                    {
                        writer.Write(1);
                        FormatOptions.Write(writer);
                    }
                    else
                    {
                        writer.Write(0);
                    }
                    writer.Write(TargetCulture);
                    break;
                default:
                    throw new NotImplementedException("Unimplemented writer for " + HistoryType.ToString());
            }
        }

        return (int)(writer.BaseStream.Position - here);
    }

    public override string ToString()
    {
        if (Value == null) return "null";

        switch (HistoryType)
        {
            case TextHistoryType.None:
                return "None, " + CultureInvariantString;
            case TextHistoryType.Base:
                return "Base, " + Namespace + ", " + Value + ", " + CultureInvariantString;
            case TextHistoryType.StringTableEntry:
                return "StringTableEntry, " + TableId + ", " + Value;
            default:
                throw new NotImplementedException("Unimplemented display for " + HistoryType.ToString());
        }
    }

    public override void FromString(string[] d, UAsset asset)
    {
        throw new NotImplementedException("TextPropertyData.FromString is currently unimplemented");
    }

    protected override void HandleCloned(PropertyData res)
    {
        TextPropertyData cloningProperty = (TextPropertyData)res;

        cloningProperty.TableId = (FName)TableId?.Clone();
        cloningProperty.Namespace = (FString)Namespace?.Clone();
        cloningProperty.CultureInvariantString = (FString)CultureInvariantString?.Clone();
    }
}