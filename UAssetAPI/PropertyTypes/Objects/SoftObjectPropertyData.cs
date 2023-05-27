using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Near synonym for <see cref="SoftObjectPropertyData"/>.
    /// </summary>
    public class AssetObjectPropertyData : PropertyData<FString>
    {
        [JsonProperty]
        public uint ID = 0;

        public AssetObjectPropertyData(FName name) : base(name)
        {

        }

        public AssetObjectPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("AssetObjectProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadFString();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            return writer.Write(Value);
        }

        public override string ToString()
        {
            return "(" + Value + ", " + ID + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            //asset.AddNameReference(FString.FromString(d[0]));
            Value = FString.FromString(d[0]);
        }
    }

    public struct FTopLevelAssetPath
    {
        /// <summary>
        /// Name of the package containing the asset e.g. /Path/To/Package
        /// If less than 5.1, this is null
        /// </summary>
        public FName PackageName;
        /// <summary>
        /// Name of the asset within the package e.g. 'AssetName'.
        /// If less than 5.1, this contains the full path instead
        /// </summary>
        public FName AssetName;

        public FTopLevelAssetPath(FName packageName, FName assetName)
        {
            PackageName = packageName;
            AssetName = assetName;
        }
    }

    /// <summary>
    /// A reference variable to another object which may be null, and may become valid or invalid at any point.
    /// </summary>
    public struct FSoftObjectPath
    {
        /// <summary>
        /// Asset path, patch to a top level object in a package. This is /package/path.assetname/
        /// </summary>
        public FTopLevelAssetPath AssetPath;
        /// <summary>
        /// Optional FString for subobject within an asset. This is the sub path after the :
        /// </summary>
        public FString SubPathString;

        public FSoftObjectPath(FName packageName, FName assetName, FString subPathString)
        {
            AssetPath = new FTopLevelAssetPath(packageName, assetName);
            SubPathString = subPathString;
        }

        public FSoftObjectPath(FTopLevelAssetPath assetPath, FString subPathString)
        {
            AssetPath = assetPath;
            SubPathString = subPathString;
        }
    }

    /// <summary>
    /// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Near synonym for <see cref="AssetObjectPropertyData"/>.
    /// </summary>
    public class SoftObjectPropertyData : PropertyData<FSoftObjectPath>
    {
        public SoftObjectPropertyData(FName name) : base(name)
        {

        }

        public SoftObjectPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("SoftObjectProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.FSOFTOBJECTPATH_REMOVE_ASSET_PATH_FNAMES)
            {
                Value = new FSoftObjectPath(reader.ReadFName(), reader.ReadFName(), reader.ReadFString());
            }
            else
            {
                Value = new FSoftObjectPath(null, reader.ReadFName(), reader.ReadFString());
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;
            if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.FSOFTOBJECTPATH_REMOVE_ASSET_PATH_FNAMES) writer.Write(Value.AssetPath.PackageName);
            writer.Write(Value.AssetPath.AssetName);
            writer.Write(Value.SubPathString);
            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return "(" + Value.AssetPath.PackageName.ToString() + ", " + Value.AssetPath.AssetName.ToString() + ", " + Value.SubPathString.ToString() + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            FName one = FName.FromString(asset, d[0]);
            FName two = FName.FromString(asset, d[0]);
            FString three = string.IsNullOrEmpty(d[1]) ? null : FString.FromString(d[1]);

            Value = new FSoftObjectPath(one, two, three);
        }
    }
}