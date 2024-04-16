using System;
using Newtonsoft.Json;

namespace UAssetAPI.UnrealTypes
{
    // TODO: JSON serializer 

    /// <summary>
    /// Unreal Object Thumbnail - Thumbnail image data for an object.
    /// </summary>
    public class FObjectThumbnail
    {
        /// <summary>Thumbnail width</summary>
        public int Width;

        /// <summary>Thumbnail height</summary>
        public int Height;

        /// <summary>Compressed image data bytes</summary>
        public byte[] CompressedImageData = Array.Empty<byte>(); // TODO: FThumbnailCompressionInterface
        // TODO: Compress on demand?

        /// <summary>Image data bytes</summary>
        [JsonIgnore]
        public byte[] ImageData; // TODO: Uncompress on demand

        public FObjectThumbnail()
        {
        }
    }
}