using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.JSON
{
    public class FNameJsonConverter : JsonConverter
    {
        public Dictionary<FName, string> ToBeFilled;
        public int currentI = 0;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FName);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var realVal = (FName)value;
            writer.WriteValue(realVal.DummyValue == null ? (realVal is null ? "null" : realVal.ToString()) : realVal.DummyValue.ToString());
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            var res = FName.DefineDummy(null, "temp", ++currentI);
            ToBeFilled[res] = Convert.ToString(reader.Value);
            return res;
        }

        public FNameJsonConverter(Dictionary<FName, string> dict) : base()
        {
            ToBeFilled = dict;
        }
    }
}
