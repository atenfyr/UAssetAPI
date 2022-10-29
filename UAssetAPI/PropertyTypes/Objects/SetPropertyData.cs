using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a set.
    /// </summary>
    public class SetPropertyData : ArrayPropertyData
    {
        public PropertyData[] ElementsToRemove = null;

        public SetPropertyData(FName name) : base(name)
        {
            Value = new PropertyData[0];
            ElementsToRemove = new PropertyData[0];
        }

        public SetPropertyData()
        {
            Value = new PropertyData[0];
            ElementsToRemove = new PropertyData[0];
        }

        private static readonly FString CurrentPropertyType = new FString("SetProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            this.ShouldSerializeStructsDifferently = false;

            if (includeHeader)
            {
                ArrayType = reader.ReadFName();
                PropertyGuid = reader.ReadPropertyGuid();
            }

            var removedItemsDummy = new ArrayPropertyData(FName.DefineDummy(reader.Asset, "ElementsToRemove"));
            removedItemsDummy.ShouldSerializeStructsDifferently = false;
            removedItemsDummy.ArrayType = ArrayType;
            removedItemsDummy.Read(reader, false, leng1, leng2);
            ElementsToRemove = removedItemsDummy.Value;

            base.Read(reader, false, leng1, leng2);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            this.ShouldSerializeStructsDifferently = false;

            if (Value.Length > 0) ArrayType = new FName(writer.Asset, Value[0].PropertyType);

            if (includeHeader)
            {
                writer.Write(ArrayType);
                writer.WritePropertyGuid(PropertyGuid);
            }

            var removedItemsDummy = new ArrayPropertyData(FName.DefineDummy(writer.Asset, "ElementsToRemove"));
            removedItemsDummy.ShouldSerializeStructsDifferently = false;
            removedItemsDummy.ArrayType = ArrayType;
            removedItemsDummy.Value = ElementsToRemove;

            int leng1 = removedItemsDummy.Write(writer, false);
            return leng1 + base.Write(writer, false);
        }

        protected override void HandleCloned(PropertyData res)
        {
            base.HandleCloned(res);
            SetPropertyData cloningProperty = (SetPropertyData)res;

            PropertyData[] newData = new PropertyData[this.ElementsToRemove.Length];
            for (int i = 0; i < this.Value.Length; i++)
            {
                newData[i] = (PropertyData)this.Value[i].Clone();
            }
            cloningProperty.ElementsToRemove = newData;
        }
    }
}