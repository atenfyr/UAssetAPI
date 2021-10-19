using Newtonsoft.Json;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    /// <summary>
    /// A struct that contains a string reference to an object, either a top level asset or a subobject.
    /// This can be used to make soft references to assets that are loaded on demand.
    /// This is stored internally as an FName pointing to the top level asset (/package/path.assetname) and an option a string subobject path.
    /// </summary>
    public class SoftObjectPathPropertyData : PropertyData
    {
        /// <summary>Asset path, patch to a top level object in a package. This is /package/path.assetname</summary>
        [JsonProperty]
        public FName AssetPathName;

        /// <summary>Optional FString for subobject within an asset. This is the sub path after the :</summary>
        [JsonProperty]
        public FString SubPathString;

        /// <summary>Used in older versions of the Unreal Engine.</summary>
        [JsonProperty]
        public FString Path;

        public SoftObjectPathPropertyData(FName name) : base(name)
        {

        }

        public SoftObjectPathPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("SoftObjectPath");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            if (reader.Asset.EngineVersion < UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                Path = reader.ReadFString();
            }
            else
            {
                AssetPathName = reader.ReadFName();
                SubPathString = reader.ReadFString();
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;

            if (writer.Asset.EngineVersion < UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                writer.Write(Path);
            }
            else
            {
                writer.Write(AssetPathName);
                writer.Write(SubPathString);
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return "(" + AssetPathName.ToString() + ", " + SubPathString.ToString() + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (asset.EngineVersion < UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                Path = FString.FromString(d[0]);
            }
            else
            {
                FName output = FName.FromString(d[0]);
                asset.AddNameReference(output.Value);
                AssetPathName = output;

                if (d.Length > 1)
                {
                    SubPathString = FString.FromString(d[1]);
                }
            }
        }
    }
}
