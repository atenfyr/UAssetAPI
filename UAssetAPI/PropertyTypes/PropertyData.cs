using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Generic Unreal property class.
    /// </summary>
    public abstract class PropertyData : ICloneable
    {
        /// <summary>
        /// The name of this property.
        /// </summary>
        public FName Name = new FName("");

        /// <summary>
        /// The duplication index of this property. Used to distinguish properties with the same name in the same struct.
        /// </summary>
        public int DuplicationIndex = 0;

        /// <summary>
        /// The offset of this property on disk. This is for the user only, and has no bearing in the API itself.
        /// </summary>
        public long Offset = -1;

        /// <summary>
        /// The asset that this property is parsed with.
        /// </summary>
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
        public virtual bool HasCustomStructSerialization { get { return false; } }
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

        /// <summary>
        /// Performs a deep clone of the current PropertyData instance.
        /// </summary>
        /// <returns>A deep copy of the current property.</returns>
        public object Clone()
        {
            var res = (PropertyData)MemberwiseClone();
            res.Name = (FName)this.Name.Clone();
            if (res.RawValue is ICloneable cloneableValue) res.RawValue = cloneableValue.Clone();

            HandleCloned(res);
            return res;
        }

        protected virtual void HandleCloned(PropertyData res)
        {
            // Child classes can implement this for custom cloning behavior
        }
    }

    public abstract class PropertyData<T> : PropertyData
    {
        /// <summary>
        /// The main value of this property, if such a concept is applicable to the property in question. Properties may contain other values as well, in which case they will be present as other fields in the child class.
        /// </summary>
        public T Value
        {
            get => GetObject<T>();
            set => SetObject(value);
        }

        public PropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public PropertyData() : base()
        {

        }
    }
}
