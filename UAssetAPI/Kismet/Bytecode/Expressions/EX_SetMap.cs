using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_SetMap"/> instruction.
    /// </summary>
    public class EX_SetMap : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_SetMap; } }

        /// <summary>
        /// Map property.
        /// </summary>
        [JsonProperty]
        public KismetExpression MapProperty;

        /// <summary>
        /// Set entries.
        /// </summary>
        [JsonProperty]
        public KismetExpression[] Elements;

        public EX_SetMap()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            MapProperty = ExpressionSerializer.ReadExpression(reader);
            int numEntries = reader.ReadInt32(); // Number of elements
            Elements = reader.ReadExpressionArray(EExprToken.EX_EndMap);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += ExpressionSerializer.WriteExpression(MapProperty, writer);
            // count is number if pairs so is half the number of elements
            writer.Write(Elements.Length / 2); offset += sizeof(int);
            for (int i = 0; i < Elements.Length; i++)
            {
                offset += ExpressionSerializer.WriteExpression(Elements[i], writer);
            }
            offset += ExpressionSerializer.WriteExpression(new EX_EndMap(), writer);
            return offset;
        }
    }
}
