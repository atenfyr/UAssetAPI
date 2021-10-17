namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_MapConst"/> instruction.
    /// </summary>
    public class EX_MapConst : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_MapConst; } }

        /// <summary>
        /// Pointer to this constant's key property (FProperty*).
        /// </summary>
        public ulong KeyProperty;

        /// <summary>
        /// Pointer to this constant's value property (FProperty*).
        /// </summary>
        public ulong ValueProperty;

        /// <summary>
        /// Set constant entries.
        /// </summary>
        public KismetExpression[] Elements;
        public EX_MapConst()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            KeyProperty = reader.XFERPTR();
            ValueProperty = reader.XFERPTR();
            int numEntries = reader.ReadInt32(); // Number of elements
            Elements = reader.ReadExpressionArray(EExprToken.EX_EndMapConst);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.XFERPTR(KeyProperty);
            writer.XFERPTR(ValueProperty);
            writer.Write(Elements.Length);
            for (int i = 0; i < Elements.Length; i++)
            {
                ExpressionSerializer.WriteExpression(Elements[i], writer);
            }
            ExpressionSerializer.WriteExpression(new EX_EndMapConst(), writer);
            return 0;
        }
    }
}
