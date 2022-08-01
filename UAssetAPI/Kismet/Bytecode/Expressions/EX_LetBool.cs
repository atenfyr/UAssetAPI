using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_LetBool"/> instruction.
    /// </summary>
    public class EX_LetBool : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_LetBool; } }

        /// <summary>
        /// Variable expression.
        /// </summary>
        [JsonProperty]
        public KismetExpression VariableExpression;

        /// <summary>
        /// Assignment expression.
        /// </summary>
        [JsonProperty]
        public KismetExpression AssignmentExpression;
        public EX_LetBool()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            VariableExpression = ExpressionSerializer.ReadExpression(reader);
            AssignmentExpression = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += ExpressionSerializer.WriteExpression(VariableExpression, writer);
            offset += ExpressionSerializer.WriteExpression(AssignmentExpression, writer);
            return offset;
        }
    }
}
