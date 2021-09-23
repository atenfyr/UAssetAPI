using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class StructPropertyData : PropertyData<IList<PropertyData>> // List
    {
        public FName StructType = null;
        public Guid StructGUID = Guid.Empty; // usually set to 0

        public StructPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Value = new List<PropertyData>();
        }

        public StructPropertyData(FName name, UAsset asset, FName forcedType) : base(name, asset)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public StructPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("StructProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        private void ReadOnce<T>(BinaryReader reader) where T: PropertyData, new()
        {
            T data = (T)Activator.CreateInstance(typeof(T), Name, Asset);
            data.Read(reader, false, 0);
            Value = new List<PropertyData> { data };
        }

        private void ReadNormal(BinaryReader reader)
        {
            IList<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            while ((data = MainSerializer.Read(Asset, reader, true)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader) // originally !isForced
            {
                StructType = reader.ReadFName(Asset);
                StructGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();
            }
            switch (StructType.Value.Value)
            {
                case "Guid": // 16 byte GUID
                    ReadOnce<GuidPropertyData>(reader);
                    break;
                case "LinearColor": // 4 floats
                    ReadOnce<LinearColorPropertyData>(reader);
                    break;
                case "Color":
                    ReadOnce<ColorPropertyData>(reader);
                    break;
                case "Vector": // 3 floats
                    ReadOnce<VectorPropertyData>(reader);
                    break;
                case "Vector2D": // 2 floats
                    ReadOnce<Vector2DPropertyData>(reader);
                    break;
                case "Vector4": // 4 floats
                    ReadOnce<Vector4PropertyData>(reader);
                    break;
                case "Box": // 2 Vectors
                    ReadOnce<BoxPropertyData>(reader);
                    break;
                case "IntPoint": // 2 ints
                    ReadOnce<IntPointPropertyData>(reader);
                    break;
                case "DateTime": // 1 long
                    ReadOnce<DateTimePropertyData>(reader);
                    break;
                case "Timespan": // 1 long
                    ReadOnce<TimespanPropertyData>(reader);
                    break;
                case "Rotator": // 3 floats
                    ReadOnce<RotatorPropertyData>(reader);
                    break;
                case "Quat": // 4 floats
                    ReadOnce<QuatPropertyData>(reader);
                    break;
                case "SoftObjectPath":
                    ReadOnce<SoftObjectPathPropertyData>(reader);
                    break;
                case "SoftAssetPath":
                    ReadOnce<SoftAssetPathPropertyData>(reader);
                    break;
                case "SoftClassPath":
                    ReadOnce<SoftClassPathPropertyData>(reader);
                    break;
                case "RichCurveKey":
                    ReadOnce<RichCurveKeyProperty>(reader);
                    break;
                case "ViewTargetBlendParams":
                    ReadOnce<ViewTargetBlendParamsPropertyData>(reader);
                    break;
                case "ColorMaterialInput":
                    ReadOnce<ColorMaterialInputPropertyData>(reader);
                    break;
                case "ExpressionInput":
                    ReadOnce<ExpressionInputPropertyData>(reader);
                    break;
                case "MaterialAttributesInput":
                    ReadOnce<MaterialAttributesInputPropertyData>(reader);
                    break;
                case "ScalarMaterialInput":
                    ReadOnce<ScalarMaterialInputPropertyData>(reader);
                    break;
                case "ShadingModelMaterialInput":
                    ReadOnce<ShadingModelMaterialInputPropertyData>(reader);
                    break;
                case "VectorMaterialInput":
                    ReadOnce<VectorMaterialInputPropertyData>(reader);
                    break;
                case "Vector2MaterialInput":
                    ReadOnce<Vector2MaterialInputPropertyData>(reader);
                    break;
                case "GameplayTagContainer":
                    ReadOnce<GameplayTagContainerPropertyData>(reader);
                    break;
                case "PerPlatformInt":
                    ReadOnce<PerPlatformIntPropertyData>(reader);
                    break;
                case "PerPlatformFloat":
                    ReadOnce<PerPlatformFloatPropertyData>(reader);
                    break;
                case "PerPlatformBool":
                    ReadOnce<PerPlatformBoolPropertyData>(reader);
                    break;
                default:
                    ReadNormal(reader);
                    break;
            }
        }

        private int WriteOnce(BinaryWriter writer)
        {
            if (Value.Count != 1) throw new InvalidOperationException("Structs with type " + StructType.Value.Value + " must have exactly one entry");
            return Value[0].Write(writer, false);
        }

        private int WriteNormal(BinaryWriter writer)
        {
            int here = (int)writer.BaseStream.Position;
            if (Value != null)
            {
                foreach (var t in Value)
                {
                    MainSerializer.Write(t, Asset, writer, true);
                }
            }
            writer.WriteFName(new FName("None"), Asset);
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WriteFName(StructType, Asset);
                writer.Write(StructGUID.ToByteArray());
                writer.Write((byte)0);
            }
            /*switch(StructType)
            {
                case "Guid":
                case "LinearColor":
                case "Quat":
                case "Vector4":
                    WriteOnce(writer);
                    return 16;
                case "Vector":
                case "Rotator":
                case "SoftObjectPath":
                    WriteOnce(writer);
                    return 12;
                case "Vector2D":
                case "IntPoint":
                case "DateTime":
                case "Timespan":
                    WriteOnce(writer);
                    return 8;
                case "Color":
                    WriteOnce(writer);
                    return 4;
                case "Box":
                    WriteOnce(writer);
                    return 25;
                case "RichCurveKey":
                    WriteOnce(writer);
                    return 27;
                case "ViewTargetBlendParams":
                    WriteOnce(writer);
                    return 13;
                default:
                    return WriteNormal(writer);
            }*/
            switch (StructType.Value.Value)
            {
                case "Guid":
                case "LinearColor":
                case "Quat":
                case "Vector4":
                case "Vector":
                case "Rotator":
                case "SoftObjectPath":
                case "SoftAssetPath":
                case "SoftClassPath":
                case "Vector2D":
                case "IntPoint":
                case "DateTime":
                case "Timespan":
                case "Color":
                case "Box":
                case "RichCurveKey":
                case "ViewTargetBlendParams":
                case "ExpressionInput":
                case "MaterialAttributesInput":
                case "ColorMaterialInput":
                case "ScalarMaterialInput":
                case "ShadingModelMaterialInput":
                case "VectorMaterialInput":
                case "Vector2MaterialInput":
                case "GameplayTagContainer":
                case "PerPlatformInt":
                case "PerPlatformFloat":
                case "PerPlatformBool":
                    return WriteOnce(writer);
                default:
                    return WriteNormal(writer);
            }
        }

        public override void FromString(string[] d)
        {
            if (d[4] != null) StructType = FName.FromString(d[4]);
        }
    }
}