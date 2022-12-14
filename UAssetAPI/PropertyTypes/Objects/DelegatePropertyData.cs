using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a function bound to an Object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FDelegate
    {
        /// <summary>
        /// References the main actor export
        /// </summary>
        [JsonProperty]
        public FPackageIndex Object;
        /// <summary>
        /// The name of the delegate
        /// </summary>
        [JsonProperty]
        public FName Delegate;

        public FDelegate(FPackageIndex _object, FName @delegate)
        {
            Object = _object;
            Delegate = @delegate;
        }

        public FDelegate()
        {

        }
    }

    /// <summary>
    /// Describes a function bound to an Object.
    /// </summary>
    public class DelegatePropertyData : PropertyData<FDelegate>
    {
        public DelegatePropertyData(FName name) : base(name)
        {

        }

        public DelegatePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("DelegateProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FDelegate(reader.XFER_OBJECT_POINTER(), reader.ReadFName());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.XFERPTR(Value.Object);
            writer.Write(Value.Delegate);

            return sizeof(int) * 3;
        }

        public override string ToString()
        {
            return null;
        }

        public override void FromString(string[] d, UAsset asset)
        {

        }

        protected override void HandleCloned(PropertyData res)
        {
            DelegatePropertyData cloningProperty = (DelegatePropertyData)res;

            cloningProperty.Value = new FDelegate(this.Value.Object, this.Value.Delegate);
        }
    }
}