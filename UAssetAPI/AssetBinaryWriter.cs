using System;
using System.IO;
using System.Text;
using UAssetAPI.Kismet.Bytecode;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI
{
    /// <summary>
    /// Writes primitive data types from Unreal Engine assets.
    /// </summary>
    public class AssetBinaryWriter : BinaryWriter
    {
        public UAsset Asset;

        public AssetBinaryWriter(UAsset asset) : base()
        {
            Asset = asset;
        }

        public AssetBinaryWriter(Stream stream, UAsset asset) : base(stream)
        {
            Asset = asset;
        }

        public AssetBinaryWriter(Stream stream, Encoding encoding, UAsset asset) : base(stream, encoding)
        {
            Asset = asset;
        }

        public AssetBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen, UAsset asset) : base(stream, encoding, leaveOpen)
        {
            Asset = asset;
        }

        private byte[] ReverseIfBigEndian(byte[] data)
        {
            if (!BitConverter.IsLittleEndian) Array.Reverse(data);
            return data;
        }

        public override void Write(short value)
        {
            this.Write(ReverseIfBigEndian(BitConverter.GetBytes(value)));
        }

        public override void Write(ushort value)
        {
            this.Write(ReverseIfBigEndian(BitConverter.GetBytes(value)));
        }

        public override void Write(int value)
        {
            this.Write(ReverseIfBigEndian(BitConverter.GetBytes(value)));
        }

        public override void Write(uint value)
        {
            this.Write(ReverseIfBigEndian(BitConverter.GetBytes(value)));
        }

        public override void Write(long value)
        {
            this.Write(ReverseIfBigEndian(BitConverter.GetBytes(value)));
        }

        public override void Write(ulong value)
        {
            this.Write(ReverseIfBigEndian(BitConverter.GetBytes(value)));
        }

        public override void Write(float value)
        {
            this.Write(ReverseIfBigEndian(BitConverter.GetBytes(value)));
        }

        public override void Write(double value)
        {
            this.Write(ReverseIfBigEndian(BitConverter.GetBytes(value)));
        }

        public override void Write(string value)
        {
            Write(new FString(value));
        }

        public virtual int Write(FString value)
        {
            switch (value?.Value)
            {
                case null:
                    this.Write((int)0);
                    return sizeof(int);
                default:
                    string nullTerminatedStr = value.Value + "\0";
                    this.Write(value.Encoding is UnicodeEncoding ? -nullTerminatedStr.Length : nullTerminatedStr.Length);
                    byte[] actualStrData = value.Encoding.GetBytes(nullTerminatedStr);
                    this.Write(actualStrData);
                    return actualStrData.Length + 4;
            }
        }

        public virtual void Write(FName name)
        {
            this.Write(name.Index);
            this.Write(name.Number);
        }

        public virtual void WritePropertyGuid(Guid? guid)
        {
            if (Asset.EngineVersion >= UE4Version.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)
            {
                Write(guid != null);
                if (guid != null) Write(((Guid)guid).ToByteArray());
            }
        }

        public int XFERSTRING(string val)
        {
            long startMetric = this.BaseStream.Position;
            this.Write(Encoding.ASCII.GetBytes(val + "\0"));
            return (int)(this.BaseStream.Position - startMetric);
        }

        public int XFERUNICODESTRING(string val)
        {
            long startMetric = this.BaseStream.Position;
            this.Write(Encoding.Unicode.GetBytes(val + "\0"));
            return (int)(this.BaseStream.Position - startMetric);
        }

        public int XFERNAME(FName val)
        {
            this.Write(val);
            return 12; // FScriptName's iCode offset is 12 bytes, not 8
        }

        public int XFER_FUNC_NAME(FName val)
        {
            return this.XFERNAME(val);
        }

        private static readonly int PointerSize = sizeof(ulong);

        public int XFERPTR(FPackageIndex val)
        {
            this.Write(val.Index);
            return PointerSize; // For the iCode offset, we return the size of a pointer in memory rather than the size of an FPackageIndex on disk
        }

        public int XFER_FUNC_POINTER(FPackageIndex val)
        {
            return this.XFERPTR(val);
        }

        public int XFER_PROP_POINTER(KismetPropertyPointer val)
        {
            if (Asset.EngineVersion >= KismetPropertyPointer.XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION)
            {
                this.Write(val.New.Path.Length);
                for (int i = 0; i < val.New.Path.Length; i++)
                {
                    this.XFERNAME(val.New.Path[i]);
                }
                this.XFER_OBJECT_POINTER(val.New.ResolvedOwner);
            }
            else
            {
                this.XFERPTR(val.Old);
            }
            return PointerSize;
        }

        public int XFER_OBJECT_POINTER(FPackageIndex val)
        {
            return this.XFERPTR(val);
        }
    }
}
