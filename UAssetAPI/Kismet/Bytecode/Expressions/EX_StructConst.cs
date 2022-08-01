using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_StructConst"/> instruction.
    /// </summary>
    public class EX_StructConst : KismetExpression<KismetExpression[]>
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_StructConst; } }

        /// <summary>
        /// Pointer to the UScriptStruct in question.
        /// </summary>
        [JsonProperty]
        public FPackageIndex Struct;

        /// <summary>
        /// The size of the struct that this constant represents in memory in bytes.
        /// </summary>
        [JsonProperty]
        public int StructSize;

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
            StructSize = reader.ReadInt32();
            Value = reader.ReadExpressionArray(EExprToken.EX_EndStructConst);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            offset += writer.XFERPTR(Struct);
            writer.Write(StructSize); offset += sizeof(int);

            for (int i = 0; i < Value.Length; i++)
            {
                offset += ExpressionSerializer.WriteExpression(Value[i], writer);
            }

            // Write end expression
            offset += ExpressionSerializer.WriteExpression(new EX_EndStructConst(), writer);
            return offset;
        }
    }
}
