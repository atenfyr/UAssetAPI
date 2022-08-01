using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// Represents a case in a Kismet bytecode switch statement.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public struct FKismetSwitchCase
    {
        /// <summary>
        /// The index value term of this case.
        /// </summary>
        [JsonProperty]
        public KismetExpression CaseIndexValueTerm;

        /// <summary>
        /// Code offset to the next case.
        /// </summary>
        [JsonProperty]
        public uint NextOffset;

        /// <summary>
        /// The main case term.
        /// </summary>
        [JsonProperty]
        public KismetExpression CaseTerm;

        public FKismetSwitchCase(KismetExpression caseIndexValueTerm, uint nextOffset, KismetExpression caseTerm)
        {
            CaseIndexValueTerm = caseIndexValueTerm;
            NextOffset = nextOffset;
            CaseTerm = caseTerm;
        }
    }

    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_SwitchValue"/> instruction.
    /// </summary>
    public class EX_SwitchValue : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_SwitchValue; } }

        /// <summary>
        /// Code offset to jump to when finished.
        /// </summary>
        [JsonProperty]
        public uint EndGotoOffset;

        /// <summary>
        /// The index term of this switch statement.
        /// </summary>
        [JsonProperty]
        public KismetExpression IndexTerm;

        /// <summary>
        /// The default term of this switch statement.
        /// </summary>
        [JsonProperty]
        public KismetExpression DefaultTerm;

        /// <summary>
        /// All the cases in this switch statement.
        /// </summary>
        [JsonProperty]
        public FKismetSwitchCase[] Cases;

        public EX_SwitchValue()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            ushort numCases = reader.ReadUInt16(); // number of cases, without default one
            EndGotoOffset = reader.ReadUInt32();
            IndexTerm = ExpressionSerializer.ReadExpression(reader);

            Cases = new FKismetSwitchCase[numCases];
            for (int i = 0; i < numCases; i++)
            {
                KismetExpression termA = ExpressionSerializer.ReadExpression(reader);
                uint termB = reader.ReadUInt32();
                KismetExpression termC = ExpressionSerializer.ReadExpression(reader);
                Cases[i] = new FKismetSwitchCase(termA, termB, termC);
            }

            DefaultTerm = ExpressionSerializer.ReadExpression(reader);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            int offset = 0;
            writer.Write((ushort)Cases.Length); offset += sizeof(ushort);
            writer.Write(EndGotoOffset); offset += sizeof(uint);
            offset += ExpressionSerializer.WriteExpression(IndexTerm, writer);
            for (int i = 0; i < Cases.Length; i++)
            {
                offset += ExpressionSerializer.WriteExpression(Cases[i].CaseIndexValueTerm, writer);
                writer.Write(Cases[i].NextOffset); offset += sizeof(uint);
                offset += ExpressionSerializer.WriteExpression(Cases[i].CaseTerm, writer);
            }
            offset += ExpressionSerializer.WriteExpression(DefaultTerm, writer);
            return offset;
        }
    }
}
