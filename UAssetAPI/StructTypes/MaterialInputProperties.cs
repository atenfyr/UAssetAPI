using Newtonsoft.Json;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public abstract class MaterialInputPropertyData<T> : PropertyData<T>
    {
        [JsonProperty]
        public byte[] Extras;
        [JsonProperty]
        public int OutputIndex;
        [JsonProperty]
        public FName InputName;
        [JsonProperty]
        public FName ExpressionName;

        public MaterialInputPropertyData()
        {

        }

        public MaterialInputPropertyData(FName name) : base(name)
        {

        }

        protected void ReadExpressionInput(AssetBinaryReader reader, bool includeHeader, long leng)
        {
            OutputIndex = reader.ReadInt32();
            InputName = reader.ReadFName();
            Extras = reader.ReadBytes(20); // always 0s
            ExpressionName = reader.ReadFName();
            return;
        }

        protected int WriteExpressionInput(AssetBinaryWriter writer, bool includeHeader)
        {
            writer.Write(OutputIndex);
            writer.Write(InputName);
            writer.Write(Extras); // always 0s
            writer.Write(ExpressionName);
            return (sizeof(int) * 2) * 2 + sizeof(int) + 20;
        }

        protected override void HandleCloned(PropertyData res)
        {
            MaterialInputPropertyData<T> cloningProperty = (MaterialInputPropertyData<T>)res;
            cloningProperty.InputName = (FName)this.InputName.Clone();
            cloningProperty.InputName = (FName)this.ExpressionName.Clone();
            cloningProperty.Extras = (byte[])this.Extras.Clone();
        }
    }

    public class ExpressionInputPropertyData : MaterialInputPropertyData<int>
    {
        public ExpressionInputPropertyData(FName name) : base(name)
        {

        }

        public ExpressionInputPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("ExpressionInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            return WriteExpressionInput(writer, false);
        }
    }

    public class MaterialAttributesInputPropertyData : MaterialInputPropertyData<int>
    {
        public MaterialAttributesInputPropertyData(FName name) : base(name)
        {

        }

        public MaterialAttributesInputPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("MaterialAttributesInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            return WriteExpressionInput(writer, false);
        }
    }

    public class ColorMaterialInputPropertyData : MaterialInputPropertyData<ColorPropertyData>
    {
        public ColorMaterialInputPropertyData(FName name) : base(name)
        {

        }

        public ColorMaterialInputPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("ColorMaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = new ColorPropertyData(Name);
            Value.Read(reader, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int expLength = WriteExpressionInput(writer, false);
            writer.Write((int)0);
            return expLength + Value.Write(writer, false) + sizeof(int);
        }
    }

    public class ScalarMaterialInputPropertyData : MaterialInputPropertyData<float>
    {
        public ScalarMaterialInputPropertyData(FName name) : base(name)
        {

        }

        public ScalarMaterialInputPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("ScalarMaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = reader.ReadSingle();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int expLength = WriteExpressionInput(writer, false);
            writer.Write((int)0);
            writer.Write(Value);
            return expLength + sizeof(float) + sizeof(int);
        }
    }

    public class ShadingModelMaterialInputPropertyData : MaterialInputPropertyData<uint>
    {
        public ShadingModelMaterialInputPropertyData(FName name) : base(name)
        {

        }

        public ShadingModelMaterialInputPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("ShadingModelMaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = reader.ReadUInt32();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int expLength = WriteExpressionInput(writer, false);
            writer.Write((int)0);
            writer.Write(Value);
            return expLength + sizeof(uint) + sizeof(int);
        }
    }

    public class VectorMaterialInputPropertyData : MaterialInputPropertyData<VectorPropertyData>
    {
        public VectorMaterialInputPropertyData(FName name) : base(name)
        {

        }

        public VectorMaterialInputPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("VectorMaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = new VectorPropertyData(Name);
            Value.Read(reader, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int expLength = WriteExpressionInput(writer, false);
            writer.Write((int)0);
            return expLength + Value.Write(writer, false) + sizeof(int);
        }
    }

    public class Vector2MaterialInputPropertyData : MaterialInputPropertyData<Vector2DPropertyData>
    {
        public Vector2MaterialInputPropertyData(FName name) : base(name)
        {

        }

        public Vector2MaterialInputPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Vector2MaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = new Vector2DPropertyData(Name);
            Value.Read(reader, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int expLength = WriteExpressionInput(writer, false);
            writer.Write((int)0);
            return expLength + Value.Write(writer, false) + sizeof(int);
        }
    }
}
