using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.Unversioned
{
    public enum ESaveGameFileVersion
    {
        InitialVersion = 1,
        // serializing custom versions into the savegame data to handle that type of versioning
        AddedCustomVersions = 2,
        // added a new UE5 version number to FPackageFileSummary
        PackageFileSummaryVersionChange = 3,

        // -----<new versions can be added above this line>-------------------------------------------------
        VersionPlusOne,
        LatestVersion = VersionPlusOne - 1
    }

    public enum ECustomVersionSerializationFormat
    {
        Unknown,
        Guids,
        Enums,
        Optimized
    }

    /// <summary>
    /// Represents an Unreal save game file. Parsing is only implemented for engine and custom version data.
    /// </summary>
    public class SaveGame
    {
        /// <summary>
        /// The path of the file on disk.
        /// </summary>
        [JsonIgnore]
        public string FilePath = null;

        public ESaveGameFileVersion SaveGameFileVersion;
        public ObjectVersion ObjectVersion;
        public ObjectVersionUE5 ObjectVersionUE5;
        public FEngineVersion EngineVersion;

        public ECustomVersionSerializationFormat CustomVersionSerializationFormat;
        /// <summary>
        /// All the custom versions stored in the archive.
        /// </summary>
        public List<CustomVersion> CustomVersionContainer = null;

        /// <summary>
        /// Creates a MemoryStream from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new MemoryStream that stores the binary data of the input file.</returns>
        public MemoryStream PathToStream(string p)
        {
            using (FileStream origStream = File.Open(p, FileMode.Open))
            {
                MemoryStream completeStream = new MemoryStream();
                origStream.CopyTo(completeStream);

                completeStream.Seek(0, SeekOrigin.Begin);
                return completeStream;
            }
        }

        /// <summary>
        /// Creates a BinaryReader from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new BinaryReader that stores the binary data of the input file.</returns>
        public UnrealBinaryReader PathToReader(string p)
        {
            return new UnrealBinaryReader(PathToStream(p));
        }

        public static readonly byte[] SAVE_MAGIC = Encoding.ASCII.GetBytes("GVAS");
        /// <summary>
        /// Reads a save game from disk.
        /// <para/>
        /// Parsing is only implemented for engine and custom version data.
        /// </summary>
        /// <param name="reader">The binary reader to use.</param>
        public void Read(UnrealBinaryReader reader)
        {
            if (!reader.ReadBytes(SAVE_MAGIC.Length).SequenceEqual(SAVE_MAGIC)) throw new FormatException("Invalid save game magic");
            SaveGameFileVersion = (ESaveGameFileVersion)reader.ReadUInt32();
            ObjectVersion = (ObjectVersion)reader.ReadUInt32();
            if (SaveGameFileVersion >= ESaveGameFileVersion.PackageFileSummaryVersionChange) ObjectVersionUE5 = (ObjectVersionUE5)reader.ReadUInt32();
            EngineVersion = new FEngineVersion(reader);
            if (SaveGameFileVersion >= ESaveGameFileVersion.AddedCustomVersions)
            {
                CustomVersionSerializationFormat = (ECustomVersionSerializationFormat)reader.ReadUInt32();
                CustomVersionContainer = reader.ReadCustomVersionContainer(CustomVersionSerializationFormat);
            }
        }

        /// <summary>
        /// Patches a .usmap file to contain the versioning info within this save file.
        /// </summary>
        /// <param name="usmapPath">The path to the .usmap file to patch.</param>
        public void PatchUsmap(string usmapPath)
        {
            byte[] restOfData = new byte[0];
            UsmapVersion ver = UsmapVersion.Initial;
            using (FileStream origStream = File.Open(usmapPath, FileMode.Open))
            {
                UnrealBinaryReader reader = new UnrealBinaryReader(origStream);

                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                ushort fileSignature = reader.ReadUInt16();
                if (fileSignature != Usmap.USMAP_MAGIC) throw new FormatException(".usmap: File signature mismatch");

                ver = (UsmapVersion)reader.ReadByte();
                if (ver < UsmapVersion.Initial || ver > UsmapVersion.Latest) throw new FormatException(".usmap: Unknown file version " + ver);
                if (ver >= UsmapVersion.PackageVersioning)
                {
                    bool bHasVersioning = reader.ReadInt32() > 0;
                    if (bHasVersioning)
                    {
                        reader.ReadUInt32();
                        reader.ReadUInt32();
                        reader.ReadCustomVersionContainer(ECustomVersionSerializationFormat.Optimized);
                        reader.ReadUInt32();
                    }
                }

                restOfData = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));
            }

            if (ver < UsmapVersion.PackageVersioning) ver = UsmapVersion.PackageVersioning;

            using (FileStream origStream = File.Open(usmapPath, FileMode.Create))
            {
                UnrealBinaryWriter writer = new UnrealBinaryWriter(origStream);

                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(Usmap.USMAP_MAGIC);
                writer.Write((byte)ver);
                writer.Write((int)1);
                writer.Write((uint)ObjectVersion);
                writer.Write((uint)ObjectVersionUE5);
                writer.WriteCustomVersionContainer(ECustomVersionSerializationFormat.Optimized, CustomVersionContainer);
                writer.Write(EngineVersion.Changelist);
                writer.Write(restOfData); 
            }
        }

        /// <summary>
        /// Reads a save game from disk and initializes a new instance of the <see cref="SaveGame"/> class to store its data in memory.
        /// <para/>
        /// Parsing is only implemented for engine and custom version data.
        /// </summary>
        /// <param name="path">The path of the .sav file on disk that this instance will read from.</param>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public SaveGame(string path)
        {
            FilePath = path;
            Read(PathToReader(FilePath));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveGame"/> class. This instance will store no file data and does not represent any file in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        public SaveGame()
        {

        }
    }
}
