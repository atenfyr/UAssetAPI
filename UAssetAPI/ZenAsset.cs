using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI
{
    public class ZenAsset : UnrealPackage
    {
        /// <summary>
        /// Reads an asset into memory.
        /// </summary>
        /// <param name="reader">The input reader.</param>
        /// <param name="manualSkips">An array of export indexes to skip parsing. For most applications, this should be left blank.</param>
        /// <param name="forceReads">An array of export indexes that must be read, overriding entries in the manualSkips parameter. For most applications, this should be left blank.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public override void Read(AssetBinaryReader reader, int[] manualSkips = null, int[] forceReads = null)
        {
            throw new NotImplementedException("IO store parsing is not implemented");
        }

        /// <summary>
        /// Serializes an asset from memory.
        /// </summary>
        /// <returns>A stream that the asset has been serialized to.</returns>
        public override MemoryStream WriteData()
        {
            throw new NotImplementedException("IO store parsing is not implemented");
        }

        /// <summary>
        /// Serializes and writes an asset to disk from memory.
        /// </summary>
        /// <param name="outputPath">The path on disk to write the asset to.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when <see cref="ObjectVersion"/> is unspecified.</exception>
        public override void Write(string outputPath)
        {
            throw new NotImplementedException("IO store parsing is not implemented");
        }
    }
}
