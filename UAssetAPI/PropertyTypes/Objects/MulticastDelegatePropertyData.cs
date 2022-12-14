using System;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a list of functions bound to an Object.
    /// </summary>
    public class MulticastDelegatePropertyData : PropertyData<FDelegate[]>
    {
        public MulticastDelegatePropertyData(FName name) : base(name)
        {

        }

        public MulticastDelegatePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MulticastDelegateProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            int numVals = reader.ReadInt32();
            Value = new FDelegate[numVals];
            for (int i = 0; i < numVals; i++)
            {
                Value[i] = new FDelegate(reader.XFER_OBJECT_POINTER(), reader.ReadFName());
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.Length);
            for (int i = 0; i < Value.Length; i++)
            {
                writer.XFERPTR(Value[i].Object);
                writer.Write(Value[i].Delegate);
            }
            return sizeof(int) + sizeof(int) * 3 * Value.Length;
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += "(" + Convert.ToString(Value[i].Object.Index) + ", " + Value[i].Delegate.Value.Value + "), ";
            }
            return oup.Substring(0, oup.Length - 2) + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {

        }

        protected override void HandleCloned(PropertyData res)
        {
            MulticastDelegatePropertyData cloningProperty = (MulticastDelegatePropertyData)res;

            FDelegate[] newData = new FDelegate[Value.Length];
            for (int i = 0; i < Value.Length; i++)
            {
                newData[i] = new FDelegate(Value[i].Object, (FName)Value[i].Delegate.Clone());
            }

            cloningProperty.Value = newData;
        }
    }


    /// <summary>
    /// Describes a list of functions bound to an Object.
    /// </summary>
    public class MulticastSparseDelegatePropertyData : MulticastDelegatePropertyData
    {
        public MulticastSparseDelegatePropertyData(FName name) : base(name)
        {

        }

        public MulticastSparseDelegatePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MulticastSparseDelegateProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }
    }


    /// <summary>
    /// Describes a list of functions bound to an Object.
    /// </summary>
    public class MulticastInlineDelegatePropertyData : MulticastDelegatePropertyData
    {
        public MulticastInlineDelegatePropertyData(FName name) : base(name)
        {

        }

        public MulticastInlineDelegatePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MulticastInlineDelegateProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }
    }
}