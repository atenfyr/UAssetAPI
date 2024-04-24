using Newtonsoft.Json;

namespace UAssetAPI.UnrealTypes
{
    public struct FTextSourceData
    {
        [JsonProperty] public FString SourceString { get; set; }
        [JsonProperty] public FLocMetadataObject SourceStringMetaData { get; set; }
    }
}