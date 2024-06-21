# IOGlobalData

Namespace: UAssetAPI.IO

Global data exported from a game's global IO store container.

```csharp
public class IOGlobalData : INameMap
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [IOGlobalData](./uassetapi.io.ioglobaldata.md)<br>
Implements [INameMap](./uassetapi.io.inamemap.md)

## Fields

### **ScriptObjectEntries**

```csharp
public FScriptObjectEntry[] ScriptObjectEntries;
```

### **ScriptObjectEntriesMap**

```csharp
public Dictionary<FPackageObjectIndex, FScriptObjectEntry> ScriptObjectEntriesMap;
```

## Constructors

### **IOGlobalData(IOStoreContainer, EngineVersion)**

```csharp
public IOGlobalData(IOStoreContainer container, EngineVersion engineVersion)
```

#### Parameters

`container` [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>

`engineVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>

## Methods

### **FixNameMapLookupIfNeeded()**

```csharp
internal void FixNameMapLookupIfNeeded()
```

### **GetNameMapIndexList()**

Returns the name map as a read-only list of FStrings.

```csharp
public IReadOnlyList<FString> GetNameMapIndexList()
```

#### Returns

[IReadOnlyList&lt;FString&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
The name map as a read-only list of FStrings.

### **ClearNameIndexList()**

Clears the name map. This method should be used with extreme caution, as it may break unparsed references to the name map.

```csharp
public void ClearNameIndexList()
```

### **SetNameReference(Int32, FString)**

Replaces a value in the name map at a particular index.

```csharp
public void SetNameReference(int index, FString value)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to overwrite in the name map.

`value` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value that will be replaced in the name map.

### **GetNameReference(Int32)**

Gets a value in the name map at a particular index.

```csharp
public FString GetNameReference(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to return the value at.

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>
The value at the index provided.

### **GetNameReferenceWithoutZero(Int32)**

Gets a value in the name map at a particular index, but with the index zero being treated as if it is not valid.

```csharp
public FString GetNameReferenceWithoutZero(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to return the value at.

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>
The value at the index provided.

### **ContainsNameReference(FString)**

Checks whether or not the value exists in the name map.

```csharp
public bool ContainsNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to search the name map for.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the value appears in the name map, otherwise false.

### **SearchNameReference(FString)**

Searches the name map for a particular value.

```csharp
public int SearchNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to search the name map for.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index at which the value appears in the name map.

#### Exceptions

[NameMapOutOfRangeException](./uassetapi.namemapoutofrangeexception.md)<br>
Thrown when the value provided does not appear in the name map.

### **AddNameReference(FString, Boolean)**

Adds a new value to the name map.

```csharp
public int AddNameReference(FString name, bool forceAddDuplicates)
```

#### Parameters

`name` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to add to the name map.

`forceAddDuplicates` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not to add a new entry if the value provided already exists in the name map.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the new value in the name map. If the value already existed in the name map beforehand, that index will be returned instead.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when forceAddDuplicates is false and the value provided is null or empty.

### **CanCreateDummies()**

Whether or not we can create dummies in this name map. If false, attempting to define a dummy will append to the name map instead.

```csharp
public bool CanCreateDummies()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
A boolean.
