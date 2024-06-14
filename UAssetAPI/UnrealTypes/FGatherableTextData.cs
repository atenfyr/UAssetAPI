using System.Collections.Generic;
using Newtonsoft.Json;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// Gatherable text data item
    /// </summary>
    public struct FGatherableTextData
    {
        [JsonProperty] public FString NamespaceName { get; set; }
        [JsonProperty] public FTextSourceData SourceData { get; set; }
        [JsonProperty] public List<FTextSourceSiteContext> SourceSiteContexts { get; set; }
    }
}