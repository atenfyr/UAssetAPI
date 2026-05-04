using System;
using System.Collections.Generic;
using UAssetAPI.CustomVersions;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes
{
    public struct FSimpleMemberReference
    {
        public FPackageIndex MemberParent;
        public FName MemberName;
        public Guid MemberGuid;
    }

    public class ActorComponentExport : NormalExport
    {
        public List<FSimpleMemberReference> UCSModifiedProperties;

        public ActorComponentExport(Export super) : base(super)
        {

        }

        public ActorComponentExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public ActorComponentExport()
        {

        }
        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);
            if (reader.Asset.GetCustomVersion<FFortniteReleaseBranchCustomObjectVersion>() >= FFortniteReleaseBranchCustomObjectVersion.ActorComponentUCSModifiedPropertiesSparseStorage)
            {
                UCSModifiedProperties = new List<FSimpleMemberReference>();
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    FPackageIndex MemberParent = new FPackageIndex(reader);
                    FName MemberName = reader.ReadFName();
                    Guid MemberGuid = new Guid(reader.ReadBytes(16));
                    UCSModifiedProperties.Add(new FSimpleMemberReference() { MemberParent = MemberParent, MemberName = MemberName, MemberGuid = MemberGuid });
                }
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            if (writer.Asset.GetCustomVersion<FFortniteReleaseBranchCustomObjectVersion>() >= FFortniteReleaseBranchCustomObjectVersion.ActorComponentUCSModifiedPropertiesSparseStorage)
            {
                writer.Write(UCSModifiedProperties.Count);
                for (int i = 0; i < UCSModifiedProperties.Count; i++)
                {
                    writer.Write(UCSModifiedProperties[i].MemberParent.Index);
                    writer.Write(UCSModifiedProperties[i].MemberName);
                    writer.Write(UCSModifiedProperties[i].MemberGuid.ToByteArray());
                }
            }
        }
    }
}
