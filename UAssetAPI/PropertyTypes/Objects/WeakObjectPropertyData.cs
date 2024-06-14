using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    public class WeakObjectPropertyData : ObjectPropertyData
    {
        public WeakObjectPropertyData(FName name) : base(name)
        {

        }

        public WeakObjectPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("WeakObjectProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }
        public override object DefaultValue { get { return FPackageIndex.FromRawIndex(0); } }
    }
}