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

        /// <summary>
        /// Converts this FName instance into a human-readable string. This is the inverse of <see cref="FromString(string)"/>.
        /// </summary>
        /// <returns>The human-readable string that represents this FName.</returns>
        public override string ToString()
        {
            if (Number > 0) return Value.ToString() + "_" + (Number - 1);
            return Value.ToString();
        }

        /// <summary>
        /// Converts a human-readable string into an FName instance. This is the inverse of <see cref="ToString"/>.
        /// </summary>
        /// <param name="val">The human-readable string to convert into an FName instance.</param>
        /// <returns>An FName instance that this string represents.</returns>
        public static FName FromString(string val)
        {
            if (val == null || val == FString.NullCase) return null;
            if (val.Length == 0) return new FName(val, 0);

            if (val[val.Length - 1] >= '0' && val[val.Length - 1] <= '9')
            {
                int i = val.Length - 1;
                while (i > 1 && (val[i] >= '0' && val[i] <= '9'))
                {
                    i--;
                }

                if (val[i] == '_')
                {
                    string startSegment = val.Substring(0, i);
                    string endSegment = val.Substring(i + 1, val.Length - i - 1);
                    if (endSegment.Length == 1 || endSegment[0] != '0')
                    {
                        if (int.TryParse(endSegment, out int endSegmentVal)) return new FName(startSegment, endSegmentVal + 1);
                    }
                }
            }
            
            return new FName(val, 0);
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

        /// <summary>
        /// Creates a new FName instance.
        /// </summary>
        /// <param name="value">The string literal that the new FName's value will be, verbatim.</param>
        /// <param name="number">The instance number of the new FName.</param>
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

        /// <summary>
        /// Creates a new FName instance.
        /// </summary>
        /// <param name="value">The FString that the FName's value will be, verbatim.</param>
        /// <param name="number">The instance number of the new FName.</param>
        public FName(FString value, int number = 0)
        {
            Value = value;
            Number = number;
        }

        /// <summary>
        /// Creates a new blank FName instance.
        /// </summary>
        public FName()
        {
            Value = new FString(string.Empty);
            Number = 0;
        }
    }
}
