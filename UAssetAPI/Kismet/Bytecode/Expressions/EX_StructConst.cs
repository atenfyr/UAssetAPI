using System.Collections.Generic;
using System.IO;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_StructConst"/> instruction.
    /// </summary>
    public class EX_StructConst : Expression<KismetExpression[]>
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_StructConst; } }

        /// <summary>
        /// Pointer to the UScriptStruct in question.
        /// </summary>
        public ulong Struct;

        public EX_StructConst()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            Struct = reader.XFERPTR();
            reader.ReadInt32(); // Struct size in bytes
            Value = reader.ReadExpressionArray(EExprToken.EX_EndStructConst);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.XFERPTR(Struct);
            long lengthPos = writer.BaseStream.Position;
            writer.Write((int)0);

            long startMetric = writer.BaseStream.Position;

            for (int i = 0; i < Value.Length; i++)
            {
                ExpressionSerializer.WriteExpression(Value[i], writer);
            }

            long endMetric = writer.BaseStream.Position;

            // Write end expression
            ExpressionSerializer.WriteExpression(new EX_EndStructConst(), writer);

            // Write out struct size in bytes
            long totalLength = endMetric - startMetric;
            long finalLoc = writer.BaseStream.Position;
            writer.Seek((int)lengthPos, SeekOrigin.Begin);
            writer.Write((int)totalLength);
            writer.Seek((int)finalLoc, SeekOrigin.Begin);
            return 0;
        }
    }
}
