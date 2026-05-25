using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS0649
namespace UAssetAPI.Unversioned
{
    // note that the following classes do not necessarily explicitly specify all the fields that jmap actually serializes, only fields relevant to us
    // all other fields are serialized in the Other dictionary

    [JsonSourceGenerationOptions(WriteIndented = false)]
    [JsonSerializable(typeof(JmapObjectBase))]
    [JsonSerializable(typeof(JmapObject))]
    [JsonSerializable(typeof(JmapPackage))]
    [JsonSerializable(typeof(JmapStruct))]
    [JsonSerializable(typeof(JmapScriptStruct))]
    [JsonSerializable(typeof(JmapClass))]
    [JsonSerializable(typeof(JmapFunction))]
    [JsonSerializable(typeof(JmapEnum))]
    [JsonSerializable(typeof(JmapPropertyBase))]
    [JsonSerializable(typeof(JmapProperty))]
    [JsonSerializable(typeof(JmapStructProperty))]
    [JsonSerializable(typeof(JmapArrayProperty))]
    [JsonSerializable(typeof(JmapEnumProperty))]
    [JsonSerializable(typeof(JmapMapProperty))]
    [JsonSerializable(typeof(JmapSetProperty))]
    [JsonSerializable(typeof(JmapByteProperty))]
    [JsonSerializable(typeof(JmapObjectProperty))]
    [JsonSerializable(typeof(JmapClassProperty))]
    [JsonSerializable(typeof(JmapWeakObjectProperty))]
    [JsonSerializable(typeof(JmapSoftObjectProperty))]
    [JsonSerializable(typeof(JmapSoftClassProperty))]
    [JsonSerializable(typeof(JmapLazyObjectProperty))]
    [JsonSerializable(typeof(JmapInterfaceProperty))]
    [JsonSerializable(typeof(JmapOptionalProperty))]
    internal partial class JmapSourceGenerationContext : JsonSerializerContext { }

    internal class JmapObjectBase
    {
        [JsonIgnore]
        public string Type { get; set; }

