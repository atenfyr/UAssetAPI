using System;

namespace UAssetAPI
{
    public class CustomVersion
    {
        public Guid Key;
        public int Version;

        public CustomVersion(Guid key, int version)
        {
            Key = key;
            Version = version;
        }

        public CustomVersion()
        {

        }
    }
}
