using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UAssetAPI.CustomVersions;
using UAssetAPI.Kismet.Bytecode;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI
{
    /// <summary>
    /// Any binary writer used in the parsing of Unreal file types.
    /// </summary>
    public class UnrealBinaryWriter : BinaryWriter
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

        public void WriteCustomVersionContainer(ECustomVersionSerializationFormat format, List<CustomVersion> CustomVersionContainer)
        {
            // TODO: support for enum-based custom versions
            int num = CustomVersionContainer == null ? 0 : CustomVersionContainer.Count;

            switch (format)
            {
                case ECustomVersionSerializationFormat.Enums:
                    throw new NotImplementedException("Custom version serialization format Enums is currently unimplemented");
                case ECustomVersionSerializationFormat.Guids:
                    long numLoc = this.BaseStream.Position;
                    Write((int)0);

                    int realNum = 0;
                    for (int i = 0; i < num; i++)
                    {
                        if (CustomVersionContainer[i].Version <= 0 || !CustomVersionContainer[i].IsSerialized) continue;
                        realNum++;
                        Write(CustomVersionContainer[i].Key.ToByteArray());
                        Write(CustomVersionContainer[i].Version);
                        Write(CustomVersionContainer[i].Name);
                    }

                    long endLoc = this.BaseStream.Position;
                    this.Seek((int)numLoc, SeekOrigin.Begin);
                    Write(realNum);
                    this.Seek((int)endLoc, SeekOrigin.Begin);
                    break;
                case ECustomVersionSerializationFormat.Optimized:
                    numLoc = this.BaseStream.Position;
                    Write((int)0);

                    realNum = 0;
                    for (int i = 0; i < num; i++)
                    {
                        if (CustomVersionContainer[i].Version < 0 || !CustomVersionContainer[i].IsSerialized) continue;
                        realNum++;
                        Write(CustomVersionContainer[i].Key.ToByteArray());
                        Write(CustomVersionContainer[i].Version);
                    }

                    endLoc = this.BaseStream.Position;
                    this.Seek((int)numLoc, SeekOrigin.Begin);
                    Write(realNum);
                    this.Seek((int)endLoc, SeekOrigin.Begin);
                    break;
            }
        }
    }

    /// <summary>
    /// Writes primitive data types from Unreal Engine assets.
    /// </summary>
    public class AssetBinaryWriter : UnrealBinaryWriter
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

        public virtual void Write(FName name)
        {
            if (name == null) name = new FName(Asset, 0, 0);
            this.Write(name.Index);
            this.Write(name.Number);
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

        public virtual void Write(FObjectThumbnail thumbnail)
        {
            Write(thumbnail.Width);
            Write(thumbnail.Height);
            Write(thumbnail.CompressedImageData.Length);
            if (thumbnail.CompressedImageData.Length > 0)
                Write(thumbnail.CompressedImageData);
        }

        public virtual void Write(FLocMetadataObject metadataObject)
        {
            Write(metadataObject.Values.Count);
            if (metadataObject.Values.Count > 0)
                throw new NotImplementedException("TODO: implement Write(FLocMetadataObject)");
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
            this.Write(val?.Index ?? 0);
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
            if (Asset.GetCustomVersion<FReleaseObjectVersion>() >= FReleaseObjectVersion.FFieldPathOwnerSerialization)
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
