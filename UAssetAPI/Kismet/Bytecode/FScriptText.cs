using Newtonsoft.Json;
using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode
{
    /// <summary>
    /// Represents an FText as serialized in Kismet bytecode.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FScriptText
    {
        [JsonProperty]
        public EBlueprintTextLiteralType TextLiteralType;

        /// <summary>
        /// Source of this text if it is localized text. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.InvariantText"/>.
        /// </summary>
        [JsonProperty]
        public KismetExpression LocalizedSource;

        /// <summary>
        /// Key of this text if it is localized text. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.InvariantText"/>.
        /// </summary>
        [JsonProperty]
        public KismetExpression LocalizedKey;

        /// <summary>
        /// Namespace of this text if it is localized text. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.InvariantText"/>.
        /// </summary>
        [JsonProperty]
        public KismetExpression LocalizedNamespace;

        /// <summary>
        /// Value of this text if it is an invariant string literal. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.InvariantText"/>.
        /// </summary>
        [JsonProperty]
        public KismetExpression InvariantLiteralString;

        /// <summary>
        /// Value of this text if it is a string literal. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.LiteralString"/>.
        /// </summary>
        [JsonProperty]
        public KismetExpression LiteralString;

        /// <summary>
        /// Pointer to this text's UStringTable. Not used at runtime, but exists for asset dependency gathering. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.StringTableEntry"/>.
        /// </summary>
        [JsonProperty]
        public FPackageIndex StringTableAsset;

        /// <summary>
        /// Table ID string literal (namespace). Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.StringTableEntry"/>.
        /// </summary>
        [JsonProperty]
        public KismetExpression StringTableId;

        /// <summary>
        /// String table key string literal. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.StringTableEntry"/>.
        /// </summary>
        [JsonProperty]
        public KismetExpression StringTableKey;

        /// <summary>
        /// Reads out an FBlueprintText from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public virtual void Read(AssetBinaryReader reader)
        {
            TextLiteralType = (EBlueprintTextLiteralType)reader.ReadByte();
            switch (TextLiteralType)
            {
                case EBlueprintTextLiteralType.Empty:
                    break;
                case EBlueprintTextLiteralType.LocalizedText:
                    LocalizedSource = ExpressionSerializer.ReadExpression(reader);
                    LocalizedKey = ExpressionSerializer.ReadExpression(reader);
                    LocalizedNamespace = ExpressionSerializer.ReadExpression(reader);
                    break;
                case EBlueprintTextLiteralType.InvariantText: // IsCultureInvariant
                    InvariantLiteralString = ExpressionSerializer.ReadExpression(reader);
                    break;
                case EBlueprintTextLiteralType.LiteralString:
                    LiteralString = ExpressionSerializer.ReadExpression(reader);
                    break;
                case EBlueprintTextLiteralType.StringTableEntry:
                    StringTableAsset = reader.XFER_OBJECT_POINTER();
                    StringTableId = ExpressionSerializer.ReadExpression(reader);
                    StringTableKey = ExpressionSerializer.ReadExpression(reader);
                    break;
                default:
                    throw new NotImplementedException("Unimplemented blueprint text literal type " + TextLiteralType);
            }

        }

        /// <summary>
        /// Writes an FBlueprintText to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public virtual int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            writer.Write((byte)TextLiteralType); offset += sizeof(byte);
            switch (TextLiteralType)
            {
                case EBlueprintTextLiteralType.Empty:
                    break;
                case EBlueprintTextLiteralType.LocalizedText:
                    offset += ExpressionSerializer.WriteExpression(LocalizedSource, writer);
                    offset += ExpressionSerializer.WriteExpression(LocalizedKey, writer);
                    offset += ExpressionSerializer.WriteExpression(LocalizedNamespace, writer);
                    break;
                case EBlueprintTextLiteralType.InvariantText: // IsCultureInvariant
                    offset += ExpressionSerializer.WriteExpression(InvariantLiteralString, writer);
                    break;
                case EBlueprintTextLiteralType.LiteralString:
                    offset += ExpressionSerializer.WriteExpression(LiteralString, writer);
                    break;
                case EBlueprintTextLiteralType.StringTableEntry:
                    offset += writer.XFER_OBJECT_POINTER(StringTableAsset);
                    offset += ExpressionSerializer.WriteExpression(StringTableId, writer);
                    offset += ExpressionSerializer.WriteExpression(StringTableKey, writer);
                    break;
                default:
                    throw new NotImplementedException("Unimplemented blueprint text literal type " + TextLiteralType);
            }
            return offset;
        }
    }
}
