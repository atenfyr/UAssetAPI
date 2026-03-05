using Newtonsoft.Json;
using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// Base class for Kismet Variable expressions
    /// </summary>
    public abstract class EX_VariableBase : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_ClassSparseDataVariable; } }

        /// <summary>
        /// A pointer to the variable in question.
        /// </summary>
        [JsonProperty]
        public KismetPropertyPointer Variable;

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            Variable = reader.XFER_PROP_POINTER();
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            return writer.XFER_PROP_POINTER(Variable);
        }

        public override void Visit(UAsset asset, ref uint offset, Action<KismetExpression, uint> visitor)
        {
            base.Visit(asset, ref offset, visitor);
            offset += 8; // Variable (KismetPropertyPointer)
        }
    }
}
