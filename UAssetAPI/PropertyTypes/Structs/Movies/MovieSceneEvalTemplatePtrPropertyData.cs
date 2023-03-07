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

    public class MovieSceneEvalTemplatePtrPropertyData : StructPropertyData 
    {
        public MovieSceneEvalTemplatePtrPropertyData(FName name) : base(name)
        {
            Value = new List<PropertyData>();
        }

        public MovieSceneEvalTemplatePtrPropertyData(FName name, FName forcedType) : base(name)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public MovieSceneEvalTemplatePtrPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneEvalTemplatePtr");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader) {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            List<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            data = new StrPropertyData(FName.DefineDummy(reader.Asset, "TypeName"));
            data.Ancestry.Initialize(Ancestry, Name);
            //data = new StrPropertyData();
            data.Read(reader, includeHeader, leng1);
            resultingList.Add(data);
            if (((StrPropertyData)data).Value != null) {
                var unversionedHeader = new FUnversionedHeader(reader);
                while ((data = MainSerializer.Read(reader, Ancestry, Name, unversionedHeader, true)) != null) {
                    resultingList.Add(data);
                }   
            }

            Value = resultingList;
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader) {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;

            if (Value != null) {
                foreach (PropertyData t in Value) {
                    if (t.Name.ToString()== "TypeName") {
                        writer.Write(((StrPropertyData)t).Value);
                    } else { MainSerializer.Write(t, writer, true); }
                }

                if(((StrPropertyData)Value[0]).Value != null) {
                    writer.Write(FName.FromString(writer.Asset, "None"));
                }
            }
            
            return (int)writer.BaseStream.Position - here;
        }

    }

    public class MovieSceneTrackImplementationPtrPropertyData : StructPropertyData
    {
        public MovieSceneTrackImplementationPtrPropertyData(FName name) : base(name)
        {
            Value = new List<PropertyData>();
        }

        public MovieSceneTrackImplementationPtrPropertyData(FName name, FName forcedType) : base(name)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public MovieSceneTrackImplementationPtrPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneTrackImplementationPtr");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }


        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            List<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            data = new StrPropertyData(FName.DefineDummy(reader.Asset, "TypeName"));
            data.Ancestry.Initialize(Ancestry, Name);
            data.Read(reader, includeHeader, leng1);
            resultingList.Add(data);
            if (((StrPropertyData)data).Value != null) {

                var unversionedHeader = new FUnversionedHeader(reader);

                while ((data = MainSerializer.Read(reader, Ancestry, Name, unversionedHeader, true)) != null) {
                    resultingList.Add(data);
                }
            }

            Value = resultingList;
        }


        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;

            if (Value != null) {
                var dat = Value;
                MainSerializer.GenerateUnversionedHeader(ref dat, Name, writer.Asset)?.Write(writer);

                foreach (var t in dat) {
                    if (t.Name.ToString() == "TypeName") {
                        writer.Write(((StrPropertyData)t).Value);
                    } else { MainSerializer.Write(t, writer, true); }
                }

                if (((StrPropertyData)dat[0]).Value != null) {
                    if (!writer.Asset.HasUnversionedProperties) writer.Write(FName.FromString(writer.Asset, "None"));
                }
            }

            return (int)writer.BaseStream.Position - here;
        }
    }
}