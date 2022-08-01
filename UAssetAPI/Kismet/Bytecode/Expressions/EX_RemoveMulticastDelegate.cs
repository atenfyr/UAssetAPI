using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_RemoveMulticastDelegate"/> instruction.
    /// </summary>
    public class EX_RemoveMulticastDelegate : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_RemoveMulticastDelegate; } }

        /// <summary>
        /// Delegate property to assign to.
        /// </summary>
        [JsonProperty]
        public KismetExpression Delegate;

        /// <summary>
        /// Delegate to add to the MC delegate for broadcast.
        /// </summary>
        [JsonProperty]
        public KismetExpression DelegateToAdd;

        public EX_RemoveMulticastDelegate()
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
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += ExpressionSerializer.WriteExpression(Delegate, writer);
            offset += ExpressionSerializer.WriteExpression(DelegateToAdd, writer);
            return offset;
        }
    }
}
