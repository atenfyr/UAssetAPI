using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_JumpIfNot"/> instruction.
    /// Conditional equivalent of the <see cref="EExprToken.EX_Jump"/> expression.
    /// </summary>
    public class EX_JumpIfNot : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_JumpIfNot; } }

        /// <summary>
        /// The offset to jump to if the provided expression evaluates to false.
        /// </summary>
        [JsonProperty]
        public uint CodeOffset;

        /// <summary>
        /// Expression to evaluate to determine whether or not a jump should be performed.
        /// </summary>
        [JsonProperty]
        public KismetExpression BooleanExpression;

        public EX_JumpIfNot()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            CodeOffset = reader.ReadUInt32();
            BooleanExpression = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            writer.Write(CodeOffset); offset += sizeof(uint);
            offset += ExpressionSerializer.WriteExpression(BooleanExpression, writer);
            return offset;
        }
    }
}
