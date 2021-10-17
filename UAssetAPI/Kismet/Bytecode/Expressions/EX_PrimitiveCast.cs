namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_PrimitiveCast"/> instruction.
    /// </summary>
    public class EX_PrimitiveCast : Expression
    {
        /// <summary>
        /// The type to cast to.
        /// </summary>
        public EExprToken ConversionType;

        /// <summary>
        /// The target of this expression.
        /// </summary>
        public Expression Target;

        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_PrimitiveCast; } }

        public EX_PrimitiveCast()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            ConversionType = (EExprToken)reader.ReadByte();
            Target = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.Write((byte)ConversionType);
            ExpressionSerializer.WriteExpression(Target, writer);
            return 0;
        }
    }
}
