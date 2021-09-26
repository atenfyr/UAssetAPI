using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    [Flags]
    public enum ETextFlag
    {
        Transient = (1 << 0),
        CultureInvariant = (1 << 1),
        ConvertedProperty = (1 << 2),
        Immutable = (1 << 3),
        InitializedFromString = (1 << 4),
    }

    // WIP revamp
    public class TextPropertyData : PropertyData<FString[]>
    {
        public ETextFlag Flags;
        public TextHistoryType HistoryType = TextHistoryType.Base;
        public FName StringTable = null;
        public FString CultureInvariantString = null;
        public FString BaseBlankString;

        public TextPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public TextPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("TextProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            if (Asset.EngineVersion < UE4Version.VER_UE4_FTEXT_HISTORY)
            {
                FString SourceStringToImplantIntoHistory = reader.ReadFStringWithEncoding();
                if (Asset.EngineVersion >= UE4Version.VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT)
                {
                    FString Namespace = reader.ReadFStringWithEncoding();
                    FString Key = reader.ReadFStringWithEncoding();
                }
                else
                {
                    FString DisplayString = reader.ReadFStringWithEncoding();
                }
            }

            Flags = (ETextFlag)reader.ReadUInt32();

            if (Asset.EngineVersion >= UE4Version.VER_UE4_FTEXT_HISTORY)
            {
                HistoryType = (TextHistoryType)reader.ReadSByte();

                switch (HistoryType)
                {
                    case TextHistoryType.None:
                        Value = new FString[0];
                        bool bHasCultureInvariantString = reader.ReadInt32() == 1;
                        if (bHasCultureInvariantString)
                        {
                            CultureInvariantString = reader.ReadFStringWithEncoding();
                        }
                        break;
                    case TextHistoryType.Base:
                        BaseBlankString = reader.ReadFStringWithEncoding();
                        Value = new FString[] { reader.ReadFStringWithEncoding(), reader.ReadFStringWithEncoding() };
                        break;
                    case TextHistoryType.StringTableEntry:
                        StringTable = reader.ReadFName(Asset);
                        Value = new FString[] { reader.ReadFStringWithEncoding() };
                        break;
                    default:
                        throw new FormatException("Unimplemented reader for " + HistoryType.ToString());
                }
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write((uint)Flags);
            writer.Write((byte)HistoryType);

            switch(HistoryType)
            {
                case TextHistoryType.None:
                    if (CultureInvariantString == null || string.IsNullOrEmpty(CultureInvariantString.Value))
                    {
                        writer.Write(0);
                    }
                    else
                    {
                        writer.Write(1);
                        writer.WriteFString(CultureInvariantString);
                    }
                    break;
                case TextHistoryType.Base:
                    writer.WriteFString(BaseBlankString);
                    for (int i = 0; i < 2; i++)
                    {
                        writer.WriteFString(Value[i]);
                    }
                    break;
                case TextHistoryType.StringTableEntry:
                    writer.WriteFName(StringTable, Asset);
                    writer.WriteFString(Value[0]);
                    break;
                default:
                    throw new FormatException("Unimplemented writer for " + HistoryType.ToString());
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            if (Value == null) return "null";

            string oup = "";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Value[i] + " ";
            }
            return oup.TrimEnd(' ');
        }

        public override void FromString(string[] d)
        {
            if (d[1] != null)
            {
                HistoryType = TextHistoryType.Base;
                Value = new FString[] { new FString(d[0]), new FString(d[1]) };
                return;
            }

            if (d[0].Equals("null"))
            {
                HistoryType = TextHistoryType.None;
                Value = new FString[] { };
                return;
            }

            HistoryType = TextHistoryType.StringTableEntry;
            Value = new FString[] { new FString(d[0]) };
        }
    }
}