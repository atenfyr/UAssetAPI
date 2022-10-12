namespace UAssetAPI.PropertyTypes.Objects
{
    public enum TextHistoryType
    {
        None = -1,
        Base = 0,
        NamedFormat,
        OrderedFormat,
        ArgumentFormat,
        AsNumber,
        AsPercent,
        AsCurrency,
        AsDate,
        AsTime,
        AsDateTime,
        Transform,
        StringTableEntry,
        TextGenerator,

        RawText // Uncertain, Back 4 Blood specific serialization
    }
}