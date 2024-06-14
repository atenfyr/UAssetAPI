using System.Collections.Generic;
using Newtonsoft.Json;

namespace UAssetAPI.UnrealTypes
{
    public class FLocMetadataObject
    {
        public class FLocMetadataValue
        {
            // TODO:
        }

        [JsonProperty] public List<FLocMetadataValue> Values { get; set; } = new List<FLocMetadataValue>();
    }
}