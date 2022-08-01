using Newtonsoft.Json;
using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{

    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_InstrumentationEvent"/> instruction.
    /// </summary>
    public class EX_InstrumentationEvent : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_InstrumentationEvent; } }

        [JsonProperty]
        public EScriptInstrumentationType EventType;
        [JsonProperty]
        public FName EventName;

        public EX_InstrumentationEvent()
        {
            
        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            EventType = (EScriptInstrumentationType)reader.ReadByte();

            if (EventType.Equals(EScriptInstrumentationType.InlineEvent))
            {
                EventName = reader.XFER_FUNC_NAME();
            }
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.Write((byte)EventType);
            if (EventType.Equals(EScriptInstrumentationType.InlineEvent)) {
                writer.XFER_FUNC_NAME(EventName);
                return 1 + 2 * sizeof(int);
            } else {
                return 1;
            }
        }
    }
}
