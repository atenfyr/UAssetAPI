using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_ArrayGetByRef"/> instruction.
    /// </summary>
    public class EX_ArrayGetByRef : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_ArrayGetByRef; } }

        /// <summary>
        /// The array variable.
        /// </summary>
        [JsonProperty]
        public KismetExpression ArrayVariable;

        /// <summary>
        /// The index to access in the array.
        /// </summary>
        [JsonProperty]
        public KismetExpression ArrayIndex;

        public EX_ArrayGetByRef()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            ArrayVariable = ExpressionSerializer.ReadExpression(reader);
            ArrayIndex = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += ExpressionSerializer.WriteExpression(ArrayVariable, writer);
            offset += ExpressionSerializer.WriteExpression(ArrayIndex, writer);
            return offset;
        }
    }
}
