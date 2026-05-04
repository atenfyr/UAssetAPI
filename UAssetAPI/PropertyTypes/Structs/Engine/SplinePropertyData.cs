using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs.Engine
{
    public class SplinePropertyData : PropertyData
    {
        /// <summary>
        /// 0 = disabled, 1 = legacy, 2 = new
        /// </summary>
        public byte CurrentImplementation;

        public SplinePropertyData(FName name) : base(name) { }

        public SplinePropertyData() { }

        private static readonly FString CurrentPropertyType = new FString("Spline");
        public override bool HasCustomStructSerialization => true;
        public override FString PropertyType => CurrentPropertyType;

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.ReadEndPropertyTag(reader);
            }

            CurrentImplementation = reader.ReadByte();
            switch (CurrentImplementation)
            {
                case 0:
                    // disabled
                    break;
                case 1:
                    // FLegacySpline
                    // todo implement this, test assets currently all have CurrentImplementation = 0
                    throw new NotImplementedException("FLegacySpline serialization not implemented");
                default: // all values other than 2 are currently interpreted as "new"
                    // FNewSpline
                    // todo implement this, test assets currently all have CurrentImplementation = 0
                    throw new NotImplementedException("FNewSpline serialization not implemented");
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.WriteEndPropertyTag(writer);
            }

            long here = writer.BaseStream.Position;

            writer.Write(CurrentImplementation);
            switch (CurrentImplementation)
            {
                case 0:
                    // disabled
                    break;
                case 1:
                    // FLegacySpline
                    // todo implement this, test assets currently all have CurrentImplementation = 0
                    throw new NotImplementedException("FLegacySpline serialization not implemented");
                default: // all values other than 2 are currently interpreted as "new"
                    // FNewSpline
                    // todo implement this, test assets currently all have CurrentImplementation = 0
                    throw new NotImplementedException("FNewSpline serialization not implemented");
            }

            return (int)(writer.BaseStream.Position - here);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            // TODO
        }

        public override string ToString()
        {
            return "(" + ")";
        }
    }
}
