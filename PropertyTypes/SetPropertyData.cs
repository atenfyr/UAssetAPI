using System;
using System.IO;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    public class SetPropertyData : ArrayPropertyData
    {
        public PropertyData[] RemovedItems;
        public StructPropertyData RemovedItemsDummyStruct;

        public SetPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "SetProperty";
            Value = new PropertyData[0];
            RemovedItems = new PropertyData[0];
        }

        public SetPropertyData()
        {
            Type = "SetProperty";
            Value = new PropertyData[0];
            RemovedItems = new PropertyData[0];
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                ArrayType = Asset.GetHeaderReference((int)reader.ReadInt64());
                reader.ReadByte(); // null byte
            }

            var removedItemsDummy = new ArrayPropertyData("RemovedItems", Asset);
            removedItemsDummy.ArrayType = ArrayType;
            removedItemsDummy.Read(reader, false, leng1, leng2);
            RemovedItems = removedItemsDummy.Value;
            RemovedItemsDummyStruct = removedItemsDummy.DummyStruct;
            base.Read(reader, false, leng1, leng2);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (Value.Length > 0) ArrayType = Value[0].Type;

            if (includeHeader)
            {
                writer.Write((long)Asset.SearchHeaderReference(ArrayType));
                writer.Write((byte)0);
            }

            var removedItemsDummy = new ArrayPropertyData("RemovedItems", Asset);
            removedItemsDummy.ArrayType = ArrayType;
            removedItemsDummy.DummyStruct = RemovedItemsDummyStruct;
            removedItemsDummy.Value = RemovedItems;

            int leng1 = removedItemsDummy.Write(writer, false);
            return leng1 + base.Write(writer, false);
        }
    }
}