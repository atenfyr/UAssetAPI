using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /*
        ForceReadNull should pretty much always be set to true for API usage
    */

    public abstract class PropertyData
    {
        public string Name;
        public string Type;
        public AssetReader Asset;
        public object RawValue;
        public bool ForceReadNull = true;

        public void SetObject(object value)
        {
            RawValue = value;
        }

        public T GetObject<T>()
        {
            return (T)RawValue;
        }

        public PropertyData(string name, AssetReader asset, bool forceReadNull)
        {
            Name = name;
            Asset = asset;
            ForceReadNull = forceReadNull;
        }

        public PropertyData()
        {

        }

        public virtual void Read(BinaryReader reader, long leng)
        {

        }

        public virtual int Write(BinaryWriter writer)
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

        public PropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {

        }

        public PropertyData()
        {

        }
    }
}
