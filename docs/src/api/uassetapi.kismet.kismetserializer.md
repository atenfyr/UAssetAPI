# KismetSerializer

Namespace: UAssetAPI.Kismet

```csharp
public static class KismetSerializer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetSerializer](./uassetapi.kismet.kismetserializer.md)

## Fields

### **asset**

```csharp
public static UAsset asset;
```

## Methods

### **SerializeScript(KismetExpression[])**

```csharp
public static JArray SerializeScript(KismetExpression[] code)
```

#### Parameters

`code` [KismetExpression[]](./uassetapi.kismet.bytecode.kismetexpression.md)<br>

#### Returns

JArray<br>

### **GetName(Int32)**

```csharp
public static string GetName(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetClassIndex()**

```csharp
public static int GetClassIndex()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **GetFullName(Int32, Boolean)**

```csharp
public static string GetFullName(int index, bool alt)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`alt` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetParentName(Int32)**

```csharp
public static string GetParentName(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FindProperty(Int32, FName, FProperty&)**

```csharp
public static bool FindProperty(int index, FName propname, FProperty& property)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`propname` [FName](./uassetapi.unrealtypes.fname.md)<br>

`property` [FProperty&](./uassetapi.fieldtypes.fproperty&.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetPropertyCategoryInfo(FProperty)**

```csharp
public static FEdGraphPinType GetPropertyCategoryInfo(FProperty prop)
```

#### Parameters

`prop` [FProperty](./uassetapi.fieldtypes.fproperty.md)<br>

#### Returns

[FEdGraphPinType](./uassetapi.kismet.kismetserializer.fedgraphpintype.md)<br>

### **FillSimpleMemberReference(Int32)**

```csharp
public static FSimpleMemberReference FillSimpleMemberReference(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[FSimpleMemberReference](./uassetapi.kismet.kismetserializer.fsimplememberreference.md)<br>

### **SerializeGraphPinType(FEdGraphPinType)**

```csharp
public static JObject SerializeGraphPinType(FEdGraphPinType pin)
```

#### Parameters

`pin` [FEdGraphPinType](./uassetapi.kismet.kismetserializer.fedgraphpintype.md)<br>

#### Returns

JObject<br>

### **ConvertPropertyToPinType(FProperty)**

```csharp
public static FEdGraphPinType ConvertPropertyToPinType(FProperty property)
```

#### Parameters

`property` [FProperty](./uassetapi.fieldtypes.fproperty.md)<br>

#### Returns

[FEdGraphPinType](./uassetapi.kismet.kismetserializer.fedgraphpintype.md)<br>

### **SerializePropertyPointer(KismetPropertyPointer, String[])**

```csharp
public static JProperty[] SerializePropertyPointer(KismetPropertyPointer pointer, String[] names)
```

#### Parameters

`pointer` [KismetPropertyPointer](./uassetapi.kismet.bytecode.kismetpropertypointer.md)<br>

`names` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

JProperty[]<br>

### **SerializeExpression(KismetExpression, Int32&, Boolean)**

```csharp
public static JObject SerializeExpression(KismetExpression expression, Int32& index, bool addindex)
```

#### Parameters

`expression` [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md)<br>

`index` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

`addindex` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

JObject<br>

### **ReadString(KismetExpression, Int32&)**

```csharp
public static string ReadString(KismetExpression expr, Int32& index)
```

#### Parameters

`expr` [KismetExpression](./uassetapi.kismet.bytecode.kismetexpression.md)<br>

`index` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
