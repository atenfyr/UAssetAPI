namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_SetArray"/> instruction.
    /// </summary>
    public class EX_SetArray : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_SetArray; } }

        /// <summary>
        /// Array property to assign to
        /// </summary>
        public KismetExpression AssigningProperty;

        /// <summary>
        /// Pointer to the array inner property (FProperty*).
        /// Only used in engine versions prior to <see cref="UE4Version.VER_UE4_CHANGE_SETARRAY_BYTECODE"/>.
        /// </summary>
        public ulong ArrayInnerProp;

        /// <summary>
        /// Array items.
        /// </summary>
        public KismetExpression[] Elements;

        public EX_SetArray()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            if (reader.Asset.EngineVersion >= UE4Version.VER_UE4_CHANGE_SETARRAY_BYTECODE)
            {
                AssigningProperty = ExpressionSerializer.ReadExpression(reader);
            }
            else
            {
                ArrayInnerProp = reader.XFERPTR();
            }

            Elements = reader.ReadExpressionArray(EExprToken.EX_EndArray);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            if (writer.Asset.EngineVersion >= UE4Version.VER_UE4_CHANGE_SETARRAY_BYTECODE)
            {
                ExpressionSerializer.WriteExpression(AssigningProperty, writer);
            }
            else
            {
                writer.XFERPTR(ArrayInnerProp);
            }

            for (int i = 0; i < Elements.Length; i++)
            {
                ExpressionSerializer.WriteExpression(Elements[i], writer);
            }
            ExpressionSerializer.WriteExpression(new EX_EndArray(), writer);
            return 0;
        }
    }
}
