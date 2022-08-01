using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Specialized;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.JSON
{
    public class FStringTableJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FStringTable);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var realVal = (FStringTable)value;

            ICollection keys = ((IOrderedDictionary)value).Keys;
            ICollection values = ((IOrderedDictionary)value).Values;
            IEnumerator valueEnumerator = values.GetEnumerator();

            writer.WriteStartObject();
            writer.WritePropertyName("TableNamespace");
            writer.WriteValue(realVal.TableNamespace?.Value);
            writer.WritePropertyName("Value");
            writer.WriteStartArray();
            foreach (object key in keys)
            {
                valueEnumerator.MoveNext();

                writer.WriteStartArray();
                serializer.Serialize(writer, key);
                serializer.Serialize(writer, valueEnumerator.Current);
                writer.WriteEndArray();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dictionary = new FStringTable();

            JObject tableJson = JObject.Load(reader);
            dictionary.TableNamespace = new FString(tableJson["TableNamespace"].ToObject<string>());
            JArray tokens = (JArray)tableJson["Value"];

            foreach (var eachToken in tokens)
            {
                FString key = eachToken[0].ToObject<FString>(serializer);
                FString value = eachToken[1].ToObject<FString>(serializer);
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }
}