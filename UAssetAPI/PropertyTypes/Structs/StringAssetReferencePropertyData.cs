using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A struct that contains a string reference to a class. Can be used to make soft references to classes.
    /// </summary>
    public class StringAssetReferencePropertyData : SoftObjectPathPropertyData
    {
        public StringAssetReferencePropertyData(FName name) : base(name)
        {

        }

        public StringAssetReferencePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("StringAssetReference");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }
    }
}
