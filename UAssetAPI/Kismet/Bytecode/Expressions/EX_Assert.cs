namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_Assert"/> instruction.
    /// </summary>
    public class EX_Assert : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_Assert; } }

        /// <summary>
        /// Line number.
        /// </summary>
        public ushort LineNumber;

        /// <summary>
        /// Whether or not this assertion is in debug mode.
        /// </summary>
        public bool DebugMode;

        /// <summary>
        /// Expression to assert.
        /// </summary>
        public KismetExpression AssertExpression;

        public EX_Assert()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            LineNumber = reader.ReadUInt16();
            DebugMode = reader.ReadBoolean();
            AssertExpression = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.Write(LineNumber);
            writer.Write(DebugMode);
            ExpressionSerializer.WriteExpression(AssertExpression, writer);
            return 0;
        }
    }
}
