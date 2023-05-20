using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// A plane in 3-D space stores the coeffecients as Xx+Yy+Zz=W.
    /// </summary>
    public class PlanePropertyData : PropertyData<FPlane>
    {
        public PlanePropertyData(FName name) : base(name)
        {

        }

        public PlanePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Plane");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
            {
                Value = new FPlane(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
            }
            else
            {
                Value = new FPlane(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
            {
                writer.Write(Value.X);
                writer.Write(Value.Y);
                writer.Write(Value.Z);
                writer.Write(Value.W);
                return sizeof(double) * 4;
            }
            else
            {
                writer.Write(Value.XFloat);
                writer.Write(Value.YFloat);
                writer.Write(Value.ZFloat);
                writer.Write(Value.WFloat);
                return sizeof(float) * 4;
            }
        }

        public override void FromString(string[] d, UAsset asset)
        {
            double.TryParse(d[0], out double X);
            double.TryParse(d[1], out double Y);
            double.TryParse(d[2], out double Z);
            double.TryParse(d[3], out double W);
            Value = new FPlane(X, Y, Z, W);
        }

        public override string ToString()
        {
            return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ", " + Value.W + ")";
        }
    }
}