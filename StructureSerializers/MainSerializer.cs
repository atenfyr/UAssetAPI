using System;
using System.IO;

namespace UAssetAPI.StructureSerializers
{
    /*
        Serializer for types 57 and 41
    */

    public static class MainSerializer
    {
        public static PropertyData Read(AssetReader asset, BinaryReader reader, bool forceReadNull = true)
        {
            PropertyData data = null;
            string name = asset.GetHeaderReference((int)reader.ReadInt64());
            if (name == "None") return null;
            string type = asset.GetHeaderReference((int)reader.ReadInt64());
            long leng = reader.ReadInt64();
            switch (type)
            {
                case "BoolProperty":
                    data = new BoolPropertyData(name, asset, forceReadNull);
                    break;
                case "IntProperty":
                    data = new IntPropertyData(name, asset, forceReadNull);
                    break;
                case "FloatProperty":
                    data = new FloatPropertyData(name, asset, forceReadNull);
                    break;
                case "TextProperty":
                    data = new TextPropertyData(name, asset, forceReadNull);
                    break;
                case "ObjectProperty":
                    data = new ObjectPropertyData(name, asset, forceReadNull);
                    break;
                case "EnumProperty":
                    data = new EnumPropertyData(name, asset, forceReadNull);
                    break;
                case "ArrayProperty":
                    data = new ArrayPropertyData(name, asset, forceReadNull);
                    break;
                case "StructProperty":
                    data = new StructPropertyData(name, asset, forceReadNull);
                    break;
                default:
                    throw new FormatException("Invalid property type: " + type);
            }
            data.Read(reader);
            return data;
        }
    }
}
