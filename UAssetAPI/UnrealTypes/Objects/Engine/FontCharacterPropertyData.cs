using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using UAssetAPI.PropertyTypes;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes
{
    public struct FFontCharacter
    {
        public int StartU;
        public int StartV;
        public int USize;
        public int VSize;
        public byte TextureIndex;
        public int VerticalOffset;

        public FFontCharacter(AssetBinaryReader reader)
        {
            StartU = reader.ReadInt32();
            StartV = reader.ReadInt32();
            USize = reader.ReadInt32();
            VSize = reader.ReadInt32();
            TextureIndex = reader.ReadByte();
            VerticalOffset = reader.ReadInt32();
        }

        public void Write(AssetBinaryWriter writer)
        {
            writer.Write(StartU);
            writer.Write(StartV);
            writer.Write(USize);
            writer.Write(VSize);
            writer.Write(TextureIndex);
            writer.Write(VerticalOffset);
        }
    }


    public class FontCharacterPropertyData : PropertyData<FFontCharacter>
    {


        public FontCharacterPropertyData(FName name) : base(name)
        {

        }

        public FontCharacterPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("FontCharacter");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FFontCharacter(reader);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader) {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;

            Value.Write(writer);
 
            return (int)writer.BaseStream.Position - here;
        }
    }
}
