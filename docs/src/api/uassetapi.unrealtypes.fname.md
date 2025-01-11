# FName

Namespace: UAssetAPI.UnrealTypes

Unreal name - consists of an FString (which is serialized as an index in the name map) and an instance number

```csharp
public class FName : System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FName](./uassetapi.unrealtypes.fname.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **Number**

Instance number.

```csharp
public int Number;
```

### **Type**

The type of this FName; i.e. whether it points to a package-level name table, container-level name table, or global name table. This value is always [EMappedNameType.Package](./uassetapi.unrealtypes.emappednametype.md#package) for non-Zen assets.

```csharp
public EMappedNameType Type;
```

### **Asset**

The asset that this FName is bound to.

```csharp
public INameMap Asset;
```

## Properties

### **Value**

```csharp
public FString Value { get; set; }
```

#### Property Value

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **IsDummy**

```csharp
public bool IsDummy { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsGlobal**

Does this FName point into the global name table? This value is always false for non-Zen assets.

```csharp
public bool IsGlobal { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **FName(INameMap, String, Int32)**

Creates a new FName instance.

```csharp
public FName(INameMap asset, string value, int number)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>
The asset that this FName is bound to.

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string literal that the new FName's value will be, verbatim.

`number` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The instance number of the new FName.

### **FName(INameMap, FString, Int32)**

Creates a new FName instance.

```csharp
public FName(INameMap asset, FString value, int number)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>
The asset that this FName is bound to.

`value` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The FString that the FName's value will be, verbatim.

`number` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The instance number of the new FName.

### **FName(INameMap, Int32, Int32)**

Creates a new FName instance.

```csharp
public FName(INameMap asset, int index, int number)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>
The asset that this FName is bound to.

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index that this FName's value will be.

`number` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The instance number of the new FName.

### **FName(INameMap)**

Creates a new blank FName instance.

```csharp
public FName(INameMap asset)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>
The asset that this FName is bound to.

### **FName()**

Creates a new blank FName instance, with no asset bound to it. An asset must be bound to this FName before setting its value.

```csharp
public FName()
```

## Methods

### **ToString()**

Converts this FName instance into a human-readable string. This is the inverse of [FName.FromString(INameMap, String)](./uassetapi.unrealtypes.fname.md#fromstringinamemap-string).

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The human-readable string that represents this FName.

### **FromStringFragments(INameMap, String, String&, Int32&)**

```csharp
internal static void FromStringFragments(INameMap asset, string val, String& str, Int32& num)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>

`val` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`str` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br>

`num` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

### **IsFromStringValid(INameMap, String)**

```csharp
public static bool IsFromStringValid(INameMap asset, string val)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>

`val` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **FromString(INameMap, String)**

Converts a human-readable string into an FName instance. This is the inverse of [FName.ToString()](./uassetapi.unrealtypes.fname.md#tostring).

```csharp
public static FName FromString(INameMap asset, string val)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>
The asset that the new FName will be bound to.

`val` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The human-readable string to convert into an FName instance.

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>
An FName instance that this string represents.

### **Transfer(INameMap)**

Creates a new FName with the same string value and number as the current instance but is bound to a different asset.

```csharp
public FName Transfer(INameMap newAsset)
```

#### Parameters

`newAsset` [INameMap](./uassetapi.inamemap.md)<br>
The asset to bound the new FName to.

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>
An equivalent FName bound to a different asset.

### **DefineDummy(INameMap, FString, Int32)**

Creates a new dummy FName.
 This can be used for cases where a valid FName must be produced without referencing a specific asset's name map.



USE WITH CAUTION! UAssetAPI must never attempt to serialize a dummy FName to disk.

```csharp
public static FName DefineDummy(INameMap asset, FString val, int number)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>
The asset that this FName is bound to.

`val` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The FString that the FName's value will be, verbatim.

`number` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The instance number of the new FName.

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>
A dummy FName instance that represents the string.

### **DefineDummy(INameMap, String, Int32)**

Creates a new dummy FName.
 This can be used for cases where a valid FName must be produced without referencing a specific asset's name map.



USE WITH CAUTION! UAssetAPI must never attempt to serialize a dummy FName to disk.

```csharp
public static FName DefineDummy(INameMap asset, string val, int number)
```

#### Parameters

`asset` [INameMap](./uassetapi.inamemap.md)<br>
The asset that this FName is bound to.

`val` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The string literal that the FName's value will be, verbatim.

`number` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The instance number of the new FName.

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>
A dummy FName instance that represents the string.

### **Equals(Object)**

```csharp
public bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetHashCode()**

```csharp
public int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Clone()**

```csharp
public object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
