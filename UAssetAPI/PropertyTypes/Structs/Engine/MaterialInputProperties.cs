using Newtonsoft.Json;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.CustomVersions;

namespace UAssetAPI.PropertyTypes.Structs;

public abstract class MaterialInputPropertyData<T> : PropertyData<T>
{
    public FPackageIndex Expression;
    public int OutputIndex;
    public FName InputName;
    public FString InputNameOld;
    public int Mask;
    public int MaskR;
    public int MaskG;
    public int MaskB;
    public int MaskA;
    public FName ExpressionName;

    public MaterialInputPropertyData() { }

    public MaterialInputPropertyData(FName name) : base(name) { }

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        //if (reader.Asset.GetCustomVersion<FCoreObjectVersion>() < FCoreObjectVersion.MaterialInputNativeSerialize)
        //{
        //    StructType = FName.DefineDummy(reader.Asset, PropertyType);
        //    base.Read(reader, false, 1, 0, PropertySerializationContext.StructFallback);
        //    return;
        //}

        if ((reader.Asset.GetEngineVersion() <= EngineVersion.VER_UE5_1 && !reader.Asset.IsFilterEditorOnly) || reader.Asset.GetEngineVersion() >= EngineVersion.VER_UE5_1)
            Expression = reader.XFERPTR();
        OutputIndex = reader.ReadInt32();
        InputName = reader.Asset.GetCustomVersion<FFrameworkObjectVersion>() >= FFrameworkObjectVersion.PinsStoreFName ? reader.ReadFName() : null;
        InputNameOld = reader.Asset.GetCustomVersion<FFrameworkObjectVersion>() >= FFrameworkObjectVersion.PinsStoreFName ? null : reader.ReadFString();
        Mask = reader.ReadInt32();
        MaskR = reader.ReadInt32();
        MaskG = reader.ReadInt32();
        MaskB = reader.ReadInt32();
        MaskA = reader.ReadInt32();
        ExpressionName = reader.Asset.GetEngineVersion() <= EngineVersion.VER_UE5_1 && reader.Asset.IsFilterEditorOnly ? reader.ReadFName() : null;
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return WriteExpressionInput(writer, false);
    }

    protected int WriteExpressionInput(AssetBinaryWriter writer, bool includeHeader)
    {
        int totalSize = 0;
        if (writer.Asset.GetCustomVersion<FCoreObjectVersion>() >= FCoreObjectVersion.MaterialInputNativeSerialize)
        {
            if ((writer.Asset.GetEngineVersion() <= EngineVersion.VER_UE5_1 && !writer.Asset.IsFilterEditorOnly) || writer.Asset.GetEngineVersion() >= EngineVersion.VER_UE5_1)
            {
                writer.XFERPTR(Expression);
                totalSize += sizeof(int);
            }

            writer.Write(OutputIndex); totalSize += sizeof(int);
            if (writer.Asset.GetCustomVersion<FFrameworkObjectVersion>() >= FFrameworkObjectVersion.PinsStoreFName)
            {
                writer.Write(InputName); totalSize += sizeof(int) * 2;
            }
            else
            {
                totalSize += writer.Write(InputNameOld);
            }
            writer.Write(Mask);
            writer.Write(MaskR);
            writer.Write(MaskG);
            writer.Write(MaskB);
            writer.Write(MaskA);
            totalSize += sizeof(int) * 5;
            if (writer.Asset.GetEngineVersion() <= EngineVersion.VER_UE5_1 && writer.Asset.IsFilterEditorOnly)
            {
                writer.Write(ExpressionName);
                totalSize += sizeof(int) * 2;
            }
            
        }
        return totalSize;
    }

    protected override void HandleCloned(PropertyData res)
    {
        MaterialInputPropertyData<T> cloningProperty = (MaterialInputPropertyData<T>)res;
        cloningProperty.InputName = (FName)this.InputName?.Clone();
        cloningProperty.InputNameOld = (FString)this.InputNameOld?.Clone();
        cloningProperty.ExpressionName = (FName)this.ExpressionName?.Clone();
    }
}

