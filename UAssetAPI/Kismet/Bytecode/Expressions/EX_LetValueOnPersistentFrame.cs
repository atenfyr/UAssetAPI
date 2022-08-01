using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_LetValueOnPersistentFrame"/> instruction.
    /// </summary>
    public class EX_LetValueOnPersistentFrame : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_LetValueOnPersistentFrame; } }

        /// <summary>
        /// Destination property pointer.
        /// </summary>
        [JsonProperty]
        public KismetPropertyPointer DestinationProperty;

        /// <summary>
        /// Assignment expression.
        /// </summary>
        [JsonProperty]
        public KismetExpression AssignmentExpression;

        public EX_LetValueOnPersistentFrame()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            DestinationProperty = reader.XFER_PROP_POINTER();
            AssignmentExpression = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += writer.XFER_PROP_POINTER(DestinationProperty);
            offset += ExpressionSerializer.WriteExpression(AssignmentExpression, writer);
            return offset;
        }
    }
}
