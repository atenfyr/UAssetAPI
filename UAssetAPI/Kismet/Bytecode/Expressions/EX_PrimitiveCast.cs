using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_PrimitiveCast"/> instruction.
    /// </summary>
    public class EX_PrimitiveCast : KismetExpression
    {
        /// <summary>
        /// The type to cast to.
        /// </summary>
        [JsonProperty]
        public ECastToken ConversionType;

        /// <summary>
        /// The target of this expression.
        /// </summary>
        [JsonProperty]
        public KismetExpression Target;

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
            ConversionType = (ECastToken)reader.ReadByte();
            Target = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            writer.Write((byte)ConversionType); offset += sizeof(byte);
            offset += ExpressionSerializer.WriteExpression(Target, writer);
            return offset;
        }
    }
}
