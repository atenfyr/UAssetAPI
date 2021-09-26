using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes a function bound to an Object.
    /// </summary>
    public class FMulticastDelegate
    {
        /** Uncertain what this is for; if you find out, please let me know */
        public int Number;
        /** Uncertain what this is for; if you find out, please let me know */
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
        public MulticastDelegatePropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public MulticastDelegatePropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("MulticastDelegateProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            int numVals = reader.ReadInt32();
            Value = new FMulticastDelegate[numVals];
            for (int i = 0; i < numVals; i++)
            {
                Value[i] = new FMulticastDelegate(reader.ReadInt32(), reader.ReadFName(Asset));
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value.Length);
            for (int i = 0; i < Value.Length; i++)
            {
                writer.Write(Value[i].Number);
                writer.WriteFName(Value[i].Delegate, Asset);
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

        public override void FromString(string[] d)
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