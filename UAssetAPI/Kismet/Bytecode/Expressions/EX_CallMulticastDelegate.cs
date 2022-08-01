using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_CallMulticastDelegate"/> instruction.
    /// </summary>
    public class EX_CallMulticastDelegate : EX_FinalFunction
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_CallMulticastDelegate; } }

        /// <summary>
        /// Delegate property.
        /// </summary>
        [JsonProperty]
        public KismetExpression Delegate;

        public EX_CallMulticastDelegate()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            StackNode = reader.XFER_FUNC_POINTER();
            Delegate = ExpressionSerializer.ReadExpression(reader);
            Parameters = reader.ReadExpressionArray(EExprToken.EX_EndFunctionParms);

        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += writer.XFER_FUNC_POINTER(StackNode);
            offset += ExpressionSerializer.WriteExpression(Delegate, writer);

            for (int i = 0; i < Parameters.Length; i++) 
            {
                offset += ExpressionSerializer.WriteExpression(Parameters[i], writer);
            }
            offset += ExpressionSerializer.WriteExpression(new EX_EndFunctionParms(), writer);
            return offset;
        }
    }
}
