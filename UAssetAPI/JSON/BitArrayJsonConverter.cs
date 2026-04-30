using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;

namespace UAssetAPI.JSON;

internal class BitArrayJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => typeof(BitArray).IsAssignableFrom(objectType);
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is null)
        {
            writer.WriteNull();
            return;
        }

        var bits = (BitArray)value;
        writer.WriteStartArray();
        for (int i = 0; i < bits.Length; i++)
        {
            writer.WriteValue(bits[i]);
        }
        writer.WriteEndArray();
    }

    public override bool CanRead => true;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        JArray array = JArray.Load(reader);
        return new BitArray(array.Select(token => token.Value<bool>()).ToArray());
    }
}
