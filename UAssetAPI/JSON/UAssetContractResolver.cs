using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.JSON
{
    public class UAssetContractResolver : DefaultContractResolver
    {
        public Dictionary<FName, string> ToBeFilled;

        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(FName).IsAssignableFrom(objectType))
            {
                return new FNameJsonConverter(ToBeFilled);
            }
            return base.ResolveContractConverter(objectType);
        }

        public UAssetContractResolver(Dictionary<FName, string> toBeFilled) : base()
        {
            ToBeFilled = toBeFilled;
        }
    }
}
