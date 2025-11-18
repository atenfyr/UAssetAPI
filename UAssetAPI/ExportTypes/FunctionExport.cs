using System.IO;
using System.Reflection.PortableExecutable;
using UAssetAPI.ExportTypes;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes
{
    /// <summary>
    /// Export data for a blueprint function.
    /// </summary>
    public class FunctionExport : StructExport
    {
        public EFunctionFlags FunctionFlags;
		public short RepOffset;
		public FPackageIndex EventGraphFunction;
		public int EventGraphCallOffset;

        public FunctionExport(Export super) : base(super)
        {
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public FunctionExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public FunctionExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);
            FunctionFlags = (EFunctionFlags)reader.ReadUInt32();
            
			if (FunctionFlags.HasFlag(EFunctionFlags.FUNC_Net))
			{
				RepOffset = reader.ReadInt16();
			}
			else
			{
				RepOffset = 0;
			}

			if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_SERIALIZE_BLUEPRINT_EVENTGRAPH_FASTCALLS_IN_UFUNCTION)
			{
				EventGraphFunction = new(reader.ReadInt32());
				EventGraphCallOffset = reader.ReadInt32();
			}
			else
			{
				EventGraphFunction = null;
				EventGraphCallOffset = 0;
			}
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((uint)FunctionFlags);

			if (FunctionFlags.HasFlag(EFunctionFlags.FUNC_Net))
			{
				writer.Write(RepOffset);
			}

			if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_SERIALIZE_BLUEPRINT_EVENTGRAPH_FASTCALLS_IN_UFUNCTION)
			{
				writer.Write(EventGraphFunction.Index);
				writer.Write(EventGraphCallOffset);
			}
		}
    }
}
