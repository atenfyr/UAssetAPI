using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

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

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
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

    /// <summary>
    /// A reference variable to another object which may be null, and may become valid or invalid at any point.
    /// </summary>
    public struct FSoftObjectPath
    {
        /** Asset path, patch to a top level object in a package. This is /package/path.assetname */
        public FName AssetPathName;
        /** Optional FString for subobject within an asset. This is the sub path after the : */
        public FString SubPathString;

        public FSoftObjectPath(FName assetPathName, FString subPathString)
        {
            AssetPathName = assetPathName;
            SubPathString = subPathString;
        }
    }

    /// <summary>
    /// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Near synonym for <see cref="AssetObjectPropertyData"/>.
    /// </summary>
    public class SoftObjectPropertyData : PropertyData<FSoftObjectPath>
    {
        [JsonProperty]
        public uint ID = 0;

        public SoftObjectPropertyData(FName name) : base(name)
        {

        }

        public SoftObjectPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("SoftObjectProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FSoftObjectPath(reader.ReadFName(), reader.ReadFString());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write(Value.AssetPathName);
            writer.Write(Value.SubPathString);
            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return "(" + Value.ToString() + ", " + ID + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            FName one = FName.FromString(asset, d[0]);
            FString two = string.IsNullOrEmpty(d[1]) ? null : FString.FromString(d[1]);

            Value = new FSoftObjectPath(one, two);
        }
    }
}