using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UAssetAPI.IO;
using UAssetAPI.Kismet.Bytecode;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI
{
    /// <summary>
    /// Any binary reader used in the parsing of Unreal file types.
    /// </summary>
    public abstract class UnrealBinaryReader : BinaryReader
    {
        public UnrealBinaryReader(Stream stream) : base(stream)
        {

        }

        protected byte[] ReverseIfBigEndian(byte[] data)
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

        public virtual FString ReadFString(FSerializedNameHeader nameHeader = null)
        {
            if (nameHeader == null)
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
            else
            {
                if (nameHeader.bIsWide)
                {
                    byte[] data = this.ReadBytes(nameHeader.Len * 2); // TODO: are we actually supposed to divide by two?
                    return new FString(Encoding.Unicode.GetString(data, 0, data.Length), Encoding.Unicode);
                }
                else
                {
                    byte[] data = this.ReadBytes(nameHeader.Len);
                    return new FString(Encoding.ASCII.GetString(data, 0, data.Length), Encoding.ASCII);
                }
            }
        }

        public virtual FString ReadNameMapString(FSerializedNameHeader nameHeader, out uint hashes)
        {
            hashes = 0;
            FString str = this.ReadFString(nameHeader);
            if (this is AssetBinaryReader abr)
            {
                if (abr.Asset is UAsset && abr.Asset.ObjectVersion >= ObjectVersion.VER_UE4_NAME_HASHES_SERIALIZED && !string.IsNullOrEmpty(str.Value)) hashes = this.ReadUInt32();
            }
            return str;
        }

        internal const ulong CityHash64 = 0x00000000C1640000;
        public void ReadNameBatch(bool VerifyHashes, out ulong HashVersion, out List<FString> nameMap)
        {
            // TODO: implement pre-ue5 serialization

            HashVersion = 0;
            nameMap = new List<FString>();

            int numStrings = ReadInt32();
            if (numStrings == 0) return;
            ReadInt32(); // length of strings in bytes

            // read hashes
            HashVersion = ReadUInt64();
            ulong[] hashes = new ulong[numStrings];
            switch (HashVersion)
            {
                case UnrealBinaryReader.CityHash64:
                    for (int i = 0; i < numStrings; i++) hashes[i] = ReadUInt64(); // CityHash64 of str.ToLowerCase();
                    break;
                default:
                    throw new InvalidOperationException("Unknown algorithm ID " + HashVersion);
            }

            // read headers
            FSerializedNameHeader[] nameHeaders = new FSerializedNameHeader[numStrings];
            for (int i = 0; i < numStrings; i++) nameHeaders[i] = FSerializedNameHeader.Read(this);

            // read strings
            for (int i = 0; i < numStrings; i++)
            {
                FString newStr = ReadNameMapString(nameHeaders[i], out _);
                nameMap.Add(newStr);
            }

            // verify hashes if requested
            if (VerifyHashes)
            {
                for (int i = 0; i < nameMap.Count; i++)
                {
                    switch (HashVersion)
                    {
                        case UnrealBinaryReader.CityHash64:
                            ulong expectedHash = CRCGenerator.CityHash64WithLower(nameMap[i]);
                            if (expectedHash != hashes[i]) throw new IOException("Expected hash \"" + expectedHash + "\", received \"" + hashes[i] + "\" for string " + nameMap[i].Value + " in name map; corrupt data?");
                            break;
                        default:
                            throw new InvalidOperationException("Unknown algorithm ID " + HashVersion);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Reads primitive data types from Unreal Engine assets.
    /// </summary>
    public class AssetBinaryReader : UnrealBinaryReader
    {
        public UnrealPackage Asset;

        public AssetBinaryReader(Stream stream, UnrealPackage asset = null) : base(stream)
        {
            Asset = asset;
        }

        public virtual Guid? ReadPropertyGuid()
        {
            if (Asset.HasUnversionedProperties) return null;
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)
            {
                bool hasPropertyGuid = ReadBoolean();
                if (hasPropertyGuid) return new Guid(ReadBytes(16));
            }
            return null;
        }

        public virtual FName ReadFName()
        {
            if (Asset is ZenAsset)
            {
                uint Index = this.ReadUInt32();
                uint Number = this.ReadUInt32();

                var res = new FName(Asset, (int)(Index & FName.IndexMask), (int)Number);
                res.Type = (EMappedNameType)((Index & FName.TypeMask) >> FName.TypeShift);
                return res;
            }
            else
            {
                int nameMapPointer = this.ReadInt32();
                int number = this.ReadInt32();
                return new FName(Asset, nameMapPointer, number);
            }
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

        public FPackageIndex XFERPTR()
        {
            return new FPackageIndex(this.ReadInt32());
        }

        public FPackageIndex XFER_FUNC_POINTER()
        {
            return this.XFERPTR();
        }

        public KismetPropertyPointer XFER_PROP_POINTER()
        {
            if (Asset.ObjectVersion >= KismetPropertyPointer.XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION)
            {
                int numEntries = this.ReadInt32();
                FName[] allNames = new FName[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    allNames[i] = this.ReadFName();
                }
                FPackageIndex owner = this.XFER_OBJECT_POINTER();
                return new KismetPropertyPointer(new FFieldPath(allNames, owner));
            }
            else
            {
                return new KismetPropertyPointer(this.XFERPTR());
            }
        }

        public FPackageIndex XFER_OBJECT_POINTER()
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
