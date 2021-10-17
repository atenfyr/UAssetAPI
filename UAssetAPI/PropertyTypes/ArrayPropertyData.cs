using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes an array.
    /// </summary>
    public class ArrayPropertyData : PropertyData<PropertyData[]> // Array
    {
        [JsonProperty]
        public FName ArrayType;
        [JsonProperty]
        public StructPropertyData DummyStruct;

        public bool ShouldSerializeDummyStruct()
        {
            return Value.Length == 0;
        }

        public ArrayPropertyData(FName name) : base(name)
        {
            Value = new PropertyData[0];
        }

        public ArrayPropertyData()
        {
            Value = new PropertyData[0];
        }

        private static readonly FName CurrentPropertyType = new FName("ArrayProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                ArrayType = reader.ReadFName();
                reader.ReadByte(); // null byte
            }

            int numEntries = reader.ReadInt32();
            if (ArrayType.Value.Value == "StructProperty")
            {
                var results = new PropertyData[numEntries];
                FName name = reader.ReadFName();
                if (name.Value.Value.Equals("None"))
                {
                    Value = results;
                    return;
                }

                if (reader.ReadFName().Value.Value != ArrayType.Value.Value) throw new FormatException("Invalid array type");
                long structLength = reader.ReadInt64(); // length value

                FName fullType = reader.ReadFName();
                Guid structGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();

                if (numEntries == 0)
                {
                    DummyStruct = new StructPropertyData(name, fullType)
                    {
                        StructGUID = structGUID
                    };
                }
                else
                {
                    for (int i = 0; i < numEntries; i++)
                    {
                        var data = new StructPropertyData(name, fullType);
                        data.Offset = reader.BaseStream.Position;
                        data.Read(reader, false, structLength);
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
                        results[i] = MainSerializer.TypeToClass(ArrayType, new FName(i.ToString()), reader.Asset);
                        results[i].Offset = reader.BaseStream.Position;
                        results[i].Read(reader, false, averageSizeEstimate1, averageSizeEstimate2);
                    }
                }
                Value = results;
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (Value.Length > 0) ArrayType = Value[0].PropertyType;

            if (includeHeader)
            {
                writer.Write(ArrayType);
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write(Value.Length);
            if (ArrayType.Value.Value == "StructProperty")
            {
                if (Value.Length == 0 && DummyStruct == null) throw new InvalidOperationException("No dummy struct present in an empty StructProperty array, cannot serialize");
                if (Value.Length > 0) DummyStruct = (StructPropertyData)Value[0];

                FName fullType = DummyStruct.StructType;

                writer.Write(DummyStruct.Name);
                writer.Write(new FName("StructProperty"));
                int lengthLoc = (int)writer.BaseStream.Position;
                writer.Write((long)0);
                writer.Write(fullType);
                writer.Write(DummyStruct.StructGUID.ToByteArray());
                writer.Write((byte)0);

                for (int i = 0; i < Value.Length; i++)
                {
                    ((StructPropertyData)Value[i]).StructType = fullType;
                    Value[i].Offset = writer.BaseStream.Position;
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
                    Value[i].Offset = writer.BaseStream.Position;
                    Value[i].Write(writer, false);
                }
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (d[4] != null) ArrayType = FName.FromString(d[4]);
        }

        protected override void HandleCloned(PropertyData res)
        {
            ArrayPropertyData cloningProperty = (ArrayPropertyData)res;
            cloningProperty.ArrayType = (FName)this.ArrayType?.Clone();
            cloningProperty.DummyStruct = (StructPropertyData)this.DummyStruct?.Clone();
        }
    }
}