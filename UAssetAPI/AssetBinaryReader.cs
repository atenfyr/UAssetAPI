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
    /// Any binary reader used in the parsing of Unreal file types.
    /// </summary>
    public class UnrealBinaryReader : BinaryReader
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

        public bool ReadBooleanInt()
        {
            var i = ReadInt32();
            return i switch
            {
                1 => true,
                0 => false,
                _ => throw new FormatException($"Invalid boolean value {i}")
            };
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
            hashes = 0;
            FString str = this.ReadFString();
            if (this is AssetBinaryReader abr)
            {
                if (abr.Asset is UAsset abrUa && abrUa.WillSerializeNameHashes != false && !string.IsNullOrEmpty(str.Value))
                {
                    hashes = this.ReadUInt32();
                    if (hashes < (1 << 10) && abrUa.ObjectVersion < ObjectVersion.VER_UE4_NAME_HASHES_SERIALIZED) // "i lied, there's actually no hashes"
                    {
                        abrUa.WillSerializeNameHashes = false;
                        hashes = 0;
                        this.BaseStream.Position -= sizeof(uint);
                    }
                    else
                    {
                        abrUa.WillSerializeNameHashes = true;
                    }
                }
            }
            return str;
        }

        public List<CustomVersion> ReadCustomVersionContainer(ECustomVersionSerializationFormat format, List<CustomVersion> oldCustomVersionContainer = null, Usmap Mappings = null)
        {
            var newCustomVersionContainer = new List<CustomVersion>();
            var existingCustomVersions = new HashSet<Guid>();
            switch (format)
            {
                case ECustomVersionSerializationFormat.Enums:
                    throw new NotImplementedException("Custom version serialization format Enums is currently unimplemented");
                case ECustomVersionSerializationFormat.Guids:
                    int numCustomVersions = ReadInt32();
                    for (int i = 0; i < numCustomVersions; i++)
                    {
                        var customVersionID = new Guid(ReadBytes(16));
                        var customVersionNumber = ReadInt32();
                        newCustomVersionContainer.Add(new CustomVersion(customVersionID, customVersionNumber) { Name = ReadFString() });
                        existingCustomVersions.Add(customVersionID);
                    }
                    break;
                case ECustomVersionSerializationFormat.Optimized:
                    numCustomVersions = ReadInt32();
                    for (int i = 0; i < numCustomVersions; i++)
                    {
                        var customVersionID = new Guid(ReadBytes(16));
                        var customVersionNumber = ReadInt32();
                        newCustomVersionContainer.Add(new CustomVersion(customVersionID, customVersionNumber));                      
                        existingCustomVersions.Add(customVersionID);
                    }
                    break;

            }    

            if (Mappings != null && Mappings.CustomVersionContainer != null && Mappings.CustomVersionContainer.Count > 0)
            {
                foreach (CustomVersion entry in Mappings.CustomVersionContainer)
                {
                    if (!existingCustomVersions.Contains(entry.Key)) newCustomVersionContainer.Add(entry.SetIsSerialized(false));
                }
            }

            if (oldCustomVersionContainer != null)
            {
                foreach (CustomVersion entry in oldCustomVersionContainer)
                {
                    if (!existingCustomVersions.Contains(entry.Key)) newCustomVersionContainer.Add(entry.SetIsSerialized(false));
                }
            }

            return newCustomVersionContainer;
        }
    }

    /// <summary>
    /// Reads primitive data types from Unreal Engine assets.
    /// </summary>
    public class AssetBinaryReader : UnrealBinaryReader
    {
        public UAsset Asset;
        public bool LoadUexp = true;

        public AssetBinaryReader(Stream stream, UAsset asset = null) : base(stream)
        {
            Asset = asset;
        }
        
        public AssetBinaryReader(Stream stream, bool inLoadUexp, UAsset asset = null) : base(stream)
        {
            Asset = asset;
            LoadUexp = inLoadUexp;
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
            int nameMapPointer = this.ReadInt32();
            int number = this.ReadInt32();
            return new FName(Asset, nameMapPointer, number);
        }

        public FObjectThumbnail ReadObjectThumbnail()
        {
            var thumb = new FObjectThumbnail();

            thumb.Width = ReadInt32();
            thumb.Height = ReadInt32();
            var imageBytesCount = ReadInt32();
            thumb.CompressedImageData = imageBytesCount > 0 ? ReadBytes(imageBytesCount) : Array.Empty<byte>();

            return thumb;
        }

        public FLocMetadataObject ReadLocMetadataObject()
        {
            var locMetadataObject = new FLocMetadataObject();

            var valueCount = ReadInt32();
            if (valueCount > 0)
                throw new NotImplementedException("TODO: implement ReadLocMetadataObject");

            return locMetadataObject;
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
            if (Asset.GetCustomVersion<FReleaseObjectVersion>() >= FReleaseObjectVersion.FFieldPathOwnerSerialization)
            {
                int numEntries = this.ReadInt32();
                FName[] allNames = new FName[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    allNames[i] = this.ReadFName();
                }
                FPackageIndex owner = this.XFER_OBJECT_POINTER();
                return new KismetPropertyPointer(new FFieldPath(allNames, owner, this.Asset.Exports.Count));
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
