using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class AssetObjectPropertyData : PropertyData<FString>
    {
        public uint ID = 0;

        public AssetObjectPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("AssetObjectProperty");
        }

        public AssetObjectPropertyData()
        {
            Type = new FName("AssetObjectProperty");
        }

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
            Type = new FName("SoftObjectProperty");
        }

        public SoftObjectPropertyData()
        {
            Type = new FName("SoftObjectProperty");
        }

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
            return "(" + Value + ", " + ID + ")";
        }

        public override void FromString(string[] d)
        {
            Asset.AddNameReference(new FString(d[0]));
            Value = new FName(d[0]);

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
            Type = new FName("SoftObjectPath");
        }

        public SoftObjectPathPropertyData()
        {
            Type = new FName("SoftObjectPath");
        }

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
            return "(" + Value + ", " + ID + ")";
        }

        public override void FromString(string[] d)
        {
            Asset.AddNameReference(new FString(d[0]));
            Value = new FName(d[0]);

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