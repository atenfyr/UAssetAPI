using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FFieldPath
    {
        /// <summary>
        /// Path to the FField object from the innermost FField to the outermost UObject (UPackage)
        /// </summary>
        [JsonProperty]
        public FName[] Path;

        /// <summary>
        /// The cached owner of this field.
        /// </summary>
        [JsonProperty]
        public FPackageIndex ResolvedOwner;

        public FFieldPath(FName[] path, FPackageIndex resolvedOwner)
        {
            Path = path;
            ResolvedOwner = resolvedOwner;
        }

        public FFieldPath()
        {

        }

        public FFieldPath(AssetBinaryReader reader)
        {
            int pathNum = reader.ReadInt32();
            Path = new FName[pathNum];
            for (int i = 0; i < pathNum; i++)
            {
                Path[i] = reader.ReadFName();
            }

            ResolvedOwner = new FPackageIndex(reader.ReadInt32());
        }

        public int Write(AssetBinaryWriter writer)
        {
            if (Path == null && ResolvedOwner == null)
            {
                return 0;
            }
            writer.Write(Path.Length);
            foreach (FName name in Path)
            {
                writer.Write(name);
            }

            writer.Write(ResolvedOwner.Index);
            return sizeof(int) * (2 + Path.Length * 2);
        }
    }
}
