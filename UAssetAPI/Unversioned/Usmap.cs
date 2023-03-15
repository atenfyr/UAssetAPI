using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.Unversioned
{
    public enum UsmapVersion
    {
        /// <summary>
        /// Initial format.
        /// </summary>
        Initial,

        /// <summary>
        /// Adds package versioning to aid with compatibility
        /// </summary>
        PackageVersioning,

        LatestPlusOne,
        Latest = LatestPlusOne - 1
     };

    public enum ECompressionMethod
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
        public IReadOnlyDictionary<int, UsmapProperty> Properties => properties;

        private Dictionary<int, UsmapProperty> properties;
        private Dictionary<Tuple<string, int>, UsmapProperty> propertiesMap;

        public UsmapProperty GetProperty(string key, int dupIndex)
        {
            var keyTuple = new Tuple<string, int>(key, dupIndex);
            return propertiesMap.ContainsKey(keyTuple) ? propertiesMap[keyTuple] : null;
        }

        public UsmapSchema(string name, string superType, ushort propCount, Dictionary<int, UsmapProperty> props)
        {
            Name = name;
            SuperType = superType;
            PropCount = propCount;
            properties = props;

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

        /// <summary>
        /// .usmap name map
        /// </summary>
        public List<string> NameMap;

        /// <summary>
        /// .usmap enum map
        /// </summary>
        public Dictionary<string, List<string>> EnumMap;

        /// <summary>
        /// .usmap schema map
        /// </summary>
        public Dictionary<string, UsmapSchema> Schemas;

        /// <summary>
        /// Pre-computed CityHash64 map for all relevant strings
        /// </summary>
        public Dictionary<ulong, string> CityHash64Map;

        private void AddCityHash64MapEntry(string val)
        {
            // a bit of a brute force method
            AddCityHash64MapEntryRaw(val);
            AddCityHash64MapEntryRaw("/Script/" + val);
            AddCityHash64MapEntryRaw("/Script/Engine/" + val);
        }

        private void AddCityHash64MapEntryRaw(string val)
        {
            ulong hsh = CRCGenerator.GenerateImportHashFromObjectPath(val);
            if (CityHash64Map.ContainsKey(hsh))
            {
                if (CRCGenerator.ToLower(CityHash64Map[hsh]) == CRCGenerator.ToLower(val)) return;
                throw new FormatException("CityHash64 hash collision between \"" + CityHash64Map[hsh] + "\" and \"" + val + "\"");
            }
            CityHash64Map.Add(hsh, val);
        }

        /// <summary>
        /// Retrieve all the properties that a particular schema can reference.
        /// </summary>
        /// <param name="schemaName">The name of the schema of interest.</param>
        /// <returns>All the properties that the schema can reference.</returns>
        public IList<UsmapProperty> GetAllProperties(string schemaName)
        {
            List<UsmapProperty> res = new List<UsmapProperty>();
            while (schemaName != null && this.Schemas.ContainsKey(schemaName))
            {
                var relevantSchema = this.Schemas[schemaName];
                res.AddRange(relevantSchema.Properties.Values);
                schemaName = relevantSchema.SuperType;
            }
            return res;
        }

        /// <summary>
        /// Retrieve all the properties that a particular schema can reference as an annotated, human-readable text file.
        /// </summary>
        /// <param name="schemaName">The name of the schema of interest.</param>
        /// <param name="customAnnotations">A map of strings to give custom annotations.</param>
        /// <param name="recursive">Whether or not to dump data for parent schemas as well.</param>
        /// <param name="headerPrefix">The prefix of the subheader for each relevant schema.</param>
        /// <param name="headerSuffix">The suffix of the subheader for each relevant schema.</param>
        /// <returns>An annotated, human-readable text file containing the properties that the schema can reference.</returns>
        public string GetAllPropertiesAnnotated(string schemaName, IDictionary<string, string> customAnnotations = null, bool recursive = true, string headerPrefix = "--- ", string headerSuffix = " ---")
        {
            List<string> res = new List<string>();
            bool hasDoneFirst = false;
            while (schemaName != null && this.Schemas.ContainsKey(schemaName))
            {
                res.Add(headerPrefix + schemaName + headerSuffix);

                var relevantSchema = this.Schemas[schemaName];
                if (recursive || !hasDoneFirst)
                {
                    foreach (UsmapProperty prop in relevantSchema.Properties.Values)
                    {
                        if (prop.ArrayIndex > 0) continue;
                        res.Add(prop.Name + (customAnnotations != null && customAnnotations.ContainsKey(prop.Name) ? (" (" + customAnnotations[prop.Name] + ")") : string.Empty));
                    }
                    if (relevantSchema.Properties.Values.Count() == 0) res.Add("N/A");
                    res.Add(string.Empty);
                }

                schemaName = relevantSchema.SuperType;
                hasDoneFirst = true;
            }
            return string.Join("\n", res.ToArray());
        }

        /// <summary>
        /// Attempts to retrieve the corresponding .usmap property, given its ancestry.
        /// </summary>
        /// <typeparam name="T">The type of property to output.</typeparam>
        /// <param name="propertyName">The name of the property to search for.</param>
        /// <param name="ancestry">The ancestry of the property to search for.</param>
        /// <param name="dupIndex">The duplication index of the property to search for. If unknown, set to 0.</param>
        /// <param name="propDat">The property.</param>
        /// <param name="idx">The index of the property.</param>
        /// <returns>Whether or not the property was successfully found.</returns>
        public bool TryGetProperty<T>(FName propertyName, AncestryInfo ancestry, int dupIndex, out T propDat, out int idx) where T : UsmapProperty
        {
            propDat = null;

            idx = 0;
            var schemaName = ancestry.Parent.Value.Value;
            while (schemaName != null && this.Schemas.ContainsKey(schemaName))
            {
                var relevantSchema = this.Schemas[schemaName];
                propDat = relevantSchema.GetProperty(propertyName.Value.Value, dupIndex) as T;
                if (propDat != null)
                {
                    idx += propDat.SchemaIndex;
                    return true;
                }

                idx += relevantSchema.PropCount;
                schemaName = relevantSchema.SuperType;
            }

            return false;
        }

        /// <summary>
        /// Attempts to retrieve the corresponding .usmap property data corresponding to a specific property, given its ancestry.
        /// </summary>
        /// <typeparam name="T">The type of property data to output.</typeparam>
        /// <param name="propertyName">The name of the property to search for.</param>
        /// <param name="ancestry">The ancestry of the property to search for.</param>
        /// <param name="propDat">The property data.</param>
        /// <returns>Whether or not the property data was successfully found.</returns>
        public bool TryGetPropertyData<T>(FName propertyName, AncestryInfo ancestry, out T propDat) where T : UsmapPropertyData
        {
            propDat = null;

            var schemaName = ancestry.Parent.Value.Value;
            while (schemaName != null && this.Schemas.ContainsKey(schemaName))
            {
                var relevantSchema = this.Schemas[schemaName];
                propDat = relevantSchema.GetProperty(propertyName.Value.Value, 0)?.PropertyData as T;
                if (propDat != null) return true;
                schemaName = relevantSchema.SuperType;
            }

            if (propertyName.IsDummy && int.TryParse(propertyName.Value.Value, out _))
            {
                // this is actually an array member; try to find its parent array
                if (this.TryGetPropertyData(ancestry.Parent, ancestry.CloneWithoutParent(), out UsmapArrayData arrDat))
                {
                    propDat = arrDat.InnerType as T;
                    if (propDat != null) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Creates a MemoryStream from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new MemoryStream that stores the binary data of the input file.</returns>
        public MemoryStream PathToStream(string p)
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
                bool bHasVersioning = reader.ReadBoolean();
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
                case ECompressionMethod.Oodle:
                    var dat = Oodle.Decompress(reader.ReadBytes((int)compressedSize), (int)compressedSize, (int)decompressedSize);
                    return new UsmapBinaryReader(new MemoryStream(dat), this);
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
                var str = reader.ReadString();
                NameMap.Add(str);
            }

            // part 2: enums
            //Console.WriteLine(reader.BaseStream.Position);
            EnumMap = new Dictionary<string, List<string>>();
            int numEnums = reader.ReadInt32();
            for (int i = 0; i < numEnums; i++)
            {
                string enumName = reader.ReadName();
                EnumMap[enumName] = new List<string>();

                byte numEnumEntries = reader.ReadByte();
                for (int j = 0; j < numEnumEntries; j++)
                {
                    EnumMap[enumName].Add(reader.ReadName());
                }
            }

            // part 3: schema
            //Console.WriteLine(reader.BaseStream.Position);
            Schemas = new Dictionary<string, UsmapSchema>();
            int numSchema = reader.ReadInt32();
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

                Schemas[schemaName] = new UsmapSchema(schemaName, schemaSuperName, numProps, props);
                AddCityHash64MapEntry(schemaName);
            }

            //Console.WriteLine(reader.BaseStream.Position);
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
        /// Reads a .usmap file from a BinaryReader and initializes a new instance of the <see cref="Usmap"/> class to store its data in memory.
        /// </summary>
        /// <param name="reader">The file's BinaryReader that this instance will read from.</param>
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
