using Newtonsoft.Json;
using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_BitFieldConst"/> instruction.
    /// Assigns to a single bit, defined by an FProperty.
    /// </summary>
    public class EX_BitFieldConst : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_BitFieldConst; } }

        /// <summary>
        /// A pointer to the bit property.
        /// </summary>
        [JsonProperty]
        public KismetPropertyPointer Property;

        /// <summary>
        /// The bit value.
        /// </summary>
        [JsonProperty]
        public byte Value;

        public EX_BitFieldConst()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            Property = reader.XFER_PROP_POINTER();
            Value = reader.ReadByte();
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = writer.XFER_PROP_POINTER(Property);
            writer.Write(Value);
            return offset + sizeof(byte);
        }

        public override void Visit(UAsset asset, ref uint offset, Action<KismetExpression, uint> visitor)
        {
            base.Visit(asset, ref offset, visitor);
            offset += 9; // Property (8) + Value (1)
        }
    }
}
