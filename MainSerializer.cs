using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UAssetAPI.FieldTypes;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI
{
    /**
     * Main serializer for most property types
     */
    public static class MainSerializer
    {
#if DEBUG
        private static PropertyData lastType;
#endif

        private static IDictionary<string, Type> propertyTypeRegistry = null;
        private static Type registryParentDataType = typeof(PropertyData);
        private static void InitializePropertyTypeRegistry()
        {
            if (propertyTypeRegistry != null) return;
            propertyTypeRegistry = new Dictionary<string, Type>();

            Assembly[] allAssemblies = new Assembly[1];
            allAssemblies[0] = registryParentDataType.Assembly;

            for (int i = 0; i < allAssemblies.Length; i++)
            {
                Type[] allPropertyDataTypes = allAssemblies[i].GetTypes().Where(t => t.IsSubclassOf(registryParentDataType)).ToArray();
                for (int j = 0; j < allPropertyDataTypes.Length; j++)
                {
                    Type currentPropertyDataType = allPropertyDataTypes[j];
                    if (currentPropertyDataType == null || currentPropertyDataType.ContainsGenericParameters) continue;

                    FName returnedPropType = currentPropertyDataType.GetProperty("PropertyType")?.GetValue(Activator.CreateInstance(currentPropertyDataType), null) as FName;
                    if (returnedPropType == null) continue;

                    propertyTypeRegistry[returnedPropType.Value.Value] = currentPropertyDataType;
                }
            }
        }

        public static PropertyData TypeToClass(FName type, FName name, UAsset asset, BinaryReader reader = null, int leng = 0, int duplicationIndex = 0, bool includeHeader = true)
        {
            InitializePropertyTypeRegistry();

            PropertyData data = null;
            if (propertyTypeRegistry.ContainsKey(type.Value.Value))
            {
                data = (PropertyData)Activator.CreateInstance(propertyTypeRegistry[type.Value.Value], name, asset);
            }
            else
            {
#if DEBUG
                Debug.WriteLine("-----------");
                Debug.WriteLine("Parsing unknown type " + type.ToString());
                Debug.WriteLine("Length: " + leng);
                if (reader != null) Debug.WriteLine("Pos: " + reader.BaseStream.Position);
                Debug.WriteLine("Last type: " + lastType.PropertyType.ToString());
                if (lastType is StructPropertyData) Debug.WriteLine("Last struct's type was " + ((StructPropertyData)lastType).StructType.ToString());
                Debug.WriteLine("-----------");
#endif
                if (leng > 0)
                {
                    data = new UnknownPropertyData(name, asset);
                    ((UnknownPropertyData)data).SetSerializingPropertyType(type);
                }
                else
                {
                    if (reader == null) throw new FormatException("Unknown property type: " + type.ToString() + " (on " + name.ToString() + ")");
                    throw new FormatException("Unknown property type: " + type.ToString() + " (on " + name.ToString() + " at " + reader.BaseStream.Position + ")");
                }
            }

#if DEBUG
            lastType = data;
#endif

            data.DuplicationIndex = duplicationIndex;
            if (reader != null)
            {
                data.Read(reader, includeHeader, leng);
            }
            return data;
        }

        public static PropertyData Read(UAsset asset, BinaryReader reader, bool includeHeader)
        {
            FName name = reader.ReadFName(asset);
            if (name.Value.Value == "None") return null;

            FName type = reader.ReadFName(asset);

            int leng = reader.ReadInt32();
            int duplicationIndex = reader.ReadInt32();
            PropertyData result = TypeToClass(type, name, asset, reader, leng, duplicationIndex, includeHeader);
            return result;
        }

        private static readonly Regex allNonLetters = new Regex("[^a-zA-Z]", RegexOptions.Compiled);
        public static FProperty ReadFProperty(UAsset asset, BinaryReader reader)
        {
            FName serializedType = reader.ReadFName(asset);
            Type requestedType = Type.GetType("UAssetAPI.FieldTypes.F" + allNonLetters.Replace(serializedType.Value.Value, string.Empty));
            if (requestedType == null) requestedType = typeof(FGenericProperty);
            var res = (FProperty)Activator.CreateInstance(requestedType);
            res.SerializedType = serializedType;
            res.Read(reader, asset);
            return res;
        }

        public static void WriteFProperty(FProperty prop, UAsset asset, BinaryWriter writer)
        {
            writer.WriteFName(prop.SerializedType, asset);
            prop.Write(writer, asset);
        }

        public static int Write(PropertyData property, UAsset asset, BinaryWriter writer, bool includeHeader) // Returns location of the length
        {
            if (property == null) return 0;

            writer.WriteFName(property.Name, asset);
            if (property is UnknownPropertyData unknownProp)
            {
                writer.WriteFName(unknownProp.SerializingPropertyType, asset);
            }
            else
            {
                writer.WriteFName(property.PropertyType, asset);
            }
            int oldLoc = (int)writer.BaseStream.Position;
            writer.Write((int)0); // initial length
            writer.Write(property.DuplicationIndex);
            int realLength = property.Write(writer, includeHeader);
            int newLoc = (int)writer.BaseStream.Position;

            writer.Seek(oldLoc, SeekOrigin.Begin);
            writer.Write(realLength);
            writer.Seek(newLoc, SeekOrigin.Begin);
            return oldLoc;
        }
    }
}
