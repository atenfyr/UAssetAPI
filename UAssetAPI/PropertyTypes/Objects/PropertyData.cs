using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    public class AncestryInfo : ICloneable
    {
        public List<FName> Ancestors = new List<FName>(5);
        public FName Parent
        {
            get
            {
                return Ancestors[Ancestors.Count - 1];
            }
            set
            {
                Ancestors[Ancestors.Count - 1] = value;
            }
        }

        public object Clone() // shallow
        {
            var res = new AncestryInfo();
            res.Ancestors.AddRange(Ancestors);
            return res;
        }

        public AncestryInfo CloneWithoutParent()
        {
            AncestryInfo res = (AncestryInfo)this.Clone();
            res.Ancestors.RemoveAt(res.Ancestors.Count - 1);
            return res;
        }

        public void Initialize(AncestryInfo ancestors, FName dad)
        {
            Ancestors.Clear();
            if (ancestors != null) Ancestors.AddRange(ancestors.Ancestors);
            SetAsParent(dad);
        }

        public void SetAsParent(FName dad)
        {
            if (dad != null) Ancestors.Add(dad);
        }
    }

    /// <summary>
    /// Generic Unreal property class.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class PropertyData : ICloneable
    {
        /// <summary>
        /// The name of this property.
        /// </summary>
        [JsonProperty]
        public FName Name = null;

        /// <summary>
        /// The ancestry of this property. Contains information about all the classes/structs that this property is contained within. Not serialized.
        /// </summary>
        [JsonIgnore]
        public AncestryInfo Ancestry = new AncestryInfo();

        /// <summary>
        /// The duplication index of this property. Used to distinguish properties with the same name in the same struct.
        /// </summary>
        [JsonProperty]
        public int DuplicationIndex = 0;

        /// <summary>
        /// An optional property GUID. Nearly always null.
        /// </summary>
        public Guid? PropertyGuid = null;

        /// <summary>
        /// The offset of this property on disk. This is for the user only, and has no bearing in the API itself.
        /// </summary>
        public long Offset = -1;

        /// <summary>
        /// An optional tag which can be set on any property in memory. This is for the user only, and has no bearing in the API itself.
        /// </summary>
        public object Tag;

        protected object _rawValue;
        public virtual object RawValue
        {
            get
            {
                if (_rawValue == null && DefaultValue != null) _rawValue = DefaultValue;
                return _rawValue;
            }
            set
            {
                _rawValue = value;
            }
        }

        public void SetObject(object value)
        {
            RawValue = value;
        }

        public T GetObject<T>()
        {
            if (RawValue is null) return default;
            return (T)RawValue;
        }

        public PropertyData(FName name)
        {
            Name = name;
        }

        public PropertyData()
        {

        }

        private static FString FallbackPropertyType = new FString(string.Empty);
        /// <summary>
        /// Determines whether or not this particular property should be registered in the property registry and automatically used when parsing assets.
        /// </summary>
        public virtual bool ShouldBeRegistered { get { return true; } }
        /// <summary>
        /// Determines whether or not this particular property has custom serialization within a StructProperty.
        /// </summary>
        public virtual bool HasCustomStructSerialization { get { return false; } }
        /// <summary>
        /// The type of this property as an FString.
        /// </summary>
        public virtual FString PropertyType { get { return FallbackPropertyType; } }
        /// <summary>
        /// The default value of this property, used as a fallback when no value is defined. Null by default.
        /// </summary>
        public virtual object DefaultValue { get { return null; } }

        /// <summary>
        /// Reads out a property from a BinaryReader.
        /// </summary>
        /// <param name="reader">The BinaryReader to read from.</param>
        /// <param name="includeHeader">Whether or not to also read the "header" of the property, which is data considered by the Unreal Engine to be data that is part of the PropertyData base class rather than any particular child class.</param>
        /// <param name="leng1">An estimate for the length of the data being read out.</param>
        /// <param name="leng2">A second estimate for the length of the data being read out.</param>
        public virtual void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {

        }

        /// <summary>
        /// Resolves the ancestry of all child properties of this property.
        /// </summary>
        public virtual void ResolveAncestries(UnrealPackage asset, AncestryInfo ancestrySoFar)
        {
            Ancestry = ancestrySoFar;
        }

        /// <summary>
        /// Writes a property to a BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write from.</param>
        /// <param name="includeHeader">Whether or not to also write the "header" of the property, which is data considered by the Unreal Engine to be data that is part of the PropertyData base class rather than any particular child class.</param>
        /// <returns>The length in bytes of the data that was written.</returns>
        public virtual int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            return 0;
        }

        /// <summary>
        /// Does the body of this property entirely consist of null bytes? If so, the body will not be serialized in unversioned properties.
        /// </summary>
        /// <param name="asset">The asset to test serialization within.</param>
        /// <returns>Whether or not the property is zero.</returns>
        public virtual bool IsZero(UnrealPackage asset)
        {
            MemoryStream testStrm = new MemoryStream(32); this.Write(new AssetBinaryWriter(testStrm, asset), false); byte[] testByteArray = testStrm.ToArray();
            foreach (byte entry in testByteArray)
            {
                if (entry != 0) return false;
            }
            return true;
        }

        /// <summary>
        /// Sets certain fields of the property based off of an array of strings.
        /// </summary>
        /// <param name="d">An array of strings to derive certain fields from.</param>
        /// <param name="asset">The asset that the property belongs to.</param>
        public virtual void FromString(string[] d, UAsset asset)
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
        /// The "main value" of this property, if such a concept is applicable to the property in question. Properties may contain other values as well, in which case they will be present as other fields in the child class.
        /// </summary>
        [JsonProperty]
        public T Value
        {
            get => GetObject<T>();
            set => SetObject(value);
        }

        public PropertyData(FName name) : base(name)
        {

        }

        public PropertyData() : base()
        {

        }
    }
}
