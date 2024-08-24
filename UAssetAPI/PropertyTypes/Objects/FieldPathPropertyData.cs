using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Near synonym for <see cref="SoftObjectPropertyData"/>.
    /// </summary>
    public class FieldPathPropertyData : PropertyData<FFieldPath>
    {
        public FieldPathPropertyData(FName name) : base(name)
        {

        }

        public FieldPathPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("FieldPathProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }
        public override object DefaultValue { get { return new FFieldPath(); } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.ReadEndPropertyTag(reader);
            }

            Value = new FFieldPath(reader);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.WriteEndPropertyTag(writer);
            }

            if (Value == null) Value = new FFieldPath();
            return Value.Write(writer);
        }

        public override string ToString()
        {
            // Expected format is: FullPackageName.Subobject[:Subobject:...]:FieldName
            return "";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            // Expected format is: FullPackageName.Subobject[:Subobject:...]:FieldName
            Value = new FFieldPath();
        }
    }
}