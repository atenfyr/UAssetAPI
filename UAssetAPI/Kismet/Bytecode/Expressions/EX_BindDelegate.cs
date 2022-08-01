using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_BindDelegate"/> instruction.
    /// </summary>
    public class EX_BindDelegate : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_BindDelegate; } }

        /// <summary>
        /// The name of the function assigned to the delegate.
        /// </summary>
        [JsonProperty]
        public FName FunctionName;

        /// <summary>
        /// Delegate property to assign to.
        /// </summary>
        [JsonProperty]
        public KismetExpression Delegate;

        /// <summary>
        /// Object to bind.
        /// </summary>
        [JsonProperty]
        public KismetExpression ObjectTerm;

        public EX_BindDelegate()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            FunctionName = reader.XFER_FUNC_NAME();
            Delegate = ExpressionSerializer.ReadExpression(reader);
            ObjectTerm = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += writer.XFER_FUNC_NAME(FunctionName);
            offset += ExpressionSerializer.WriteExpression(Delegate, writer);
            offset += ExpressionSerializer.WriteExpression(ObjectTerm, writer);
            return offset;
        }
    }
}
