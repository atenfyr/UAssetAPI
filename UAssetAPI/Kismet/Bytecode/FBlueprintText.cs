using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAssetAPI.Kismet.Bytecode
{
    /// <summary>
    /// Represents an FText as serialized in Kismet bytecode.
    /// </summary>
    public class FBlueprintText
    {
        public EBlueprintTextLiteralType TextLiteralType;

        /// <summary>
        /// Source of this text if it is localized text. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.InvariantText"/>.
        /// </summary>
        public Expression LocalizedSource;

        /// <summary>
        /// Key of this text if it is localized text. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.InvariantText"/>.
        /// </summary>
        public Expression LocalizedKey;

        /// <summary>
        /// Namespace of this text if it is localized text. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.InvariantText"/>.
        /// </summary>
        public Expression LocalizedNamespace;

        /// <summary>
        /// Value of this text if it is an invariant string literal. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.InvariantText"/>.
        /// </summary>
        public Expression InvariantLiteralString;

        /// <summary>
        /// Value of this text if it is a string literal. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.LiteralString"/>.
        /// </summary>
        public Expression LiteralString;

        /// <summary>
        /// Pointer to this text's UStringTable. Not used at runtime, but exists for asset dependency gathering. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.StringTableEntry"/>.
        /// </summary>
        public ulong StringTableAsset;

        /// <summary>
        /// Table ID string literal (namespace). Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.StringTableEntry"/>.
        /// </summary>
        public Expression StringTableId;

        /// <summary>
        /// String table key string literal. Used when <see cref="TextLiteralType"/> is <see cref="EBlueprintTextLiteralType.StringTableEntry"/>.
        /// </summary>
        public Expression StringTableKey;

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
        /// <returns>The length in bytes of the data that was written.</returns>
        public virtual int Write(AssetBinaryWriter writer)
        {
            writer.Write((byte)TextLiteralType);
            switch (TextLiteralType)
            {
                case EBlueprintTextLiteralType.Empty:
                    break;
                case EBlueprintTextLiteralType.LocalizedText:
                    ExpressionSerializer.WriteExpression(LocalizedSource, writer);
                    ExpressionSerializer.WriteExpression(LocalizedKey, writer);
                    ExpressionSerializer.WriteExpression(LocalizedNamespace, writer);
                    break;
                case EBlueprintTextLiteralType.InvariantText: // IsCultureInvariant
                    ExpressionSerializer.WriteExpression(InvariantLiteralString, writer);
                    break;
                case EBlueprintTextLiteralType.LiteralString:
                    ExpressionSerializer.WriteExpression(LiteralString, writer);
                    break;
                case EBlueprintTextLiteralType.StringTableEntry:
                    writer.XFER_OBJECT_POINTER(StringTableAsset);
                    ExpressionSerializer.WriteExpression(StringTableId, writer);
                    ExpressionSerializer.WriteExpression(StringTableKey, writer);
                    break;
                default:
                    throw new NotImplementedException("Unimplemented blueprint text literal type " + TextLiteralType);
            }
            return 0;
        }
    }
}
