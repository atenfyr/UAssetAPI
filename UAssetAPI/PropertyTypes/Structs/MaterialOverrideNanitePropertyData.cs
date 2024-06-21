using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class MaterialOverrideNanitePropertyData : PropertyData
    {
        public FSoftObjectPath OverrideMaterialRef;
        public bool bEnableOverride;
        public FPackageIndex OverrideMaterial;
        public bool bSerializeAsCookedData;

        public MaterialOverrideNanitePropertyData(FName name) : base(name)
        {

        }

        public MaterialOverrideNanitePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MaterialOverrideNanite");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            if (reader.Asset.GetCustomVersion<FFortniteReleaseBranchCustomObjectVersion>() < FFortniteReleaseBranchCustomObjectVersion.NaniteMaterialOverrideUsesEditorOnly)
            {
                OverrideMaterialRef = FSoftObjectPath.Read(reader);
                bEnableOverride = reader.ReadInt32() == 1;
                OverrideMaterial = FPackageIndex.FromRawIndex(reader.ReadInt32());
                return;
            }

            bSerializeAsCookedData = reader.ReadInt32() == 1;
            if (bSerializeAsCookedData) OverrideMaterial = FPackageIndex.FromRawIndex(reader.ReadInt32());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader && !writer.Asset.HasUnversionedProperties)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;

            if (writer.Asset.GetCustomVersion<FFortniteReleaseBranchCustomObjectVersion>() < FFortniteReleaseBranchCustomObjectVersion.NaniteMaterialOverrideUsesEditorOnly)
            {
                OverrideMaterialRef.Write(writer);
                writer.Write(bEnableOverride ? 1 : 0);
                writer.Write(OverrideMaterial.Index);
            }

            writer.Write(bSerializeAsCookedData ? 1 : 0);
            if (bSerializeAsCookedData) writer.Write(OverrideMaterial.Index);

            return (int)writer.BaseStream.Position - here;
        }

        public override void FromString(string[] d, UAsset asset)
        {

        }
    }
}
