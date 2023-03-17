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
    /// Any binary writer used in the parsing of Unreal file types.
    /// </summary>
    public abstract class UnrealBinaryWriter : BinaryWriter
    {
        public UnrealBinaryWriter() : base()
        {

        }

        public UnrealBinaryWriter(Stream stream) : base(stream)
        {

        }

        public UnrealBinaryWriter(Stream stream, Encoding encoding) : base(stream, encoding)
        {

        }

        public UnrealBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen) : base(stream, encoding, leaveOpen)
        {

        }

        protected byte[] ReverseIfBigEndian(byte[] data)
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

        public void WriteNameBatch(ulong HashVersion, IList<FString> nameMap)
        {
            Write(nameMap.Count);
            if (nameMap.Count == 0) return;
            long numBytesOfStringsPos = this.BaseStream.Position;
            Write((int)0);

            // write hashes
            Write(HashVersion);
            switch (HashVersion)
            {
                case UnrealBinaryReader.CityHash64:
                    for (int i = 0; i < nameMap.Count; i++)
                    {
                        Write(CRCGenerator.CityHash64(CRCGenerator.ToLower(nameMap[i].Value), nameMap[i].Encoding));
                    }
                    break;
                default:
                    throw new InvalidOperationException("Unknown algorithm ID " + HashVersion);
            }

            // write headers
            for (int i = 0; i < nameMap.Count; i++)
            {
                FSerializedNameHeader.Write(this, nameMap[i].Encoding is UnicodeEncoding, nameMap[i].Value.Length);
            }

            // write strings
            long stringsStartPos = BaseStream.Position;
            for (int i = 0; i < nameMap.Count; i++)
            {
                Write(nameMap[i].Encoding.GetBytes(nameMap[i].Value));
            }
            long stringsEndPos = BaseStream.Position;

            // fix length
            Seek((int)numBytesOfStringsPos, SeekOrigin.Begin);
            Write((int)(stringsEndPos - stringsStartPos));
            Seek((int)stringsEndPos, SeekOrigin.Begin);
        }
    }

    /// <summary>
    /// Writes primitive data types from Unreal Engine assets.
    /// </summary>
    public class AssetBinaryWriter : UnrealBinaryWriter
    {
        public UnrealPackage Asset;

        public AssetBinaryWriter(UnrealPackage asset) : base()
        {
            Asset = asset;
        }

        public AssetBinaryWriter(Stream stream, UnrealPackage asset) : base(stream)
        {
            Asset = asset;
        }

        public AssetBinaryWriter(Stream stream, Encoding encoding, UnrealPackage asset) : base(stream, encoding)
        {
            Asset = asset;
        }

        public AssetBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen, UnrealPackage asset) : base(stream, encoding, leaveOpen)
        {
            Asset = asset;
        }

        public virtual void Write(FName name)
        {
            if (name == null) name = new FName(Asset, 0, 0);
            if (Asset is ZenAsset)
            {
                this.Write(((uint)name.Type << FName.TypeShift) | (uint)name.Index);
                this.Write(name.Number);
            }
            else
            {
                this.Write(name.Index);
                this.Write(name.Number);
            }
        }

        public virtual void WritePropertyGuid(Guid? guid)
        {
            if (Asset.HasUnversionedProperties) return;
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)
            {
                Write(guid != null);
                if (guid != null) Write(((Guid)guid).ToByteArray());
            }
        }

        /*
        !!!!!

        THE FOLLOWING METHODS ARE INTENDED ONLY TO BE USED IN PARSING KISMET BYTECODE; PLEASE DO NOT USE THEM FOR ANY OTHER PURPOSE!

        !!!!!
        */

        /// <summary>This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!</summary>
        public int XFERSTRING(string val)
        {
            long startMetric = this.BaseStream.Position;
            this.Write(Encoding.ASCII.GetBytes(val + "\0"));
            return (int)(this.BaseStream.Position - startMetric);
        }

        /// <summary>This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!</summary>
        public int XFERUNICODESTRING(string val)
        {
            long startMetric = this.BaseStream.Position;
            this.Write(Encoding.Unicode.GetBytes(val + "\0"));
            return (int)(this.BaseStream.Position - startMetric);
        }

        /// <summary>This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!</summary>
        public int XFERNAME(FName val)
        {
            this.Write(val);
            return 12; // FScriptName's iCode offset is 12 bytes, not 8
        }

        /// <summary>This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!</summary>
        public int XFER_FUNC_NAME(FName val)
        {
            return this.XFERNAME(val);
        }

        private static readonly int PointerSize = sizeof(ulong);

        /// <summary>This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!</summary>
        public int XFERPTR(FPackageIndex val)
        {
            this.Write(val.Index);
            return PointerSize; // For the iCode offset, we return the size of a pointer in memory rather than the size of an FPackageIndex on disk
        }

        /// <summary>This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!</summary>
        public int XFER_FUNC_POINTER(FPackageIndex val)
        {
            return this.XFERPTR(val);
        }

        /// <summary>This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!</summary>
        public int XFER_PROP_POINTER(KismetPropertyPointer val)
        {
            if (Asset.ObjectVersion >= KismetPropertyPointer.XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION)
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

        /// <summary>This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!</summary>
        public int XFER_OBJECT_POINTER(FPackageIndex val)
        {
            return this.XFERPTR(val);
        }
    }
}
