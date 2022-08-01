using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_MapConst"/> instruction.
    /// </summary>
    public class EX_MapConst : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_MapConst; } }

        /// <summary>
        /// Pointer to this constant's key property (FProperty*).
        /// </summary>
        [JsonProperty]
        public KismetPropertyPointer KeyProperty;

        /// <summary>
        /// Pointer to this constant's value property (FProperty*).
        /// </summary>
        [JsonProperty]
        public KismetPropertyPointer ValueProperty;

        /// <summary>
        /// Set constant entries.
        /// </summary>
        [JsonProperty]
        public KismetExpression[] Elements;
        
        public EX_MapConst()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            KeyProperty = reader.XFER_PROP_POINTER();
            ValueProperty = reader.XFER_PROP_POINTER();
            int numEntries = reader.ReadInt32(); // Number of elements
            Elements = reader.ReadExpressionArray(EExprToken.EX_EndMapConst);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += writer.XFER_PROP_POINTER(KeyProperty);
            offset += writer.XFER_PROP_POINTER(ValueProperty);
            writer.Write(Elements.Length); offset += sizeof(int);
            for (int i = 0; i < Elements.Length; i++)
            {
                offset += ExpressionSerializer.WriteExpression(Elements[i], writer);
            }
            offset += ExpressionSerializer.WriteExpression(new EX_EndMapConst(), writer);
            return offset;
        }
    }
}
