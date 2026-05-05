# CustomVersion

Namespace: UAssetAPI

A custom version. Controls more specific serialization than the main engine object version does.

```csharp
public class CustomVersion : System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CustomVersion](./uassetapi.customversion.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **Name**

```csharp
public FString Name;
```

### **Key**

```csharp
public Guid Key;
```

### **FriendlyName**

```csharp
public string FriendlyName;
```

### **Version**

```csharp
public int Version;
```

### **IsSerialized**

```csharp
public bool IsSerialized;
```

### **GuidToCustomVersionStringMap**

Static map of custom version GUIDs to the object or enum that they represent in the Unreal Engine. This list is not necessarily exhaustive, so feel free to add to it if need be.

```csharp
public static Dictionary<Guid, string> GuidToCustomVersionStringMap;
```

### **UnusedCustomVersionKey**

A GUID that represents an unused custom version.

```csharp
public static Guid UnusedCustomVersionKey;
```

## Constructors

### **CustomVersion(String, Int32)**

Initializes a new instance of the [CustomVersion](./uassetapi.customversion.md) class given an object or enum name and a version number.

```csharp
public CustomVersion(string friendlyName, int version)
```

#### Parameters

`friendlyName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The friendly name to use when initializing this custom version.

`version` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The version number to use when initializing this custom version.

### **CustomVersion(Guid, Int32)**

Initializes a new instance of the [CustomVersion](./uassetapi.customversion.md) class given a custom version GUID and a version number.

```csharp
public CustomVersion(Guid key, int version)
```

#### Parameters

`key` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
The GUID to use when initializing this custom version.

`version` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The version number to use when initializing this custom version.

### **CustomVersion()**

Initializes a new instance of the [CustomVersion](./uassetapi.customversion.md) class.

```csharp
public CustomVersion()
```

## Methods

### **GetCustomVersionFriendlyNameFromGuid(Guid)**

Returns the name of the object or enum that a custom version GUID represents, as specified in [CustomVersion.GuidToCustomVersionStringMap](./uassetapi.customversion.md#guidtocustomversionstringmap).

```csharp
public static string GetCustomVersionFriendlyNameFromGuid(Guid guid)
```

#### Parameters

`guid` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
A GUID that represents a custom version.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A string that represents the friendly name of the corresponding custom version.

### **GetCustomVersionGuidFromFriendlyName(String)**

Returns the GUID of the custom version that the object or enum name provided represents.

```csharp
public static Guid GetCustomVersionGuidFromFriendlyName(string friendlyName)
```

#### Parameters

`friendlyName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of a custom version object or enum.

#### Returns

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
A GUID that represents the custom version

### **SetIsSerialized(Boolean)**

```csharp
public CustomVersion SetIsSerialized(bool val)
```

#### Parameters

`val` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[CustomVersion](./uassetapi.customversion.md)<br>

### **Clone()**

```csharp
public object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
