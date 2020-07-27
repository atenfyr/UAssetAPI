using System;
using System.Diagnostics;
using System.IO;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI
{
    /*
        Main serializer for most category types
    */

    public static class MainSerializer
    {
        public static PropertyData TypeToClass(string type, string name, AssetReader asset, BinaryReader reader = null, long leng = 0, bool forceReadNull = true)
        {
            PropertyData data;
            switch (type)
            {
                case "BoolProperty":
                    data = new BoolPropertyData(name, asset);
                    break;
                case "Int8Property":
                    data = new Int8PropertyData(name, asset);
                    break;
                case "Int16Property":
                    data = new Int16PropertyData(name, asset);
                    break;
                case "IntProperty":
                    data = new IntPropertyData(name, asset);
                    break;
                case "Int64Property":
                    data = new Int64PropertyData(name, asset);
                    break;
                case "UInt16Property":
                    data = new UInt16PropertyData(name, asset);
                    break;
                case "UInt32Property":
                    data = new UInt32PropertyData(name, asset);
                    break;
                case "UInt64Property":
                    data = new UInt64PropertyData(name, asset);
                    break;
                case "FloatProperty":
                    data = new FloatPropertyData(name, asset);
                    break;
                case "TextProperty":
                    data = new TextPropertyData(name, asset);
                    break;
                case "StrProperty":
                    data = new StrPropertyData(name, asset);
                    break;
                case "ObjectProperty":
                    data = new ObjectPropertyData(name, asset);
                    break;
                case "EnumProperty":
                    data = new EnumPropertyData(name, asset);
                    break;
                case "ByteProperty":
                    data = new BytePropertyData(name, asset);
                    break;
                case "NameProperty":
                    data = new NamePropertyData(name, asset);
                    break;
                case "ArrayProperty":
                    data = new ArrayPropertyData(name, asset);
                    break;
                case "MapProperty":
                    data = new MapPropertyData(name, asset);
                    break;
                case "StructProperty":
                    data = new StructPropertyData(name, asset);
                    break;
                case "Guid":
                    data = new GuidPropertyData(name, asset);
                    break;
                case "LinearColor":
                    data = new LinearColorPropertyData(name, asset);
                    break;
                case "Color":
                    data = new ColorPropertyData(name, asset);
                    break;
                case "Vector":
                    data = new VectorPropertyData(name, asset);
                    break;
                case "Vector2D":
                    data = new Vector2DPropertyData(name, asset);
                    break;
                case "IntPoint":
                    data = new IntPointPropertyData(name, asset);
                    break;
                case "DateTime":
                    data = new DateTimePropertyData(name, asset);
                    break;
                case "Rotator":
                    data = new RotatorPropertyData(name, asset);
                    break;
                case "Quat":
                    data = new QuatPropertyData(name, asset);
                    break;
                case "SoftObjectProperty":
                    data = new SoftObjectPropertyData(name, asset);
                    break;
                case "MulticastDelegateProperty":
                    data = new MulticastDelegatePropertyData(name, asset);
                    break;
                default:
                    if (reader == null) throw new FormatException("Unknown property type: " + type + " (on " + name + ")");
                    throw new FormatException("Unknown property type: " + type + " (on " + name + " at " + reader.BaseStream.Position + ")");
            }
            if (reader != null)
            {
                if (forceReadNull == false) data.ForceReadNull = false;
                data.Read(reader, leng);
            }
            return data;
        }

        public static PropertyData Read(AssetReader asset, BinaryReader reader, bool forceReadNull = true)
        {
            string name = asset.GetHeaderReference((int)reader.ReadInt64());
            if (name.Equals("None")) return null;

            int typeNum = (int)reader.ReadInt64();
            string type = name;
            if (typeNum > 0) type = asset.GetHeaderReference(typeNum);

            long leng = reader.ReadInt64();
            return TypeToClass(type, name, asset, reader, leng, forceReadNull);
        }

        public static int Write(PropertyData property, AssetReader asset, BinaryWriter writer) // Returns location of the length
        {
            writer.Write((long)asset.SearchHeaderReference(property.Name));
            writer.Write((long)asset.SearchHeaderReference(property.Type));
            int oldLoc = (int)writer.BaseStream.Position;
            writer.Write((long)0); // initial length
            int realLength = property.Write(writer);
            int newLoc = (int)writer.BaseStream.Position;

            writer.Seek(oldLoc, SeekOrigin.Begin);
            writer.Write((long)realLength);
            writer.Seek(newLoc, SeekOrigin.Begin);
            return oldLoc;
        }
    }
}
