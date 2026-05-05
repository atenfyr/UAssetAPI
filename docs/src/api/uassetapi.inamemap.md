# INameMap

Namespace: UAssetAPI

```csharp
public interface INameMap
```

## Methods

### **GetNameMapIndexList()**

```csharp
IReadOnlyList<FString> GetNameMapIndexList()
```

#### Returns

[IReadOnlyList&lt;FString&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **ClearNameIndexList()**

```csharp
void ClearNameIndexList()
```

### **SetNameReference(Int32, FString)**

```csharp
void SetNameReference(int index, FString value)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`value` [FString](./uassetapi.unrealtypes.fstring.md)<br>

### **GetNameReference(Int32)**

```csharp
FString GetNameReference(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **ContainsNameReference(FString)**

```csharp
bool ContainsNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **SearchNameReference(FString)**

```csharp
int SearchNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AddNameReference(FString, Boolean, Boolean)**

```csharp
int AddNameReference(FString name, bool forceAddDuplicates, bool skipFixes)
```

#### Parameters

`name` [FString](./uassetapi.unrealtypes.fstring.md)<br>

`forceAddDuplicates` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`skipFixes` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CanCreateDummies()**

```csharp
bool CanCreateDummies()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
