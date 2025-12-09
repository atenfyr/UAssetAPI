using System;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_UnicodeStringConst"/> instruction.
    /// </summary>
    public class EX_UnicodeStringConst : KismetExpression<string>
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_UnicodeStringConst; } }

        public EX_UnicodeStringConst()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            Value = reader.XFERUNICODESTRING();
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            return writer.XFERUNICODESTRING(Value);
        }

        public override void Visit(UAsset asset, ref uint offset, Action<KismetExpression, uint> visitor)
        {
            base.Visit(asset, ref offset, visitor);
            offset += 2 * ((uint)Value.Length + 1); // Unicode string + null terminator (2 bytes per char)
        }
    }
}
