using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class SoftAssetPathPropertyData : SoftObjectPathPropertyData
    {
        public SoftAssetPathPropertyData(FName name) : base(name)
        {

        }

        public SoftAssetPathPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("SoftAssetPath");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }
    }
}
