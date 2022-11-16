using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using UAssetAPI.FieldTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI
{
    /// <summary>
    /// An entry in the property type registry. Contains the class Type used for standard and struct property serialization.
    /// </summary>
    internal class RegistryEntry
    {
        internal Type PropertyType;
        internal bool HasCustomStructSerialization;
        internal Func<FName, PropertyData> Creator;

        public RegistryEntry()
        {

        }
    }

    /// <summary>
    /// The main serializer for most property types in UAssetAPI.
    /// </summary>
    public static class MainSerializer
    {
#if DEBUG_VERBOSE
        private static PropertyData lastType;
#endif

        private static IDictionary<string, RegistryEntry> _propertyTypeRegistry;

        /// <summary>
        /// The property type registry. Maps serialized property names to their types.
        /// </summary>
        internal static IDictionary<string, RegistryEntry> PropertyTypeRegistry
        {
            get
            {
                InitializePropertyTypeRegistry();
                return _propertyTypeRegistry;
            }
            set => _propertyTypeRegistry = value; // I hope you know what you're doing!
        }

        private static IEnumerable<Assembly> GetDependentAssemblies(Assembly analyzedAssembly)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Where(a => GetNamesOfAssembliesReferencedBy(a).Contains(analyzedAssembly.FullName));
        }

        public static IEnumerable<string> GetNamesOfAssembliesReferencedBy(Assembly assembly)
        {
            return assembly.GetReferencedAssemblies().Select(assemblyName => assemblyName.FullName);
        }

        private static Type registryParentDataType = typeof(PropertyData);

        /// <summary>
        /// Initializes the property type registry.
        /// </summary>
        private static void InitializePropertyTypeRegistry()
        {
            if (_propertyTypeRegistry != null) return;
            _propertyTypeRegistry = new Dictionary<string, RegistryEntry>();

            Assembly[] allDependentAssemblies = GetDependentAssemblies(registryParentDataType.Assembly).ToArray();
            Assembly[] allAssemblies = new Assembly[allDependentAssemblies.Length + 1];
            allAssemblies[0] = registryParentDataType.Assembly;
            Array.Copy(allDependentAssemblies, 0, allAssemblies, 1, allDependentAssemblies.Length);

            for (int i = 0; i < allAssemblies.Length; i++)
            {
                Type[] allPropertyDataTypes = allAssemblies[i].GetTypes().Where(t => t.IsSubclassOf(registryParentDataType)).ToArray();
                for (int j = 0; j < allPropertyDataTypes.Length; j++)
                {
                    Type currentPropertyDataType = allPropertyDataTypes[j];
                    if (currentPropertyDataType == null || currentPropertyDataType.ContainsGenericParameters) continue;

                    var testInstance = Activator.CreateInstance(currentPropertyDataType);

                    FString returnedPropType = currentPropertyDataType.GetProperty("PropertyType")?.GetValue(testInstance, null) as FString;
                    if (returnedPropType == null) continue;
                    bool? returnedHasCustomStructSerialization = currentPropertyDataType.GetProperty("HasCustomStructSerialization")?.GetValue(testInstance, null) as bool?;
                    if (returnedHasCustomStructSerialization == null) continue;
                    bool? returnedShouldBeRegistered = currentPropertyDataType.GetProperty("ShouldBeRegistered")?.GetValue(testInstance, null) as bool?;
                    if (returnedShouldBeRegistered == null) continue;

                    if ((bool)returnedShouldBeRegistered)
                    {
                        RegistryEntry res = new RegistryEntry();
                        res.PropertyType = currentPropertyDataType;
                        res.HasCustomStructSerialization = (bool)returnedHasCustomStructSerialization;

                        var nameParam = Expression.Parameter(typeof(FName));
                        res.Creator = Expression.Lambda<Func<FName, PropertyData>>(
                           Expression.New(currentPropertyDataType.GetConstructor(new[] { typeof(FName), }), new[] { nameParam, }),
                           nameParam
                        ).Compile();

                        _propertyTypeRegistry[returnedPropType.Value] = res;
                    }
                }
            }

            // Fetch the current git commit while we're here
            UAPUtils.CurrentCommit = string.Empty;
            using (Stream stream = registryParentDataType.Assembly.GetManifestResourceStream("UAssetAPI.git_commit.txt"))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        if (reader != null) UAPUtils.CurrentCommit = reader.ReadToEnd().Trim();
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the correct PropertyData class based off of serialized name, type, etc.
        /// </summary>
        /// <param name="type">The serialized type of this property.</param>
        /// <param name="name">The serialized name of this property.</param>
        /// <param name="parentName">The name of the parent class/struct of this property.</param>
        /// <param name="asset">The UAsset which this property is contained within.</param>
        /// <param name="reader">The BinaryReader to read from. If left unspecified, you must call the <see cref="PropertyData.Read(AssetBinaryReader, FName, bool, long, long)"/> method manually.</param>
        /// <param name="leng">The length of this property on disk in bytes.</param>
        /// <param name="duplicationIndex">The duplication index of this property.</param>
        /// <param name="includeHeader">Does this property serialize its header in the current context?</param>
        /// <returns>A new PropertyData instance based off of the passed parameters.</returns>
        public static PropertyData TypeToClass(FName type, FName name, FName parentName, UAsset asset, AssetBinaryReader reader = null, int leng = 0, int duplicationIndex = 0, bool includeHeader = true)
        {
            long startingOffset = 0;
            if (reader != null) startingOffset = reader.BaseStream.Position;

            if (type.Value.Value == "None") return null;

            PropertyData data = null;
            if (PropertyTypeRegistry.ContainsKey(type.Value.Value))
            {
                data = PropertyTypeRegistry[type.Value.Value].Creator.Invoke(name);
            }
            else
            {
#if DEBUG_VERBOSE
                Debug.WriteLine("-----------");
                Debug.WriteLine("Parsing unknown type " + type.ToString());
                Debug.WriteLine("Length: " + leng);
                if (reader != null) Debug.WriteLine("Pos: " + reader.BaseStream.Position);
                Debug.WriteLine("Last type: " + lastType.PropertyType?.ToString());
                if (lastType is ArrayPropertyData) Debug.WriteLine("Last array's type was " + ((ArrayPropertyData)lastType).ArrayType?.ToString());
                if (lastType is StructPropertyData) Debug.WriteLine("Last struct's type was " + ((StructPropertyData)lastType).StructType?.ToString());
                if (lastType is MapPropertyData lastTypeMap)
                {
                    if (lastTypeMap.Value.Count == 0)
                    {
                        Debug.WriteLine("Last map's key type was " + lastTypeMap.KeyType?.ToString());
                        Debug.WriteLine("Last map's value type was " + lastTypeMap.ValueType?.ToString());
                    }
                    else
                    {
                        Debug.WriteLine("Last map's key type was " + lastTypeMap.Value.Keys.ElementAt(0).PropertyType?.ToString());
                        Debug.WriteLine("Last map's value type was " + lastTypeMap.Value[0].PropertyType?.ToString());
                    }
                }
                Debug.WriteLine("-----------");
#endif
                if (leng > 0)
                {
                    data = new UnknownPropertyData(name);
                    ((UnknownPropertyData)data).SetSerializingPropertyType(type.Value);
                }
                else
                {
                    if (reader == null) throw new FormatException("Unknown property type: " + type.ToString() + " (on " + name.ToString() + ")");
                    throw new FormatException("Unknown property type: " + type.ToString() + " (on " + name.ToString() + " at " + reader.BaseStream.Position + ")");
                }
            }

#if DEBUG_VERBOSE
            lastType = data;
#endif

            data.DuplicationIndex = duplicationIndex;
            if (reader != null)
            {
                try
                {
                    data.Read(reader, parentName, includeHeader, leng);
                }
                catch (Exception ex)
                {
                    if (data is StructPropertyData)
                    {
                        data = new RawStructPropertyData(name);
                        data.DuplicationIndex = duplicationIndex;
                        data.Read(reader, parentName, includeHeader, leng);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                if (data.Offset == 0) data.Offset = startingOffset; // fallback
            }

            return data;
        }

        /// <summary>
        /// Reads a property into memory.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from. The underlying stream should be at the position of the property to be read.</param>
        /// <param name="parentName">The name of the parent class/struct of this property.</param>
        /// <param name="includeHeader">Does this property serialize its header in the current context?</param>
        /// <returns>The property read from disk.</returns>
        public static PropertyData Read(AssetBinaryReader reader, FName parentName, bool includeHeader)
        {
            long startingOffset = reader.BaseStream.Position;
            FName name = reader.ReadFName();
            if (name.Value.Value == "None") return null;

            FName type = reader.ReadFName();

            int leng = reader.ReadInt32();
            int duplicationIndex = reader.ReadInt32();
            PropertyData result = TypeToClass(type, name, parentName, reader.Asset, reader, leng, duplicationIndex, includeHeader);
            result.Offset = startingOffset;
            return result;
        }

        internal static readonly Regex allNonLetters = new Regex("[^a-zA-Z]", RegexOptions.Compiled);

        /// <summary>
        /// Reads an FProperty into memory. Primarily used as a part of <see cref="StructExport"/> serialization.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from. The underlying stream should be at the position of the FProperty to be read.</param>
        /// <returns>The FProperty read from disk.</returns>
        public static FProperty ReadFProperty(AssetBinaryReader reader)
        {
            FName serializedType = reader.ReadFName();
            Type requestedType = Type.GetType("UAssetAPI.FieldTypes.F" + allNonLetters.Replace(serializedType.Value.Value, string.Empty));
            if (requestedType == null) requestedType = typeof(FGenericProperty);
            var res = (FProperty)Activator.CreateInstance(requestedType);
            res.SerializedType = serializedType;
            res.Read(reader);
            return res;
        }

        /// <summary>
        /// Serializes an FProperty from memory.
        /// </summary>
        /// <param name="prop">The FProperty to serialize.</param>
        /// <param name="writer">The BinaryWriter to serialize the FProperty to.</param>
        public static void WriteFProperty(FProperty prop, AssetBinaryWriter writer)
        {
            writer.Write(prop.SerializedType);
            prop.Write(writer);
        }

        /// <summary>
        /// Reads a UProperty into memory. Primarily used as a part of <see cref="PropertyExport"/> serialization.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from. The underlying stream should be at the position of the UProperty to be read.</param>
        /// <param name="serializedType">The type of UProperty to be read.</param>
        /// <returns>The FProperty read from disk.</returns>
        public static UProperty ReadUProperty(AssetBinaryReader reader, FName serializedType)
        {
            return ReadUProperty(reader, Type.GetType("UAssetAPI.FieldTypes.U" + allNonLetters.Replace(serializedType.Value.Value, string.Empty)));
        }

        /// <summary>
        /// Reads a UProperty into memory. Primarily used as a part of <see cref="PropertyExport"/> serialization.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from. The underlying stream should be at the position of the UProperty to be read.</param>
        /// <param name="requestedType">The type of UProperty to be read.</param>
        /// <returns>The FProperty read from disk.</returns>
        public static UProperty ReadUProperty(AssetBinaryReader reader, Type requestedType)
        {
            if (requestedType == null) requestedType = typeof(UGenericProperty);
            var res = (UProperty)Activator.CreateInstance(requestedType);
            res.Read(reader);
            return res;
        }

        /// <summary>
        /// Reads a UProperty into memory. Primarily used as a part of <see cref="PropertyExport"/> serialization.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from. The underlying stream should be at the position of the UProperty to be read.</param>
        /// <returns>The FProperty read from disk.</returns>
        public static T ReadUProperty<T>(AssetBinaryReader reader) where T : UProperty
        {
            var res = (UProperty)Activator.CreateInstance(typeof(T));
            res.Read(reader);
            return (T)res;
        }

        /// <summary>
        /// Serializes a UProperty from memory.
        /// </summary>
        /// <param name="prop">The UProperty to serialize.</param>
        /// <param name="writer">The BinaryWriter to serialize the UProperty to.</param>
        public static void WriteUProperty(UProperty prop, AssetBinaryWriter writer)
        {
            prop.Write(writer);
        }

        /// <summary>
        /// Serializes a property from memory.
        /// </summary>
        /// <param name="property">The property to serialize.</param>
        /// <param name="writer">The BinaryWriter to serialize the property to.</param>
        /// <param name="includeHeader">Does this property serialize its header in the current context?</param>
        /// <returns>The serial offset where the length of the property is stored.</returns>
        public static int Write(PropertyData property, AssetBinaryWriter writer, bool includeHeader)
        {
            if (property == null) return 0;

            property.Offset = writer.BaseStream.Position;
            writer.Write(property.Name);
            if (property is UnknownPropertyData unknownProp)
            {
                writer.Write(new FName(writer.Asset, unknownProp.SerializingPropertyType));
            }
            else if (property is RawStructPropertyData)
            {
                writer.Write(new FName(writer.Asset, FString.FromString("StructProperty")));
            }
            else
            {
                writer.Write(new FName(writer.Asset, property.PropertyType));
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
