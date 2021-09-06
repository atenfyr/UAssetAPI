using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UAssetAPI
{
    /*
        Unreal string - consists of a string and an encoding
    */
    public class FString
    {
        public string Value;
        public Encoding Encoding;

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            FString str = obj as FString;
            if (str == null) return false;
            return this.Value == str.Value && this.Encoding == str.Encoding;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public FString(string value, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.ASCII;

            Value = value;
            Encoding = encoding;
        }

        public FString()
        {

        }
    }

    /*
        Unreal name - consists of an FString (which is serialized as an index in the name map) and an instance number
    */
    public class FName
    {
        public FString Value;
        public int Number; // Instance number

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            FName name = obj as FName;
            if (name == null) return false;
            return this.Value == name.Value && this.Number == name.Number;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Number.GetHashCode();
        }

        public FName(string value, int number = 0)
        {
            if (value == null)
            {
                Value = new FString(null);
            }
            else
            {
                Value = new FString(value, Encoding.UTF8.GetByteCount(value) == value.Length ? Encoding.ASCII : Encoding.Unicode);
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

        }
    }
}
