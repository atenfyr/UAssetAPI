using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAssetAPI.PropertyTypes
{
    public class UnknownPropertyData : PropertyData<byte[]>
    {
        public UnknownPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "UnknownProperty";
        }

        public UnknownPropertyData()
        {
            Type = "UnknownProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadBytes((int)leng);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return Value.Length;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }
}
