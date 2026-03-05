using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Near synonym for <see cref="SoftObjectPropertyData"/>.
/// </summary>
public class FieldPathPropertyData : BasePropertyData<FFieldPath>
{
    public FieldPathPropertyData(FName name) : base(name) { }

    public FieldPathPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("FieldPathProperty");
    public override FString PropertyType => CurrentPropertyType;
    public override bool HasCustomStructSerialization => false;
    public override object DefaultValue => new FFieldPath();

    public override string ToString()
    {
        // Expected format is: FullPackageName.Subobject[:Subobject:...]:FieldName
        return "";
    }

    public override void FromString(string[] d, UAsset asset)
    {
        // Expected format is: FullPackageName.Subobject[:Subobject:...]:FieldName
        Value = new FFieldPath();
    }
}