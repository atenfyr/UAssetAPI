using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UAssetAPI.Kismet.Bytecode;

namespace UAssetAPI
{
    public class AssetBinaryReader : BinaryReader
    {
        public UAsset Asset;

        public AssetBinaryReader(Stream stream, UAsset asset) : base(stream)
        {
            Asset = asset;
        }

        private byte[] ReverseIfBigEndian(byte[] data)
        {
            if (!BitConverter.IsLittleEndian) Array.Reverse(data);
            return data;
        }

        public override short ReadInt16()
        {
            return BitConverter.ToInt16(ReverseIfBigEndian(base.ReadBytes(2)), 0);
        }

        public override ushort ReadUInt16()
        {
            return BitConverter.ToUInt16(ReverseIfBigEndian(base.ReadBytes(2)), 0);
        }

        public override int ReadInt32()
        {
            return BitConverter.ToInt32(ReverseIfBigEndian(base.ReadBytes(4)), 0);
        }

        public override uint ReadUInt32()
        {
            return BitConverter.ToUInt32(ReverseIfBigEndian(base.ReadBytes(4)), 0);
        }

        public override long ReadInt64()
        {
            return BitConverter.ToInt64(ReverseIfBigEndian(base.ReadBytes(8)), 0);
        }

        public override ulong ReadUInt64()
        {
            return BitConverter.ToUInt64(ReverseIfBigEndian(base.ReadBytes(8)), 0);
        }

        public override float ReadSingle()
        {
            return BitConverter.ToSingle(ReverseIfBigEndian(base.ReadBytes(4)), 0);
        }

        public override double ReadDouble()
        {
            return BitConverter.ToDouble(ReverseIfBigEndian(base.ReadBytes(8)), 0);
        }

        public override string ReadString()
        {
            return ReadFString()?.Value;
        }

        public virtual FString ReadFString()
        {
            int length = this.ReadInt32();
            switch (length)
            {
                case 0:
                    return null;
                default:
                    if (length < 0)
                    {
                        byte[] data = this.ReadBytes(-length * 2);
                        return new FString(Encoding.Unicode.GetString(data, 0, data.Length - 2), Encoding.Unicode);
                    }
                    else
                    {
                        byte[] data = this.ReadBytes(length);
                        return new FString(Encoding.ASCII.GetString(data, 0, data.Length - 1), Encoding.ASCII);
                    }
            }
        }

        public virtual FString ReadNameMapString(out uint hashes)
        {
            FString str = this.ReadFString();
            if (!string.IsNullOrEmpty(str.Value))
            {
                hashes = this.ReadUInt32();
            }
            else
            {
                hashes = 0;
            }
            return str;
        }

        public virtual FName ReadFName()
        {
            int nameMapPointer = this.ReadInt32();
            int number = this.ReadInt32();
            return new FName(Asset.GetNameReference(nameMapPointer), number);
        }

        public string XFERSTRING()
        {
            List<byte> readData = new List<byte>();
            while (true)
            {
                byte newVal = this.ReadByte();
                if (newVal == 0) break;
                readData.Add(newVal);
            }
            return Encoding.ASCII.GetString(readData.ToArray());
        }

        public string XFERUNICODESTRING()
        {
            List<byte> readData = new List<byte>();
            while (true)
            {
                byte newVal1 = this.ReadByte();
                byte newVal2 = this.ReadByte();
                if (newVal1 == 0 && newVal2 == 0) break;
                readData.Add(newVal1);
                readData.Add(newVal2);
            }
            return Encoding.Unicode.GetString(readData.ToArray());
        }

        public void XFERTEXT()
        {

        }

        public FName XFERNAME()
        {
            return this.ReadFName();
        }

        public FName XFER_FUNC_NAME()
        {
            return this.XFERNAME();
        }

        public ulong XFERPTR()
        {
            return this.ReadUInt64();
        }

        public ulong XFER_FUNC_POINTER()
        {
            return this.XFERPTR();
        }

        public ulong XFER_PROP_POINTER()
        {
            return this.XFERPTR();
        }

        public ulong XFER_OBJECT_POINTER()
        {
            return this.XFERPTR();
        }

        public KismetExpression[] ReadExpressionArray(EExprToken endToken)
        {
            List<KismetExpression> newData = new List<KismetExpression>();
            KismetExpression currExpression = null;
            while (currExpression == null || currExpression.Token != endToken)
            {
                if (currExpression != null) newData.Add(currExpression);
                currExpression = ExpressionSerializer.ReadExpression(this);
            }
            return newData.ToArray();
        }
    }
}
