using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Structs
{
    /*
        The code within this file is modified from LongerWarrior's UEAssetToolkitGenerator project, which is licensed under the Apache License 2.0.
        Please see the NOTICE.md file distributed with UAssetAPI and UAssetGUI for more information.
    */


    public class MovieSceneSegmentPropertyData : PropertyData<FMovieSceneSegment>
    {
        public MovieSceneSegmentPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneSegmentPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneSegment");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }
            Value = new FMovieSceneSegment();

            Value.Range = new FFrameNumberRange(reader);
            Value.ID = new FMovieSceneSegmentIdentifier(reader.ReadInt32());
            Value.bAllowEmpty = reader.ReadInt32() != 0;
            int length = reader.ReadInt32();

            List<PropertyData>[] items = new List<PropertyData>[length];
            for (int i = 0; i < length; i++)
            {
                List<PropertyData> resultingList = new List<PropertyData>();
                PropertyData data = null;
                var unversionedHeader = new FUnversionedHeader(reader);
                while ((data = MainSerializer.Read(reader, Ancestry, Name, unversionedHeader, true)) != null)
                {
                    resultingList.Add(data);
                }
                items[i] = resultingList;
            }
            Value.Impls = items;


            //Value.Impls = new StructPropertyData[length];
            //for (int i = 0; i< length;i++) {
            //    Value.Impls[i] = new StructPropertyData(new FName("Impls"), new FName("SectionEvaluationData"));
            //    Value.Impls[i].Read(reader, false, 1);
            //}
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;
            Value.Range.Write(writer);
            writer.Write(Value.ID.IdentifierIndex);
            writer.Write(Value.bAllowEmpty ? 1 : 0);

            writer.Write(Value.Impls.Length);
            for (int i = 0; i < Value.Impls.Length; i++)
            {
                if (Value.Impls[i] != null)
                {
                    var dat = Value.Impls[i];
                    MainSerializer.GenerateUnversionedHeader(ref dat, Name, writer.Asset)?.Write(writer);
                    foreach (var t in dat)
                    {
                        MainSerializer.Write(t, writer, true);
                    }
                }
                if (!writer.Asset.HasUnversionedProperties) writer.Write(FName.FromString(writer.Asset, "None"));
                //Value.Impls[i].Write(writer, false);
            }

            return (int)writer.BaseStream.Position - here;
        }
    }

    public class MovieSceneSegmentIdentifierPropertyData : PropertyData<FMovieSceneSegmentIdentifier>
    {
        public MovieSceneSegmentIdentifierPropertyData(FName name) : base(name) {

        }

        public MovieSceneSegmentIdentifierPropertyData() {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneSegmentIdentifier");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMovieSceneSegmentIdentifier(reader.ReadInt32());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write(Value.IdentifierIndex);

            return (int)writer.BaseStream.Position - here;
        }
    }
}
