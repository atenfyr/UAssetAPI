using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    [Flags]
    public enum ETextFlag
    {
        Transient = 1 << 0,
        CultureInvariant = 1 << 1,
        ConvertedProperty = 1 << 2,
        Immutable = 1 << 3,
        InitializedFromString = 1 << 4
    }

    public class TextPropertyData : PropertyData<FString>
    {
        /// <summary>Flags with various information on what sort of FText this is</summary>
        public ETextFlag Flags = 0;
        /// <summary>The HistoryType of this FText.</summary>
        public TextHistoryType HistoryType = TextHistoryType.Base;
        /// <summary>The string table ID being referenced, if applicable</summary>
        public FName TableId = null;
        /// <summary>A namespace to use when parsing texts that use LOCTEXT</summary>
        public FString Namespace = null;
        /// <summary>The source string for this FText. In the Unreal Engine, this is also known as SourceString.</summary>
        public FString CultureInvariantString = null;

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
                CultureInvariantString = reader.ReadFStringWithEncoding();
                if (Asset.EngineVersion >= UE4Version.VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT)
                {
                    Namespace = reader.ReadFStringWithEncoding();
                    Value = reader.ReadFStringWithEncoding();
                }
                else
                {
                    Namespace = null;
                    Value = reader.ReadFStringWithEncoding();
                }
            }

            Flags = (ETextFlag)reader.ReadUInt32();

            if (Asset.EngineVersion >= UE4Version.VER_UE4_FTEXT_HISTORY)
            {
                HistoryType = (TextHistoryType)reader.ReadSByte();

                switch (HistoryType)
                {
                    case TextHistoryType.None:
                        Value = null;
                        if (Asset.GetCustomVersion<FEditorObjectVersion>() >= FEditorObjectVersion.CultureInvariantTextSerializationKeyStability)
                        {
                            bool bHasCultureInvariantString = reader.ReadInt32() == 1;
                            if (bHasCultureInvariantString)
                            {
                                CultureInvariantString = reader.ReadFStringWithEncoding();
                            }
                        }
                        break;
                    case TextHistoryType.Base:
                        Namespace = reader.ReadFStringWithEncoding();
                        Value = reader.ReadFStringWithEncoding();
                        CultureInvariantString = reader.ReadFStringWithEncoding();
                        break;
                    case TextHistoryType.StringTableEntry:
                        TableId = reader.ReadFName(Asset);
                        Value = reader.ReadFStringWithEncoding();
                        break;
                    default:
                        throw new NotImplementedException("Unimplemented reader for " + HistoryType.ToString());
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

            if (Asset.EngineVersion < UE4Version.VER_UE4_FTEXT_HISTORY)
            {
                writer.WriteFString(CultureInvariantString);
                if (Asset.EngineVersion >= UE4Version.VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT)
                {
                    writer.WriteFString(Namespace);
                    writer.WriteFString(Value);
                }
                else
                {
                    writer.WriteFString(Value);
                }
            }

            writer.Write((uint)Flags);

            if (Asset.EngineVersion >= UE4Version.VER_UE4_FTEXT_HISTORY)
            {
                writer.Write((sbyte)HistoryType);

                switch (HistoryType)
                {
                    case TextHistoryType.None:
                        if (Asset.GetCustomVersion<FEditorObjectVersion>() >= FEditorObjectVersion.CultureInvariantTextSerializationKeyStability)
                        {
                            if (CultureInvariantString == null || string.IsNullOrEmpty(CultureInvariantString.Value))
                            {
                                writer.Write(0);
                            }
                            else
                            {
                                writer.Write(1);
                                writer.WriteFString(CultureInvariantString);
                            }
                        }
                        break;
                    case TextHistoryType.Base:
                        writer.WriteFString(Namespace);
                        writer.WriteFString(Value);
                        writer.WriteFString(CultureInvariantString);
                        break;
                    case TextHistoryType.StringTableEntry:
                        writer.WriteFName(TableId, Asset);
                        writer.WriteFString(Value);
                        break;
                    default:
                        throw new NotImplementedException("Unimplemented writer for " + HistoryType.ToString());
                }
            }

            return (int)writer.BaseStream.Position - here;
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

        public override void FromString(string[] d)
        {
            throw new NotImplementedException("TextPropertyData.FromString is currently unimplemented");
        }

        protected override void HandleCloned(PropertyData res)
        {
            TextPropertyData cloningProperty = (TextPropertyData)res;

            cloningProperty.TableId = (FName)TableId.Clone();
            cloningProperty.Namespace = (FString)Namespace.Clone();
            cloningProperty.Namespace = (FString)CultureInvariantString.Clone();
        }
    }
}