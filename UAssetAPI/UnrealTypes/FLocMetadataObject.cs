using Newtonsoft.Json;
using System.Collections.Generic;

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

    public enum ELocMetadataType : int
    {
        None,
        Boolean,
        String,
        Array,
        Object,
    };
}