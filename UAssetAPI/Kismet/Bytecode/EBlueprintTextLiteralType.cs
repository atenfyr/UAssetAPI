namespace UAssetAPI.Kismet.Bytecode
{
    /// <summary>
    /// Kinds of text literals
    /// </summary>
    public enum EBlueprintTextLiteralType : byte
	{
		/// <summary>
		/// Text is an empty string. The bytecode contains no strings, and you should use FText::GetEmpty() to initialize the FText instance.
		/// </summary>
		Empty,
		/// <summary>
		/// Text is localized. The bytecode will contain three strings - source, key, and namespace - and should be loaded via FInternationalization
		/// </summary>
		LocalizedText,
		/// <summary>
		/// Text is culture invariant. The bytecode will contain one string, and you should use FText::AsCultureInvariant to initialize the FText instance.
		/// </summary>
		InvariantText,
		/// <summary>
		/// Text is a literal FString. The bytecode will contain one string, and you should use FText::FromString to initialize the FText instance.
		/// </summary>
		LiteralString,
		/// <summary>
		/// Text is from a string table. The bytecode will contain an object pointer (not used) and two strings - the table ID, and key - and should be found via FText::FromStringTable
		/// </summary>
		StringTableEntry
	}
}
