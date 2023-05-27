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
    public class SoftObjectPathPropertyData : PropertyData<FSoftObjectPath>
    {
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

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
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
                Value = FSoftObjectPath.Read(reader);
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
                Value.Write(writer);
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return "(" + Value.AssetPath.PackageName.ToString() + ", " + Value.AssetPath.AssetName.ToString() + ", " + Value.SubPathString.ToString() + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (asset.ObjectVersion < ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                Path = FString.FromString(d[0]);
            }
            else
            {
                FName one = FName.FromString(asset, d[0]);
                FName two = FName.FromString(asset, d[1]);
                FString three = string.IsNullOrEmpty(d[2]) ? null : FString.FromString(d[2]);

                Value = new FSoftObjectPath(one, two, three);
            }
        }
    }
}
