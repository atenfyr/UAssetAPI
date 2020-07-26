using System;
using System.Diagnostics;
using System.IO;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    public class ArrayPropertyData : PropertyData<PropertyData[]> // Array
    {
        public string ArrayType;
        public StructPropertyData DummyStruct;

        public ArrayPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "ArrayProperty";
            Value = new PropertyData[0];
        }

        public ArrayPropertyData()
        {
            Type = "ArrayProperty";
            Value = new PropertyData[0];
        }

        public override void Read(BinaryReader reader, long leng)
        {
            ArrayType = Asset.GetHeaderReference((int)reader.ReadInt64());
            if (ForceReadNull) reader.ReadByte(); // null byte
            int numEntries = reader.ReadInt32();
            if (ArrayType == "StructProperty")
            {
                var results = new PropertyData[numEntries];
                string name = Asset.GetHeaderReference((int)reader.ReadInt64());
                if (name.Equals("None"))
                {
                    Value = results;
                    return;
                }

                Debug.Assert(Asset.GetHeaderReference((int)reader.ReadInt64()) == ArrayType);
                reader.ReadInt64(); // length value

                string fullType = Asset.GetHeaderReference((int)reader.ReadInt64());
                Guid structGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();

                if (numEntries == 0)
                {
                    DummyStruct = new StructPropertyData(name, Asset, true, fullType)
                    {
                        StructGUID = structGUID
                    };
                }
                else
                {
                    for (int i = 0; i < numEntries; i++)
                    {
                        var data = new StructPropertyData(name, Asset, true, fullType);
                        data.Read(reader, 0);
                        data.StructGUID = structGUID;
                        results[i] = data;
                    }
                }
                Value = results;
            }
            else
            {
                var results = new PropertyData[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    results[i] = MainSerializer.TypeToClass(ArrayType, Name, Asset, reader, 0, false);
                }
                Value = results;
            }
        }

        public override int Write(BinaryWriter writer)
        {
            if (Value.Length > 0) ArrayType = Value[0].Type;

            writer.Write((long)Asset.SearchHeaderReference(ArrayType));
            if (ForceReadNull) writer.Write((byte)0);

            int here = (int)writer.BaseStream.Position;
            writer.Write(Value.Length);
            if (ArrayType == "StructProperty")
            {
                StructPropertyData firstElem = Value.Length == 0 ? DummyStruct : (StructPropertyData)Value[0];
                string fullType = firstElem.GetStructType();

                writer.Write((long)Asset.SearchHeaderReference(firstElem.Name));
                writer.Write((long)Asset.SearchHeaderReference("StructProperty"));
                int lengthLoc = (int)writer.BaseStream.Position;
                writer.Write((long)0);
                writer.Write((long)Asset.SearchHeaderReference(fullType));
                writer.Write(firstElem.StructGUID.ToByteArray());
                writer.Write((byte)0);

                for (int i = 0; i < Value.Length; i++)
                {
                    Value[i].ForceReadNull = true;
                    ((StructPropertyData)Value[i]).SetForced(fullType);
                    Value[i].Write(writer);
                }

                int fullLen = (int)writer.BaseStream.Position - lengthLoc;
                int newLoc = (int)writer.BaseStream.Position;
                writer.Seek(lengthLoc, SeekOrigin.Begin);
                writer.Write(fullLen - 32 - (ForceReadNull ? 1 : 0));
                writer.Seek(newLoc, SeekOrigin.Begin);
            }
            else
            {
                for (int i = 0; i < Value.Length; i++)
                {
                    Value[i].ForceReadNull = false;
                    Value[i].Write(writer);
                }
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override void FromString(string[] d)
        {
            if (d[4] != null) ArrayType = d[4];
        }
    }
}