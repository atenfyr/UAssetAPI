using System;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_NothingInt32"/> instruction.
    /// No operation with an int32 argument (useful for debugging script disassembly).
    /// </summary>
    public class EX_NothingInt32 : KismetExpression<int>
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_NothingInt32; } }

        public EX_NothingInt32()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            Value = reader.ReadInt32();
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.Write(Value);
            return sizeof(int);
        }

        public override void Visit(UAsset asset, ref uint offset, Action<KismetExpression, uint> visitor)
        {
            base.Visit(asset, ref offset, visitor);
            offset += 4; // Value
        }
    }
}
