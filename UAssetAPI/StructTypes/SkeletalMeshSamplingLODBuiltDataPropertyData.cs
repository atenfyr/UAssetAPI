using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class SkeletalMeshSamplingLODBuiltDataPropertyData : PropertyData<SkeletalMeshAreaWeightedTriangleSamplerPropertyData>
    {
        public SkeletalMeshSamplingLODBuiltDataPropertyData(FName name) : base(name)
        {

        }

        public SkeletalMeshSamplingLODBuiltDataPropertyData()
        {

        }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new SkeletalMeshAreaWeightedTriangleSamplerPropertyData(new FName("AreaWeightedTriangleSampler"));
            Value.Read(reader, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            return Value.Write(writer, false);
        }

        private static readonly FName CurrentPropertyType = new FName("SkeletalMeshSamplingLODBuiltData");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}