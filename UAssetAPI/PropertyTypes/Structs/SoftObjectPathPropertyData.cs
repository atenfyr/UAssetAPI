using Newtonsoft.Json;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
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

        private static readonly FString CurrentPropertyType = new FString("SoftObjectPath");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            if (reader.Asset.ObjectVersion < ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
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
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;

            if (writer.Asset.ObjectVersion < ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
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
            if (asset.ObjectVersion < ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                Path = FString.FromString(d[0]);
            }
            else
            {
                FName output = FName.FromString(asset, d[0]);
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
