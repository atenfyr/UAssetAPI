namespace UAssetAPI
{
    /// <summary>
    /// UObject resource type for objects that are referenced by this package, but contained within another package.
    /// </summary>
    public class Import : FObjectResource
    {
        public FName ClassPackage;
        public FName ClassName;

        public Import(string classPackage, string className, FPackageIndex outerIndex, string objectName) : base(new FName(objectName), outerIndex)
        {
            ClassPackage = new FName(classPackage);
            ClassName = new FName(className);
        }

        public Import(FName classPackage, FName className, FPackageIndex outerIndex, FName objectName) : base(objectName, outerIndex)
        {
            ClassPackage = classPackage;
            ClassName = className;
        }

        public Import()
        {

        }
    }
}
