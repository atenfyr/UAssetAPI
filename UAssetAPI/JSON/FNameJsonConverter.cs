using Newtonsoft.Json;
using System;

namespace UAssetAPI
{
    public class FNameJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FName);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var realVal = (FName)value;
            writer.WriteValue(realVal is null ? "null" : realVal.ToString());
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            return FName.FromString(Convert.ToString(reader.Value));
        }
    }
}
