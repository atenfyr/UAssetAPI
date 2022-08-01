using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_CrossInterfaceCast"/> instruction.
    /// </summary>
    public class EX_CrossInterfaceCast : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_CrossInterfaceCast; } }

        /// <summary>
        /// A pointer to the interface class to convert to.
        /// </summary>
        [JsonProperty]
        public FPackageIndex ClassPtr;

        /// <summary>
        /// The target of this expression.
        /// </summary>
        [JsonProperty]
        public KismetExpression Target;

        public EX_CrossInterfaceCast()
        {

        }

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
    }
}
