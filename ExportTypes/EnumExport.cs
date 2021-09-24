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

    public class EnumExport : NormalExport
    {
        /** List of pairs of all enum names and values. */
        public List<Tuple<FName, long>> Names;

        /** How the enum was originally defined. */
        public ECppForm CppForm;

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

            Names = new List<Tuple<FName, long>>();
            if (Asset.EngineVersion < UE4Version.VER_UE4_TIGHTLY_PACKED_ENUMS)
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName(Asset);
                    Names.Add(new Tuple<FName, long>(tempName, i));
                }
            }
            else if (Asset.GetCustomVersion<FCoreObjectVersion>() < FCoreObjectVersion.EnumProperties)
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName(Asset);
                    byte tempVal = reader.ReadByte();
                    Names.Add(new Tuple<FName, long>(tempName, tempVal));
                }
            }
            else
            {
                int numEntries = reader.ReadInt32();
                for (int i = 0; i < numEntries; i++)
                {
                    FName tempName = reader.ReadFName(Asset);
                    long tempVal = reader.ReadInt64();
                    Names.Add(new Tuple<FName, long>(tempName, tempVal));
                }
            }

            if (Asset.EngineVersion < UE4Version.VER_UE4_ENUM_CLASS_SUPPORT)
            {
                bool bIsNamespace = reader.ReadInt32() == 1;
                CppForm = bIsNamespace ? ECppForm.Namespaced : ECppForm.Regular;
            }
            else
            {
                CppForm = (ECppForm)reader.ReadByte();
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((int)0);

            writer.Write(Names.Count);
            if (Asset.EngineVersion < UE4Version.VER_UE4_TIGHTLY_PACKED_ENUMS)
            {
                var namesForSerialization = new Dictionary<long, FName>();
                for (int i = 0; i < Names.Count; i++) namesForSerialization.Add(Names[i].Item2, Names[i].Item1);
                for (int i = 0; i < Names.Count; i++)
                {
                    if (namesForSerialization.ContainsKey(i)) writer.WriteFName(namesForSerialization[i], Asset);
                }
            }
            else if (Asset.GetCustomVersion<FCoreObjectVersion>() < FCoreObjectVersion.EnumProperties)
            {
                for (int i = 0; i < Names.Count; i++)
                {
                    writer.WriteFName(Names[i].Item1, Asset);
                    writer.Write((byte)Names[i].Item2);
                }
            }
            else
            {
                for (int i = 0; i < Names.Count; i++)
                {
                    writer.WriteFName(Names[i].Item1, Asset);
                    writer.Write(Names[i].Item2);
                }
            }

            if (Asset.EngineVersion < UE4Version.VER_UE4_ENUM_CLASS_SUPPORT)
            {
                writer.Write(CppForm == ECppForm.Namespaced ? 1 : 0);
            }
            else
            {
                writer.Write((byte)CppForm);
            }
        }
    }
}