        // from Object
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("vtable")]
        public string VTable { get; set; }
        [JsonPropertyName("object_flags")]
        public string ObjectFlags { get; set; }
        [JsonPropertyName("outer")]
        public string Outer { get; set; }
        [JsonPropertyName("class")]
        public string Class { get; set; }
    }

    internal class JmapObject : JmapObjectBase
    {

    }

    internal class JmapPackage : JmapObjectBase
    {
        // from Package
        // (none)
    }

    internal class JmapStruct : JmapObjectBase
    {
        // from Struct
        [JsonPropertyName("super_struct")]
        public string SuperStruct { get; set; }
        [JsonPropertyName("properties")]
        public List<JmapPropertyBase> Properties { get; set; }
    }

    internal class JmapScriptStruct : JmapStruct
    {
        // from ScriptStruct
        // (struct_flags left unserialized)
    }

    internal class JmapClass : JmapStruct
    {
        // from Class
        // (all these fields left unserialized)
    }

    internal class JmapFunction : JmapStruct
    {
        // from Struct
        // (all these fields left unserialized)
    }

    internal class JmapEnum : JmapObjectBase
    {
        // from Enum
        [JsonPropertyName("cpp_type")]
        public string CppType { get; set; }
        [JsonPropertyName("enum_flags")]
        public string EnumFlags { get; set; }
        [JsonPropertyName("cpp_form")]
        public string CppForm { get; set; }
        [JsonPropertyName("names")]
        [JsonConverter(typeof(JmapEnumNamesConverter))]
        public Dictionary<long, string> Values { get; set; }
    }

    internal class JmapPropertyBase
    {
        [JsonIgnore]
        public string Type { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("offset")]
        public long Offset { get; set; }
        [JsonPropertyName("array_dim")]
        public int ArrayDim { get; set; }
    }

    internal class JmapProperty : JmapPropertyBase
    {

    }

    internal class JmapStructProperty : JmapPropertyBase
    {
        [JsonPropertyName("struct")]
        public string Struct { get; set; }
    }

    internal class JmapArrayProperty : JmapPropertyBase
    {
        [JsonPropertyName("inner")]
        public JmapPropertyBase Inner { get; set; }
    }

    internal class JmapEnumProperty : JmapPropertyBase
    {
        [JsonPropertyName("container")]
        public JmapPropertyBase Container { get; set; }
        [JsonPropertyName("enum")]
        public string Enum { get; set; }
    }

    internal class JmapMapProperty : JmapPropertyBase
    {
        [JsonPropertyName("key_prop")]
        public JmapPropertyBase Key { get; set; }
        [JsonPropertyName("value_prop")]
        public JmapPropertyBase Value { get; set; }
    }

    internal class JmapSetProperty : JmapPropertyBase
    {
        [JsonPropertyName("key_prop")]
        public JmapPropertyBase Key { get; set; }
    }

    internal class JmapByteProperty : JmapPropertyBase
    {
        [JsonPropertyName("enum")]
        public string Enum { get; set; }
    }

    internal class JmapObjectProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        public string PropertyClass { get; set; }
    }

    internal class JmapClassProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        public string PropertyClass { get; set; }
        [JsonPropertyName("meta_class")]
        public string MetaClass { get; set; }
    }

    internal class JmapWeakObjectProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        public string PropertyClass { get; set; }
    }

    internal class JmapSoftObjectProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        public string PropertyClass { get; set; }
    }

    internal class JmapSoftClassProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        public string PropertyClass { get; set; }
        [JsonPropertyName("meta_class")]
        public string MetaClass { get; set; }
    }

    internal class JmapLazyObjectProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        public string PropertyClass { get; set; }
    }

    internal class JmapInterfaceProperty : JmapPropertyBase
    {
        [JsonPropertyName("interface_class")]
        public string InterfaceClass { get; set; }
    }

    internal class JmapOptionalProperty : JmapPropertyBase
    {
        [JsonPropertyName("inner")]
        public JmapPropertyBase Inner { get; set; }
    }

    internal class JmapObjectConverter : JsonConverter<JmapObjectBase>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(JmapObjectBase);
        }

        public override JmapObjectBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerCopy = reader;

            bool doneReading = false;
            string typeDiscriminator = null;
            int depth = 0;
            while (readerCopy.Read())
            {
                switch (readerCopy.TokenType)
                {
                    case JsonTokenType.StartObject:
                    case JsonTokenType.StartArray:
                        depth++;
                        break;
                    case JsonTokenType.EndObject:
                    case JsonTokenType.EndArray:
                        if (--depth < 0) doneReading = true;
                        break;
                    case JsonTokenType.PropertyName:
                        if (depth == 0 && readerCopy.ValueTextEquals("type"))
                        {
                            readerCopy.Read();
                            typeDiscriminator = readerCopy.GetString();
                            doneReading = true;
                        }
                        break;
                }
                if (doneReading) break;
            }

            if (typeDiscriminator == null) throw new JsonException();

            JmapObjectBase res = typeDiscriminator switch
            {
                "Object" => JsonSerializer.Deserialize<JmapObject>(ref reader, options),
                "Package" => JsonSerializer.Deserialize<JmapPackage>(ref reader, options),
                "ScriptStruct" => JsonSerializer.Deserialize<JmapScriptStruct>(ref reader, options),
                "Class" => JsonSerializer.Deserialize<JmapClass>(ref reader, options),
                "Function" => JsonSerializer.Deserialize<JmapFunction>(ref reader, options),
                "Enum" => JsonSerializer.Deserialize<JmapEnum>(ref reader, options),
                _ => null,
            };
            res?.Type = typeDiscriminator;

            return res;
        }

        public override void Write(Utf8JsonWriter writer, JmapObjectBase value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal class JmapPropertyConverter : JsonConverter<JmapPropertyBase>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(JmapPropertyBase);
        }

        public override JmapPropertyBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerCopy = reader;

            bool doneReading = false;
            string typeDiscriminator = null;
            int depth = 0;
            while (readerCopy.Read())
            {
                switch (readerCopy.TokenType)
                {
                    case JsonTokenType.StartObject:
                    case JsonTokenType.StartArray:
                        depth++;
                        break;
                    case JsonTokenType.EndObject:
                    case JsonTokenType.EndArray:
                        if (--depth < 0) doneReading = true;
                        break;
                    case JsonTokenType.PropertyName:
                        if (depth == 0 && readerCopy.ValueTextEquals("type"))
                        {
                            readerCopy.Read();
                            typeDiscriminator = readerCopy.GetString();
                            doneReading = true;
                        }
                        break;
                }
                if (doneReading) break;
            }

            if (typeDiscriminator == null) throw new JsonException();

            JmapPropertyBase res = typeDiscriminator switch
            {
                "StructProperty" => JsonSerializer.Deserialize<JmapStructProperty>(ref reader, options),
                "ArrayProperty" => JsonSerializer.Deserialize<JmapArrayProperty>(ref reader, options),
                "EnumProperty" => JsonSerializer.Deserialize<JmapEnumProperty>(ref reader, options),
                "MapProperty" => JsonSerializer.Deserialize<JmapMapProperty>(ref reader, options),
                "SetProperty" => JsonSerializer.Deserialize<JmapSetProperty>(ref reader, options),
                "ByteProperty" => JsonSerializer.Deserialize<JmapByteProperty>(ref reader, options),
                "ObjectProperty" => JsonSerializer.Deserialize<JmapObjectProperty>(ref reader, options),
                "ClassProperty" => JsonSerializer.Deserialize<JmapClassProperty>(ref reader, options),
                "WeakObjectProperty" => JsonSerializer.Deserialize<JmapWeakObjectProperty>(ref reader, options),
                "SoftObjectProperty" => JsonSerializer.Deserialize<JmapSoftObjectProperty>(ref reader, options),
                "SoftClassProperty" => JsonSerializer.Deserialize<JmapSoftClassProperty>(ref reader, options),
                "LazyObjectProperty" => JsonSerializer.Deserialize<JmapLazyObjectProperty>(ref reader, options),
                "InterfaceProperty" => JsonSerializer.Deserialize<JmapInterfaceProperty>(ref reader, options),
                "OptionalProperty" => JsonSerializer.Deserialize<JmapOptionalProperty>(ref reader, options),
                _ => JsonSerializer.Deserialize<JmapProperty>(ref reader, options),
            };
            res.Type = typeDiscriminator;

            return res;
        }

        public override void Write(Utf8JsonWriter writer, JmapPropertyBase value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal class JmapEnumNamesConverter : JsonConverter<Dictionary<long, string>>
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
    internal static class JmapHelper
    {
        /// <summary>
        /// Converts a jmap type string to its corresponding usmap UsmapPropertyType enum value.
        /// </summary>
        /// <param name="type">Input jmap type string.</param>
        /// <returns>The corresponding usmap UsmapPropertyType enum value.</returns>
        private static UsmapPropertyType UsmapPropertyTypeStringToEnum(string type)
        {
            return type switch
            {
                "StructProperty" => UsmapPropertyType.StructProperty,
                "StrProperty" => UsmapPropertyType.StrProperty,
                "NameProperty" => UsmapPropertyType.NameProperty,
                "TextProperty" => UsmapPropertyType.TextProperty,
                "MulticastInlineDelegateProperty" => UsmapPropertyType.MulticastDelegateProperty,
                "MulticastSparseDelegateProperty" => UsmapPropertyType.MulticastDelegateProperty,
                "MulticastDelegateProperty" => UsmapPropertyType.MulticastDelegateProperty,
                "DelegateProperty" => UsmapPropertyType.DelegateProperty,
                "BoolProperty" => UsmapPropertyType.BoolProperty,
                "ArrayProperty" => UsmapPropertyType.ArrayProperty,
                "EnumProperty" => UsmapPropertyType.EnumProperty,
                "MapProperty" => UsmapPropertyType.MapProperty,
                "SetProperty" => UsmapPropertyType.SetProperty,
                "FloatProperty" => UsmapPropertyType.FloatProperty,
                "DoubleProperty" => UsmapPropertyType.DoubleProperty,
                "ByteProperty" => UsmapPropertyType.ByteProperty,
                "UInt16Property" => UsmapPropertyType.UInt16Property,
                "UInt32Property" => UsmapPropertyType.UInt32Property,
                "UInt64Property" => UsmapPropertyType.UInt64Property,
                "Int8Property" => UsmapPropertyType.Int8Property,
                "Int16Property" => UsmapPropertyType.Int16Property,
                "IntProperty" => UsmapPropertyType.IntProperty,
                "Int64Property" => UsmapPropertyType.Int64Property,
                "ObjectProperty" => UsmapPropertyType.ObjectProperty,
                "ClassProperty" => UsmapPropertyType.ObjectProperty,
                "WeakObjectProperty" => UsmapPropertyType.WeakObjectProperty,
                "SoftObjectProperty" => UsmapPropertyType.SoftObjectProperty,
                "SoftClassProperty" => UsmapPropertyType.SoftObjectProperty,
                "LazyObjectProperty" => UsmapPropertyType.LazyObjectProperty,
                "InterfaceProperty" => UsmapPropertyType.InterfaceProperty,
                "FieldPathProperty" => UsmapPropertyType.FieldPathProperty,
                "OptionalProperty" => UsmapPropertyType.OptionalProperty,
                "FUtf8StrProperty" => UsmapPropertyType.Utf8StrProperty,
                "AnsiStrProperty" => UsmapPropertyType.AnsiStrProperty,
                _ => UsmapPropertyType.Unknown,
            };
        }

        private static string SubstringAfterLast(this string value, string substring)
        {
            var index = value.LastIndexOf(substring);
            return index == -1 ? value : value.Substring(index + substring.Length);
        }

        private static UsmapPropertyData SetupUsmapProperty(JmapPropertyBase jmapProp)
        {
            UsmapPropertyType typEnum = UsmapPropertyTypeStringToEnum(jmapProp.Type);
            return typEnum switch
            {
                UsmapPropertyType.EnumProperty => new UsmapEnumData
                {
                    InnerType = SetupUsmapProperty(((JmapEnumProperty)jmapProp).Container),
                    // strip module path from StructType (usmap convention)
                    Name = ((JmapEnumProperty)jmapProp).Enum.SubstringAfterLast(".")
                },
                UsmapPropertyType.StructProperty => new UsmapStructData
                {
                    // strip module path from StructType (usmap convention)
                    StructType = ((JmapStructProperty)jmapProp).Struct.SubstringAfterLast(".")
                },
                UsmapPropertyType.SetProperty => new UsmapArrayData(typEnum)
                {
                    InnerType = SetupUsmapProperty(((JmapSetProperty)jmapProp).Key)
                },
                UsmapPropertyType.ArrayProperty => new UsmapArrayData(typEnum)
                {
                    InnerType = SetupUsmapProperty(((JmapArrayProperty)jmapProp).Inner)
                },
                UsmapPropertyType.OptionalProperty => new UsmapArrayData(typEnum)
                {
                    InnerType = SetupUsmapProperty(((JmapOptionalProperty)jmapProp).Inner)
                },
                UsmapPropertyType.MapProperty => new UsmapMapData()
                {
                    InnerType = SetupUsmapProperty(((JmapMapProperty)jmapProp).Key),
                    ValueType = SetupUsmapProperty(((JmapMapProperty)jmapProp).Value),
                },
                _ => new UsmapPropertyData(typEnum),
            };
        }

        public static JsonSerializerOptions JmapDefaultOptions()
        {
            return new JsonSerializerOptions()
            {
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
                TypeInfoResolver = JmapSourceGenerationContext.Default,
                Converters =
                {
                    new JmapObjectConverter(),
                    new JmapPropertyConverter()
                }
            };
        }

        public static JmapObjectBase GetObjectBase(string objectJSON, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<JmapObjectBase>(objectJSON, options ?? Usmap.SerializerOptions ?? JmapDefaultOptions());
        }

        public static JmapObjectBase GetObjectBase(ReadOnlySpan<byte> objectJSON, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<JmapObjectBase>(objectJSON, options ?? Usmap.SerializerOptions ?? JmapDefaultOptions());
        }

        public static void ReadSchema(JmapObjectBase objectBase, UsmapSchema templateSchema)
        {
            if (objectBase is JmapStruct str)
            {
                int ind = 0;
                if (str.SuperStruct != null && (ind = str.SuperStruct.LastIndexOf('.')) > 0)
                {
                    templateSchema.SuperType = str.SuperStruct[(ind + 1)..];
                    templateSchema.SuperTypeModulePath = str.SuperStruct[..ind];
                }
                else
                {
                    templateSchema.SuperType = str.SuperStruct;
                    templateSchema.SuperTypeModulePath = null;
                }

                templateSchema.propertiesInternal = new System.Collections.Concurrent.ConcurrentDictionary<int, UsmapProperty>();
                int propIdx = 0;
                foreach (JmapPropertyBase jmapProp in str.Properties)
                {
                    UsmapProperty usmapProp = new UsmapProperty(jmapProp.Name, propIdx, 0, jmapProp.ArrayDim, null)
                    {
                        PropertyData = SetupUsmapProperty(jmapProp)
                    };

                    templateSchema.propertiesInternal[propIdx] = usmapProp;
                    for (int i = 1; i < jmapProp.ArrayDim; i++)
                    {
                        UsmapProperty cln = (UsmapProperty)usmapProp.Clone();
                        cln.SchemaIndex += i;
                        cln.ArrayIndex = i;
                        templateSchema.propertiesInternal[propIdx + i] = cln;
                    }
                    propIdx += jmapProp.ArrayDim;
                }
                templateSchema.ConstructPropertiesMap(false);
                templateSchema.PropCount = templateSchema.propertiesInternal.Count;
            }

            templateSchema.StructKind = (objectBase is JmapClass) ? UsmapStructKind.UClass : (objectBase is JmapScriptStruct ? UsmapStructKind.UScriptStruct : UsmapStructKind.None);
        }

        public static void ReadSchema(ReadOnlySpan<byte> objectJSON, UsmapSchema templateSchema)
        {
            ReadSchema(GetObjectBase(objectJSON), templateSchema);
        }

        public static void ReadSchema(string objectJSON, UsmapSchema templateSchema)
        {
            ReadSchema(GetObjectBase(objectJSON), templateSchema);
        }

        public static void ReadEnum(JmapObjectBase objectBase, UsmapEnum templateSchema)
        {
            if (objectBase is JmapEnum str)
            {
                templateSchema.EnumFlags = 0; // not used by UAssetAPI in practice, can populate if needed (need to convert string to int)
                templateSchema._Values = new System.Collections.Concurrent.ConcurrentDictionary<long, string>(str.Values);
            }
        }

        public static void ReadEnum(ReadOnlySpan<byte> objectJSON, UsmapEnum templateEnum)
        {
            ReadEnum(GetObjectBase(objectJSON), templateEnum);
        }

        public static void ReadEnum(string objectJSON, UsmapEnum templateEnum)
        {
            ReadEnum(GetObjectBase(objectJSON), templateEnum);
        }
    }
}
#pragma warning restore CS0649
