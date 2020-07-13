using System.Collections.Generic;
using UAssetAPI.StructureSerializers;

namespace UAssetAPI
{
    public class Category
    {
        public CategoryReference ReferenceData;
        public bool IsRaw = false;
        public IList<PropertyData> Data;
        public byte[] RawData;
        public int NumExtraZeros = 0;

        public void SetRawData(byte[] data)
        {
            IsRaw = true;
            RawData = data;
        }

        public void SetData(IList<PropertyData> data, int numExtraZeros = 0)
        {
            IsRaw = false;
            Data = data;
            NumExtraZeros = numExtraZeros;
        }

        public Category(CategoryReference reference, byte[] data)
        {
            ReferenceData = reference;
            SetRawData(data);
        }

        public Category(CategoryReference reference, IList<PropertyData> data, int numExtraZeros)
        {
            ReferenceData = reference;
            SetData(data, numExtraZeros);
        }

        public Category(CategoryReference reference)
        {
            ReferenceData = reference;
            Data = new List<PropertyData>();
        }

        public Category()
        {
            ReferenceData = new CategoryReference();
            Data = new List<PropertyData>();
        }
    }
}
