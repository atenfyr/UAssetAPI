using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UAssetAPI.Unversioned
{
    // note that the following classes do not necessarily explicitly specify all the fields that jmap actually serializes, only fields relevant to us
    // all other fields are serialized in the Other dictionary

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(JmapObject), typeDiscriminator: "Object")]
    [JsonDerivedType(typeof(JmapPackage), typeDiscriminator: "Package")]
    [JsonDerivedType(typeof(JmapStruct), typeDiscriminator: "Struct")] // this is non-standard, type discriminator "Struct" does not actually exist in jmap
    [JsonDerivedType(typeof(JmapScriptStruct), typeDiscriminator: "ScriptStruct")]
    [JsonDerivedType(typeof(JmapClass), typeDiscriminator: "Class")]
    [JsonDerivedType(typeof(JmapFunction), typeDiscriminator: "Function")]
    [JsonDerivedType(typeof(JmapEnum), typeDiscriminator: "Enum")]
    public class JmapObject
    {
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

    public class JmapPackage : JmapObject
    {
        // from Package
        // (none)
    }

    public class JmapStruct : JmapObject
    {
        // from Struct
        [JsonPropertyName("super_struct")]
        [JsonInclude]
        public string SuperStruct;
        [JsonPropertyName("properties")]
        [JsonInclude]
        public List<JmapProperty> Properties;
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

    public class JmapEnum : JmapObject
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

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
    [JsonDerivedType(typeof(JmapProperty), typeDiscriminator: "Property")] // "Property" type discriminator is non-standard
    [JsonDerivedType(typeof(JmapStructProperty), typeDiscriminator: "StructProperty")]
    [JsonDerivedType(typeof(JmapArrayProperty), typeDiscriminator: "ArrayProperty")]
    [JsonDerivedType(typeof(JmapEnumProperty), typeDiscriminator: "EnumProperty")]
    [JsonDerivedType(typeof(JmapMapProperty), typeDiscriminator: "MapProperty")]
    [JsonDerivedType(typeof(JmapSetProperty), typeDiscriminator: "SetProperty")]
    [JsonDerivedType(typeof(JmapByteProperty), typeDiscriminator: "ByteProperty")]
    [JsonDerivedType(typeof(JmapObjectProperty), typeDiscriminator: "ObjectProperty")]
    [JsonDerivedType(typeof(JmapClassProperty), typeDiscriminator: "ClassProperty")]
    [JsonDerivedType(typeof(JmapWeakObjectProperty), typeDiscriminator: "WeakObjectProperty")]
    [JsonDerivedType(typeof(JmapSoftObjectProperty), typeDiscriminator: "SoftObjectProperty")]
    [JsonDerivedType(typeof(JmapSoftClassProperty), typeDiscriminator: "SoftClassProperty")]
    [JsonDerivedType(typeof(JmapLazyObjectProperty), typeDiscriminator: "LazyObjectProperty")]
    [JsonDerivedType(typeof(JmapInterfaceProperty), typeDiscriminator: "InterfaceProperty")]
    [JsonDerivedType(typeof(JmapOptionalProperty), typeDiscriminator: "OptionalProperty")]
    public class JmapProperty
    {
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

    public class JmapStructProperty : JmapProperty
    {
        [JsonPropertyName("struct")]
        [JsonInclude]
        public string Struct;
    }

    public class JmapArrayProperty : JmapProperty
    {
        [JsonPropertyName("inner")]
        [JsonInclude]
        public JmapProperty Inner;
    }

    public class JmapEnumProperty : JmapProperty
    {
        [JsonPropertyName("container")]
        [JsonInclude]
        public JmapProperty Container;
        [JsonPropertyName("enum")]
        [JsonInclude]
        public string Enum;
    }

    public class JmapMapProperty : JmapProperty
    {
        [JsonPropertyName("key_prop")]
        [JsonInclude]
        public JmapProperty Key;
        [JsonPropertyName("value_prop")]
        [JsonInclude]
        public JmapProperty Value;
    }

    public class JmapSetProperty : JmapProperty
    {
        [JsonPropertyName("key_prop")]
        [JsonInclude]
        public JmapProperty Key;
    }

    public class JmapByteProperty : JmapProperty
    {
        [JsonPropertyName("enum")]
        [JsonInclude]
        public string Enum;
    }


    public class JmapObjectProperty : JmapProperty
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
    }

    public class JmapClassProperty : JmapProperty
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
        [JsonPropertyName("meta_class")]
        [JsonInclude]
        public string MetaClass;
    }

    public class JmapWeakObjectProperty : JmapProperty
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
    }

    public class JmapSoftObjectProperty : JmapProperty
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
    }

    public class JmapSoftClassProperty : JmapProperty
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
        [JsonPropertyName("meta_class")]
        [JsonInclude]
        public string MetaClass;
    }

    public class JmapLazyObjectProperty : JmapProperty
    {
        [JsonPropertyName("property_class")]
        [JsonInclude]
        public string PropertyClass;
    }

    public class JmapInterfaceProperty : JmapProperty
    {
        [JsonPropertyName("interface_class")]
        [JsonInclude]
        public string InterfaceClass;
    }

    public class JmapOptionalProperty : JmapProperty
    {
        [JsonPropertyName("inner")]
        [JsonInclude]
        public JmapProperty Inner;
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
        private static Dictionary<string, EPropertyType> GetPropertyTypesAsEnum(string objectJSON)
        {
            Dictionary<string, EPropertyType> output = new Dictionary<string, EPropertyType>();
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(objectJSON));

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "properties")
                {
                    string name = null;
                    string type = null;
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.EndObject)
                        {
                            name = null;
                            type = null;
                        }

                        if (reader.TokenType == JsonTokenType.PropertyName)
                        {
                            switch(reader.GetString())
                            {
                                case "name":
                                    reader.Read();
                                    name = reader.GetString();
                                    break;
                                case "type":
                                    reader.Read();
                                    type = reader.GetString();
                                    break;
                            }
                        }

                        if (name != null && type != null)
                        {
                            EPropertyType typeAsEnum = EPropertyType.Unknown;
                            switch(type)
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
                                    typeAsEnum = EPropertyType.Unknown;
                                    break;
                                case "WeakObjectProperty":
                                    typeAsEnum = EPropertyType.WeakObjectProperty;
                                    break;
                                case "SoftObjectProperty":
                                    typeAsEnum = EPropertyType.SoftObjectProperty;
                                    break;
                                case "SoftClassProperty":
                                    typeAsEnum = EPropertyType.Unknown;
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

                            output[name] = typeAsEnum;
                        }
                    }
                }
            }

            return output;
        }

        public static void ReadSchema(string objectJSON, UsmapSchema templateSchema)
        {
            Dictionary<string, EPropertyType> propertyTypes = GetPropertyTypesAsEnum(objectJSON);

            JmapObject serializedObject = JsonSerializer.Deserialize<JmapObject>(objectJSON, new JsonSerializerOptions()
            {
                AllowOutOfOrderMetadataProperties = true,
            });
            if (serializedObject is JmapStruct str)
            {
                templateSchema.SuperType = str.SuperStruct;
                if (str.SuperStruct.Contains(".")) templateSchema.SuperTypeModulePath = str.SuperStruct.Substring(0, str.SuperStruct.LastIndexOf('.'));

                templateSchema.propertiesInternal = new System.Collections.Concurrent.ConcurrentDictionary<int, UsmapProperty>();
                int propIdx = 0;
                foreach (JmapProperty jmapProp in str.Properties)
                {
                    UsmapProperty usmapProp = new UsmapProperty(jmapProp.Name, propIdx, 0, jmapProp.ArrayDim, null);
                    //usmapProp.PropertyData = Usmap.

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
                templateSchema.PropCount = templateSchema.Properties.Count;
            }

            templateSchema.StructKind = (serializedObject is JmapClass) ? UsmapStructKind.UClass : (serializedObject is JmapScriptStruct ? UsmapStructKind.UScriptStruct : UsmapStructKind.None);
        }
    }
}
