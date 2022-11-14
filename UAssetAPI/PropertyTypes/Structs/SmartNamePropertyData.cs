using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// Special FName struct used within animations.
    /// </summary>
    public class SmartNamePropertyData : PropertyData
    {
        /// <summary>
        /// The display name of this FSmartName.
        /// </summary>
        public FName DisplayName;

        /// <summary>
        /// SmartName::UID_Type - for faster access
        /// </summary>
        public ushort SmartNameID;

        /// <summary>
        /// Uncertain
        /// </summary>
        public Guid TempGUID;

        public SmartNamePropertyData(FName name) : base(name)
        {

        }

        public SmartNamePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("SmartName");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            DisplayName = reader.ReadFName();
            if (reader.Asset.GetCustomVersion<FAnimPhysObjectVersion>() < FAnimPhysObjectVersion.RemoveUIDFromSmartNameSerialize)
            {
                SmartNameID = reader.ReadUInt16();
            }
            if (reader.Asset.GetCustomVersion<FAnimPhysObjectVersion>() < FAnimPhysObjectVersion.SmartNameRefactorForDeterministicCooking)
            {
                TempGUID = new Guid(reader.ReadBytes(16));
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            long here = writer.BaseStream.Position;

            writer.Write(DisplayName);
            if (writer.Asset.GetCustomVersion<FAnimPhysObjectVersion>() < FAnimPhysObjectVersion.RemoveUIDFromSmartNameSerialize)
            {
                writer.Write(SmartNameID);
            }
            if (writer.Asset.GetCustomVersion<FAnimPhysObjectVersion>() < FAnimPhysObjectVersion.SmartNameRefactorForDeterministicCooking)
            {
                writer.Write(TempGUID.ToByteArray());
            }

            return (int)(writer.BaseStream.Position - here);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            DisplayName = FName.FromString(asset, d[0]);
            if (ushort.TryParse(d[1], out ushort rawSmartNameID)) SmartNameID = rawSmartNameID;
            TempGUID = d[2].ConvertToGUID();
        }

        public override string ToString()
        {
            return "(" + ")";
        }
    }
}
