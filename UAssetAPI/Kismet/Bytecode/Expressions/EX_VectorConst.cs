using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_VectorConst"/> instruction.
    /// </summary>
    public class EX_VectorConst : KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_VectorConst; } }

        [JsonProperty]
        public FVector Value;

        public EX_VectorConst()
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
                Value = new FVector(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
            }
            else
            {
                Value = new FVector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
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
                writer.Write(Value.X);
                writer.Write(Value.Y);
                writer.Write(Value.Z);
                return sizeof(double) * 3;
            }
            else
            {
                writer.Write(Value.XFloat);
                writer.Write(Value.YFloat);
                writer.Write(Value.ZFloat);
                return sizeof(float) * 3;
            }
        }
    }
}
