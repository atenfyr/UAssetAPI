namespace UAssetAPI.StructTypes
{
    public class SkeletalMeshAreaWeightedTriangleSamplerPropertyData : WeightedRandomSamplerPropertyData
    {
        public SkeletalMeshAreaWeightedTriangleSamplerPropertyData(FName name) : base(name)
        {

        }

        public SkeletalMeshAreaWeightedTriangleSamplerPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("SkeletalMeshAreaWeightedTriangleSampler");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }
    }
}