using System.Collections.Generic;
using UAssetAPI.StructureSerializers;

namespace UAssetAPI
{
    public class Category
    {
        public bool IsRaw = false;
        public IList<PropertyData> Data;
        public byte[] RawData;
        public int NumExtraZeros = 0;

        public Category(byte[] data)
        {
            IsRaw = true;
            RawData = data;
        }

        public Category(IList<PropertyData> data, int numExtraZeros)
        {
            IsRaw = false;
            Data = data;
            NumExtraZeros = numExtraZeros;
        }

        public Category()
        {
            Data = new List<PropertyData>();
        }
    }
}
