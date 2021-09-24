using System;
using System.Collections.Generic;
using System.IO;

namespace UAssetAPI
{
    /** How this enum is declared in C++, affects the internal naming of enum values */
    public enum ECppForm
    {
        Regular,
		Namespaced,
		EnumClass
    }

    public class UEnum
    {
        /** List of pairs of all enum names and values. */
        public List<Tuple<FName, long>> Names;

        /** How the enum was originally defined. */
        public ECppForm CppForm = ECppForm.Regular;

        public void Read(BinaryReader reader, UAsset asset)
        {
            if (asset.EngineVersion < UE4Version.VER_UE4_TIGHTLY_PACKED_ENUMS)
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName(asset);
                    Names.Add(new Tuple<FName, long>(tempName, i));
                }
            }
            else if (asset.GetCustomVersion<FCoreObjectVersion>() < FCoreObjectVersion.EnumProperties)
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName(asset);
                    byte tempVal = reader.ReadByte();
                    Names.Add(new Tuple<FName, long>(tempName, tempVal));
                }
            }
            else
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName(asset);
                    long tempVal = reader.ReadInt64();
                    Names.Add(new Tuple<FName, long>(tempName, tempVal));
                }
            }

            if (asset.EngineVersion < UE4Version.VER_UE4_ENUM_CLASS_SUPPORT)
            {
                bool bIsNamespace = reader.ReadInt32() == 1;
                CppForm = bIsNamespace ? ECppForm.Namespaced : ECppForm.Regular;
            }
            else
            {
                CppForm = (ECppForm)reader.ReadByte();
            }
        }

        public void Write(BinaryWriter writer, UAsset asset)
        {
            writer.Write(Names.Count);
            if (asset.EngineVersion < UE4Version.VER_UE4_TIGHTLY_PACKED_ENUMS)
            {
                var namesForSerialization = new Dictionary<long, FName>();
                for (int i = 0; i < Names.Count; i++) namesForSerialization.Add(Names[i].Item2, Names[i].Item1);
                for (int i = 0; i < Names.Count; i++)
                {
                    if (namesForSerialization.ContainsKey(i)) writer.WriteFName(namesForSerialization[i], asset);
                }
            }
            else if (asset.GetCustomVersion<FCoreObjectVersion>() < FCoreObjectVersion.EnumProperties)
            {
                for (int i = 0; i < Names.Count; i++)
                {
                    writer.WriteFName(Names[i].Item1, asset);
                    writer.Write((byte)Names[i].Item2);
                }
            }
            else
            {
                for (int i = 0; i < Names.Count; i++)
                {
                    writer.WriteFName(Names[i].Item1, asset);
                    writer.Write(Names[i].Item2);
                }
            }

            if (asset.EngineVersion < UE4Version.VER_UE4_ENUM_CLASS_SUPPORT)
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


    public class EnumExport : NormalExport
    {
        /** The enum that is stored in this export. */
        public UEnum Enum;

        public EnumExport(Export super) : base(super)
        {

        }

        public EnumExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);
            reader.ReadInt32();

            Enum = new UEnum();
            Enum.Read(reader, Asset);
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((int)0);

            Enum.Write(writer, Asset);
        }
    }
}
