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

            JmapObjectBase res = null;
            switch (typeDiscriminator)
            {
                case "Object":
                    res = JsonSerializer.Deserialize<JmapObject>(ref reader, options);
                    break;
                case "Package":
                    res = JsonSerializer.Deserialize<JmapPackage>(ref reader, options);
                    break;
                case "ScriptStruct":
                    res = JsonSerializer.Deserialize<JmapScriptStruct>(ref reader, options);
                    break;
                case "Class":
                    res = JsonSerializer.Deserialize<JmapClass>(ref reader, options);
                    break;
                case "Function":
                    res = JsonSerializer.Deserialize<JmapFunction>(ref reader, options);
                    break;
                case "Enum":
                    res = JsonSerializer.Deserialize<JmapEnum>(ref reader, options);
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

            JmapPropertyBase res = null;
            switch(typeDiscriminator)
            {
                case "StructProperty":
                    res = JsonSerializer.Deserialize<JmapStructProperty>(ref reader, options);
                    break;
                case "ArrayProperty":
                    res = JsonSerializer.Deserialize<JmapArrayProperty>(ref reader, options);
                    break;
                case "EnumProperty":
                    res = JsonSerializer.Deserialize<JmapEnumProperty>(ref reader, options);
                    break;
                case "MapProperty":
                    res = JsonSerializer.Deserialize<JmapMapProperty>(ref reader, options);
                    break;
                case "SetProperty":
                    res = JsonSerializer.Deserialize<JmapSetProperty>(ref reader, options);
                    break;
                case "ByteProperty":
                    res = JsonSerializer.Deserialize<JmapByteProperty>(ref reader, options);
                    break;
                case "ObjectProperty":
                    res = JsonSerializer.Deserialize<JmapObjectProperty>(ref reader, options);
                    break;
                case "ClassProperty":
                    res = JsonSerializer.Deserialize<JmapClassProperty>(ref reader, options);
                    break;
                case "WeakObjectProperty":
                    res = JsonSerializer.Deserialize<JmapWeakObjectProperty>(ref reader, options);
                    break;
                case "SoftObjectProperty":
                    res = JsonSerializer.Deserialize<JmapSoftObjectProperty>(ref reader, options);
                    break;
                case "SoftClassProperty":
                    res = JsonSerializer.Deserialize<JmapSoftClassProperty>(ref reader, options);
                    break;
                case "LazyObjectProperty":
                    res = JsonSerializer.Deserialize<JmapLazyObjectProperty>(ref reader, options);
                    break;
                case "InterfaceProperty":
                    res = JsonSerializer.Deserialize<JmapInterfaceProperty>(ref reader, options);
                    break;
                case "OptionalProperty":
                    res = JsonSerializer.Deserialize<JmapOptionalProperty>(ref reader, options);
                    break;
                default:
                    res = JsonSerializer.Deserialize<JmapProperty>(ref reader, options);
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
            UsmapPropertyType typeAsEnum = UsmapPropertyType.Unknown;
            switch (type)
            {
                case "StructProperty":
                    typeAsEnum = UsmapPropertyType.StructProperty;
                    break;
                case "StrProperty":
                    typeAsEnum = UsmapPropertyType.StrProperty;
                    break;
                case "NameProperty":
                    typeAsEnum = UsmapPropertyType.NameProperty;
                    break;
                case "TextProperty":
                    typeAsEnum = UsmapPropertyType.TextProperty;
                    break;
                case "MulticastInlineDelegateProperty":
                    typeAsEnum = UsmapPropertyType.MulticastDelegateProperty;
                    break;
                case "MulticastSparseDelegateProperty":
                    typeAsEnum = UsmapPropertyType.MulticastDelegateProperty;
                    break;
                case "MulticastDelegateProperty":
                    typeAsEnum = UsmapPropertyType.MulticastDelegateProperty;
                    break;
                case "DelegateProperty":
                    typeAsEnum = UsmapPropertyType.DelegateProperty;
                    break;
                case "BoolProperty":
                    typeAsEnum = UsmapPropertyType.BoolProperty;
                    break;
                case "ArrayProperty":
                    typeAsEnum = UsmapPropertyType.ArrayProperty;
                    break;
                case "EnumProperty":
                    typeAsEnum = UsmapPropertyType.EnumProperty;
                    break;
                case "MapProperty":
                    typeAsEnum = UsmapPropertyType.MapProperty;
                    break;
                case "SetProperty":
                    typeAsEnum = UsmapPropertyType.SetProperty;
                    break;
                case "FloatProperty":
                    typeAsEnum = UsmapPropertyType.FloatProperty;
                    break;
                case "DoubleProperty":
                    typeAsEnum = UsmapPropertyType.DoubleProperty;
                    break;
                case "ByteProperty":
                    typeAsEnum = UsmapPropertyType.ByteProperty;
                    break;
                case "UInt16Property":
                    typeAsEnum = UsmapPropertyType.UInt16Property;
                    break;
                case "UInt32Property":
                    typeAsEnum = UsmapPropertyType.UInt32Property;
                    break;
                case "UInt64Property":
                    typeAsEnum = UsmapPropertyType.UInt64Property;
                    break;
                case "Int8Property":
                    typeAsEnum = UsmapPropertyType.Int8Property;
                    break;
                case "Int16Property":
                    typeAsEnum = UsmapPropertyType.Int16Property;
                    break;
                case "IntProperty":
                    typeAsEnum = UsmapPropertyType.IntProperty;
                    break;
                case "Int64Property":
                    typeAsEnum = UsmapPropertyType.Int64Property;
                    break;
                case "ObjectProperty":
                    typeAsEnum = UsmapPropertyType.ObjectProperty;
                    break;
                case "ClassProperty":
                    typeAsEnum = UsmapPropertyType.ObjectProperty;
                    break;
                case "WeakObjectProperty":
                    typeAsEnum = UsmapPropertyType.WeakObjectProperty;
                    break;
                case "SoftObjectProperty":
                    typeAsEnum = UsmapPropertyType.SoftObjectProperty;
                    break;
                case "SoftClassProperty":
                    typeAsEnum = UsmapPropertyType.SoftObjectProperty;
                    break;
                case "LazyObjectProperty":
                    typeAsEnum = UsmapPropertyType.LazyObjectProperty;
                    break;
                case "InterfaceProperty":
                    typeAsEnum = UsmapPropertyType.InterfaceProperty;
                    break;
                case "FieldPathProperty":
                    typeAsEnum = UsmapPropertyType.FieldPathProperty;
                    break;
                case "OptionalProperty":
                    typeAsEnum = UsmapPropertyType.OptionalProperty;
                    break;
                case "FUtf8StrProperty":
                    typeAsEnum = UsmapPropertyType.Utf8StrProperty;
                    break;
                case "AnsiStrProperty":
                    typeAsEnum = UsmapPropertyType.AnsiStrProperty;
                    break;
                default:
                    typeAsEnum = UsmapPropertyType.Unknown;
                    break;
            }

            return typeAsEnum;
        }

        private static UsmapPropertyData SetupUsmapProperty(JmapPropertyBase jmapProp)
        {
            UsmapPropertyType typEnum = UsmapPropertyTypeStringToEnum(jmapProp.Type);
            UsmapPropertyData res = Usmap.InitPropData(typEnum);
            switch (typEnum)
            {
                case UsmapPropertyType.EnumProperty:
                    ((UsmapEnumData)res).InnerType = SetupUsmapProperty(((JmapEnumProperty)jmapProp).Container);
                    ((UsmapEnumData)res).Name = ((JmapEnumProperty)jmapProp).Enum;

                    // strip module path from Name (usmap convention)
                    if (((UsmapEnumData)res).Name.Contains('.')) ((UsmapEnumData)res).Name = ((UsmapEnumData)res).Name.Substring(((UsmapEnumData)res).Name.LastIndexOf('.') + 1);
                    break;
                case UsmapPropertyType.StructProperty:
                    ((UsmapStructData)res).StructType = ((JmapStructProperty)jmapProp).Struct;

                    // strip module path from StructType (usmap convention)
                    if (((UsmapStructData)res).StructType.Contains('.')) ((UsmapStructData)res).StructType = ((UsmapStructData)res).StructType.Substring(((UsmapStructData)res).StructType.LastIndexOf('.') + 1);
                    break;
                case UsmapPropertyType.SetProperty:
                    ((UsmapArrayData)res).InnerType = SetupUsmapProperty(((JmapSetProperty)jmapProp).Key);
                    break;
                case UsmapPropertyType.ArrayProperty:
                    ((UsmapArrayData)res).InnerType = SetupUsmapProperty(((JmapArrayProperty)jmapProp).Inner);
                    break;
                case UsmapPropertyType.OptionalProperty:
                    ((UsmapArrayData)res).InnerType = SetupUsmapProperty(((JmapOptionalProperty)jmapProp).Inner);
                    break;
                case UsmapPropertyType.MapProperty:
                    ((UsmapMapData)res).InnerType = SetupUsmapProperty(((JmapMapProperty)jmapProp).Key);
                    ((UsmapMapData)res).ValueType = SetupUsmapProperty(((JmapMapProperty)jmapProp).Value);
                    break;
                default:
                    break;
            }

            return res;
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
