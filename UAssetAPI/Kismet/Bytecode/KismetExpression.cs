using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode
{
    /// <summary>
    /// A Kismet bytecode instruction.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class KismetExpression
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public virtual EExprToken Token { get { return EExprToken.EX_Nothing; } }

        /// <summary>
        /// The token of this instruction expressed as a string.
        /// </summary>
        public string Inst { get { return Token.ToString().Substring(3, Token.ToString().Length - 3); } }

        /// <summary>
        /// An optional tag which can be set on any expression in memory. This is for the user only, and has no bearing in the API itself.
        /// </summary>
        public object Tag;

        public object RawValue;

        public void SetObject(object value)
        {
            RawValue = value;
        }

        public T GetObject<T>()
        {
            return (T)RawValue;
        }

        public KismetExpression()
        {

        }

        /// <summary>
        /// Reads out an expression from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        public virtual void Read(AssetBinaryReader reader)
        {

        }

        /// <summary>
        /// Writes an expression to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <returns>The iCode offset of the data that was written.</returns>
        public virtual int Write(AssetBinaryWriter writer)
        {
            return 0;
        }
    }

    public abstract class KismetExpression<T> : KismetExpression
    {
        /// <summary>
        /// The value of this expression if it is a constant.
        /// </summary>
        [JsonProperty]
        public T Value
        {
            get => GetObject<T>();
            set => SetObject(value);
        }

        public KismetExpression() : base()
        {

        }
    }
}
