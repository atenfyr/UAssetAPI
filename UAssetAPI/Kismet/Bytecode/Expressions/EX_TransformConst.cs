using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_TransformConst"/> instruction.
    /// </summary>
    public class EX_TransformConst : KismetExpression<FTransform>
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_TransformConst; } }

        public EX_TransformConst()
        {

        }

        /// <summary>
        /// Reads out the expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public override void Read(AssetBinaryReader reader)
        {
            var rotation = new FQuat(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            var translation = new FVector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            var scale = new FVector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            Value = new FTransform(rotation, translation, scale);
        }

        /// <summary>
        /// Writes the expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public override int Write(AssetBinaryWriter writer)
        {
            writer.Write(Value.Rotation.X);
            writer.Write(Value.Rotation.Y);
            writer.Write(Value.Rotation.Z);
            writer.Write(Value.Rotation.W);
            writer.Write(Value.Translation.X);
            writer.Write(Value.Translation.Y);
            writer.Write(Value.Translation.Z);
            writer.Write(Value.Scale3D.X);
            writer.Write(Value.Scale3D.Y);
            writer.Write(Value.Scale3D.Z);
            return sizeof(float) * 10;
        }
    }
}
