using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A struct that contains a string reference to a class. Can be used to make soft references to classes.
    /// </summary>
    public class SoftClassPathPropertyData : SoftObjectPathPropertyData
    {
        public SoftClassPathPropertyData(FName name) : base(name)
        {

        }

        public SoftClassPathPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("SoftClassPath");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }
    }
}
