namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_AddMulticastDelegate"/> instruction.
    /// </summary>
    public class EX_AddMulticastDelegate : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_AddMulticastDelegate; } }

        /// <summary>
        /// Delegate property to assign to.
        /// </summary>
        public KismetExpression Delegate;

        /// <summary>
        /// Delegate to add to the MC delegate for broadcast.
        /// </summary>
        public KismetExpression DelegateToAdd;

        public EX_AddMulticastDelegate()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            Delegate = ExpressionSerializer.ReadExpression(reader);
            DelegateToAdd = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            ExpressionSerializer.WriteExpression(Delegate, writer);
            ExpressionSerializer.WriteExpression(DelegateToAdd, writer);
            return 0;
        }
    }
}
