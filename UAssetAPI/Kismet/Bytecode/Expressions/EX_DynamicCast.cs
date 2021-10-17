namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_DynamicCast"/> instruction.
    /// </summary>
    public class EX_DynamicCast : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_DynamicCast; } }

        /// <summary>
        /// A pointer to the relevant class (UClass*).
        /// </summary>
        public ulong ClassPtr;

        /// <summary>
        /// The target expression.
        /// </summary>
        public KismetExpression TargetExpression;

        public EX_DynamicCast()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            ClassPtr = reader.XFER_OBJECT_POINTER();
            TargetExpression = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.Write(ClassPtr);
            ExpressionSerializer.WriteExpression(TargetExpression, writer);
            return 0;
        }
    }
}
