using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class StructPropertyData : PropertyData<IList<PropertyData>> // List
    {
        public bool IsForced = false;
        public string StructType = null;
        public Guid StructGUID = Guid.Empty; // usually set to 0

        public StructPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            IsForced = false;
            Type = "StructProperty";
            Value = new List<PropertyData>();
        }

        public StructPropertyData(string name, AssetReader asset, bool forceReadNull, string forcedType) : base(name, asset, forceReadNull)
        {
            IsForced = true;
            StructType = forcedType;
            Type = "StructProperty";
            Value = new List<PropertyData>();
        }

        public StructPropertyData()
        {

        }

        public string GetStructType()
        {
            return StructType;
        }

        public void SetStructType(string type)
        {
            StructType = type;
        }

        public void SetForced(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                IsForced = false;
                StructType = null;
            }
            else
            {
                IsForced = true;
                StructType = type;
            }
        }

        private void ReadOnce<T>(BinaryReader reader) where T: PropertyData, new()
        {
            T data = (T)Activator.CreateInstance(typeof(T), Name, Asset, false);
            data.Read(reader, 0);
            Value = new List<PropertyData> { data };
        }

        private void ReadNormal(BinaryReader reader)
        {
            IList<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            while ((data = MainSerializer.Read(Asset, reader)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (!IsForced)
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
                case "IntPoint": // 2 ints
                    ReadOnce<IntPointPropertyData>(reader);
                    break;
                case "DateTime": // 1 long
                    ReadOnce<DateTimePropertyData>(reader);
                    break;
                case "Rotator": // 3 floats
                    ReadOnce<RotatorPropertyData>(reader);
                    break;
                case "Quat": // 4 floats
                    ReadOnce<QuatPropertyData>(reader);
                    break;
                default:
                    ReadNormal(reader);
                    break;
            }
        }

        private void WriteOnce(BinaryWriter writer)
        {
            Value[0].Write(writer);
        }

        private int WriteNormal(BinaryWriter writer)
        {
            int here = (int)writer.BaseStream.Position;
            if (Value != null)
            {
                foreach (var t in Value)
                {
                    MainSerializer.Write(t, Asset, writer);
                }
            }
            writer.Write((long)Asset.SearchHeaderReference("None"));
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer)
        {
            if (!IsForced)
            {
                writer.Write((long)Asset.SearchHeaderReference(StructType));
                writer.Write(StructGUID.ToByteArray());
                if (ForceReadNull) writer.Write((byte)0);
            }
            switch(StructType)
            {
                case "Guid":
                case "LinearColor":
                case "Quat":
                    WriteOnce(writer);
                    return 16;
                case "Vector":
                case "Rotator":
                    WriteOnce(writer);
                    return 12;
                case "Vector2D":
                case "IntPoint":
                case "DateTime":
                    WriteOnce(writer);
                    return 8;
                case "Color":
                    WriteOnce(writer);
                    return 4;
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