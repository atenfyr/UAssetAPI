using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UAssetAPI.JSON;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

public class FMetaData
{
    /// <summary>
    /// Mapping between an object, and its key->value meta-data pairs.
    /// </summary>
    [JsonConverter(typeof(FMetaDataObjectMetaDataMapJsonConverter))]
    public TMap<FSoftObjectPath, TMap<FName, FString>> ObjectMetaDataMap = [];
    /// <summary>
    /// Root-level (not associated with a particular object) key->value meta-data pairs.
    /// Meta-data associated with the package itself should be stored here.
    /// </summary>
    [JsonConverter(typeof(TMapJsonConverter<FName, FString>))]
    public TMap<FName, FString> RootMetaDataMap = [];

    public FMetaData() { }

    public FMetaData(AssetBinaryReader Ar)
    {
        var objectMDCount = Ar.ReadInt32();
        var rootMDCount = Ar.ReadInt32();
        ObjectMetaDataMap = Ar.ReadMap(objectMDCount, () => new FSoftObjectPath(Ar), () => Ar.ReadMap(Ar.ReadFName, Ar.ReadFString));
        RootMetaDataMap = Ar.ReadMap(rootMDCount, Ar.ReadFName, Ar.ReadFString);
    }

    public void Write(AssetBinaryWriter Ar)
    {
        Ar.Write(ObjectMetaDataMap.Count);
        Ar.Write(RootMetaDataMap.Count);
        foreach (var kvp in ObjectMetaDataMap)
        {
            kvp.Key.Write(Ar);
            Ar.Write(kvp.Value.Count);
            foreach (var (name, value) in kvp.Value)
            {
                Ar.Write(name);
                Ar.Write(value);
            }
        }

        foreach (var (name, value) in RootMetaDataMap)
        {
            Ar.Write(name);
            Ar.Write(value);
        }
    }

    public class FMetaDataObjectMetaDataMapJsonConverter : JsonConverter
    {
        private static readonly TMapJsonConverter<FName, FString> InnerMapConverter = new();

        public override bool CanConvert(Type objectType) => objectType == typeof(TMap<FSoftObjectPath, TMap<FName, FString>>);
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            var map = (TMap<FSoftObjectPath, TMap<FName, FString>>)value;

            writer.WriteStartArray();
            foreach (var kvp in map)
            {
                writer.WriteStartArray();
                serializer.Serialize(writer, kvp.Key);
                InnerMapConverter.WriteJson(writer, kvp.Value, serializer);
                writer.WriteEndArray();
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

            var result = new TMap<FSoftObjectPath, TMap<FName, FString>>();
            JArray outer = JArray.Load(reader);

            foreach (JToken entry in outer)
            {
                FSoftObjectPath key = entry[0].ToObject<FSoftObjectPath>(serializer);
                using JsonReader valueReader = entry[1].CreateReader();
                var value = (TMap<FName, FString>)InnerMapConverter.ReadJson(valueReader, typeof(TMap<FName, FString>), null, serializer);

                result.Add(key, value);
            }

            return result;
        }
    }
}