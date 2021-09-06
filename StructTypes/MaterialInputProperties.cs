using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public abstract class MaterialInputPropertyData<T> : PropertyData<T>
    {
        public int OutputIndex;
        public NamePropertyData InputName;
        public NamePropertyData ExpressionName;

        public MaterialInputPropertyData()
        {

        }

        public MaterialInputPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        protected void ReadExpressionInput(BinaryReader reader, bool includeHeader, long leng)
        {
            OutputIndex = reader.ReadInt32();
            InputName = new NamePropertyData(Name, Asset);
            InputName.Read(reader, false, 0);
            reader.ReadBytes(20); // always 0s
            ExpressionName = new NamePropertyData(Name, Asset);
            ExpressionName.Read(reader, false, 0);
            return;
        }

        protected int WriteExpressionInput(BinaryWriter writer, bool includeHeader)
        {
            writer.Write(OutputIndex);
            int nameSizeA = InputName.Write(writer, false);
            writer.Write(new byte[20]); // always 0s
            int nameSizeB = ExpressionName.Write(writer, false);
            return nameSizeA + nameSizeB + sizeof(int) + 20;
        }
    }

    public class ExpressionInputPropertyData : MaterialInputPropertyData<int>
    {
        public ExpressionInputPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("ExpressionInput");
        }

        public ExpressionInputPropertyData()
        {
            Type = new FName("ExpressionInput");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
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
        public MaterialAttributesInputPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("MaterialAttributesInput");
        }

        public MaterialAttributesInputPropertyData()
        {
            Type = new FName("MaterialAttributesInput");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
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
        public ColorMaterialInputPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("ColorMaterialInput");
        }

        public ColorMaterialInputPropertyData()
        {
            Type = new FName("ColorMaterialInput");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = new ColorPropertyData(Name, Asset);
            Value.Read(reader, false, 0);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
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
        public ScalarMaterialInputPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("ScalarMaterialInput");
        }

        public ScalarMaterialInputPropertyData()
        {
            Type = new FName("ScalarMaterialInput");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = reader.ReadSingle();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
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
        public ShadingModelMaterialInputPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("ShadingModelMaterialInput");
        }

        public ShadingModelMaterialInputPropertyData()
        {
            Type = new FName("ShadingModelMaterialInput");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = reader.ReadUInt32();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
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
        public VectorMaterialInputPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("VectorMaterialInput");
        }

        public VectorMaterialInputPropertyData()
        {
            Type = new FName("VectorMaterialInput");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = new VectorPropertyData(Name, Asset);
            Value.Read(reader, false, 0);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
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
        public Vector2MaterialInputPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("Vector2MaterialInput");
        }

        public Vector2MaterialInputPropertyData()
        {
            Type = new FName("Vector2MaterialInput");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            ReadExpressionInput(reader, false, 0);
            reader.ReadInt32(); // always 0
            Value = new Vector2DPropertyData(Name, Asset);
            Value.Read(reader, false, 0);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
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
