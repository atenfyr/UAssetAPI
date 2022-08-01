using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_PopExecutionFlowIfNot"/> instruction.
    /// Conditional equivalent of the <see cref="EExprToken.EX_PopExecutionFlow"/> expression.
    /// </summary>
    public class EX_PopExecutionFlowIfNot : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_PopExecutionFlowIfNot; } }

        /// <summary>
        /// Expression to evaluate to determine whether or not a pop should be performed.
        /// </summary>
        [JsonProperty]
        public KismetExpression BooleanExpression;

        public EX_PopExecutionFlowIfNot()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            BooleanExpression = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            return ExpressionSerializer.WriteExpression(BooleanExpression, writer);
        }
    }
}
