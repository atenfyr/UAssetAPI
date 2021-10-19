using Newtonsoft.Json;
using System;
using System.Text;

namespace UAssetAPI
{
    /// <summary>
    /// Unreal string - consists of a string and an encoding
    /// </summary>
    [JsonConverter(typeof(FStringJsonConverter))]
    public class FString : ICloneable
    {
        public string Value;
        public Encoding Encoding;
        public static readonly string NullCase = "null";

        public override string ToString()
        {
            if (this == null || Value == null) return NullCase;
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is FString fStr)
            {
                if (fStr == null) return false;
                return this.Value == fStr.Value && this.Encoding == fStr.Encoding;
            }
            else if (obj is string str)
            {
                return this.Value == str;
            }

            return false;
        }

        public static bool operator ==(FString one, FString two)
        {
            if (one is null || two is null) return one is null && two is null;
            return one.Equals(two);
        }

        public static bool operator !=(FString one, FString two)
        {
            if (one is null || two is null) return !(one is null && two is null);
            return !one.Equals(two);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public object Clone()
        {
            return new FString(Value, Encoding);
        }

        public static FString FromString(string value, Encoding encoding = null)
        {
            if (value == NullCase) return null;
            return new FString(value, encoding);
        }

        public FString(string value, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8.GetByteCount(value) == value.Length ? Encoding.ASCII : Encoding.Unicode;

            Value = value;
            Encoding = encoding;
        }

        public FString()
        {

        }
    }

    /// <summary>
    /// Unreal name - consists of an FString (which is serialized as an index in the name map) and an instance number
    /// </summary>
    [JsonConverter(typeof(FNameJsonConverter))]
    public class FName : ICloneable
    {
        public FString Value;
        /// <summary>Instance number.</summary>
        public int Number;

        public override string ToString()
        {
            if (Number == int.MinValue) return Value.ToString();
            return Value.ToString() + "(" + Number + ")";
        }

        /** Inverse of FName.ToString() */
        public static FName FromString(string val)
        {
            if (val == null || val == "null") return null;
            if (val.Length == 0 || val[val.Length - 1] != ')') return new FName(val);

            int locLastLeftBracket = val.LastIndexOf('(');
            if (locLastLeftBracket < 0) return new FName(val);

            string discriminatorRaw = val.Substring(locLastLeftBracket + 1, val.Length - locLastLeftBracket - 2);
            if (!int.TryParse(discriminatorRaw, out int discriminator)) return new FName(val);

            string realStr = val.Substring(0, locLastLeftBracket);
            return new FName(realStr, discriminator);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FName name)) return false;
            return (this.Value == name.Value || this.Value.Value == name.Value.Value) && this.Number == name.Number;
        }

        public static bool operator ==(FName one, FName two)
        {
            if (one is null || two is null) return one is null && two is null;
            return one.Equals(two);
        }

        public static bool operator !=(FName one, FName two)
        {
            if (one is null || two is null) return !(one is null && two is null);
            return !one.Equals(two);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Number.GetHashCode();
        }

        public object Clone()
        {
            return new FName((FString)Value.Clone(), Number);
        }

        public FName(string value, int number = 0)
        {
            if (value == null)
            {
                Value = new FString(null);
            }
            else
            {
                Value = new FString(value);
            }
            Number = number;
        }

        public FName(FString value, int number = 0)
        {
            Value = value;
            Number = number;
        }

        public FName()
        {
            Value = new FString(string.Empty);
            Number = 0;
        }
    }
}