public class ExpressionInputPropertyData : MaterialInputPropertyData<int>
{
    public ExpressionInputPropertyData(FName name) : base(name) { }

    public ExpressionInputPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("ExpressionInput");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class MaterialAttributesInputPropertyData : MaterialInputPropertyData<int>
{
    public MaterialAttributesInputPropertyData(FName name) : base(name) { }

    public MaterialAttributesInputPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MaterialAttributesInput");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class ColorMaterialInputPropertyData : MaterialInputPropertyData<ColorPropertyData>
{
    public ColorMaterialInputPropertyData(FName name) : base(name) { }

    public ColorMaterialInputPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("ColorMaterialInput");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        base.Read(reader, includeHeader, leng1, leng2, serializationContext);

        reader.ReadInt32(); // bUseConstantValue; always false
        Value = new ColorPropertyData(Name);
        Value.Ancestry.Initialize(Ancestry, Name);
        Value.Read(reader, false, 0);
    }

    public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
    {
        var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
        ancestryNew.SetAsParent(Name);

        Value.ResolveAncestries(asset, ancestryNew);
        base.ResolveAncestries(asset, ancestrySoFar);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        int expLength = base.Write(writer, includeHeader, serializationContext);
        writer.Write(0);
        return expLength + Value.Write(writer, false) + sizeof(int);
    }
}

public class ScalarMaterialInputPropertyData : MaterialInputPropertyData<float>
{
    public ScalarMaterialInputPropertyData(FName name) : base(name) { }

    public ScalarMaterialInputPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("ScalarMaterialInput");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        base.Read(reader, includeHeader, leng1, leng2, serializationContext);

        reader.ReadInt32(); // bUseConstantValue; always false
        Value = reader.ReadSingle();
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        int expLength = WriteExpressionInput(writer, false);
        writer.Write(0);
        writer.Write(Value);
        return expLength + sizeof(float) + sizeof(int);
    }
}

public class VectorMaterialInputPropertyData : MaterialInputPropertyData<VectorPropertyData>
{
    public VectorMaterialInputPropertyData(FName name) : base(name) { }

    public VectorMaterialInputPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("VectorMaterialInput");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        base.Read(reader, includeHeader, leng1, leng2, serializationContext);

        reader.ReadInt32(); // bUseConstantValue; always false
        Value = new VectorPropertyData(Name);
        Value.Ancestry.Initialize(Ancestry, Name);
        Value.Read(reader, false, 0);
    }

    public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
    {
        var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
        ancestryNew.SetAsParent(Name);

        Value.ResolveAncestries(asset, ancestryNew);
        base.ResolveAncestries(asset, ancestrySoFar);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        int expLength = WriteExpressionInput(writer, false);
        writer.Write(0);
        return expLength + Value.Write(writer, false) + sizeof(int);
    }
}

public class Vector2MaterialInputPropertyData : MaterialInputPropertyData<Vector2DPropertyData>
{
    public Vector2MaterialInputPropertyData(FName name) : base(name) { }

    public Vector2MaterialInputPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector2MaterialInput");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        base.Read(reader, includeHeader, leng1, leng2, serializationContext);

        reader.ReadInt32(); // bUseConstantValue; always false
        Value = new Vector2DPropertyData(Name);
        Value.Ancestry.Initialize(Ancestry, Name);
        Value.Read(reader, false, 0);
    }

    public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
    {
        var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
        ancestryNew.SetAsParent(Name);

        Value.ResolveAncestries(asset, ancestryNew);
        base.ResolveAncestries(asset, ancestrySoFar);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        int expLength = WriteExpressionInput(writer, false);
        writer.Write(0);
        return expLength + Value.Write(writer, false) + sizeof(int);
    }
}