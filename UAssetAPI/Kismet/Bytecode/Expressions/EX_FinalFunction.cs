using System.Collections.Generic;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_FinalFunction"/> instruction.
    /// </summary>
    public class EX_FinalFunction : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_FinalFunction; } }

        /// <summary>
        /// Stack node.
        /// </summary>
        public ulong StackNode;

        /// <summary>
        /// List of parameters for this function.
        /// </summary>
        public KismetExpression[] Parameters;

        public EX_FinalFunction()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            reader.XFER_FUNC_POINTER();

            Parameters = reader.ReadExpressionArray(EExprToken.EX_EndFunctionParms);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.XFER_FUNC_POINTER(StackNode);

            for (int i = 0; i < Parameters.Length; i++)
            {
                ExpressionSerializer.WriteExpression(Parameters[i], writer);
            }
            ExpressionSerializer.WriteExpression(new EX_EndFunctionParms(), writer);
            return 0;
        }
    }
}
