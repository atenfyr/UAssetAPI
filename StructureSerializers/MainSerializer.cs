using System;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI.StructureSerializers
{
    /*
        Main serializer for most category types
    */

    public static class MainSerializer
    {
        public static PropertyData TypeToClass(string type, string name, AssetReader asset, BinaryReader reader = null, long leng = 0, bool forceReadNull = true)
        {
            PropertyData data = null;
            switch (type)
            {
                case "BoolProperty":
                    data = new BoolPropertyData(name, asset, forceReadNull);
                    break;
                case "Int8Property":
                    data = new Int8PropertyData(name, asset, forceReadNull);
                    break;
                case "Int16Property":
                    data = new Int16PropertyData(name, asset, forceReadNull);
                    break;
                case "IntProperty":
                    data = new IntPropertyData(name, asset, forceReadNull);
                    break;
                case "Int64Property":
                    data = new Int64PropertyData(name, asset, forceReadNull);
                    break;
                case "UInt16Property":
                    data = new UInt16PropertyData(name, asset, forceReadNull);
                    break;
                case "UInt32Property":
                    data = new UInt32PropertyData(name, asset, forceReadNull);
                    break;
                case "UInt64Property":
                    data = new UInt64PropertyData(name, asset, forceReadNull);
                    break;
                case "FloatProperty":
                    data = new FloatPropertyData(name, asset, forceReadNull);
                    break;
                case "TextProperty":
                    data = new TextPropertyData(name, asset, forceReadNull);
                    break;
                case "StrProperty":
                    data = new StrPropertyData(name, asset, forceReadNull);
                    break;
                case "ObjectProperty":
                    data = new ObjectPropertyData(name, asset, forceReadNull);
                    break;
                case "EnumProperty":
                    data = new EnumPropertyData(name, asset, forceReadNull);
                    break;
                case "ByteProperty":
                    data = new BytePropertyData(name, asset, forceReadNull);
                    break;
                case "NameProperty":
                    data = new NamePropertyData(name, asset, forceReadNull);
                    break;
                case "ArrayProperty":
                    data = new ArrayPropertyData(name, asset, forceReadNull);
                    break;
                case "MapProperty":
                    data = new MapPropertyData(name, asset, forceReadNull);
                    break;
                case "StructProperty":
                    data = new StructPropertyData(name, asset, forceReadNull);
                    break;
                case "GUID":
                case "Guid":
                    data = new GuidPropertyData(name, asset, forceReadNull);
                    break;
                case "LinearColor":
                    data = new LinearColorPropertyData(name, asset, forceReadNull);
                    break;
                case "Color":
                    data = new ColorPropertyData(name, asset, forceReadNull);
                    break;
                case "Vector":
                    data = new VectorPropertyData(name, asset, forceReadNull);
                    break;
                case "Vector2D":
                    data = new Vector2DPropertyData(name, asset, forceReadNull);
                    break;
                case "Rotator":
                    data = new RotatorPropertyData(name, asset, forceReadNull);
                    break;
                case "Quat":
                    data = new QuatPropertyData(name, asset, forceReadNull);
                    break;
                case "SoftObjectProperty":
                    data = new SoftObjectPropertyData(name, asset, forceReadNull);
                    break;
                case "MulticastDelegateProperty":
                    data = new MulticastDelegatePropertyData(name, asset, forceReadNull);
                    break;
                default:
                    if (reader == null) throw new FormatException("Unknown property type: " + type + " (on " + name + ")");
                    throw new FormatException("Unknown property type: " + type + " (on " + name + " at " + reader.BaseStream.Position + ")");
            }
            if (reader != null) data.Read(reader, leng);
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
