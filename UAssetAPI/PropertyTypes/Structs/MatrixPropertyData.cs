using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
    /// </summary>
    public class MatrixPropertyData : PropertyData<FMatrix>
    {
        public MatrixPropertyData(FName name) : base(name)
        {

        }

        public MatrixPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Matrix");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMatrix(reader);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            return Value.Write(writer);
        }

        //public override void FromString(string[] d, UAsset asset)
        //{
        //    double.TryParse(d[0], out double X);
        //    double.TryParse(d[1], out double Y);
        //    double.TryParse(d[2], out double Z);
        //    Value = new FVector(X, Y, Z);
        //}

        //public override string ToString()
        //{
        //    return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ")";
        //}
    }
}