using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class StructPropertyData : PropertyData<IList<PropertyData>> // List
    {
        public string StructType = null;
        public Guid StructGUID = Guid.Empty; // usually set to 0

        public StructPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "StructProperty";
            Value = new List<PropertyData>();
        }

        public StructPropertyData(string name, AssetReader asset, string forcedType) : base(name, asset)
        {
            StructType = forcedType;
            Type = "StructProperty";
            Value = new List<PropertyData>();
        }

        public StructPropertyData()
        {
            Type = "StructProperty";
        }

        private void ReadOnce<T>(BinaryReader reader) where T: PropertyData, new()
        {
            T data = (T)Activator.CreateInstance(typeof(T), Name, Asset);
            data.Read(reader, false, 0);
            Value = new List<PropertyData> { data };
        }

        private void ReadNormal(BinaryReader reader)
        {
            IList<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            while ((data = MainSerializer.Read(Asset, reader, true)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            if (includeHeader) // originally !isForced
            {
                StructType = Asset.GetHeaderReference((int)reader.ReadInt64());
                StructGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();
            }
            switch (StructType)
            {
                case "Guid": // 16 byte GUID
                    ReadOnce<GuidPropertyData>(reader);
                    break;
                case "LinearColor": // 4 floats
                    ReadOnce<LinearColorPropertyData>(reader);
                    break;
                case "Color":
                    ReadOnce<ColorPropertyData>(reader);
                    break;
                case "Vector": // 3 floats
                    ReadOnce<VectorPropertyData>(reader);
                    break;
                case "Vector2D": // 2 floats
                    ReadOnce<Vector2DPropertyData>(reader);
                    break;
                case "Vector4": // 4 floats
                    ReadOnce<Vector4PropertyData>(reader);
                    break;
                case "Box": // 2 Vectors
                    ReadOnce<BoxPropertyData>(reader);
                    break;
                case "IntPoint": // 2 ints
                    ReadOnce<IntPointPropertyData>(reader);
                    break;
                case "DateTime": // 1 long
                    ReadOnce<DateTimePropertyData>(reader);
                    break;
                case "Timespan": // 1 long
                    ReadOnce<TimespanPropertyData>(reader);
                    break;
                case "Rotator": // 3 floats
                    ReadOnce<RotatorPropertyData>(reader);
                    break;
                case "Quat": // 4 floats
                    ReadOnce<QuatPropertyData>(reader);
                    break;
                case "SoftObjectPath":
                    ReadOnce<SoftObjectPropertyData>(reader);
                    break;
                case "RichCurveKey":
                    ReadOnce<RichCurveKeyProperty>(reader);
                    break;
                default:
                    ReadNormal(reader);
                    break;
            }
        }

        private void WriteOnce(BinaryWriter writer)
        {
            Value[0].Write(writer, false);
        }

        private int WriteNormal(BinaryWriter writer)
        {
            int here = (int)writer.BaseStream.Position;
            if (Value != null)
            {
                foreach (var t in Value)
                {
                    MainSerializer.Write(t, Asset, writer, true);
                }
            }
            writer.Write((long)Asset.SearchHeaderReference("None"));
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((long)Asset.SearchHeaderReference(StructType));
                writer.Write(StructGUID.ToByteArray());
                writer.Write((byte)0);
            }
            switch(StructType)
            {
                case "Guid":
                case "LinearColor":
                case "Quat":
                case "Vector4":
                    WriteOnce(writer);
                    return 16;
                case "Vector":
                case "Rotator":
                case "SoftObjectPath":
                    WriteOnce(writer);
                    return 12;
                case "Vector2D":
                case "IntPoint":
                case "DateTime":
                case "Timespan":
                    WriteOnce(writer);
                    return 8;
                case "Color":
                    WriteOnce(writer);
                    return 4;
                case "Box":
                    WriteOnce(writer);
                    return 25;
                case "RichCurveKey":
                    WriteOnce(writer);
                    return 27;
                default:
                    return WriteNormal(writer);
            }
        }

        public override void FromString(string[] d)
        {
            if (d[4] != null) StructType = d[4];
        }
    }
}