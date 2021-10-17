namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_ArrayGetByRef"/> instruction.
    /// </summary>
    public class EX_ArrayGetByRef : Expression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_ArrayGetByRef; } }

        /// <summary>
        /// The array variable.
        /// </summary>
        public Expression ArrayVariable;

        /// <summary>
        /// The index to access in the array.
        /// </summary>
        public Expression ArrayIndex;

        public EX_ArrayGetByRef()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            Expression ArrayVariable = ExpressionSerializer.ReadExpression(reader);
            Expression ArrayIndex = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            ExpressionSerializer.WriteExpression(ArrayVariable, writer);
            ExpressionSerializer.WriteExpression(ArrayIndex, writer);
            return 0;
        }
    }
}
