using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UAssetAPI.Unversioned
{
    // note that the following classes do not necessarily explicitly specify all the fields that jmap actually serializes, only fields relevant to us
    // all other fields are serialized in the Other dictionary

    public class JmapObjectBase
    {
        [JsonIgnore]
        public string Type;

        // from Object
        [JsonPropertyName("address")]
        [JsonInclude]
        public string Address;
        [JsonPropertyName("vtable")]
        [JsonInclude]
        public string VTable;
        [JsonPropertyName("object_flags")]
        [JsonInclude]
        public string ObjectFlags;
        [JsonPropertyName("outer")]
        [JsonInclude]
        public string Outer;
        [JsonPropertyName("class")]
        [JsonInclude]
        public string Class;

        // unserialized
        [JsonExtensionData]
        [JsonInclude]
        public IDictionary<string, object> Other;
    }

    public class JmapObject : JmapObjectBase
    {

    }

    public class JmapPackage : JmapObjectBase
    {
        // from Package
        // (none)
    }

    public class JmapStruct : JmapObjectBase
    {
        // from Struct
        [JsonPropertyName("super_struct")]
        [JsonInclude]
        public string SuperStruct;
        [JsonPropertyName("properties")]
        [JsonInclude]
        public List<JmapPropertyBase> Properties;
    }

    public class JmapScriptStruct : JmapStruct
    {
        // from ScriptStruct
        // (struct_flags left unserialized)
    }

    public class JmapClass : JmapStruct
    {
        // from Class
        // (all these fields left unserialized)
    }

    public class JmapFunction : JmapStruct
    {
        // from Struct
        // (all these fields left unserialized)
    }

    public class JmapEnum : JmapObjectBase
    {
        // from Enum
        [JsonPropertyName("cpp_type")]
        [JsonInclude]
        public string CppType;
        [JsonPropertyName("enum_flags")]
        [JsonInclude]
        public string EnumFlags;
        [JsonPropertyName("cpp_form")]
        [JsonInclude]
        public string CppForm;
        [JsonPropertyName("names")]
        [JsonInclude]
        [JsonConverter(typeof(JmapEnumNamesConverter))]
        public Dictionary<long, string> Values;
    }

    public class JmapPropertyBase
    {
        [JsonIgnore]
        public string Type;

        [JsonPropertyName("address")]
        [JsonInclude]
        public string Address;
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name;
        [JsonPropertyName("offset")]
        [JsonInclude]
        public long Offset;
        [JsonPropertyName("array_dim")]
        [JsonInclude]
        public int ArrayDim;

        // unserialized
        [JsonExtensionData]
        [JsonInclude]
        public IDictionary<string, object> Other;
    }

    public class JmapProperty : JmapPropertyBase
    {

    }

    public class JmapStructProperty : JmapPropertyBase
    {
        [JsonPropertyName("struct")]
        [JsonInclude]
        public string Struct;
    }

    public class JmapArrayProperty : JmapPropertyBase
    {
        [JsonPropertyName("inner")]
        [JsonInclude]
        public JmapPropertyBase Inner;
    }

    public class JmapEnumProperty : JmapPropertyBase
    {
        [JsonPropertyName("container")]
        [JsonInclude]
        public JmapPropertyBase Container;
        [JsonPropertyName("enum")]
        [JsonInclude]
        public string Enum;
    }

    public class JmapMapProperty : JmapPropertyBase
    {
        [JsonPropertyName("key_prop")]
        [JsonInclude]
        public JmapPropertyBase Key;
        [JsonPropertyName("value_prop")]
        [JsonInclude]
        public JmapPropertyBase Value;
    }

    public class JmapSetProperty : JmapPropertyBase
    {
        [JsonPropertyName("key_prop")]
        [JsonInclude]
        public JmapPropertyBase Key;
    }

    public class JmapByteProperty : JmapPropertyBase
    {
        [JsonPropertyName("enum")]
        [JsonInclude]
        public string Enum;
    }

    public class JmapObjectProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
    }

    public class JmapClassProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
        [JsonPropertyName("meta_class")]
        [JsonInclude]
        public string MetaClass;
    }

    public class JmapWeakObjectProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
    }

    public class JmapSoftObjectProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
    }

    public class JmapSoftClassProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
        [JsonPropertyName("meta_class")]
        [JsonInclude]
        public string MetaClass;
    }

    public class JmapLazyObjectProperty : JmapPropertyBase
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
    }

    public class JmapInterfaceProperty : JmapPropertyBase
    {
        [JsonPropertyName("interface_class")]
        [JsonInclude]
        public string InterfaceClass;
    }

    public class JmapOptionalProperty : JmapPropertyBase
    {
        [JsonPropertyName("inner")]
        [JsonInclude]
        public JmapPropertyBase Inner;
    }

    public class JmapObjectConverter : JsonConverter<JmapObjectBase>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(JmapObjectBase);
        }

        public override JmapObjectBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            if (!jsonDoc.RootElement.TryGetProperty("type", out var typeProp)) throw new JsonException();
            string typeDiscriminator = typeProp.GetString();

            JmapObjectBase res = null;
            switch (typeDiscriminator)
            {
                case "Object":
                    res = JsonSerializer.Deserialize<JmapObject>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "Package":
                    res = JsonSerializer.Deserialize<JmapPackage>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "ScriptStruct":
                    res = JsonSerializer.Deserialize<JmapScriptStruct>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "Class":
                    res = JsonSerializer.Deserialize<JmapClass>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "Function":
                    res = JsonSerializer.Deserialize<JmapFunction>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "Enum":
                    res = JsonSerializer.Deserialize<JmapEnum>(jsonDoc.RootElement.GetRawText(), options);
                    break;
            }

            res.Type = typeDiscriminator;

            return res;
        }

        public override void Write(Utf8JsonWriter writer, JmapObjectBase value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    public class JmapPropertyConverter : JsonConverter<JmapPropertyBase>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(JmapPropertyBase);
        }

        public override JmapPropertyBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            if (!jsonDoc.RootElement.TryGetProperty("type", out var typeProp)) throw new JsonException();
            string typeDiscriminator = typeProp.GetString();

            JmapPropertyBase res = null;
            switch(typeDiscriminator)
            {
                case "StructProperty":
                    res = JsonSerializer.Deserialize<JmapStructProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "ArrayProperty":
                    res = JsonSerializer.Deserialize<JmapArrayProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "EnumProperty":
                    res = JsonSerializer.Deserialize<JmapEnumProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "MapProperty":
                    res = JsonSerializer.Deserialize<JmapMapProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "SetProperty":
                    res = JsonSerializer.Deserialize<JmapSetProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "ByteProperty":
                    res = JsonSerializer.Deserialize<JmapByteProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "ObjectProperty":
                    res = JsonSerializer.Deserialize<JmapObjectProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "ClassProperty":
                    res = JsonSerializer.Deserialize<JmapClassProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "WeakObjectProperty":
                    res = JsonSerializer.Deserialize<JmapWeakObjectProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "SoftObjectProperty":
                    res = JsonSerializer.Deserialize<JmapSoftObjectProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "SoftClassProperty":
                    res = JsonSerializer.Deserialize<JmapSoftClassProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "LazyObjectProperty":
                    res = JsonSerializer.Deserialize<JmapLazyObjectProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "InterfaceProperty":
                    res = JsonSerializer.Deserialize<JmapInterfaceProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                case "OptionalProperty":
                    res = JsonSerializer.Deserialize<JmapOptionalProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
                default:
                    res = JsonSerializer.Deserialize<JmapProperty>(jsonDoc.RootElement.GetRawText(), options);
                    break;
            }

            res.Type = typeDiscriminator;

            return res;
        }

        public override void Write(Utf8JsonWriter writer, JmapPropertyBase value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
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
        /// <summary>
        /// Converts a jmap type string to its corresponding usmap EPropertyType enum value.
        /// </summary>
        /// <param name="type">Input jmap type string.</param>
        /// <returns>The corresponding usmap EPropertyType enum value.</returns>
        private static EPropertyType EPropertyTypeStringToEnum(string type)
        {
            EPropertyType typeAsEnum = EPropertyType.Unknown;
            switch (type)
            {
                case "StructProperty":
                    typeAsEnum = EPropertyType.StructProperty;
                    break;
                case "StrProperty":
                    typeAsEnum = EPropertyType.StrProperty;
                    break;
                case "NameProperty":
                    typeAsEnum = EPropertyType.NameProperty;
                    break;
                case "TextProperty":
                    typeAsEnum = EPropertyType.TextProperty;
                    break;
                case "MulticastInlineDelegateProperty":
                    typeAsEnum = EPropertyType.MulticastDelegateProperty;
                    break;
                case "MulticastSparseDelegateProperty":
                    typeAsEnum = EPropertyType.MulticastDelegateProperty;
                    break;
                case "MulticastDelegateProperty":
                    typeAsEnum = EPropertyType.MulticastDelegateProperty;
                    break;
                case "DelegateProperty":
                    typeAsEnum = EPropertyType.DelegateProperty;
                    break;
                case "BoolProperty":
                    typeAsEnum = EPropertyType.BoolProperty;
                    break;
                case "ArrayProperty":
                    typeAsEnum = EPropertyType.ArrayProperty;
                    break;
                case "EnumProperty":
                    typeAsEnum = EPropertyType.EnumProperty;
                    break;
                case "MapProperty":
                    typeAsEnum = EPropertyType.MapProperty;
                    break;
                case "SetProperty":
                    typeAsEnum = EPropertyType.SetProperty;
                    break;
                case "FloatProperty":
                    typeAsEnum = EPropertyType.FloatProperty;
                    break;
                case "DoubleProperty":
                    typeAsEnum = EPropertyType.DoubleProperty;
                    break;
                case "ByteProperty":
                    typeAsEnum = EPropertyType.ByteProperty;
                    break;
                case "UInt16Property":
                    typeAsEnum = EPropertyType.UInt16Property;
                    break;
                case "UInt32Property":
                    typeAsEnum = EPropertyType.UInt32Property;
                    break;
                case "UInt64Property":
                    typeAsEnum = EPropertyType.UInt64Property;
                    break;
                case "Int8Property":
                    typeAsEnum = EPropertyType.Int8Property;
                    break;
                case "Int16Property":
                    typeAsEnum = EPropertyType.Int16Property;
                    break;
                case "IntProperty":
                    typeAsEnum = EPropertyType.IntProperty;
                    break;
                case "Int64Property":
                    typeAsEnum = EPropertyType.Int64Property;
                    break;
                case "ObjectProperty":
                    typeAsEnum = EPropertyType.ObjectProperty;
                    break;
                case "ClassProperty":
                    typeAsEnum = EPropertyType.ObjectProperty;
                    break;
                case "WeakObjectProperty":
                    typeAsEnum = EPropertyType.WeakObjectProperty;
                    break;
                case "SoftObjectProperty":
                    typeAsEnum = EPropertyType.SoftObjectProperty;
                    break;
                case "SoftClassProperty":
                    typeAsEnum = EPropertyType.SoftObjectProperty;
                    break;
                case "LazyObjectProperty":
                    typeAsEnum = EPropertyType.LazyObjectProperty;
                    break;
                case "InterfaceProperty":
                    typeAsEnum = EPropertyType.InterfaceProperty;
                    break;
                case "FieldPathProperty":
                    typeAsEnum = EPropertyType.FieldPathProperty;
                    break;
                case "OptionalProperty":
                    typeAsEnum = EPropertyType.OptionalProperty;
                    break;
                case "FUtf8StrProperty":
                    typeAsEnum = EPropertyType.Utf8StrProperty;
                    break;
                case "AnsiStrProperty":
                    typeAsEnum = EPropertyType.AnsiStrProperty;
                    break;
                default:
                    typeAsEnum = EPropertyType.Unknown;
                    break;
            }

            return typeAsEnum;
        }

        private static UsmapPropertyData SetupUsmapProperty(JmapPropertyBase jmapProp)
        {
            EPropertyType typEnum = EPropertyTypeStringToEnum(jmapProp.Type);
            UsmapPropertyData res = Usmap.InitPropData(typEnum);
            switch (typEnum)
            {
                case EPropertyType.EnumProperty:
                    ((UsmapEnumData)res).InnerType = SetupUsmapProperty(((JmapEnumProperty)jmapProp).Container);
                    ((UsmapEnumData)res).Name = ((JmapEnumProperty)jmapProp).Enum;

                    // strip module path from Name (usmap convention)
                    if (((UsmapEnumData)res).Name.Contains('.')) ((UsmapEnumData)res).Name = ((UsmapEnumData)res).Name.Substring(((UsmapEnumData)res).Name.LastIndexOf('.') + 1);
                    break;
                case EPropertyType.StructProperty:
                    ((UsmapStructData)res).StructType = ((JmapStructProperty)jmapProp).Struct;

                    // strip module path from StructType (usmap convention)
                    if (((UsmapStructData)res).StructType.Contains('.')) ((UsmapStructData)res).StructType = ((UsmapStructData)res).StructType.Substring(((UsmapStructData)res).StructType.LastIndexOf('.') + 1);
                    break;
                case EPropertyType.SetProperty:
                    ((UsmapArrayData)res).InnerType = SetupUsmapProperty(((JmapSetProperty)jmapProp).Key);
                    break;
                case EPropertyType.ArrayProperty:
                    ((UsmapArrayData)res).InnerType = SetupUsmapProperty(((JmapArrayProperty)jmapProp).Inner);
                    break;
                case EPropertyType.OptionalProperty:
                    ((UsmapArrayData)res).InnerType = SetupUsmapProperty(((JmapOptionalProperty)jmapProp).Inner);
                    break;
                case EPropertyType.MapProperty:
                    ((UsmapMapData)res).InnerType = SetupUsmapProperty(((JmapMapProperty)jmapProp).Key);
                    ((UsmapMapData)res).ValueType = SetupUsmapProperty(((JmapMapProperty)jmapProp).Value);
                    break;
                default:
                    break;
            }

            return res;
        }

        public static void ReadSchema(string objectJSON, UsmapSchema templateSchema)
        {
            //Dictionary<string, EPropertyType> propertyTypes = GetPropertyTypesAsEnum(objectJSON);

            JmapObjectBase serializedObject = JsonSerializer.Deserialize<JmapObjectBase>(objectJSON, new JsonSerializerOptions()
            {
                AllowOutOfOrderMetadataProperties = true,
                Converters =
                {
                    new JmapObjectConverter(),
                    new JmapPropertyConverter()
                }
            });
            if (serializedObject is JmapStruct str)
            {
                if (str.SuperStruct != null && str.SuperStruct.Contains("."))
                {
                    templateSchema.SuperType = str.SuperStruct.Substring(str.SuperStruct.LastIndexOf('.') + 1);
                    templateSchema.SuperTypeModulePath = str.SuperStruct.Substring(0, str.SuperStruct.LastIndexOf('.'));
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
                    UsmapProperty usmapProp = new UsmapProperty(jmapProp.Name, propIdx, 0, jmapProp.ArrayDim, null);
                    usmapProp.PropertyData = SetupUsmapProperty(jmapProp);

                    templateSchema.propertiesInternal[propIdx] = usmapProp;
                    for (int i = 0; i < jmapProp.ArrayDim; i++)
                    {
                        UsmapProperty cln = (UsmapProperty)usmapProp.Clone();
                        cln.SchemaIndex = propIdx + i;
                        cln.ArrayIndex = i;
                        templateSchema.propertiesInternal[propIdx + i] = cln;
                    }
                    propIdx += jmapProp.ArrayDim;
                }
                templateSchema.ConstructPropertiesMap(false);
                templateSchema.PropCount = templateSchema.propertiesInternal.Count;
            }

            templateSchema.StructKind = (serializedObject is JmapClass) ? UsmapStructKind.UClass : (serializedObject is JmapScriptStruct ? UsmapStructKind.UScriptStruct : UsmapStructKind.None);
        }

        public static void ReadEnum(string objectJSON, UsmapEnum templateSchema)
        {
            //Dictionary<string, EPropertyType> propertyTypes = GetPropertyTypesAsEnum(objectJSON);

            JmapObjectBase serializedObject = JsonSerializer.Deserialize<JmapObjectBase>(objectJSON, new JsonSerializerOptions()
            {
                AllowOutOfOrderMetadataProperties = true,
                Converters =
                {
                    new JmapObjectConverter(),
                    new JmapPropertyConverter()
                }
            });
            if (serializedObject is JmapEnum str)
            {
                templateSchema.EnumFlags = 0; // not used by UAssetAPI in practice, can populate if needed (need to convert string to int)
                templateSchema._Values = new System.Collections.Concurrent.ConcurrentDictionary<long, string>(str.Values);
                foreach (KeyValuePair<long, string> entry in str.Values)
                {
                    templateSchema._Values[entry.Key] = entry.Value;
                }
            }
        }
    }
}
