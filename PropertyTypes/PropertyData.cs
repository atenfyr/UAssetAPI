using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public abstract class PropertyData
    {
        public string Name = "";
        public int WidgetData = 0;
        public string Type = "";
        public AssetReader Asset;
        public object RawValue;

        public void SetObject(object value)
        {
            RawValue = value;
        }

        public T GetObject<T>()
        {
            return (T)RawValue;
        }

        public PropertyData(string name, AssetReader asset)
        {
            Name = name;
            Asset = asset;
        }

        public PropertyData()
        {

        }

        public virtual void Read(BinaryReader reader, bool includeHeader, long leng)
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

        public PropertyData(string name, AssetReader asset) : base(name, asset)
        {

        }

        public PropertyData()
        {

        }
    }
}
