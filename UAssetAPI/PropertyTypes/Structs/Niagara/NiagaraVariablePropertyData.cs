using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class NiagaraVariableBasePropertyData : StructPropertyData
{
    public FName VariableName;
    public StructPropertyData TypeDef;

    public NiagaraVariableBasePropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public NiagaraVariableBasePropertyData(FName name) : base(name) { }
    public NiagaraVariableBasePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NiagaraVariableBase");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (reader.Asset.GetEngineVersion() <= EngineVersion.VER_UE4_25)
        {
            StructType = FName.DefineDummy(reader.Asset, PropertyType);
            base.Read(reader, includeHeader, 1, 0, PropertySerializationContext.StructFallback);
            return;
        }

        VariableName = reader.ReadFName();
        TypeDef = new StructPropertyData(FName.DefineDummy(reader.Asset, "TypeDef"), FName.DefineDummy(reader.Asset, "NiagaraTypeDefinition"));
        TypeDef.Read(reader, false, 1, 0, serializationContext);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (writer.Asset.GetEngineVersion() <= EngineVersion.VER_UE4_25)
        {
            StructType = FName.DefineDummy(writer.Asset, PropertyType);
            return base.Write(writer, includeHeader, PropertySerializationContext.StructFallback);
        }

        var offset = writer.BaseStream.Position;
        writer.Write(VariableName);
        if (TypeDef == null) TypeDef = new StructPropertyData(FName.DefineDummy(writer.Asset, "TypeDef"), FName.DefineDummy(writer.Asset, "NiagaraTypeDefinition"));
        TypeDef.Write(writer, false);
        return (int)(writer.BaseStream.Position - offset);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        VariableName = FName.FromString(asset, d[0]);
    }
}

public class NiagaraVariablePropertyData : NiagaraVariableBasePropertyData
{
    public byte[] VarData;

    public NiagaraVariablePropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public NiagaraVariablePropertyData(FName name) : base(name) { }
    public NiagaraVariablePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NiagaraVariable");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        base.Read(reader, includeHeader, leng1, leng2, serializationContext);

        if (reader.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_26)
        {
            int varDataSize = reader.ReadInt32();
            VarData = reader.ReadBytes(varDataSize);
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        int sz = base.Write(writer, includeHeader, serializationContext);
        if (writer.Asset.GetEngineVersion() <= EngineVersion.VER_UE4_25)
            return sz;

        if (VarData == null) VarData = Array.Empty<byte>();
        writer.Write(VarData.Length); sz += sizeof(int);
        writer.Write(VarData); sz += VarData.Length;
        return sz;
    }

    public override void FromString(string[] d, UAsset asset)
    {
        base.FromString(d, asset);
        VarData = d[2].ConvertStringToByteArray();
    }
}

public class NiagaraVariableWithOffsetPropertyData : NiagaraVariableBasePropertyData
{
    public int VariableOffset;

    public NiagaraVariableWithOffsetPropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public NiagaraVariableWithOffsetPropertyData(FName name) : base(name) { }
    public NiagaraVariableWithOffsetPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NiagaraVariableWithOffset");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        base.Read(reader, includeHeader, leng1, leng2, serializationContext);

        if (reader.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_26)
            VariableOffset = reader.ReadInt32();
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        int sz = base.Write(writer, includeHeader);
        if (writer.Asset.GetEngineVersion() <= EngineVersion.VER_UE4_25)
            return sz;

        writer.Write(VariableOffset);
        sz += sizeof(int);
        return sz;
    }

    public override void FromString(string[] d, UAsset asset)
    {
        base.FromString(d, asset);
        VariableOffset = 0;
        if (int.TryParse(d[2], out int res)) VariableOffset = res;
    }
}