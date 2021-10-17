namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_LetMulticastDelegate"/> instruction.
    /// </summary>
    public class EX_LetMulticastDelegate : Expression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_LetMulticastDelegate; } }

        /// <summary>
        /// Variable expression.
        /// </summary>
        public Expression VariableExpression;

        /// <summary>
        /// Assignment expression.
        /// </summary>
        public Expression AssignmentExpression;

        public EX_LetMulticastDelegate()
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
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            ExpressionSerializer.WriteExpression(VariableExpression, writer);
            ExpressionSerializer.WriteExpression(AssignmentExpression, writer);
            return 0;
        }
    }
}
