using Newtonsoft.Json;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public abstract class MaterialInputPropertyData<T> : PropertyData<T>
    {
        [JsonProperty]
        public int OutputIndex;
        [JsonProperty]
        public FString InputName;
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
            if (reader.Asset.GetCustomVersion<FCoreObjectVersion>() >= FCoreObjectVersion.MaterialInputNativeSerialize)
            {
                OutputIndex = reader.ReadInt32();
                if (reader.Asset.GetCustomVersion<FFrameworkObjectVersion>() >= FFrameworkObjectVersion.PinsStoreFName)
                {
                    InputName = reader.ReadFName().Value;
                }
                else
                {
                    InputName = reader.ReadFString();
                }
                reader.ReadBytes(20); // editor only data placeholder
                ExpressionName = reader.ReadFName();
            }
        }

        protected int WriteExpressionInput(AssetBinaryWriter writer, bool includeHeader)
        {
            int totalSize = 0;
            if (writer.Asset.GetCustomVersion<FCoreObjectVersion>() >= FCoreObjectVersion.MaterialInputNativeSerialize)
            {
                writer.Write(OutputIndex); totalSize += sizeof(int);
                if (writer.Asset.GetCustomVersion<FFrameworkObjectVersion>() >= FFrameworkObjectVersion.PinsStoreFName)
                {
                    writer.Write(new FName(writer.Asset, InputName)); totalSize += sizeof(int) * 2;
                }
                else
                {
                    totalSize += writer.Write(InputName);
                }
                writer.Write(new byte[20]); totalSize += 20;
                writer.Write(ExpressionName); totalSize += sizeof(int) * 2;
            }
            return totalSize;
        }

        protected override void HandleCloned(PropertyData res)
        {
            MaterialInputPropertyData<T> cloningProperty = (MaterialInputPropertyData<T>)res;
            cloningProperty.InputName = (FString)this.InputName.Clone();
            cloningProperty.ExpressionName = (FName)this.ExpressionName.Clone();
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

        private static readonly FString CurrentPropertyType = new FString("ExpressionInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            ReadExpressionInput(reader, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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

        private static readonly FString CurrentPropertyType = new FString("MaterialAttributesInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            ReadExpressionInput(reader, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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

        private static readonly FString CurrentPropertyType = new FString("ColorMaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // bUseConstantValue; always false
            Value = new ColorPropertyData(Name);
            Value.Read(reader, parentName, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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

        private static readonly FString CurrentPropertyType = new FString("ScalarMaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // bUseConstantValue; always false
            Value = reader.ReadSingle();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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

        private static readonly FString CurrentPropertyType = new FString("ShadingModelMaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // bUseConstantValue; always false
            Value = reader.ReadUInt32();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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

        private static readonly FString CurrentPropertyType = new FString("VectorMaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // bUseConstantValue; always false
            Value = new VectorPropertyData(Name);
            Value.Read(reader, parentName, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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

        private static readonly FString CurrentPropertyType = new FString("Vector2MaterialInput");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // bUseConstantValue; always false
            Value = new Vector2DPropertyData(Name);
            Value.Read(reader, parentName, false, 0);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int expLength = WriteExpressionInput(writer, false);
            writer.Write((int)0);
            return expLength + Value.Write(writer, false) + sizeof(int);
        }
    }
}
