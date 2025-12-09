using Newtonsoft.Json;
using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// Base class for several Let (assignment) expressions
    /// </summary>
    public abstract class EX_LetBase : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_LetDelegate; } }

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

        public override void Visit(UAsset asset, ref uint offset, Action<KismetExpression, uint> visitor)
        {
            base.Visit(asset, ref offset, visitor);
            VariableExpression.Visit(asset, ref offset, visitor);
            AssignmentExpression.Visit(asset, ref offset, visitor);
        }
    }
}
