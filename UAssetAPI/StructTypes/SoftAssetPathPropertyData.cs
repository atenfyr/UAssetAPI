namespace UAssetAPI.StructTypes
{
    public class SoftAssetPathPropertyData : SoftObjectPathPropertyData
    {
        public SoftAssetPathPropertyData(FName name) : base(name)
        {

        }

        public SoftAssetPathPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("SoftAssetPath");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }
    }
}
