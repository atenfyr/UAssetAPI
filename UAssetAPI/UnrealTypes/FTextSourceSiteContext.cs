using Newtonsoft.Json;

namespace UAssetAPI.UnrealTypes
{
    public struct FTextSourceSiteContext
    {
        [JsonProperty] public FString KeyName { get; set; }
        [JsonProperty] public FString SiteDescription { get; set; }
        [JsonProperty] public bool IsEditorOnly { get; set; }
        [JsonProperty] public bool IsOptional { get; set; }
        [JsonProperty] public FLocMetadataObject InfoMetaData { get; set; }
        [JsonProperty] public FLocMetadataObject KeyMetaData { get; set; }
    }
}