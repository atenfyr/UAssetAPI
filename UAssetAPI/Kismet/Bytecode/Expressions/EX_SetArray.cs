using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_SetArray"/> instruction.
    /// </summary>
    public class EX_SetArray : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_SetArray; } }

        /// <summary>
        /// Array property to assign to
        /// </summary>
        [JsonProperty]
        public KismetExpression AssigningProperty;

        /// <summary>
        /// Pointer to the array inner property (FProperty*).
        /// Only used in engine versions prior to <see cref="ObjectVersion.VER_UE4_CHANGE_SETARRAY_BYTECODE"/>.
        /// </summary>
        [JsonProperty]
        public FPackageIndex ArrayInnerProp;

        /// <summary>
        /// Array items.
        /// </summary>
        [JsonProperty]
        public KismetExpression[] Elements;

        public EX_SetArray()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_CHANGE_SETARRAY_BYTECODE)
            {
                AssigningProperty = ExpressionSerializer.ReadExpression(reader);
            }
            else
            {
                ArrayInnerProp = reader.XFERPTR();
            }

            Elements = reader.ReadExpressionArray(EExprToken.EX_EndArray);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_CHANGE_SETARRAY_BYTECODE)
            {
                offset += ExpressionSerializer.WriteExpression(AssigningProperty, writer);
            }
            else
            {
                offset += writer.XFERPTR(ArrayInnerProp);
            }

            for (int i = 0; i < Elements.Length; i++)
            {
                offset += ExpressionSerializer.WriteExpression(Elements[i], writer);
            }
            offset += ExpressionSerializer.WriteExpression(new EX_EndArray(), writer);
            return offset;
        }
    }
}
