using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.Unversioned
{
    public enum UsmapVersion
    {
        INITIAL,

		LATEST_PLUS_ONE,
		LATEST = LATEST_PLUS_ONE - 1
	};

    public enum ECompressionMethod
    {
        None,
		Oodle,
		Brotli,

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

    public class UsmapProperty
    {
        public string Name;
        public ushort SchemaIndex;
        public byte ArraySize;
        public UsmapPropertyData PropertyData;

        public UsmapProperty(string name, ushort schemaIndex, byte arraySize, UsmapPropertyData propertyData)
        {
            Name = name;
            SchemaIndex = schemaIndex;
            ArraySize = arraySize;
            PropertyData = propertyData;
        }

        public override string ToString()
        {
            return Name + " : " + SchemaIndex + " : " + ArraySize + " : (" + PropertyData.ToString() + ")";
        }
    }

    public class UsmapSchema
    {
        public string Name;
        public string SuperType;
        public ushort PropCount;
        public List<UsmapProperty> Properties;

        public UsmapSchema(string name, string superType, ushort propCount, List<UsmapProperty> properties)
        {
            Name = name;
            SuperType = superType;
            PropCount = propCount;
            Properties = properties;
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
                    // we only support uncompressed .usmap files at the moment
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

            // part 1: names
            //Console.WriteLine(reader.BaseStream.Position);
            NameMap = new List<string>();
            int numNames = reader.ReadInt32();
            for (int i = 0; i < numNames; i++)
            {
                NameMap.Add(reader.ReadString());
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
                List<UsmapProperty> props = new List<UsmapProperty>();
                for (int j = 0; j < serializablePropCount; j++)
                {
                    ushort SchemaIdx = reader.ReadUInt16();
                    byte ArraySize = reader.ReadByte();
                    string Name = reader.ReadName();

                    var currProp = new UsmapProperty(Name, SchemaIdx, ArraySize, null);
                    currProp.PropertyData = DeserializePropData(reader);
                    props.Add(currProp);
                }

                Schemas.Add(schemaName, new UsmapSchema(schemaName, schemaSuperName, numProps, props));
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
