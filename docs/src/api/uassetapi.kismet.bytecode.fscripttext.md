# FScriptText

Namespace: UAssetAPI.Kismet.Bytecode

Represents an FText as serialized in Kismet bytecode.

```csharp
public class FScriptText
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FScriptText](./uassetapi.kismet.bytecode.fscripttext.md)

## Fields

### **TextLiteralType**

```csharp
public EBlueprintTextLiteralType TextLiteralType;
```

### **LocalizedSource**

Source of this text if it is localized text. Used when [FScriptText.TextLiteralType](./uassetapi.kismet.bytecode.fscripttext.md#textliteraltype) is [EBlueprintTextLiteralType.InvariantText](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md#invarianttext).

```csharp
public KismetExpression LocalizedSource;
```

### **LocalizedKey**

Key of this text if it is localized text. Used when [FScriptText.TextLiteralType](./uassetapi.kismet.bytecode.fscripttext.md#textliteraltype) is [EBlueprintTextLiteralType.InvariantText](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md#invarianttext).

```csharp
public KismetExpression LocalizedKey;
```

### **LocalizedNamespace**

Namespace of this text if it is localized text. Used when [FScriptText.TextLiteralType](./uassetapi.kismet.bytecode.fscripttext.md#textliteraltype) is [EBlueprintTextLiteralType.InvariantText](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md#invarianttext).

```csharp
public KismetExpression LocalizedNamespace;
```

### **InvariantLiteralString**

Value of this text if it is an invariant string literal. Used when [FScriptText.TextLiteralType](./uassetapi.kismet.bytecode.fscripttext.md#textliteraltype) is [EBlueprintTextLiteralType.InvariantText](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md#invarianttext).

```csharp
public KismetExpression InvariantLiteralString;
```

### **LiteralString**

Value of this text if it is a string literal. Used when [FScriptText.TextLiteralType](./uassetapi.kismet.bytecode.fscripttext.md#textliteraltype) is [EBlueprintTextLiteralType.LiteralString](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md#literalstring).

```csharp
public KismetExpression LiteralString;
```

### **StringTableAsset**

Pointer to this text's UStringTable. Not used at runtime, but exists for asset dependency gathering. Used when [FScriptText.TextLiteralType](./uassetapi.kismet.bytecode.fscripttext.md#textliteraltype) is [EBlueprintTextLiteralType.StringTableEntry](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md#stringtableentry).

```csharp
public FPackageIndex StringTableAsset;
```

### **StringTableId**

Table ID string literal (namespace). Used when [FScriptText.TextLiteralType](./uassetapi.kismet.bytecode.fscripttext.md#textliteraltype) is [EBlueprintTextLiteralType.StringTableEntry](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md#stringtableentry).

```csharp
public KismetExpression StringTableId;
```

### **StringTableKey**

String table key string literal. Used when [FScriptText.TextLiteralType](./uassetapi.kismet.bytecode.fscripttext.md#textliteraltype) is [EBlueprintTextLiteralType.StringTableEntry](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md#stringtableentry).

```csharp
public KismetExpression StringTableKey;
```

## Constructors

### **FScriptText()**

```csharp
public FScriptText()
```

## Methods

### **Read(AssetBinaryReader)**

Reads out an FBlueprintText from a BinaryReader.

```csharp
public void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from.

### **Write(AssetBinaryWriter)**

Writes an FBlueprintText to a BinaryWriter.

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
The BinaryWriter to write from.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The iCode offset of the data that was written.
