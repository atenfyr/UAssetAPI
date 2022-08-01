using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

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
        [JsonProperty]
        public ushort LineNumber;

        /// <summary>
        /// Whether or not this assertion is in debug mode.
        /// </summary>
        [JsonProperty]
        public bool DebugMode;

        /// <summary>
        /// Expression to assert.
        /// </summary>
        [JsonProperty]
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
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            writer.Write(LineNumber); offset += sizeof(ushort);
            writer.Write(DebugMode); offset += sizeof(bool);
            offset += ExpressionSerializer.WriteExpression(AssertExpression, writer);
            return offset;
        }
    }
}
