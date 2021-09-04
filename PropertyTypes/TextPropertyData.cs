using System;
using System.Collections.Generic;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class TextPropertyData : PropertyData<string[]>
    {
        public int Flag;
        public TextHistoryType HistoryType = TextHistoryType.Base;
        public byte[] Extras;
        public UString BaseBlankString;

        public TextPropertyData(string name, UAsset asset) : base(name, asset)
        {
            Type = "TextProperty";
        }

        public TextPropertyData()
        {
            Type = "TextProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Flag = reader.ReadInt32();
            HistoryType = (TextHistoryType)reader.ReadSByte();

            switch(HistoryType)
            {
                case TextHistoryType.None:
                    Extras = reader.ReadBytes(4);
                    List<string> ValueList = new List<string>();
                    for (int i = 0; i < BitConverter.ToInt32(Extras, 0); i++)
                    {
                        ValueList.Add(reader.ReadUString());
                    }
                    Value = ValueList.ToArray();
                    break;
                case TextHistoryType.Base:
                    //Extras = reader.ReadBytes(4);
                    //Console.WriteLine("EXT: " + BitConverter.ToInt32(Extras, 0));
                    Extras = new byte[0];
                    BaseBlankString = reader.ReadUStringWithEncoding();
                    Value = new string[] { reader.ReadUString(), reader.ReadUString() };
                    break;
                case TextHistoryType.StringTableEntry:
                    Extras = reader.ReadBytes(8);
                    Value = new string[] { reader.ReadUString() };
                    break;
                default:
                    throw new FormatException("Unimplemented reader for " + HistoryType.ToString());
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write(Flag);
            writer.Write((byte)HistoryType);
            writer.Write(Extras);

            switch(HistoryType)
            {
                case TextHistoryType.None:
                    writer.Seek(-4, SeekOrigin.Current);
                    writer.Write(Value.Length);
                    foreach (string val in Value)
                    {
                        writer.WriteUString(val);
                    }
                    break;
                case TextHistoryType.Base:
                    writer.WriteUString(BaseBlankString);
                    for (int i = 0; i < 2; i++)
                    {
                        writer.WriteUString(Value[i]);
                    }
                    break;
                case TextHistoryType.StringTableEntry:
                    writer.WriteUString(Value[0]);
                    break;
                default:
                    throw new FormatException("Unimplemented writer for " + HistoryType.ToString());
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            if (Value == null) return "null";

            string oup = "";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Value[i] + " ";
            }
            return oup.TrimEnd(' ');
        }

        public override void FromString(string[] d)
        {
            if (d[1] != null)
            {
                HistoryType = TextHistoryType.Base;
                Value = new string[] { d[0], d[1] };
                return;
            }

            if (d[0].Equals("null"))
            {
                HistoryType = TextHistoryType.None;
                Value = new string[] { };
                return;
            }

            HistoryType = TextHistoryType.StringTableEntry;
            Value = new string[] { d[0] };
        }
    }
}