using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public abstract class PropertyData
    {
        public FName Name = new FName("");
        public int DuplicationIndex = 0;
        public UAsset Asset;
        public object RawValue;

        public void SetObject(object value)
        {
            RawValue = value;
        }

        public T GetObject<T>()
        {
            return (T)RawValue;
        }

        public PropertyData(FName name, UAsset asset)
        {
            Name = name;
            Asset = asset;
        }

        public PropertyData()
        {

        }

        private static FName FallbackPropertyType = new FName(string.Empty);
        public virtual FName PropertyType { get { return FallbackPropertyType; } }

        public virtual void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {

        }

        public virtual int Write(BinaryWriter writer, bool includeHeader)
        {
            return 0;
        }


        public virtual void FromString(string[] d)
        {

        }
    }

    public abstract class PropertyData<T> : PropertyData
    {
        public T Value
        {
            get => GetObject<T>();
            set => SetObject(value);
        }

        public PropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public PropertyData()
        {

        }
    }
}
