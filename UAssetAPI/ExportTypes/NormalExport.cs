using System;
using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.ExportTypes
{
    /// <summary>
    /// Enum flags that indicate that additional data may be serialized prior to actual tagged property serialization
    /// Those extensions are used to store additional function to control how TPS will resolved. e.g. use overridable serialization
    /// </summary>
    public enum EClassSerializationControlExtension
    {
        NoExtension = 0x00,
        ReserveForFutureUse = 0x01, // Can be use to add a next group of extension

        ////////////////////////////////////////////////
        // First extension group
        OverridableSerializationInformation = 0x02,

        //
        // Add more extension for the first group here
        //
    }

    /// <summary>
    /// A regular export representing a UObject, with no special serialization.
    /// </summary>
    public class NormalExport : Export
    {
        public List<PropertyData> Data;
        public Guid? ObjectGuid;
        public EClassSerializationControlExtension SerializationControl;
        public EOverriddenPropertyOperation Operation;

        /// <summary>
        /// Gets or sets the value associated with the specified key. This operation loops linearly, so it may not be suitable for high-performance environments.
        /// </summary>
        /// <param name="key">The key associated with the value to get or set.</param>
        public virtual PropertyData this[FName key]
        {
            get
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].Name == key) return Data[i];
                }
                return null;
            }
            set
            {
                value.Name = key;

                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].Name == key)
                    {
                        Data[i] = value;
                        return;
                    }
                }

                Data.Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key. This operation loops linearly, so it may not be suitable for high-performance environments.
        /// </summary>
        /// <param name="key">The key associated with the value to get or set.</param>
        public virtual PropertyData this[string key]
        {
            get
            {
                return this[FName.FromString(Asset, key)];
            }
            set
            {
                this[FName.FromString(Asset, key)] = value;
            }
        }


        /// <summary>
        /// Gets or sets the value at the specified index.
        /// </summary>
        /// <param name="index">The index of the value to get or set.</param>
        public PropertyData this[int index]
        {
            get
            {
                return Data[index];
            }
            set
            {
                Data[index] = value;
            }
        }

        public NormalExport(Export super)
        {
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public NormalExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public NormalExport(List<PropertyData> data, UAsset asset, byte[] extras) : base(asset, extras)
        {
            Data = data;
        }

        public NormalExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting = 0)
        {
            // 5.4-specific problem; unclear why this occurs
            if (reader.Asset.ObjectVersionUE5 > ObjectVersionUE5.DATA_RESOURCES && reader.Asset.ObjectVersionUE5 < ObjectVersionUE5.ASSETREGISTRY_PACKAGEBUILDDEPENDENCIES && !ObjectFlags.HasFlag(EObjectFlags.RF_ClassDefaultObject))
            {
                int dummy = reader.ReadInt32();
                if (dummy != 0) throw new FormatException("Expected 4 null bytes at start of NormalExport; got " + dummy);
            }

            Data = new List<PropertyData>();
            PropertyData bit;

            var unversionedHeader = new FUnversionedHeader(reader);
            if (!reader.Asset.HasUnversionedProperties && reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.PROPERTY_TAG_EXTENSION_AND_OVERRIDABLE_SERIALIZATION)
            {
                SerializationControl = (EClassSerializationControlExtension)reader.ReadByte();

                if (SerializationControl.HasFlag(EClassSerializationControlExtension.OverridableSerializationInformation))
                {
                    Operation = (EOverriddenPropertyOperation)reader.ReadByte();
                }
            }
            FName parentName = GetClassTypeForAncestry(reader.Asset, out FName parentModulePath);
            while ((bit = MainSerializer.Read(reader, null, parentName, parentModulePath, unversionedHeader, true)) != null)
            {
                Data.Add(bit);
            }

            ObjectGuid = null;
            if (!this.ObjectFlags.HasFlag(EObjectFlags.RF_ClassDefaultObject) && reader.ReadBooleanInt()) ObjectGuid = new Guid(reader.ReadBytes(16));
        }

        public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
        {
            var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
            ancestryNew.SetAsParent(GetClassTypeForAncestry(asset, out FName modulePath), modulePath);

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++) Data[i]?.ResolveAncestries(asset, ancestryNew);
            }
            base.ResolveAncestries(asset, ancestrySoFar);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            // 5.4-specific problem; unclear why this occurs
            if (writer.Asset.ObjectVersionUE5 > ObjectVersionUE5.DATA_RESOURCES && writer.Asset.ObjectVersionUE5 < ObjectVersionUE5.ASSETREGISTRY_PACKAGEBUILDDEPENDENCIES && !ObjectFlags.HasFlag(EObjectFlags.RF_ClassDefaultObject))
            {
                writer.Write((int)0); // "false" bool?
            }

            FName parentName = GetClassTypeForAncestry(writer.Asset, out FName parentModulePath);

            MainSerializer.GenerateUnversionedHeader(ref Data, parentName, parentModulePath, writer.Asset)?.Write(writer);

            if (!writer.Asset.HasUnversionedProperties && writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.PROPERTY_TAG_EXTENSION_AND_OVERRIDABLE_SERIALIZATION)
            {
                writer.Write((byte)SerializationControl);

                if (SerializationControl.HasFlag(EClassSerializationControlExtension.OverridableSerializationInformation))
                {
                    writer.Write((byte)Operation);
                }
            }

            for (int j = 0; j < Data.Count; j++)
            {
                PropertyData current = Data[j];
                MainSerializer.Write(current, writer, true);
            }
            if (!writer.Asset.HasUnversionedProperties) writer.Write(new FName(writer.Asset, "None"));

            if (this.ObjectFlags.HasFlag(EObjectFlags.RF_ClassDefaultObject)) return;
            if (ObjectGuid == null)
            {
                writer.Write((int)0);
            }
            else
            {
                writer.Write((int)1);
                writer.Write(((Guid)ObjectGuid).ToByteArray());
            }
        }
    }
}
