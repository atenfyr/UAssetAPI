using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using UAssetAPI.PropertyTypes;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes
{
    public enum EFontHinting : byte
    {
        /** Use the default hinting specified in the font. */
        Default,
        /** Force the use of an automatic hinting algorithm. */
        Auto,
        /** Force the use of an automatic light hinting algorithm, optimized for non-monochrome displays. */
        AutoLight,
        /** Force the use of an automatic hinting algorithm optimized for monochrome displays. */
        Monochrome,
        /** Do not use hinting. */
        None,
    }

    public enum EFontLoadingPolicy : byte
    {
        /** Lazy load the entire font into memory. This will consume more memory than Streaming, however there will be zero file-IO when rendering glyphs within the font, although the initial load may cause a hitch. */
        LazyLoad,
        /** Stream the font from disk. This will consume less memory than LazyLoad or Inline, however there will be file-IO when rendering glyphs, which may cause hitches under certain circumstances or on certain platforms. */
        Stream,
        /** Embed the font data within the asset. This will consume more memory than Streaming, however it is guaranteed to be hitch free (only valid for font data within a Font Face asset). */
        Inline,
    }

    public class FFontData
    {
        public FPackageIndex LocalFontFaceAsset; // UObject
        public FString FontFilename;
        public EFontHinting Hinting;
        public EFontLoadingPolicy LoadingPolicy;
        public int SubFaceIndex;
        public bool bIsCooked;

        public FFontData(AssetBinaryReader reader)
        {
            if (reader.Asset.GetCustomVersion<FEditorObjectVersion>() < FEditorObjectVersion.AddedFontFaceAssets) return;

            bIsCooked = reader.ReadInt32() != 0;
            if (bIsCooked)
            {
                LocalFontFaceAsset = reader.XFER_OBJECT_POINTER();

                if (LocalFontFaceAsset.Index == 0)
                {
                    FontFilename = reader.ReadFString();
                    Hinting =(EFontHinting)reader.ReadByte();
                    LoadingPolicy = (EFontLoadingPolicy)reader.ReadByte();
                }

                SubFaceIndex = reader.ReadInt32();
            }
        }

        public void Write(AssetBinaryWriter writer)
        {
            if (writer.Asset.GetCustomVersion<FEditorObjectVersion>() < FEditorObjectVersion.AddedFontFaceAssets) return;

            writer.Write(bIsCooked ? 1 : 0);
            if (bIsCooked)
            {
                writer.XFER_OBJECT_POINTER(LocalFontFaceAsset);

                if (LocalFontFaceAsset.Index == 0)
                {
                    writer.Write(FontFilename);
                    writer.Write((byte)Hinting);
                    writer.Write((byte)LoadingPolicy);
                }

                writer.Write(SubFaceIndex);
            }
        }

    }

    public class FontDataPropertyData : PropertyData<FFontData>
    {
        public FontDataPropertyData(FName name) : base(name)
        {

        }

        public FontDataPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("FontData");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FFontData(reader);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;

            Value.Write(writer);
            
            return (int)writer.BaseStream.Position-here;
        }
    }
}
