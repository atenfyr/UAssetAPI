# UsmapEnum

Namespace: UAssetAPI.Unversioned

```csharp
public class UsmapEnum
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UsmapEnum](./uassetapi.unversioned.usmapenum.md)

## Properties

### **Name**

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ModulePath**

```csharp
public string ModulePath { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EnumFlags**

```csharp
public int EnumFlags { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Values**

```csharp
public ConcurrentDictionary<long, string> Values { get; set; }
```

#### Property Value

ConcurrentDictionary&lt;Int64, String&gt;<br>

## Constructors

### **UsmapEnum(String, ConcurrentDictionary&lt;Int64, String&gt;)**

```csharp
public UsmapEnum(string name, ConcurrentDictionary<long, string> values)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`values` ConcurrentDictionary&lt;Int64, String&gt;<br>

### **UsmapEnum()**

```csharp
public UsmapEnum()
```
