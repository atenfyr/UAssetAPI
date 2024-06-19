using Newtonsoft.Json;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;

namespace UAssetAPI.UnrealTypes
{
    /*
        The code within this file is modified from LongerWarrior's UEAssetToolkitGenerator project, which is licensed under the Apache License 2.0.
        Please see the NOTICE.md file distributed with UAssetAPI and UAssetGUI for more information.
    */

    public class NiagaraVariableBasePropertyData : PropertyData
    {
        [JsonProperty]
        public FName VariableName;
        [JsonProperty]
        public StructPropertyData TypeDef;

        public NiagaraVariableBasePropertyData(FName name) : base(name)
        {
            //Value = new List<PropertyData>();
        }

        public NiagaraVariableBasePropertyData(FName name, FName forcedType) : base(name)
        {
            //StructType = forcedType;
            //Value = new List<PropertyData>();
        }

        public NiagaraVariableBasePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("NiagaraVariableBase");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            VariableName = reader.ReadFName();
            TypeDef = new StructPropertyData(FName.DefineDummy(reader.Asset, "TypeDef"), FName.DefineDummy(reader.Asset, "NiagaraTypeDefinition"));
            TypeDef.Read(reader, false, 1, 0, serializationContext);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            int here = (int)writer.BaseStream.Position;
            writer.Write(VariableName);
            TypeDef.Write(writer, false);
            return (int)writer.BaseStream.Position - here;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            VariableName = FName.FromString(asset, d[0]);
        }
    }

    public class NiagaraVariablePropertyData : NiagaraVariableBasePropertyData
    {
        [JsonProperty]
        public byte[] VarData;

        public NiagaraVariablePropertyData(FName name) : base(name)
        {
            //Value = new List<PropertyData>();
        }

        public NiagaraVariablePropertyData(FName name, FName forcedType) : base(name)
        {
            //StructType = forcedType;
            //Value = new List<PropertyData>();
        }

        public NiagaraVariablePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("NiagaraVariable");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            base.Read(reader, includeHeader, leng1, leng2, serializationContext);
            int varDataSize = reader.ReadInt32();
            VarData = reader.ReadBytes(varDataSize);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            int sz = base.Write(writer, includeHeader, serializationContext);
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

        [JsonProperty]
        public int VariableOffset;

        public NiagaraVariableWithOffsetPropertyData(FName name) : base(name)
        {
            //Value = new List<PropertyData>();
        }

        public NiagaraVariableWithOffsetPropertyData(FName name, FName forcedType) : base(name)
        {
            //StructType = forcedType;
            //Value = new List<PropertyData>();
        }

        public NiagaraVariableWithOffsetPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("NiagaraVariableWithOffset");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            base.Read(reader, includeHeader, leng1, leng2, serializationContext);
            VariableOffset = reader.ReadInt32();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            int sz = base.Write(writer, includeHeader);
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
}