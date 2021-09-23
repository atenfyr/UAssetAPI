using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class StructPropertyData : PropertyData<IList<PropertyData>> // List
    {
        public FName StructType = null;
        public Guid StructGUID = Guid.Empty; // usually set to 0

        public StructPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Value = new List<PropertyData>();
        }

        public StructPropertyData(FName name, UAsset asset, FName forcedType) : base(name, asset)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public StructPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("StructProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        private void ReadOnce(BinaryReader reader, Type T)
        {
            var data = Activator.CreateInstance(T, Name, Asset) as PropertyData;
            if (data == null) return;
            data.Read(reader, false, 0);
            Value = new List<PropertyData> { data };
        }

        private void ReadNTPL(BinaryReader reader)
        {
            IList<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            while ((data = MainSerializer.Read(Asset, reader, true)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader) // originally !isForced
            {
                StructType = reader.ReadFName(Asset);
                StructGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();
            }

            MainSerializer.PropertyTypeRegistry.TryGetValue(StructType.Value.Value, out RegistryEntry targetEntry);

            if (targetEntry != null && targetEntry.HasCustomStructSerialization)
            {
                ReadOnce(reader, targetEntry.PropertyType);
            }
            else
            {
                ReadNTPL(reader);
            }
        }

        private int WriteOnce(BinaryWriter writer)
        {
            if (Value.Count != 1) throw new InvalidOperationException("Structs with type " + StructType.Value.Value + " must have exactly one entry");
            return Value[0].Write(writer, false);
        }

        private int WriteNTPL(BinaryWriter writer)
        {
            int here = (int)writer.BaseStream.Position;
            if (Value != null)
            {
                foreach (var t in Value)
                {
                    MainSerializer.Write(t, Asset, writer, true);
                }
            }
            writer.WriteFName(new FName("None"), Asset);
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WriteFName(StructType, Asset);
                writer.Write(StructGUID.ToByteArray());
                writer.Write((byte)0);
            }

            MainSerializer.PropertyTypeRegistry.TryGetValue(StructType.Value.Value, out RegistryEntry targetEntry);

            if (targetEntry != null && targetEntry.HasCustomStructSerialization) return WriteOnce(writer);
            return WriteNTPL(writer);
        }

        public override void FromString(string[] d)
        {
            if (d[4] != null) StructType = FName.FromString(d[4]);
        }
    }
}