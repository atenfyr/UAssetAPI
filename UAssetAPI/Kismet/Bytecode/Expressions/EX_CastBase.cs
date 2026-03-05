using Newtonsoft.Json;
using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// Base class for several type cast expressions
    /// </summary>
    public abstract class EX_CastBase : KismetExpression
    {
        /// <summary>
        /// The interface class to convert to.
        /// </summary>
        [JsonProperty]
        public FPackageIndex ClassPtr;

        /// <summary>
        /// The target of this expression.
        /// </summary>
        [JsonProperty]
        public KismetExpression Target;

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            ClassPtr = reader.XFER_OBJECT_POINTER();
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
            offset += writer.XFER_OBJECT_POINTER(ClassPtr);
            offset += ExpressionSerializer.WriteExpression(Target, writer);
            return offset;
        }

        public override void Visit(UAsset asset, ref uint offset, Action<KismetExpression, uint> visitor)
        {
            base.Visit(asset, ref offset, visitor);
            offset += 8; // ClassPtr (8)
            Target.Visit(asset, ref offset, visitor);
        }
    }
}
