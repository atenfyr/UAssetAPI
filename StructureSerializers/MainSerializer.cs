using System;
using System.IO;

namespace UAssetAPI.StructureSerializers
{
    /*
        Main serializer for most category types
    */

    public static class MainSerializer
    {
        public static PropertyData TypeToClass(string type, string name, AssetReader asset, BinaryReader reader, bool forceReadNull = true)
        {
            PropertyData data = null;
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
                case "Vector":
                    data = new VectorPropertyData(name, asset, forceReadNull);
                    break;
                case "Rotator":
                    data = new RotatorPropertyData(name, asset, forceReadNull);
                    break;
                default:
                    throw new FormatException("Invalid property type: " + type + " (on " + name + " at " + reader.BaseStream.Position + ")");
            }
            data.Read(reader);
            return data;
        }

        public static PropertyData Read(AssetReader asset, BinaryReader reader, bool forceReadNull = true)
        {
            string name = asset.GetHeaderReference((int)reader.ReadInt64());
            if (name == "None") return null;

            int typeNum = (int)reader.ReadInt64();
            string type = name;
            if (typeNum > 0) type = asset.GetHeaderReference(typeNum);

            long leng = reader.ReadInt64();
            return TypeToClass(type, name, asset, reader, forceReadNull);
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
