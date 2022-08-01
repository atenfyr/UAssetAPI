using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_Context"/> instruction.
    /// </summary>
    public class EX_Context : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_Context; } }

        /// <summary>
        /// Object expression.
        /// </summary>
        [JsonProperty]
        public KismetExpression ObjectExpression;

        /// <summary>
        /// Code offset for NULL expressions.
        /// </summary>
        [JsonProperty]
        public uint Offset;

        /// <summary>
        /// Property corresponding to the r-value data, in case the l-value needs to be mem-zero'd. FField*
        /// </summary>
        [JsonProperty]
        public KismetPropertyPointer RValuePointer;

        /// <summary>
        /// Context expression.
        /// </summary>
        [JsonProperty]
        public KismetExpression ContextExpression;

        public EX_Context()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            ObjectExpression = ExpressionSerializer.ReadExpression(reader);
            Offset = reader.ReadUInt32();
            RValuePointer = reader.XFER_PROP_POINTER();
            ContextExpression = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += ExpressionSerializer.WriteExpression(ObjectExpression, writer);
            writer.Write(Offset); offset += sizeof(uint);
            offset += writer.XFER_PROP_POINTER(RValuePointer);
            offset += ExpressionSerializer.WriteExpression(ContextExpression, writer);
            return offset;
        }
    }
}
