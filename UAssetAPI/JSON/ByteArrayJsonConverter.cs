using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UAssetAPI.JSON
{
    public class ByteArrayJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(byte[]);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(Convert.ToBase64String((byte[])value));
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string val = reader.Value as string;

            // backwards compatibility for fun
            if (reader.TokenType == JsonToken.StartObject)
            {
                reader.Read();
                while (reader.TokenType != JsonToken.EndObject)
                {
                    if (reader.TokenType != JsonToken.PropertyName) throw new FormatException("Expected PropertyName, got " + reader.TokenType);
                    string propertyName = reader.Value.ToString();
                    reader.Read();
                    object propertyValue = reader.Value;
                    reader.Read();

                    if (propertyName == "$value")
                    {
                        val = propertyValue as string;
                    }
                }
            }

            if (val == null) return null;
            return Convert.FromBase64String(val);
        }
    }
}
