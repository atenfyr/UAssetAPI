using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_RotationConst"/> instruction.
    /// </summary>
    public class EX_RotationConst : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_RotationConst; } }

        [JsonProperty]
        public FRotator Value;

        public EX_RotationConst()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
            {
                Value = new FRotator(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
            }
            else
            {
                Value = new FRotator(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            }
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
            {
                writer.Write(Value.Pitch);
                writer.Write(Value.Yaw);
                writer.Write(Value.Roll);
                return sizeof(double) * 3;
            }
            else
            {
                writer.Write(Value.PitchFloat);
                writer.Write(Value.YawFloat);
                writer.Write(Value.RollFloat);
                return sizeof(float) * 3;
            }
        }
    }
}
