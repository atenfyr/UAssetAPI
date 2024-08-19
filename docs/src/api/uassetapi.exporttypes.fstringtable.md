# FStringTable

Namespace: UAssetAPI.ExportTypes

A string table. Holds Key-&gt;SourceString pairs of text.

```csharp
public class FStringTable : UAssetAPI.UnrealTypes.TMap`2[[UAssetAPI.UnrealTypes.FString],[UAssetAPI.UnrealTypes.FString]], UAssetAPI.UnrealTypes.IOrderedDictionary`2[[UAssetAPI.UnrealTypes.FString],[UAssetAPI.UnrealTypes.FString]], System.Collections.Generic.IDictionary`2[[UAssetAPI.UnrealTypes.FString],[UAssetAPI.UnrealTypes.FString]], System.Collections.Generic.ICollection`1[[System.Collections.Generic.KeyValuePair`2[[UAssetAPI.UnrealTypes.FString],[UAssetAPI.UnrealTypes.FString]]]], System.Collections.Generic.IEnumerable`1[[System.Collections.Generic.KeyValuePair`2[[UAssetAPI.UnrealTypes.FString],[UAssetAPI.UnrealTypes.FString]]]], System.Collections.IEnumerable, System.Collections.Specialized.IOrderedDictionary, System.Collections.IDictionary, System.Collections.ICollection
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TMap&lt;FString, FString&gt;](./uassetapi.unrealtypes.tmap-2.md) → [FStringTable](./uassetapi.exporttypes.fstringtable.md)<br>
Implements [IOrderedDictionary&lt;FString, FString&gt;](./uassetapi.unrealtypes.iordereddictionary-2.md), [IDictionary&lt;FString, FString&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2), [ICollection&lt;KeyValuePair&lt;FString, FString&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1), [IEnumerable&lt;KeyValuePair&lt;FString, FString&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1), [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable), IOrderedDictionary, [IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary), [ICollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection)

## Fields

### **TableNamespace**

```csharp
public FString TableNamespace;
```

## Properties

### **Item**

```csharp
public FString Item { get; set; }
```

#### Property Value

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **Item**

```csharp
public FString Item { get; set; }
```

#### Property Value

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **Count**

Gets the number of items in the dictionary

```csharp
public int Count { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Keys**

Gets all the keys in the ordered dictionary in their proper order.

```csharp
public ICollection<FString> Keys { get; }
```

#### Property Value

[ICollection&lt;FString&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)<br>

### **Values**

Gets all the values in the ordered dictionary in their proper order.

```csharp
public ICollection<FString> Values { get; }
```

#### Property Value

[ICollection&lt;FString&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)<br>

### **Comparer**

Gets the key comparer for this dictionary

```csharp
public IEqualityComparer<FString> Comparer { get; }
```

#### Property Value

[IEqualityComparer&lt;FString&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iequalitycomparer-1)<br>

## Constructors

### **FStringTable(FString)**

```csharp
public FStringTable(FString tableNamespace)
```

#### Parameters

`tableNamespace` [FString](./uassetapi.unrealtypes.fstring.md)<br>

### **FStringTable()**

```csharp
public FStringTable()
```
