using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes
{
    /// <summary>
    /// A regular export, with no special serialization. Serialized as a None-terminated property list.
    /// </summary>
    public class NormalExport : Export
    {
        public List<PropertyData> Data;

        /// <summary>
        /// Gets or sets the value associated with the specified key. This operation loops linearly, so it may not be suitable for high-performance environments.
        /// </summary>
        /// <param name="key">The key associated with the value to get or set.</param>
        public PropertyData this[FName key]
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
                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].Name == key)
                    {
                        Data[i] = value;
                        Data[i].Name = key;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key. This operation loops linearly, so it may not be suitable for high-performance environments.
        /// </summary>
        /// <param name="key">The key associated with the value to get or set.</param>
        public PropertyData this[string key]
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
            Data = new List<PropertyData>();
            PropertyData bit;
            while ((bit = MainSerializer.Read(reader, reader.Asset.GetParentClassExportName(), true)) != null)
            {
                Data.Add(bit);
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            for (int j = 0; j < Data.Count; j++)
            {
                PropertyData current = Data[j];
                MainSerializer.Write(current, writer, true);
            }
            writer.Write(new FName(writer.Asset, "None"));
        }
    }
}
