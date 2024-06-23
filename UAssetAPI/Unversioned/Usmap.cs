using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAssetAPI.ExportTypes;
using UAssetAPI.FieldTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.Unversioned
{
    public enum UsmapVersion : byte
    {
        /// <summary>
        /// Initial format.
        /// </summary>
        Initial,

        /// <summary>
        /// Adds optional asset package versioning
        /// </summary>
        PackageVersioning,

        /// <summary>
        /// 16-bit wide names in name map
        /// </summary>
        LongFName,

        /// <summary>
        /// 16-bit enum entry count
        /// </summary>
        LargeEnums,

        LatestPlusOne,
        Latest = LatestPlusOne - 1
    }

    public enum UsmapExtensionLayoutVersion : byte
    {
        /// <summary>
        /// Initial format.
        /// </summary>
        Initial
    }

    public enum UsmapStructKind : byte
    {
        None = 0,
        UScriptStruct = 1,
        UClass = 2,
    }

    public enum ECompressionMethod : byte
    {
        None,
        Oodle,
        Brotli,
        ZStandard,

        Unknown = 0xFF
    };

    public enum EPropertyType
    {
        ByteProperty,
        BoolProperty,
        IntProperty,
        FloatProperty,
        ObjectProperty,
        NameProperty,
        DelegateProperty,
        DoubleProperty,
        ArrayProperty,
        StructProperty,
        StrProperty,
        TextProperty,
        InterfaceProperty,
        MulticastDelegateProperty,
        WeakObjectProperty, //
        LazyObjectProperty, // When deserialized, these 3 properties will be SoftObjects
        AssetObjectProperty, //
        SoftObjectProperty,
        UInt64Property,
        UInt32Property,
        UInt16Property,
        Int64Property,
        Int16Property,
        Int8Property,
        MapProperty,
        SetProperty,
        EnumProperty,
        FieldPathProperty,

        Unknown = 0xFF
    };

    public class UsmapMapData : UsmapPropertyData
    {
        public UsmapPropertyData InnerType;
        public UsmapPropertyData ValueType;

        public UsmapMapData()
        {
            Type = EPropertyType.MapProperty;
        }
        public override string ToString()
        {
            return base.ToString() + " : (" + InnerType.ToString() + ") : (" + ValueType.ToString() + ")";
        }

    }

    public class UsmapArrayData : UsmapPropertyData
    {
        public UsmapPropertyData InnerType;

        public UsmapArrayData(EPropertyType type) // array or map
        {
            Type = type;
        }

        public override string ToString()
        {
            return base.ToString() + " : (" + InnerType.ToString() + ")";
        }
    }

    public class UsmapStructData : UsmapPropertyData
    {
        public string StructType;

        public UsmapStructData(string structType)
        {
            StructType = structType;
            Type = EPropertyType.StructProperty;
        }

        public UsmapStructData()
        {
            Type = EPropertyType.StructProperty;
        }

        public override string ToString()
        {
            return base.ToString() + " : " + StructType;
        }
    }

    public class UsmapEnumData : UsmapPropertyData
    {
        public UsmapPropertyData InnerType;
        public string Name;
        public List<string> Values;

        public UsmapEnumData(string name, List<string> values)
        {
            Name = name;
            Values = values;
            Type = EPropertyType.EnumProperty;
        }

        public UsmapEnumData()
        {
            Type = EPropertyType.EnumProperty;
        }

        public override string ToString()
        {
            return base.ToString() + " : " + Name + " : " + string.Join(", ", Values ?? new List<string>()) + " : (" + InnerType.ToString() + ")";
        }
    }

    public class UsmapPropertyData
    {
        public EPropertyType Type = EPropertyType.Unknown;

        public UsmapPropertyData(EPropertyType type)
        {
            Type = type;
        }

        public UsmapPropertyData()
        {

        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }

    public class UsmapProperty : ICloneable
    {
        public string Name;
        public ushort SchemaIndex;
        public ushort ArrayIndex; // not serialized
        public byte ArraySize;
        public EPropertyFlags PropertyFlags;
        public UsmapPropertyData PropertyData;

        public UsmapProperty(string name, ushort schemaIndex, ushort arrayIndex, byte arraySize, UsmapPropertyData propertyData)
        {
            Name = name;
            SchemaIndex = schemaIndex;
            ArrayIndex = arrayIndex;
            ArraySize = arraySize;
            PropertyData = propertyData;
        }

        public object Clone()
        {
            return new UsmapProperty(Name, SchemaIndex, ArrayIndex, ArraySize, PropertyData);
        }

        public override string ToString()
        {
            return Name + " : " + SchemaIndex + " : " + ArrayIndex + " : " + ArraySize + " : (" + PropertyData.ToString() + ")";
        }
    }

    public class UsmapSchema
    {
        public string Name;
        public string SuperType;
        public ushort PropCount;
        public string ModulePath;
        /// <summary>
        /// Whether or not this schema was retrieved from a .uasset file.
        /// </summary>
        public bool FromAsset = false;

        public IReadOnlyDictionary<int, UsmapProperty> Properties => properties;

        private Dictionary<int, UsmapProperty> properties;
        private Dictionary<Tuple<string, int>, UsmapProperty> propertiesMap;

        public UsmapStructKind StructKind;
        public int StructOrClassFlags;

        public UsmapProperty GetProperty(string key, int dupIndex)
        {
            var keyTuple = new Tuple<string, int>(key, dupIndex);
            return propertiesMap.ContainsKey(keyTuple) ? propertiesMap[keyTuple] : null;
        }

        public UsmapSchema(string name, string superType, ushort propCount, Dictionary<int, UsmapProperty> props, bool fromAsset = false)
        {
            Name = name;
            SuperType = superType;
            PropCount = propCount;
            properties = props;
            FromAsset = fromAsset;

            propertiesMap = new Dictionary<Tuple<string, int>, UsmapProperty>();
            foreach (KeyValuePair<int, UsmapProperty> entry in props)
            {
                propertiesMap[new Tuple<string, int>(entry.Value.Name, entry.Value.ArrayIndex)] = entry.Value;
            }
        }

        public UsmapSchema()
        {

        }
    }

    public class UsmapEnum
    {
        public string Name;
        public string ModulePath;
        public int EnumFlags;
        public Dictionary<long, string> Values;

        public UsmapEnum(string name, Dictionary<long, string> values)
        {
            Name = name;
            Values = values;
        }

        public UsmapEnum()
        {

        }
    }


    public class Usmap
    {
        /// <summary>
        /// The path of the file on disk. This does not need to be specified for regular parsing.
        /// </summary>
        public string FilePath;

        /// <summary>
        /// Magic number for the .usmap format
        /// </summary>
        public static readonly ushort USMAP_MAGIC = 0x30C4;

        /// <summary>
        /// .usmap file version
        /// </summary>
        internal UsmapVersion Version;

        /// <summary>
        /// Game UE4 object version
        /// </summary>
        public ObjectVersion FileVersionUE4;

        /// <summary>
        /// Game UE5 object version
        /// </summary>
        public ObjectVersionUE5 FileVersionUE5;

        /// <summary>
        /// All the custom versions stored in the archive.
        /// </summary>
        public List<CustomVersion> CustomVersionContainer = null;

        public uint NetCL;

        private bool _AreFNamesCaseInsensitive = true;
        /// <summary>
        /// Whether or not FNames are case insensitive. Modifying this property is an expensive operation, and will re-construct several dictionaries.
        /// </summary>
        public bool AreFNamesCaseInsensitive
        {
            get
            {
                return _AreFNamesCaseInsensitive;
            }
            set
            {
                if (value == _AreFNamesCaseInsensitive) return;
                _AreFNamesCaseInsensitive = value;

                var EnumMapNuevo = new Dictionary<string, UsmapEnum>(value ? StringComparer.InvariantCultureIgnoreCase : StringComparer.InvariantCulture);
                foreach (var entry in EnumMap) EnumMapNuevo.Add(entry.Key, entry.Value);
                EnumMap = EnumMapNuevo;

                var SchemasNuevo = new Dictionary<string, UsmapSchema>(value ? StringComparer.InvariantCultureIgnoreCase : StringComparer.InvariantCulture);
                foreach (var entry in Schemas) SchemasNuevo.Add(entry.Key, entry.Value);
                Schemas = SchemasNuevo;
            }
        }

        /// <summary>
        /// Whether or not to skip blueprint schemas serialized in this mappings file. Only useful for testing.
        /// </summary>
        public bool SkipBlueprintSchemas = false;

        /// <summary>
        /// .usmap name map
        /// </summary>
        public List<string> NameMap;

        /// <summary>
        /// .usmap enum map
        /// </summary>
        public Dictionary<string, UsmapEnum> EnumMap;

        /// <summary>
        /// .usmap schema map
        /// </summary>
        public Dictionary<string, UsmapSchema> Schemas;

        /// <summary>
        /// Pre-computed CityHash64 map for all relevant strings
        /// </summary>
        public Dictionary<ulong, string> CityHash64Map;

        /// <summary>
        /// List of extensions that failed to parse.
        /// </summary>
        public List<string> FailedExtensions;

        private void AddCityHash64MapEntry(string val)
        {
            ulong hsh = CRCGenerator.GenerateImportHashFromObjectPath(val);
            if (CityHash64Map.ContainsKey(hsh))
            {
                if (CRCGenerator.ToLower(CityHash64Map[hsh]) == CRCGenerator.ToLower(val)) return;
                throw new FormatException("CityHash64 hash collision between \"" + CityHash64Map[hsh] + "\" and \"" + val + "\"");
            }
            CityHash64Map.Add(hsh, val);
        }

        private static UsmapPropertyData ConvertFPropertyToUsmapPropertyData(StructExport exp, FProperty entry)
        {
            var typ = entry.GetUsmapPropertyType();
            UsmapPropertyData converted1;
            switch (typ)
            {
                case EPropertyType.EnumProperty:
                    FPackageIndex enumIndex = (entry as FEnumProperty).Enum;
                    var underlyingProp = (entry as FEnumProperty).UnderlyingProp;
                    if (enumIndex.IsExport())
                    {
                        var exp2 = enumIndex.ToExport(exp.Asset) as EnumExport;
                        var allNames = new List<string>();
                        foreach (var cosa in exp2.Enum.Names) allNames.Add(cosa.Item1.ToString());
                        converted1 = new UsmapEnumData(exp2.ObjectName.ToString(), allNames);
                        ((UsmapEnumData)converted1).InnerType = ConvertFPropertyToUsmapPropertyData(exp, underlyingProp);
                    }
                    else if (enumIndex.IsImport())
                    {
                        string enumName = enumIndex.ToImport(exp.Asset).ObjectName?.Value.Value;
                        if (enumName == null || !exp.Asset.Mappings.EnumMap.ContainsKey(enumName)) throw new InvalidOperationException("Attempt to index into non-existent enum " + enumName);
                        var allNames = new List<string>();
                        foreach (var cosa in exp.Asset.Mappings.EnumMap[enumName].Values) allNames.Add(cosa.ToString());
                        converted1 = new UsmapEnumData(enumName, allNames);
                        ((UsmapEnumData)converted1).InnerType = ConvertFPropertyToUsmapPropertyData(exp, underlyingProp);
                    }
                    else
                    {
                        converted1 = null;
                    }
                    break;
                case EPropertyType.StructProperty:
                    var strucstr = Export.GetClassTypeForAncestry((entry as FStructProperty).Struct, exp.Asset);
                    converted1 = new UsmapStructData(strucstr.ToString());
                    break;
                case EPropertyType.SetProperty:
                    converted1 = new UsmapArrayData(typ);
                    (converted1 as UsmapArrayData).InnerType = ConvertFPropertyToUsmapPropertyData(exp, (entry as FSetProperty).ElementProp);
                    break;
                case EPropertyType.ArrayProperty:
                    converted1 = new UsmapArrayData(typ);
                    (converted1 as UsmapArrayData).InnerType = ConvertFPropertyToUsmapPropertyData(exp, (entry as FArrayProperty).Inner);
                    break;
                case EPropertyType.MapProperty:
                    converted1 = new UsmapMapData();
                    (converted1 as UsmapMapData).InnerType = ConvertFPropertyToUsmapPropertyData(exp, (entry as FMapProperty).KeyProp);
                    (converted1 as UsmapMapData).ValueType = ConvertFPropertyToUsmapPropertyData(exp, (entry as FMapProperty).ValueProp);
                    break;
                default:
                    converted1 = new UsmapPropertyData(typ);
                    break;
            }
            return converted1;
        }

        public static UsmapSchema GetSchemaFromStructExport(string exportName, UnrealPackage asset)
        {
            if (asset == null) throw new InvalidOperationException("Cannot evaluate struct export without package reference");
            foreach (var exp in asset.Exports)
            {
                if (exp.ObjectName.Value.Value == exportName && exp is StructExport sexp) return GetSchemaFromStructExport(sexp);
            }
            return null;
        }

        public static UsmapSchema GetSchemaFromStructExport(StructExport exp)
        {
            var res = new Dictionary<int, UsmapProperty>();
            int idx = 0;
            foreach (FProperty entry in exp.LoadedProperties)
            {
                UsmapProperty converted = new UsmapProperty(entry.Name.ToString(), (ushort)idx, 0, 1, ConvertFPropertyToUsmapPropertyData(exp, entry));
                res.Add(idx, converted);
                idx++;
            }
            return new UsmapSchema(exp.ObjectName.ToString(), exp.SuperStruct.IsImport() ? exp.SuperStruct.ToImport(exp.Asset).ObjectName.ToString() : null, (ushort)res.Count, res, true);
        }

        /// <summary>
        /// Retrieve all the properties that a particular schema can reference.
        /// </summary>
        /// <param name="schemaName">The name of the schema of interest.</param>
        /// <param name="asset">An asset to also search for schemas within.</param>
        /// <returns>All the properties that the schema can reference.</returns>
        public IList<UsmapProperty> GetAllProperties(string schemaName, UnrealPackage asset = null)
        {
            List<UsmapProperty> res = new List<UsmapProperty>();
            UsmapSchema relevantSchema = this.GetSchemaFromName(schemaName, asset);
            while (schemaName != null && relevantSchema != null)
            {
                res.AddRange(relevantSchema.Properties.Values);
                schemaName = relevantSchema.SuperType;
                relevantSchema = this.GetSchemaFromName(schemaName, asset);
            }
            return res;
        }

        /// <summary>
        /// Retrieve all the properties that a particular schema can reference as an annotated, human-readable text file.
        /// </summary>
        /// <param name="schemaName">The name of the schema of interest.</param>
        /// <param name="asset">An asset to also search for schemas within.</param>
        /// <param name="customAnnotations">A map of strings to give custom annotations.</param>
        /// <param name="recursive">Whether or not to dump data for parent schemas as well.</param>
        /// <param name="headerPrefix">The prefix of the subheader for each relevant schema.</param>
        /// <param name="headerSuffix">The suffix of the subheader for each relevant schema.</param>
        /// <returns>An annotated, human-readable text file containing the properties that the schema can reference.</returns>
        public string GetAllPropertiesAnnotated(string schemaName, UnrealPackage asset, IDictionary<string, string> customAnnotations = null, bool recursive = true, string headerPrefix = "--- ", string headerSuffix = " ---")
        {
            List<string> res = new List<string>();
            bool hasDoneFirst = false;
            UsmapSchema relevantSchema = this.GetSchemaFromName(schemaName, asset);
            while (schemaName != null && relevantSchema != null)
            {
                string schemaAnnotation = relevantSchema.FromAsset ? " (blueprint)" : string.Empty;
                res.Add(headerPrefix + schemaName + schemaAnnotation + headerSuffix);

                if (recursive || !hasDoneFirst)
                {
                    foreach (UsmapProperty prop in relevantSchema.Properties.Values)
                    {
                        if (prop.ArrayIndex > 0) continue;
                        string propAnnotation = customAnnotations != null && customAnnotations.ContainsKey(prop.Name) ? (" (" + customAnnotations[prop.Name] + ")") : string.Empty;
                        res.Add(prop.Name + propAnnotation);
                        res.Add("\t" + (prop.PropertyData?.Type.ToString() ?? "Unknown type"));
                    }
                    if (relevantSchema.Properties.Values.Count() == 0) res.Add("N/A");
                    res.Add(string.Empty);
                }

                schemaName = relevantSchema.SuperType;
                relevantSchema = this.GetSchemaFromName(schemaName, asset);
                hasDoneFirst = true;
            }
            return string.Join("\n", res.ToArray());
        }

        public ISet<string> PathsAlreadyProcessedForSchemas = new HashSet<string>();
        public UsmapSchema GetSchemaFromName(string nm, UnrealPackage asset = null, bool throwExceptions = true)
        {
            if (string.IsNullOrEmpty(nm)) return null;

            UsmapSchema relevantSchema = null;
            if (this.Schemas.ContainsKey(nm))
            {
                relevantSchema = this.Schemas[nm];
            }
            else if (asset != null)
            {
                // note: this is probably not needed anymore since we now collate schemas on asset load
                relevantSchema = Usmap.GetSchemaFromStructExport(nm, asset);
            }
            if (throwExceptions && relevantSchema == null) throw new FormatException("Failed to find a valid schema for parent name " + nm);
            return relevantSchema;
        }

        /// <summary>
        /// Attempts to retrieve the corresponding .usmap property, given its ancestry.
        /// </summary>
        /// <typeparam name="T">The type of property to output.</typeparam>
        /// <param name="propertyName">The name of the property to search for.</param>
        /// <param name="ancestry">The ancestry of the property to search for.</param>
        /// <param name="dupIndex">The duplication index of the property to search for. If unknown, set to 0.</param>
        /// <param name="asset">An asset to also search for schemas within.</param>
        /// <param name="propDat">The property.</param>
        /// <param name="idx">The index of the property.</param>
        /// <returns>Whether or not the property was successfully found.</returns>
        public bool TryGetProperty<T>(FName propertyName, AncestryInfo ancestry, int dupIndex, UnrealPackage asset, out T propDat, out int idx) where T : UsmapProperty
        {
            propDat = null;

            idx = 0;
            var schemaName = ancestry.Parent.Value.Value;
            UsmapSchema relevantSchema = this.GetSchemaFromName(schemaName, asset);
            while (schemaName != null && relevantSchema != null)
            {
                propDat = relevantSchema.GetProperty(propertyName.Value.Value, dupIndex) as T;
                if (propDat != null)
                {
                    idx += propDat.SchemaIndex;
                    return true;
                }

                idx += relevantSchema.PropCount;
                schemaName = relevantSchema.SuperType;
                relevantSchema = this.GetSchemaFromName(schemaName, asset);
            }

            return false;
        }

        /// <summary>
        /// Attempts to retrieve the corresponding .usmap property data corresponding to a specific property, given its ancestry.
        /// </summary>
        /// <typeparam name="T">The type of property data to output.</typeparam>
        /// <param name="propertyName">The name of the property to search for.</param>
        /// <param name="ancestry">The ancestry of the property to search for.</param>
        /// <param name="asset">An asset to also search for schemas within.</param>
        /// <param name="propDat">The property data.</param>
        /// <returns>Whether or not the property data was successfully found.</returns>
        public bool TryGetPropertyData<T>(FName propertyName, AncestryInfo ancestry, UnrealPackage asset, out T propDat) where T : UsmapPropertyData
        {
            propDat = null;

            if (propertyName.IsDummy && int.TryParse(propertyName.Value.Value, out _))
            {
                // this is actually an array member; try to find its parent array
                if (this.TryGetPropertyData(ancestry.Parent, ancestry.CloneWithoutParent(), asset, out UsmapArrayData arrDat))
                {
                    propDat = arrDat.InnerType as T;
                    if (propDat != null) return true;
                }
            }

            var schemaName = ancestry.Parent.Value.Value;
            UsmapSchema relevantSchema = this.GetSchemaFromName(schemaName, asset);
            while (schemaName != null && relevantSchema != null)
            {
                propDat = relevantSchema.GetProperty(propertyName.Value.Value, 0)?.PropertyData as T;
                if (propDat != null) return true;
                schemaName = relevantSchema.SuperType;
                relevantSchema = this.GetSchemaFromName(schemaName, asset);
            }

            return false;
        }

        /// <summary>
        /// Creates a MemoryStream from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new MemoryStream that stores the binary data of the input file.</returns>
        public static MemoryStream PathToStream(string p)
        {
            using (FileStream origStream = File.Open(p, FileMode.Open))
            {
                MemoryStream completeStream = new MemoryStream();
                origStream.CopyTo(completeStream);

                completeStream.Seek(0, SeekOrigin.Begin);
                return completeStream;
            }
        }

        /// <summary>
        /// Creates a BinaryReader from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new BinaryReader that stores the binary data of the input file.</returns>
        public UsmapBinaryReader PathToReader(string p)
        {
            return new UsmapBinaryReader(PathToStream(p), this);
        }

        public UsmapBinaryReader ReadHeader(UsmapBinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            ushort fileSignature = reader.ReadUInt16();
            if (fileSignature != USMAP_MAGIC) throw new FormatException(".usmap: File signature mismatch");

            Version = (UsmapVersion)reader.ReadByte();
            if (Version < UsmapVersion.Initial || Version > UsmapVersion.Latest) throw new FormatException(".usmap: Unknown file version " + Version);

            // package versioning
            if (Version >= UsmapVersion.PackageVersioning)
            {
                bool bHasVersioning = reader.ReadInt32() > 0;
                if (bHasVersioning)
                {
                    FileVersionUE4 = (ObjectVersion)reader.ReadInt32();
                    FileVersionUE5 = (ObjectVersionUE5)reader.ReadInt32();

                    CustomVersionContainer = new List<CustomVersion>();
                    int numCustomVersions = reader.ReadInt32();
                    for (int i = 0; i < numCustomVersions; i++)
                    {
                        var customVersionID = new Guid(reader.ReadBytes(16));
                        var customVersionNumber = reader.ReadInt32();
                        CustomVersionContainer.Add(new CustomVersion(customVersionID, customVersionNumber));
                    }

                    NetCL = reader.ReadUInt32();
                }
            }

            ECompressionMethod compressionMethod = (ECompressionMethod)reader.ReadByte();

            uint compressedSize = reader.ReadUInt32();
            uint decompressedSize = reader.ReadUInt32();

            switch (compressionMethod)
            {
                case ECompressionMethod.None:
                    if (compressedSize != decompressedSize) throw new FormatException(".usmap: Compressed size must be equal to decompressed size");
                    return reader;
                case ECompressionMethod.ZStandard:
                    {
                        byte[] dat = new ZstdSharp.Decompressor().Unwrap(reader.ReadBytes((int)compressedSize), (int)decompressedSize).ToArray();
                        return new UsmapBinaryReader(new MemoryStream(dat), this);
                    }
                case ECompressionMethod.Oodle:
                    {
                        byte[] dat = Oodle.Decompress(reader.ReadBytes((int)compressedSize), (int)compressedSize, (int)decompressedSize);
                        return new UsmapBinaryReader(new MemoryStream(dat), this);
                    }
                default:
                    throw new NotImplementedException(".usmap: Compression method " + compressionMethod + " is unimplemented");
            }
        }

        private UsmapPropertyData InitPropData(EPropertyType typ)
        {
            switch (typ)
            {
                case EPropertyType.EnumProperty:
                    return new UsmapEnumData();
                case EPropertyType.StructProperty:
                    return new UsmapStructData();
                case EPropertyType.SetProperty:
                case EPropertyType.ArrayProperty:
                    return new UsmapArrayData(typ);
                case EPropertyType.MapProperty:
                    return new UsmapMapData();
            }

            return new UsmapPropertyData(typ);
        }

        private UsmapPropertyData DeserializePropData(UsmapBinaryReader reader)
        {
            var res = InitPropData((EPropertyType)reader.ReadByte());
            switch (res.Type)
            {
                case EPropertyType.EnumProperty:
                    ((UsmapEnumData)res).InnerType = DeserializePropData(reader);
                    ((UsmapEnumData)res).Name = reader.ReadName();
                    break;
                case EPropertyType.StructProperty:
                    ((UsmapStructData)res).StructType = reader.ReadName();
                    break;
                case EPropertyType.SetProperty:
                case EPropertyType.ArrayProperty:
                    ((UsmapArrayData)res).InnerType = DeserializePropData(reader);
                    break;
                case EPropertyType.MapProperty:
                    ((UsmapMapData)res).InnerType = DeserializePropData(reader);
                    ((UsmapMapData)res).ValueType = DeserializePropData(reader);
                    break;
                default:
                    break;
            }
            return res;
        }

        public void Read(UsmapBinaryReader compressedReader)
        {
            var reader = ReadHeader(compressedReader);
            CityHash64Map = new Dictionary<ulong, string>();

            // part 1: names
            //Console.WriteLine(reader.BaseStream.Position);
            NameMap = new List<string>();
            int numNames = reader.ReadInt32();
            for (int i = 0; i < numNames; i++)
            {
                int fixedLength = Version >= UsmapVersion.LongFName ? (int)reader.ReadInt16() : (int)reader.ReadByte();
                var str = reader.ReadString(fixedLength);
                NameMap.Add(str);
            }

            // part 2: enums
            //Console.WriteLine(reader.BaseStream.Position);
            EnumMap = new Dictionary<string, UsmapEnum>(AreFNamesCaseInsensitive ? StringComparer.InvariantCultureIgnoreCase : StringComparer.InvariantCulture);
            int numEnums = reader.ReadInt32();
            UsmapEnum[] enumIndexMap = new UsmapEnum[numEnums];
            for (int i = 0; i < numEnums; i++)
            {
                string enumName = reader.ReadName();

                var newEnum = new UsmapEnum(enumName, new Dictionary<long, string>());
                int numEnumEntries = Version >= UsmapVersion.LargeEnums ? (int)reader.ReadInt16() : (int)reader.ReadByte();
                for (int j = 0; j < numEnumEntries; j++)
                {
                    newEnum.Values.Add(j, reader.ReadName());
                }

                if (!EnumMap.ContainsKey(enumName))
                {
                    enumIndexMap[i] = newEnum;
                    EnumMap[enumName] = newEnum;
                }
            }

            // part 3: schema
            //Console.WriteLine(reader.BaseStream.Position);
            Schemas = new Dictionary<string, UsmapSchema>(AreFNamesCaseInsensitive ? StringComparer.InvariantCultureIgnoreCase : StringComparer.InvariantCulture);
            int numSchema = reader.ReadInt32();
            UsmapSchema[] schemaIndexMap = new UsmapSchema[numSchema];
            for (int i = 0; i < numSchema; i++)
            {
                string schemaName = reader.ReadName();
                string schemaSuperName = reader.ReadName();
                ushort numProps = reader.ReadUInt16();
                ushort serializablePropCount = reader.ReadUInt16();
                Dictionary<int, UsmapProperty> props = new Dictionary<int, UsmapProperty>();
                for (int j = 0; j < serializablePropCount; j++)
                {
                    ushort SchemaIdx = reader.ReadUInt16();
                    byte ArraySize = reader.ReadByte();
                    string Name = reader.ReadName();

                    var currProp = new UsmapProperty(Name, SchemaIdx, 0, ArraySize, null);
                    currProp.PropertyData = DeserializePropData(reader);
                    for (int k = 0; k < ArraySize; k++)
                    {
                        var cln = (UsmapProperty)currProp.Clone();
                        cln.SchemaIndex = (ushort)(SchemaIdx + k);
                        cln.ArrayIndex = (ushort)k;
                        props.Add(SchemaIdx + k, cln);
                    }
                }

                if (SkipBlueprintSchemas && schemaName.Length >= 2 && schemaName.EndsWith("_C"))
                {
                    continue;
                }

                var newSchema = new UsmapSchema(schemaName, schemaSuperName, numProps, props);
                schemaIndexMap[i] = newSchema;
                Schemas[schemaName] = newSchema;
            }

            void ReadExtension(string extId, uint extLeng)
            {
                long endPos = reader.BaseStream.Position + extLeng;

                switch(extId)
                {
                    case "PPTH": // Replaces MODL, reuses name map and added full names for Enums
                        byte ppthVer = reader.ReadByte();
                        if (ppthVer > 0) break;

                        int ppthNumEnums = reader.ReadInt32();
                        for (int i = 0; i < ppthNumEnums; i++)
                        {
                            enumIndexMap[i].ModulePath = reader.ReadName();
                            AddCityHash64MapEntry(enumIndexMap[i].ModulePath + "." + enumIndexMap[i].Name);
                        }
                        int ppthNumSchemas = reader.ReadInt32();
                        for (int i = 0; i < ppthNumSchemas; i++)
                        {
                            schemaIndexMap[i].ModulePath = reader.ReadName();
                            AddCityHash64MapEntry(schemaIndexMap[i].ModulePath + "." + schemaIndexMap[i].Name);
                        }

                        if (reader.BaseStream.Position != endPos) throw new FormatException("Failed to parse extension " + extId + ": ended at " + reader.BaseStream.Position + ", expected " + endPos);
                        break;
                    case "EATR": // Extended Attributes
                        byte eatrVer = reader.ReadByte();
                        if (eatrVer > 0) break;

                        int eatrNumEnums = reader.ReadInt32();
                        for (int i = 0; i < eatrNumEnums; i++)
                        {
                            enumIndexMap[i].EnumFlags = reader.ReadInt32();
                        }
                        int eatrNumSchemas = reader.ReadInt32();
                        for (int i = 0; i < eatrNumSchemas; i++)
                        {
                            schemaIndexMap[i].StructKind = (UsmapStructKind)reader.ReadByte();
                            schemaIndexMap[i].StructOrClassFlags = reader.ReadInt32();
                            int eatrNumProps = reader.ReadInt32();
                            for (int j = 0; j < eatrNumProps; j++)
                            {
                                var flgs = (EPropertyFlags)reader.ReadUInt64();
                                if (j < schemaIndexMap[i].Properties.Count) schemaIndexMap[i].Properties[j].PropertyFlags = flgs;
                            }
                        }

                        if (reader.BaseStream.Position != endPos) throw new FormatException("Failed to parse extension " + extId + ": ended at " + reader.BaseStream.Position + ", expected " + endPos);
                        break;
                    case "ENVP": // Enum Name Value Pairs
                        byte envpVer = reader.ReadByte();
                        if (envpVer > 0) break;

                        int envpNumEnums = reader.ReadInt32();
                        for (int i = 0; i < envpNumEnums; i++)
                        {
                            enumIndexMap[i].Values.Clear();
                            int envpNumEnumEntries = reader.ReadInt32(); // not a byte this time!!!
                            for (int j = 0; j < envpNumEnumEntries; j++)
                            {
                                string envpEntryVal = reader.ReadName();
                                long envpEntryKey = reader.ReadInt64();
                                enumIndexMap[i].Values[envpEntryKey] = envpEntryVal;
                            }
                        }

                        if (reader.BaseStream.Position != endPos) throw new FormatException("Failed to parse extension " + extId + ": ended at " + reader.BaseStream.Position + ", expected " + endPos);
                        break;
                    case "MODL": // traditional list of module paths
                        ushort numModulePaths = reader.ReadUInt16();
                        string[] modulePaths = new string[numModulePaths];
                        for (int i = 0; i < numModulePaths; i++) modulePaths[i] = reader.ReadString();
                        for (int i = 0; i < schemaIndexMap.Length; i++)
                        {
                            schemaIndexMap[i].ModulePath = modulePaths[numModulePaths > byte.MaxValue ? reader.ReadUInt16() : reader.ReadByte()];
                            AddCityHash64MapEntry(schemaIndexMap[i].ModulePath + "." + schemaIndexMap[i].Name);
                        }

                        if (reader.BaseStream.Position != endPos) throw new FormatException("Failed to parse extension " + extId + ": ended at " + reader.BaseStream.Position + ", expected " + endPos);
                        break;
                    default:
                        break;
                }

                reader.BaseStream.Position = endPos;
            }

            // read extension data if it's present
            FailedExtensions = new List<string>();
            if (reader.BaseStream.Length > reader.BaseStream.Position)
            {
                uint usmapExtensionsMagic = reader.ReadUInt32();
                if (usmapExtensionsMagic == 0x54584543) // "CEXT"
                {
                    UsmapExtensionLayoutVersion layoutVer = (UsmapExtensionLayoutVersion)reader.ReadByte();
                    switch(layoutVer)
                    {
                        case UsmapExtensionLayoutVersion.Initial:
                            int numExtensions = reader.ReadInt32();
                            for (int i = 0; i < numExtensions; i++)
                            {
                                string extId = reader.ReadString(4);
                                uint extLeng = reader.ReadUInt32();
                                try
                                {
                                    ReadExtension(extId, extLeng);
                                }
                                catch
                                {
                                    FailedExtensions.Add(extId);
                                }
                            }
                            break;
                        default:
                            throw new InvalidOperationException("Unknown extension layout version " + layoutVer);
                    }
                }
                else if (usmapExtensionsMagic == 1) // legacy
                {
                    ReadExtension("MODL", (uint)(reader.BaseStream.Length - reader.BaseStream.Position));
                }
            }
        }

        /// <summary>
        /// Reads a .usmap file from disk and initializes a new instance of the <see cref="Usmap"/> class to store its data in memory.
        /// </summary>
        /// <param name="path">The path of the file file on disk that this instance will read from.</param>
        /// <exception cref="FormatException">Throw when the file cannot be parsed correctly.</exception>
        public Usmap(string path)
        {
            this.FilePath = path;
            Read(PathToReader(path));
        }

        /// <summary>
        /// Reads a .usmap file from a UsmapBinaryReader and initializes a new instance of the <see cref="Usmap"/> class to store its data in memory.
        /// </summary>
        /// <param name="reader">The file's UsmapBinaryReader that this instance will read from.</param>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public Usmap(UsmapBinaryReader reader)
        {
            Read(reader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Usmap"/> class. This instance will store no data and does not represent any file in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        public Usmap()
        {

        }
    }
}
