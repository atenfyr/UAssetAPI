using System.Collections.Generic;
using UAssetAPI.StructureSerializers;

namespace UAssetAPI
{
    public class Category
    {
        public bool IsRaw = false;
        public IList<PropertyData> Data;
        public byte[] RawData;

        public Category(byte[] data)
        {
            IsRaw = true;
            RawData = data;
        }

        public Category(IList<PropertyData> data)
        {
            IsRaw = false;
            Data = data;
        }

        public Category()
        {
            Data = new List<PropertyData>();
        }
    }
}
