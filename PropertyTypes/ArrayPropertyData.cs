using System;
using System.IO;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    public class ArrayPropertyData : PropertyData<PropertyData[]> // Array
    {
        public FName ArrayType;
        public StructPropertyData DummyStruct;

        public ArrayPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("ArrayProperty");
            Value = new PropertyData[0];
        }

        public ArrayPropertyData()
        {
            Type = new FName("ArrayProperty");
            Value = new PropertyData[0];
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                ArrayType = reader.ReadFName(Asset);
                reader.ReadByte(); // null byte
            }

            int numEntries = reader.ReadInt32();
            if (ArrayType.Value.Value == "StructProperty")
            {
                var results = new PropertyData[numEntries];
                FName name = reader.ReadFName(Asset);
                if (name.Value.Value.Equals("None"))
                {
                    Value = results;
                    return;
                }

                if (reader.ReadFName(Asset).Value.Value != ArrayType.Value.Value) throw new FormatException("Invalid array type");
                reader.ReadInt64(); // length value

                FName fullType = reader.ReadFName(Asset);
                Guid structGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();

                if (numEntries == 0)
                {
                    DummyStruct = new StructPropertyData(name, Asset, fullType)
                    {
                        StructGUID = structGUID
                    };
                }
                else
                {
                    for (int i = 0; i < numEntries; i++)
                    {
                        var data = new StructPropertyData(name, Asset, fullType);
                        data.Read(reader, false, 0);
                        data.StructGUID = structGUID;
                        results[i] = data;
                    }
                    DummyStruct = (StructPropertyData)results[0];
                }
                Value = results;
            }
            else
            {
                var results = new PropertyData[numEntries];
                if (numEntries > 0)
                {
                    int averageSizeEstimate1 = (int)(leng1 / numEntries);
                    int averageSizeEstimate2 = (int)((leng1 - 4) / numEntries);
                    for (int i = 0; i < numEntries; i++)
                    {
                        results[i] = MainSerializer.TypeToClass(ArrayType, Name, Asset);
                        results[i].Read(reader, false, averageSizeEstimate1, averageSizeEstimate2);
                    }
                }
                Value = results;
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (Value.Length > 0) ArrayType = Value[0].Type;

            if (includeHeader)
            {
                writer.WriteFName(ArrayType, Asset);
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write(Value.Length);
            if (ArrayType.Value.Value == "StructProperty")
            {
                if (Value.Length == 0 && DummyStruct == null) throw new InvalidOperationException("No dummy struct present in an empty StructProperty array, cannot serialize");
                if (Value.Length > 0) DummyStruct = (StructPropertyData)Value[0];

                FName fullType = DummyStruct.StructType;

                writer.WriteFName(DummyStruct.Name, Asset);
                writer.WriteFName(new FName("StructProperty"), Asset);
                int lengthLoc = (int)writer.BaseStream.Position;
                writer.Write((long)0);
                writer.WriteFName(fullType, Asset);
                writer.Write(DummyStruct.StructGUID.ToByteArray());
                writer.Write((byte)0);

                for (int i = 0; i < Value.Length; i++)
                {
                    ((StructPropertyData)Value[i]).StructType = fullType;
                    Value[i].Write(writer, false);
                }

                int fullLen = (int)writer.BaseStream.Position - lengthLoc;
                int newLoc = (int)writer.BaseStream.Position;
                writer.Seek(lengthLoc, SeekOrigin.Begin);
                writer.Write(fullLen - 32 - (includeHeader ? 1 : 0));
                writer.Seek(newLoc, SeekOrigin.Begin);
            }
            else
            {
                for (int i = 0; i < Value.Length; i++)
                {
                    Value[i].Write(writer, false);
                }
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override void FromString(string[] d)
        {
            if (d[4] != null) ArrayType = FName.FromString(d[4]);
        }
    }
}