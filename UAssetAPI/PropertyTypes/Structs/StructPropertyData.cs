using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class StructPropertyData : PropertyData<List<PropertyData>> // List
    {
        [JsonProperty]
        public FName StructType = null;
        [JsonProperty]
        public bool SerializeNone = true;
        [JsonProperty]
        public Guid StructGUID = Guid.Empty; // usually set to 0

        public StructPropertyData(FName name) : base(name)
        {
            Value = new List<PropertyData>();
        }

        public StructPropertyData(FName name, FName forcedType) : base(name)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public StructPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("StructProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        private void ReadOnce(AssetBinaryReader reader, Type T, long offset)
        {
            var data = Activator.CreateInstance(T, Name) as PropertyData;
            if (data == null) return;
            data.Offset = offset;
            data.Read(reader, false, 0);
            Value = new List<PropertyData> { data };
        }

        private void ReadNTPL(AssetBinaryReader reader)
        {
            List<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            while ((data = MainSerializer.Read(reader, true)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;
        }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader) // originally !isForced
            {
                StructType = reader.ReadFName();
                if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG) StructGUID = new Guid(reader.ReadBytes(16));
                PropertyGuid = reader.ReadPropertyGuid();
            }

            RegistryEntry targetEntry = null;
            string structTypeVal = StructType?.Value?.Value;
            if (structTypeVal != null) MainSerializer.PropertyTypeRegistry.TryGetValue(structTypeVal, out targetEntry);
            bool hasCustomStructSerialization = targetEntry != null && targetEntry.HasCustomStructSerialization;

            if (structTypeVal == "FloatRange")
            {
                // FloatRange is a special case; it can either be manually serialized as two floats (TRange<float>) or as a regular struct (FFloatRange), but the first is overridden to use the same name as the second
                // The best solution is to just check and see if the next bit is an FName or not

                int nextFourBytes = reader.ReadInt32();
                reader.BaseStream.Position -= sizeof(int);
                hasCustomStructSerialization = !(nextFourBytes >= 0 && nextFourBytes < reader.Asset.GetNameMapIndexList().Count && reader.Asset.GetNameReference(nextFourBytes).Value == "LowerBound");
            }
            if (structTypeVal == "RichCurveKey" && reader.Asset.ObjectVersion < ObjectVersion.VER_UE4_SERIALIZE_RICH_CURVE_KEY) hasCustomStructSerialization = false;
            if (structTypeVal == "MovieSceneTrackIdentifier" && reader.Asset.GetCustomVersion<FEditorObjectVersion>() < FEditorObjectVersion.MovieSceneMetaDataSerialization) hasCustomStructSerialization = false;
            if (structTypeVal == "MovieSceneFloatChannel" && reader.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannelCompletely && reader.Asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() < FFortniteMainBranchObjectVersion.SerializeFloatChannelShowCurve) hasCustomStructSerialization = false;

            if (leng1 == 0)
            {
                SerializeNone = false;
                Value = new List<PropertyData>();
                return;
            }

            if (targetEntry != null && hasCustomStructSerialization)
            {
                ReadOnce(reader, targetEntry.PropertyType, reader.BaseStream.Position);
            }
            else
            {
                ReadNTPL(reader);
            }
        }

        private int WriteOnce(AssetBinaryWriter writer)
        {
            if (Value.Count != 1) throw new InvalidOperationException("Structs with type " + StructType.Value.Value + " must have exactly one entry");
            Value[0].Offset = writer.BaseStream.Position;
            return Value[0].Write(writer, false);
        }

        private int WriteNTPL(AssetBinaryWriter writer)
        {
            int here = (int)writer.BaseStream.Position;
            if (Value != null)
            {
                foreach (var t in Value)
                {
                    MainSerializer.Write(t, writer, true);
                }
            }
            writer.Write(new FName(writer.Asset, "None"));
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write(StructType);
                if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG) writer.Write(StructGUID.ToByteArray());
                writer.WritePropertyGuid(PropertyGuid);
            }

            RegistryEntry targetEntry = null;
            string structTypeVal = StructType?.Value?.Value;
            if (structTypeVal != null) MainSerializer.PropertyTypeRegistry.TryGetValue(structTypeVal, out targetEntry);
            bool hasCustomStructSerialization = targetEntry != null && targetEntry.HasCustomStructSerialization;

            if (structTypeVal == "FloatRange") hasCustomStructSerialization = Value.Count == 1 && Value[0] is FloatRangePropertyData;
            if (structTypeVal == "RichCurveKey" && writer.Asset.ObjectVersion < ObjectVersion.VER_UE4_SERIALIZE_RICH_CURVE_KEY) hasCustomStructSerialization = false;
            if (structTypeVal == "MovieSceneTrackIdentifier" && writer.Asset.GetCustomVersion<FEditorObjectVersion>() < FEditorObjectVersion.MovieSceneMetaDataSerialization) hasCustomStructSerialization = false;
            if (structTypeVal == "MovieSceneFloatChannel" && writer.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannelCompletely && writer.Asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() < FFortniteMainBranchObjectVersion.SerializeFloatChannelShowCurve) hasCustomStructSerialization = false;

            if (targetEntry != null && hasCustomStructSerialization) return WriteOnce(writer);
            if (Value.Count == 0 && !SerializeNone) return 0;
            return WriteNTPL(writer);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (d[4] != null && d[4] != "Generic") StructType = FName.FromString(asset, d[4]);
            if (StructType == null) StructType = FName.DefineDummy(asset, "Generic");
        }

        protected override void HandleCloned(PropertyData res)
        {
            StructPropertyData cloningProperty = (StructPropertyData)res;
            cloningProperty.StructType = (FName)this.StructType.Clone();
            cloningProperty.StructGUID = new Guid(this.StructGUID.ToByteArray());

            List<PropertyData> newData = new List<PropertyData>(this.Value.Count);
            for (int i = 0; i < this.Value.Count; i++)
            {
                newData.Add((PropertyData)this.Value[i].Clone());
            }
            cloningProperty.Value = newData;
        }
    }
}