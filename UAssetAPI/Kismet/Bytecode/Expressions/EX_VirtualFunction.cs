using Newtonsoft.Json;
using System.Collections.Generic;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_VirtualFunction"/> instruction.
    /// </summary>
    public class EX_VirtualFunction : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_VirtualFunction; } }

        /// <summary>
        /// Virtual function name.
        /// </summary>
        [JsonProperty]
        public FName VirtualFunctionName;

        /// <summary>
        /// List of parameters for this function.
        /// </summary>
        [JsonProperty]
        public KismetExpression[] Parameters;

        public EX_VirtualFunction()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            VirtualFunctionName = reader.XFER_FUNC_NAME();

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
            offset += writer.XFER_FUNC_NAME(VirtualFunctionName);

            for (int i = 0; i < Parameters.Length; i++)
            {
                offset += ExpressionSerializer.WriteExpression(Parameters[i], writer);
            }
            offset += ExpressionSerializer.WriteExpression(new EX_EndFunctionParms(), writer);
            return offset;
        }
    }
}
