namespace UAssetAPI.StructTypes
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

        private static readonly FName CurrentPropertyType = new FName("SoftClassPath");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }
    }
}
