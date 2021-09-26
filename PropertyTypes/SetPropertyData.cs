using System.IO;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    public class SetPropertyData : ArrayPropertyData
    {
        public PropertyData[] RemovedItems;
        public StructPropertyData RemovedItemsDummyStruct;

        public SetPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Value = new PropertyData[0];
            RemovedItems = new PropertyData[0];
        }

        public SetPropertyData()
        {
            Value = new PropertyData[0];
            RemovedItems = new PropertyData[0];
        }

        private static readonly FName CurrentPropertyType = new FName("SetProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                ArrayType = reader.ReadFName(Asset);
                reader.ReadByte(); // null byte
            }

            var removedItemsDummy = new ArrayPropertyData(new FName("RemovedItems"), Asset);
            removedItemsDummy.ArrayType = ArrayType;
            removedItemsDummy.Read(reader, false, leng1, leng2);
            RemovedItems = removedItemsDummy.Value;
            RemovedItemsDummyStruct = removedItemsDummy.DummyStruct;
            base.Read(reader, false, leng1, leng2);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (Value.Length > 0) ArrayType = Value[0].PropertyType;

            if (includeHeader)
            {
                writer.WriteFName(ArrayType, Asset);
                writer.Write((byte)0);
            }

            var removedItemsDummy = new ArrayPropertyData(new FName("RemovedItems"), Asset);
            removedItemsDummy.ArrayType = ArrayType;
            removedItemsDummy.DummyStruct = RemovedItemsDummyStruct;
            removedItemsDummy.Value = RemovedItems;

            int leng1 = removedItemsDummy.Write(writer, false);
            return leng1 + base.Write(writer, false);
        }

        protected override void HandleCloned(PropertyData res)
        {
            base.HandleCloned(res);
            SetPropertyData cloningProperty = (SetPropertyData)res;

            PropertyData[] newData = new PropertyData[this.RemovedItems.Length];
            for (int i = 0; i < this.Value.Length; i++)
            {
                newData[i] = (PropertyData)this.Value[i].Clone();
            }
            cloningProperty.RemovedItems = newData;

            cloningProperty.RemovedItemsDummyStruct = (StructPropertyData)this.RemovedItemsDummyStruct.Clone();
        }
    }
}