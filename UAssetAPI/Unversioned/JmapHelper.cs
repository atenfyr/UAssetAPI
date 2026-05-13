using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UAssetAPI.Unversioned
{
    // note that the following classes do not necessarily explicitly specify all the fields that jmap actually serializes, only fields relevant to us
    // all other fields are serialized in the Other dictionary

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(JmapObject), typeDiscriminator: "Object")]
    public class JmapObject
    {
        // from Object
        [JsonPropertyName("address")]
        public string Address;
        [JsonPropertyName("vtable")]
        public string VTable;
        [JsonPropertyName("object_flags")]
        public string ObjectFlags;
        [JsonPropertyName("outer")]
        public string Outer;
        [JsonPropertyName("class")]
        public string Class;

        // unserialized
        [JsonExtensionData]
        public IDictionary<string, object> Other;
    }

    [JsonDerivedType(typeof(JmapPackage), typeDiscriminator: "Package")]
    public class JmapPackage : JmapObject
    {
        // from Package
        // (none)
    }

    [JsonDerivedType(typeof(JmapStruct), typeDiscriminator: "Struct")] // this is non-standard, type discriminator "Struct" does not actually exist in jmap
    public class JmapStruct : JmapObject
    {
        // from Struct
        [JsonPropertyName("super_struct")]
        public string SuperStruct;
        [JsonPropertyName("properties")]
        public List<JmapProperty> Properties;
    }

    [JsonDerivedType(typeof(JmapScriptStruct), typeDiscriminator: "ScriptStruct")]
    public class JmapScriptStruct : JmapStruct
    {
        // from ScriptStruct
        // (struct_flags left unserialized)
    }

    [JsonDerivedType(typeof(JmapClass), typeDiscriminator: "Class")]
    public class JmapClass : JmapStruct
    {
        // from Class
        // (all these fields left unserialized)
    }

    [JsonDerivedType(typeof(JmapFunction), typeDiscriminator: "Function")]
    public class JmapFunction
    {
        // unserialized
        [JsonExtensionData]
        public IDictionary<string, object> Other;
    }

    [JsonDerivedType(typeof(JmapEnum), typeDiscriminator: "Enum")]
    public class JmapEnum : JmapObject
    {
        // from Enum
        [JsonPropertyName("cpp_type")]
        public string CppType;
        [JsonPropertyName("enum_flags")]
        public string EnumFlags;
        [JsonPropertyName("cpp_form")]
        public string CppForm;
        [JsonPropertyName("names")]
        [JsonConverter(typeof(JmapEnumNamesConverter))]
        public Dictionary<long, string> Values;
    }

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
    [JsonDerivedType(typeof(JmapProperty), typeDiscriminator: "Property")]

    public class JmapProperty
    {
        [JsonPropertyName("address")]
        public string Address;
        [JsonPropertyName("name")]
        public string Name;
        [JsonPropertyName("offset")]
        public long Offset;
        [JsonPropertyName("array_dim")]
        public long ArrayDim;

        // todo implement the sub-classes of JmapProperty with additional properties

        // unserialized
        [JsonExtensionData]
        public IDictionary<string, object> Other;
    }

    public class JmapEnumNamesConverter : JsonConverter<Dictionary<long, string>>
    {
        public override Dictionary<long, string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Dictionary<long, string> output = new Dictionary<long, string>();

            if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    reader.Read();
                    string key = reader.GetString();
                    reader.Read();
                    long value = reader.GetInt64();

                    output[value] = key;

                    // skip to end of array
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray) { }
                }
            }

            return output;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<long, string> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Helper class for operations on .jmap files.
    /// https://github.com/trumank/jmap/blob/master/jmap/src/lib.rs
    /// </summary>
    public static class JmapHelper
    {
    }
}
