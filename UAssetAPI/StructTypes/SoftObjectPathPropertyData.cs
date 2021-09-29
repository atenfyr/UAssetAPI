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
        public FName AssetPathName;

        /// <summary>Optional FString for subobject within an asset. This is the sub path after the :</summary>
        public FString SubPathString;

        /// <summary>Used in older versions of the Unreal Engine.</summary>
        public FString Path;

        public SoftObjectPathPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public SoftObjectPathPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("SoftObjectPath");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            if (Asset.EngineVersion < UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                Path = reader.ReadFStringWithEncoding();
            }
            else
            {
                AssetPathName = reader.ReadFName(Asset);
                SubPathString = reader.ReadFStringWithEncoding();
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;

            if (Asset.EngineVersion < UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                writer.WriteFString(Path);
            }
            else
            {
                writer.WriteFName(AssetPathName, Asset);
                writer.WriteFString(SubPathString);
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return "(" + AssetPathName.ToString() + ", " + SubPathString.ToString() + ")";
        }

        public override void FromString(string[] d)
        {
            FName output = FName.FromString(d[0]);
            Asset.AddNameReference(output.Value);
            AssetPathName = output;

            if (d.Length > 1)
            {
                SubPathString = new FString(d[1]);
            }
        }
    }
}
