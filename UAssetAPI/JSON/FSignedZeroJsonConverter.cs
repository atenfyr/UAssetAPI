using Newtonsoft.Json;
using System;
using System.Diagnostics;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.JSON
{
    public class FSignedZeroJsonConverter : JsonConverter
    {
        private static readonly decimal negativeZero = decimal.Negate(decimal.Zero);

        private static bool IsNegativeZero(double x)
        {
            return x == 0.0 && double.IsNegativeInfinity(1.0 / x);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(float) || objectType == typeof(double);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double us = Convert.ToDouble(value);
            if (us == 0)
            {
                writer.WriteValue(IsNegativeZero(us) ? "-0" : "+0");
            }
            else
            {
                writer.WriteValue(us);
            }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is null) return 0;

            if (reader.Value is string)
            {
                if (reader.Value == null || reader.Value.Equals("+0")) return Convert.ChangeType(0.0, objectType);
                if (reader.Value.Equals("-0")) return Convert.ChangeType(negativeZero, objectType);
            }

            object res = Convert.ChangeType(reader.Value, objectType);
            if (res is null) return 0;
            return res;
        }
    }
}
