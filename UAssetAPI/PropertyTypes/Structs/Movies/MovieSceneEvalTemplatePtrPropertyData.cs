using System;
using System.Linq;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneTemplatePropertyData : StructPropertyData 
{
    public MovieSceneTemplatePropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public MovieSceneTemplatePropertyData(FName name) : base(name) { }
    public MovieSceneTemplatePropertyData() { }

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        StrPropertyData type = new StrPropertyData(FName.DefineDummy(reader.Asset, "TypeName"));
        type.Ancestry.Initialize(Ancestry, Name);
        type.Read(reader, includeHeader, leng1);
        
        if (type.Value != null)
        {
            StructType = FName.DefineDummy(reader.Asset, type.Value.ToString().Split(".")[1]);
            base.Read(reader, false, 1, 0, PropertySerializationContext.StructFallback);
        }

        Value.Insert(0, type);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        var offset = writer.BaseStream.Position;

        if (Value != null)
        {
            var type = (StrPropertyData)Value.Find(x => x.Name.ToString() == "TypeName");
            if (type is null) throw new FormatException($"TypeName property not found in {PropertyType}");
            writer.Write(type.Value);
            if (type.Value != null)
            {
                var dat = Value.Except([type]).ToList();
                MainSerializer.GenerateUnversionedHeader(ref dat, Name, null, writer.Asset)?.Write(writer);

                foreach (var t in dat)
                {
                    MainSerializer.Write(t, writer, true);
                }
                if (!writer.Asset.HasUnversionedProperties) writer.Write(new FName(writer.Asset, "None"));
            }
        }

        return (int)(writer.BaseStream.Position - offset);
    }
}

public class MovieSceneEvalTemplatePtrPropertyData : MovieSceneTemplatePropertyData
{
    public MovieSceneEvalTemplatePtrPropertyData(FName name, FName forcedType) : base(name, forcedType) {}
    public MovieSceneEvalTemplatePtrPropertyData(FName name) : base(name) { }
    public MovieSceneEvalTemplatePtrPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneEvalTemplatePtr");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class MovieSceneTrackImplementationPtrPropertyData : MovieSceneTemplatePropertyData
{
    public MovieSceneTrackImplementationPtrPropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public MovieSceneTrackImplementationPtrPropertyData(FName name) : base(name) { }
    public MovieSceneTrackImplementationPtrPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneTrackImplementationPtr");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}