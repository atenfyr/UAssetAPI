using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_Let"/> instruction.
    /// </summary>
    public class EX_Let : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_Let; } }

        /// <summary>
        /// A pointer to the variable.
        /// </summary>
        [JsonProperty]
        public KismetPropertyPointer Value;
        [JsonProperty]
        public KismetExpression Variable;
        [JsonProperty]
        public KismetExpression Expression;

        public EX_Let()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            if (reader.Asset.ObjectVersion > ObjectVersion.VER_UE4_SERIALIZE_BLUEPRINT_EVENTGRAPH_FASTCALLS_IN_UFUNCTION)
            {
                Value = reader.XFER_PROP_POINTER();
            }
            Variable = ExpressionSerializer.ReadExpression(reader);
            Expression = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            if (writer.Asset.ObjectVersion > ObjectVersion.VER_UE4_SERIALIZE_BLUEPRINT_EVENTGRAPH_FASTCALLS_IN_UFUNCTION)
            {
                offset += writer.XFER_PROP_POINTER(Value);
            }
            offset += ExpressionSerializer.WriteExpression(Variable, writer);
            offset += ExpressionSerializer.WriteExpression(Expression, writer);
            return offset;
        }
    }
}
