using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a function bound to an Object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class FMulticastDelegate
    {
        /** Uncertain what this is for; if you find out, please let me know */
        [JsonProperty]
        public int Number;
        /** Uncertain what this is for; if you find out, please let me know */
        [JsonProperty]
        public FName Delegate;

        public FMulticastDelegate(int number, FName @delegate)
        {
            Number = number;
            Delegate = @delegate;
        }

        public FMulticastDelegate()
        {

        }
    }

    /// <summary>
    /// Describes a list of functions bound to an Object.
    /// </summary>
    public class MulticastDelegatePropertyData : PropertyData<FMulticastDelegate[]>
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
            Value = new FMulticastDelegate[numVals];
            for (int i = 0; i < numVals; i++)
            {
                Value[i] = new FMulticastDelegate(reader.ReadInt32(), reader.ReadFName());
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
                writer.Write(Value[i].Number);
                writer.Write(Value[i].Delegate);
            }
            return sizeof(int) + sizeof(int) * 3 * Value.Length;
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += "(" + Convert.ToString(Value[i].Number) + ", " + Value[i].Delegate.Value.Value + "), ";
            }
            return oup.Substring(0, oup.Length - 2) + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            
        }

        protected override void HandleCloned(PropertyData res)
        {
            MulticastDelegatePropertyData cloningProperty = (MulticastDelegatePropertyData)res;

            FMulticastDelegate[] newData = new FMulticastDelegate[this.Value.Length];
            for (int i = 0; i < this.Value.Length; i++)
            {
                newData[i] = new FMulticastDelegate(this.Value[i].Number, (FName)this.Value[i].Delegate.Clone());
            }

            cloningProperty.Value = newData;
        }
    }
}