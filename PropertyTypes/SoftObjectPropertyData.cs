using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class AssetObjectPropertyData : PropertyData<FString>
    {
        public uint ID = 0;

        public AssetObjectPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public AssetObjectPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("AssetObjectProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadFStringWithEncoding();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            return writer.WriteFString(Value);
        }

        public override string ToString()
        {
            return "(" + Value + ", " + ID + ")";
        }

        public override void FromString(string[] d)
        {
            Asset.AddNameReference(new FString(d[0]));
            Value = new FString(d[0]);
        }
    }

    public class SoftObjectPropertyData : PropertyData<FName>
    {
        public uint ID = 0;

        public SoftObjectPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public SoftObjectPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("SoftObjectProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadFName(Asset);
            ID = reader.ReadUInt32();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.WriteFName(Value, Asset);
            writer.Write(ID);
            return sizeof(int) * 3;
        }

        public override string ToString()
        {
            return "(" + Value.ToString() + ", " + ID + ")";
        }

        public override void FromString(string[] d)
        {
            FName output = FName.FromString(d[0]);
            Asset.AddNameReference(output.Value);
            Value = output;

            if (uint.TryParse(d[1], out uint res2))
            {
                ID = res2;
            }
            else
            {
                ID = 0;
            }
        }
    }

    public class SoftObjectPathPropertyData : PropertyData<FName>
    {
        public uint ID = 0;

        public SoftObjectPathPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public SoftObjectPathPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("SoftObjectPath");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadFName(Asset);
            ID = reader.ReadUInt32();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.WriteFName(Value, Asset);
            writer.Write(ID);
            return sizeof(int) * 3;
        }

        public override string ToString()
        {
            return "(" + Value.ToString() + ", " + ID + ")";
        }

        public override void FromString(string[] d)
        {
            FName output = FName.FromString(d[0]);
            Asset.AddNameReference(output.Value);
            Value = output;

            if (uint.TryParse(d[1], out uint res2))
            {
                ID = res2;
            }
            else
            {
                ID = 0;
            }
        }
    }
}