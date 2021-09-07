namespace UAssetAPI
{
    /**
     * UObject resource type for objects that are referenced by this package, but contained
     * within another package.
     */
    public class Import
    {
        public FName ClassPackage;
        public FName ClassName;
        public int OuterIndex;
        public FName ObjectName;
        public int Index;

        public Import(string classPackage, string className, int outerIndex, string objectName, int index = 0)
        {
            ClassPackage = new FName(classPackage);
            ClassName = new FName(className);
            OuterIndex = outerIndex;
            ObjectName = new FName(objectName);
            Index = index;
        }

        public Import(FName classPackage, FName className, int outerIndex, FName objectName, int index = 0)
        {
            ClassPackage = classPackage;
            ClassName = className;
            OuterIndex = outerIndex;
            ObjectName = objectName;
            Index = index;
        }

        public Import()
        {

        }
    }
}
