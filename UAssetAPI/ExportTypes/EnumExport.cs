using System;
using System.Collections.Generic;
using UAssetAPI.CustomVersions;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes
{
    /// <summary>How this enum is declared in C++. Affects the internal naming of enum values.</summary>
    public enum ECppForm
    {
        Regular,
		Namespaced,
		EnumClass
    }

    /// <summary>
    /// Reflection data for an enumeration.
    /// </summary>
    public class UEnum
    {
        /// <summary>List of pairs of all enum names and values.</summary>
        public List<Tuple<FName, long>> Names;

        /// <summary>How the enum was originally defined.</summary>
        public ECppForm CppForm = ECppForm.Regular;

        public void Read(AssetBinaryReader reader, UAsset asset)
        {
            if (asset.ObjectVersion < ObjectVersion.VER_UE4_TIGHTLY_PACKED_ENUMS)
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName();
                    Names.Add(new Tuple<FName, long>(tempName, i));
                }
            }
            else if (asset.GetCustomVersion<FCoreObjectVersion>() < FCoreObjectVersion.EnumProperties)
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName();
                    byte tempVal = reader.ReadByte();
                    Names.Add(new Tuple<FName, long>(tempName, tempVal));
                }
            }
            else
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName();
                    long tempVal = reader.ReadInt64();
                    Names.Add(new Tuple<FName, long>(tempName, tempVal));
                }
            }

            if (asset.ObjectVersion < ObjectVersion.VER_UE4_ENUM_CLASS_SUPPORT)
            {
                bool bIsNamespace = reader.ReadInt32() == 1;
                CppForm = bIsNamespace ? ECppForm.Namespaced : ECppForm.Regular;
            }
            else
            {
                CppForm = (ECppForm)reader.ReadByte();
            }
        }

        public void Write(AssetBinaryWriter writer, UAsset asset)
        {
            writer.Write(Names.Count);
            if (asset.ObjectVersion < ObjectVersion.VER_UE4_TIGHTLY_PACKED_ENUMS)
            {
                var namesForSerialization = new Dictionary<long, FName>();
                for (int i = 0; i < Names.Count; i++) namesForSerialization.Add(Names[i].Item2, Names[i].Item1);
                for (int i = 0; i < Names.Count; i++)
                {
                    if (namesForSerialization.ContainsKey(i)) writer.Write(namesForSerialization[i]);
                }
            }
            else if (asset.GetCustomVersion<FCoreObjectVersion>() < FCoreObjectVersion.EnumProperties)
            {
                for (int i = 0; i < Names.Count; i++)
                {
                    writer.Write(Names[i].Item1);
                    writer.Write((byte)Names[i].Item2);
                }
            }
            else
            {
                for (int i = 0; i < Names.Count; i++)
                {
                    writer.Write(Names[i].Item1);
                    writer.Write(Names[i].Item2);
                }
            }

            if (asset.ObjectVersion < ObjectVersion.VER_UE4_ENUM_CLASS_SUPPORT)
            {
                writer.Write(CppForm == ECppForm.Namespaced ? 1 : 0);
            }
            else
            {
                writer.Write((byte)CppForm);
            }
        }

        public UEnum()
        {
            Names = new List<Tuple<FName, long>>();
        }
    }

    /// <summary>
    /// Export data for an enumeration. See <see cref="UEnum"/>.
    /// </summary>
    public class EnumExport : NormalExport
    {
        /// <summary>The enum that is stored in this export.</summary>
        public UEnum Enum;

        public EnumExport(Export super) : base(super)
        {

        }

        public EnumExport(UAsset asset, byte[] extras) : base(asset, extras)
        {

        }

        public EnumExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            Enum = new UEnum();
            Enum.Read(reader, Asset);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);

            Enum.Write(writer, Asset);
        }
    }
}
