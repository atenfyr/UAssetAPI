using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using UAssetAPI.ExportTypes;
using UAssetAPI.FieldTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

#if DEBUGVERBOSE
using System.Diagnostics;
#endif

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
#if DEBUGVERBOSE
        private static PropertyData lastType;
#endif
        public static string[] AdditionalPropertyRegistry = ["ClassProperty", "SoftClassProperty", "AssetClassProperty"];

        private static IDictionary<string, RegistryEntry> _propertyTypeRegistry;

        /// <summary>
        /// The property type registry. Maps serialized property names to their types.
        /// </summary>
        internal static IDictionary<string, RegistryEntry> PropertyTypeRegistry
        {
            get => _propertyTypeRegistry;
            set => _propertyTypeRegistry = value; // I hope you know what you're doing!
        }

        static MainSerializer()
        {
            InitializePropertyTypeRegistry();
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
        /// Generates an unversioned header based on a list of properties, and sorts the list in the correct order to be serialized.
        /// </summary>
        /// <param name="data">The list of properties to sort and generate an unversioned header from.</param>
        /// <param name="parentName">The name of the parent of all the properties.</param>
        /// <param name="parentModulePath">The path to the module that the parent class/struct of this property is contained within.</param>
        /// <param name="asset">The UAsset which the properties are contained within.</param>
        public static FUnversionedHeader GenerateUnversionedHeader(ref List<PropertyData> data, FName parentName, FName parentModulePath, UAsset asset)
        {
            var sortedProps = new List<PropertyData>();
            if (!asset.HasUnversionedProperties) return null; // no point in wasting time generating it
            if (asset.Mappings == null) return null;

            int firstNumAll = int.MaxValue;
            int lastNumAll = int.MinValue;
            HashSet<int> propertiesToTouch = new HashSet<int>();
            Dictionary<int, PropertyData> propMap = new Dictionary<int, PropertyData>();
            HashSet<int> zeroProps = new HashSet<int>();
            foreach (PropertyData entry in data)
            {
                if (!asset.Mappings.TryGetProperty<UsmapProperty>(entry.Name, entry.Ancestry, entry.ArrayIndex, asset, out _, out int idx)) throw new FormatException("No valid property \"" + entry.Name.ToString() + "\" in class " + entry.Ancestry.Parent.ToString());
                propMap[idx] = entry;
                if (entry.CanBeZero(asset) && entry.IsZero) zeroProps.Add(idx);

                if (idx < firstNumAll) firstNumAll = idx;
                if (idx > lastNumAll) lastNumAll = idx;
                propertiesToTouch.Add(idx);
            }

            int lastNumBefore = -1;
            List<FFragment> allFrags = new List<FFragment>();
            if (propertiesToTouch.Count > 0)
            {
                while (true)
                {
                    HashSet<int> fragmentHasAnyZeros = new HashSet<int>(); // add 0 if any zeros from 0 to (FFragment.ValueMax-1), 1 if any zeros from FFragment.ValueMax to (FFragment.ValueMax*2-1), etc.

                    int firstNum = lastNumBefore + 1;
                    while (!propertiesToTouch.Contains(firstNum) && firstNum <= lastNumAll) firstNum++;
                    if (firstNum > lastNumAll) break;

#if DEBUGVERBOSE
                    if (allFrags.Count > 0) Debug.WriteLine("W: " + allFrags[allFrags.Count - 1]);
#endif

                    int lastNum = firstNum;
                    while (propertiesToTouch.Contains(lastNum))
                    {
                        if (zeroProps.Contains(lastNum))
                        {
                            int valueNum = lastNum - firstNum + 1;
                            fragmentHasAnyZeros.Add(valueNum / FFragment.ValueMax);
                        }
                        sortedProps.Add(propMap[lastNum]);

                        lastNum++;
                    }
                    lastNum--;

                    var newFrag = FFragment.GetFromBounds(lastNumBefore, firstNum, lastNum, fragmentHasAnyZeros.Contains(0), false);

                    // add extra 127s if we went over the max for either skip or value
                    while (newFrag.SkipNum > FFragment.SkipMax)
                    {
                        allFrags.Add(new FFragment(FFragment.SkipMax, 0, false, false));
                        newFrag.SkipNum -= FFragment.SkipMax;
                    }
                    int fragIdx = 0;
                    while (newFrag.ValueNum > FFragment.ValueMax)
                    {
                        allFrags.Add(new FFragment(newFrag.SkipNum, FFragment.ValueMax, false, fragmentHasAnyZeros.Contains(fragIdx), firstNum + FFragment.ValueMax * fragIdx));
                        newFrag.ValueNum -= FFragment.ValueMax;
                        newFrag.FirstNum += FFragment.ValueMax;
                        newFrag.SkipNum = 0;
                        newFrag.bHasAnyZeroes = fragmentHasAnyZeros.Contains(++fragIdx);
                    }

                    allFrags.Add(newFrag);
                    lastNumBefore = lastNum;
                }
                allFrags[allFrags.Count - 1].bIsLast = true;
#if DEBUGVERBOSE
                Debug.WriteLine("W: " + allFrags[allFrags.Count - 1]);
#endif
            }
            else
            {
                // add "blank" fragment
                // i'm pretty sure that any SkipNum should work here as long as ValueNum = 0, but this is what the engine does
                string highestSchema = parentName?.ToString();

                // i doubt that this is true, empirically tested; need more data
                int numSkip = 0;
                if (asset.ObjectVersionUE5 >= ObjectVersionUE5.ADD_SOFTOBJECTPATH_LIST)
                {
                    numSkip = Math.Min(asset.Mappings.GetAllProperties(highestSchema, parentModulePath?.ToString(), asset).Count, FFragment.SkipMax);
                }
                else
                {
                    numSkip = asset.Mappings.Schemas[highestSchema].Properties.Count == 0 ? 0 : Math.Min(asset.Mappings.GetAllProperties(highestSchema, parentModulePath?.ToString(), asset).Count, FFragment.SkipMax);
                }
                allFrags.Add(new FFragment(numSkip, 0, true, false));
            }

            // generate zero mask
            bool bHasNonZeroValues = false;
            List<bool> zeroMaskList = new List<bool>();
            foreach (var frag in allFrags)
            {
                if (frag.bHasAnyZeroes)
                {
                    for (int i = 0; i < frag.ValueNum; i++)
                    {
                        bool isZero = zeroProps.Contains(frag.FirstNum + i);
                        if (!isZero) bHasNonZeroValues = true;
                        zeroMaskList.Add(isZero);
                    }
                }
            }
            BitArray zeroMask = new BitArray(zeroMaskList.ToArray());

            var res = new FUnversionedHeader();
            res.Fragments = new LinkedList<FFragment>();
            foreach (var frag in allFrags) res.Fragments.AddLast(frag);
            res.ZeroMask = zeroMask;
            res.bHasNonZeroValues = bHasNonZeroValues;
            if (res.Fragments.Count > 0)
            {
                res.CurrentFragment = res.Fragments.First;
                res.UnversionedPropertyIndex = res.CurrentFragment.Value.FirstNum;
            }

            data.Clear();
            data.AddRange(sortedProps);
            return res;
        }

        /// <summary>
        /// Initializes the correct PropertyData class based off of serialized name, type, etc.
        /// </summary>
        /// <param name="type">The serialized type of this property.</param>
        /// <param name="name">The serialized name of this property.</param>
        /// <param name="ancestry">The ancestry of the parent of this property.</param>
        /// <param name="parentName">The name of the parent class/struct of this property.</param>
        /// <param name="parentModulePath">The path to the module that the parent class/struct of this property is contained within.</param>
        /// <param name="asset">The UAsset which this property is contained within.</param>
        /// <param name="reader">The BinaryReader to read from. If left unspecified, you must call the <see cref="PropertyData.Read(AssetBinaryReader, bool, long, long, PropertySerializationContext)"/> method manually.</param>
        /// <param name="leng">The length of this property on disk in bytes.</param>
        /// <param name="propertyTagFlags">Property tag flags, if available.</param>
        /// <param name="ArrayIndex">The duplication index of this property.</param>
        /// <param name="includeHeader">Does this property serialize its header in the current context?</param>
        /// <param name="isZero">Is the body of this property empty?</param>
        /// <returns>A new PropertyData instance based off of the passed parameters.</returns>
        public static PropertyData TypeToClass(FName type, FName name, AncestryInfo ancestry, FName parentName, FName parentModulePath, UAsset asset, AssetBinaryReader reader = null, int leng = 0, EPropertyTagFlags propertyTagFlags = EPropertyTagFlags.None, int ArrayIndex = 0, bool includeHeader = true, bool isZero = false)
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
#if DEBUGVERBOSE
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

#if DEBUGVERBOSE
            lastType = data;
#endif

            data.IsZero = isZero;
            data.PropertyTagFlags = propertyTagFlags;
            data.Ancestry.Initialize(ancestry, parentName, parentModulePath);
            data.ArrayIndex = ArrayIndex;
            if (reader != null && !isZero)
            {
                long posBefore = reader.BaseStream.Position;
                try
                {
                    data.Read(reader, includeHeader, leng);
                }
                catch (Exception)
                {
                    // if asset is unversioned, bubble the error up to make the whole export fail
                    // because unversioned headers aren't properly reconstructed currently
                    if (data is StructPropertyData && !reader.Asset.HasUnversionedProperties)
                    {
                        reader.BaseStream.Position = posBefore;
                        data = new RawStructPropertyData(name);
                        data.Ancestry.Initialize(ancestry, parentName, parentModulePath);
                        data.ArrayIndex = ArrayIndex;
                        data.Read(reader, includeHeader, leng);
                    }
                    else
                    {
                        throw;
                    }
                }
                if (data.Offset == 0) data.Offset = startingOffset; // fallback
            }
            else if (reader != null && isZero)
            {
                data.InitializeZero(reader);
            }

            return data;
        }

        /// <summary>
        /// Reads a property into memory.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from. The underlying stream should be at the position of the property to be read.</param>
        /// <param name="ancestry">The ancestry of the parent of this property.</param>
        /// <param name="parentName">The name of the parent class/struct of this property.</param>
        /// <param name="parentModulePath">The path to the module that the parent class/struct of this property is contained within.</param>
        /// <param name="header">The unversioned header to be used when reading this property. Leave null if none exists.</param>
        /// <param name="includeHeader">Does this property serialize its header in the current context?</param>
        /// <returns>The property read from disk.</returns>
        public static PropertyData Read(AssetBinaryReader reader, AncestryInfo ancestry, FName parentName, FName parentModulePath, FUnversionedHeader header, bool includeHeader)
        {
            long startingOffset = reader.BaseStream.Position;
            FName name = null;
            FName type = null;
            int leng = 0;
            EPropertyTagFlags propertyTagFlags = EPropertyTagFlags.None;
            int ArrayIndex = 0;
            string structType = null;
            bool isZero = false;

            if (reader.Asset.HasUnversionedProperties)
            {
                if (reader.Asset.Mappings == null)
                {
                    throw new InvalidMappingsException();
                }

                UsmapSchema relevantSchema = reader.Asset.Mappings.GetSchemaFromName(parentName.Value.Value, reader.Asset, parentModulePath?.Value.Value);
                while (header.UnversionedPropertyIndex > header.CurrentFragment.Value.LastNum)
                {
                    if (header.CurrentFragment.Value.bIsLast) return null;
                    header.CurrentFragment = header.CurrentFragment.Next;
                    header.UnversionedPropertyIndex = header.CurrentFragment.Value.FirstNum;
                }

                int practicingUnversionedPropertyIndex = header.UnversionedPropertyIndex;
                while (practicingUnversionedPropertyIndex >= relevantSchema.PropCount) // if needed, go to parent struct
                {
                    practicingUnversionedPropertyIndex -= relevantSchema.PropCount;
                    relevantSchema = (relevantSchema.SuperType != null && reader.Asset.Mappings.Schemas.ContainsKey(relevantSchema.SuperType)) ? reader.Asset.Mappings.Schemas[relevantSchema.SuperType] : null;
                    if (relevantSchema == null) throw new FormatException("Failed to find a valid property for schema index " + header.UnversionedPropertyIndex + " in the class " + parentName.Value.Value);
                }
                UsmapProperty relevantProperty = relevantSchema.Properties[practicingUnversionedPropertyIndex];
                header.UnversionedPropertyIndex += 1;

                name = FName.DefineDummy(reader.Asset, relevantProperty.Name);
                type = FName.DefineDummy(reader.Asset, relevantProperty.PropertyData.Type.ToString());
                leng = 1; // not serialized
                ArrayIndex = relevantProperty.ArrayIndex;
                if (relevantProperty.PropertyData is UsmapStructData usmapStruc) structType = usmapStruc.StructType;

                // check if property is zero
                if (header.CurrentFragment.Value.bHasAnyZeroes)
                {
                    isZero = header.ZeroMaskIndex >= header.ZeroMask.Count ? false : header.ZeroMask.Get(header.ZeroMaskIndex);
                    header.ZeroMaskIndex++;
                }
            }
            else if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.PROPERTY_TAG_COMPLETE_TYPE_NAME)
            {
                name = reader.ReadFName();
                if (name.Value.Value == "None") return null;

                List<FName> types = new List<FName>();
                int numNamesLeft = 1;
                while (numNamesLeft > 0)
                {
                    types.Add(reader.ReadFName());
                    numNamesLeft += reader.ReadInt32();
                    numNamesLeft--;
                }

                // SUPER DUPER TODO: information is lost by doing this, ideally we should be able to pass a new FPropertyTypeName type into TypeToClass
                type = types.Count == 0 ? new FName(reader.Asset, "None") : types[0];

                leng = reader.ReadInt32();
                propertyTagFlags = (EPropertyTagFlags)reader.ReadByte();

                if (propertyTagFlags.HasFlag(EPropertyTagFlags.HasArrayIndex))
                {
                    ArrayIndex = reader.ReadInt32();
                }
            }
            else
            {
                name = reader.ReadFName();
                if (name.Value.Value == "None") return null;

                type = reader.ReadFName();

                leng = reader.ReadInt32();
                ArrayIndex = reader.ReadInt32();
            }

            PropertyData result = TypeToClass(type, name, ancestry, parentName, parentModulePath, reader.Asset, reader, leng, propertyTagFlags, ArrayIndex, includeHeader, isZero);
            if (structType != null && result is StructPropertyData strucProp) strucProp.StructType = FName.DefineDummy(reader.Asset, structType);
            result.Offset = startingOffset;
            //Debug.WriteLine(type);
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
            if (property == null) return -1;

            property.Offset = writer.BaseStream.Position;

            if (writer.Asset.HasUnversionedProperties)
            {
                if (!property.IsZero || !property.CanBeZero(writer.Asset)) property.Write(writer, includeHeader);
                return -1; // length is not serialized
            }
            else if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.PROPERTY_TAG_COMPLETE_TYPE_NAME)
            {
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
                writer.Write((int)0); // dummy InnerCount; again super duper todo, should serialize whole FPropertyTypeName tree, be able to reconstruct it with mappings if needed

                // update flags appropriately
                if (property is BoolPropertyData bProp)
                {
                    if (bProp.Value) property.PropertyTagFlags |= EPropertyTagFlags.BoolTrue;
                    else property.PropertyTagFlags &= ~EPropertyTagFlags.BoolTrue;
                }

                if (property.ArrayIndex != 0) property.PropertyTagFlags |= EPropertyTagFlags.HasArrayIndex;
                else property.PropertyTagFlags &= ~EPropertyTagFlags.HasArrayIndex;

                if (property.PropertyGuid != null) property.PropertyTagFlags |= EPropertyTagFlags.HasPropertyGuid;
                else property.PropertyTagFlags &= ~EPropertyTagFlags.HasPropertyGuid;

                int oldLoc = (int)writer.BaseStream.Position;
                writer.Write((int)0); // initial length
                writer.Write((byte)property.PropertyTagFlags);
                if (property.ArrayIndex != 0) writer.Write(property.ArrayIndex);
                int realLength = property.Write(writer, includeHeader);
                int newLoc = (int)writer.BaseStream.Position;

                writer.Seek(oldLoc, SeekOrigin.Begin);
                writer.Write(realLength);
                writer.Seek(newLoc, SeekOrigin.Begin);
                return oldLoc;
            }
            else
            {
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
                writer.Write(property.ArrayIndex);
                int realLength = property.Write(writer, includeHeader);
                int newLoc = (int)writer.BaseStream.Position;

                writer.Seek(oldLoc, SeekOrigin.Begin);
                writer.Write(realLength);
                writer.Seek(newLoc, SeekOrigin.Begin);
                return oldLoc;
            }

        }
    }
}
