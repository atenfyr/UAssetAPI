namespace UAssetAPI
{
    public struct FFieldPath
    {
        /// <summary>
        /// Path to the FField object from the innermost FField to the outermost UObject (UPackage)
        /// </summary>
        public FName[] Path;

        /// <summary>
        /// The cached owner of this field.
        /// </summary>
        public FPackageIndex ResolvedOwner;

        public FFieldPath(FName[] path, FPackageIndex resolvedOwner)
        {
            Path = path;
            ResolvedOwner = resolvedOwner;
        }
    }
}
