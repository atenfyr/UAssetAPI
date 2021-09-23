using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class SkeletalMeshSamplingLODBuiltDataPropertyData : PropertyData<SkeletalMeshAreaWeightedTriangleSamplerPropertyData>
    {
        public SkeletalMeshSamplingLODBuiltDataPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public SkeletalMeshSamplingLODBuiltDataPropertyData()
        {

        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = new SkeletalMeshAreaWeightedTriangleSamplerPropertyData(new FName("AreaWeightedTriangleSampler"), Asset);
            Value.Read(reader, false, 0);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            Value.Asset = Asset;
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