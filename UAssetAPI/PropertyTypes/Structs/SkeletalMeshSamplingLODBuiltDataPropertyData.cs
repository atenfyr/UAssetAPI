using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class SkeletalMeshSamplingLODBuiltDataPropertyData : PropertyData<SkeletalMeshAreaWeightedTriangleSamplerPropertyData>
    {
        public SkeletalMeshSamplingLODBuiltDataPropertyData(FName name) : base(name)
        {

        }

        public SkeletalMeshSamplingLODBuiltDataPropertyData()
        {

        }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new SkeletalMeshAreaWeightedTriangleSamplerPropertyData(FName.DefineDummy(reader.Asset, "AreaWeightedTriangleSampler"));
            Value.Read(reader, parentName, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            return Value.Write(writer, false);
        }

        private static readonly FString CurrentPropertyType = new FString("SkeletalMeshSamplingLODBuiltData");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}