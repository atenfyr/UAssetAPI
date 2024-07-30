# UAPUtils

Namespace: UAssetAPI

```csharp
public static class UAPUtils
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UAPUtils](./uassetapi.uaputils.md)

## Fields

### **CurrentCommit**

```csharp
public static string CurrentCommit;
```

## Methods

### **SerializeJson(Object, Boolean)**

```csharp
public static string SerializeJson(object obj, bool isFormatted)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

`isFormatted` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FindAllInstances&lt;T&gt;(Object)**

```csharp
public static List<T> FindAllInstances<T>(object parent)
```

#### Type Parameters

`T`<br>

#### Parameters

`parent` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

List&lt;T&gt;<br>

### **Clamp&lt;T&gt;(T, T, T)**

```csharp
public static T Clamp<T>(T val, T min, T max)
```

#### Type Parameters

`T`<br>

#### Parameters

`val` T<br>

`min` T<br>

`max` T<br>

#### Returns

T<br>

### **GetOrderedFields&lt;T&gt;()**

```csharp
public static FieldInfo[] GetOrderedFields<T>()
```

#### Type Parameters

`T`<br>

#### Returns

[FieldInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **GetOrderedFields(Type)**

```csharp
public static FieldInfo[] GetOrderedFields(Type t)
```

#### Parameters

`t` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[FieldInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo)<br>

### **GetOrderedMembers&lt;T&gt;()**

```csharp
public static MemberInfo[] GetOrderedMembers<T>()
```

#### Type Parameters

`T`<br>

#### Returns

[MemberInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo)<br>

### **GetOrderedMembers(Type)**

```csharp
public static MemberInfo[] GetOrderedMembers(Type t)
```

#### Parameters

`t` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[MemberInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo)<br>

### **GetValue(MemberInfo, Object)**

```csharp
public static object GetValue(MemberInfo memberInfo, object forObject)
```

#### Parameters

`memberInfo` [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo)<br>

`forObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **SetValue(MemberInfo, Object, Object)**

```csharp
public static void SetValue(MemberInfo memberInfo, object forObject, object forVal)
```

#### Parameters

`memberInfo` [MemberInfo](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo)<br>

`forObject` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

`forVal` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **GetImportNameReferenceWithoutZero(Int32, UAsset)**

```csharp
public static FString GetImportNameReferenceWithoutZero(int j, UAsset asset)
```

#### Parameters

`j` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **InterpretAsGuidAndConvertToUnsignedInts(String)**

```csharp
public static UInt32[] InterpretAsGuidAndConvertToUnsignedInts(string value)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[UInt32[]](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **ConvertStringToByteArray(String)**

```csharp
public static Byte[] ConvertStringToByteArray(string val)
```

#### Parameters

`val` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **ToUnsignedInts(Guid)**

```csharp
public static UInt32[] ToUnsignedInts(Guid value)
```

#### Parameters

`value` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[UInt32[]](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **GUID(UInt32, UInt32, UInt32, UInt32)**

```csharp
public static Guid GUID(uint value1, uint value2, uint value3, uint value4)
```

#### Parameters

`value1` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`value2` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`value3` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`value4` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

### **ConvertToGUID(String)**

```csharp
public static Guid ConvertToGUID(string GuidString)
```

#### Parameters

`GuidString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

### **ConvertToString(Guid)**

```csharp
public static string ConvertToString(Guid val)
```

#### Parameters

`val` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ConvertHexStringToByteArray(String)**

```csharp
public static Byte[] ConvertHexStringToByteArray(string hexString)
```

#### Parameters

`hexString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **AlignPadding(Int64, Int32)**

```csharp
public static long AlignPadding(long pos, int align)
```

#### Parameters

`pos` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`align` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **AlignPadding(Int32, Int32)**

```csharp
public static int AlignPadding(int pos, int align)
```

#### Parameters

`pos` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`align` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **DivideAndRoundUp(Int32, Int32)**

```csharp
public static int DivideAndRoundUp(int a, int b)
```

#### Parameters

`a` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`b` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FixDirectorySeparatorsForDisk(String)**

```csharp
public static string FixDirectorySeparatorsForDisk(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SortByDependencies&lt;T&gt;(IEnumerable&lt;T&gt;, IDictionary&lt;T, IList&lt;T&gt;&gt;)**

```csharp
public static List<T> SortByDependencies<T>(IEnumerable<T> allExports, IDictionary<T, IList<T>> dependencies)
```

#### Type Parameters

`T`<br>

#### Parameters

`allExports` IEnumerable&lt;T&gt;<br>

`dependencies` IDictionary&lt;T, IList&lt;T&gt;&gt;<br>

#### Returns

List&lt;T&gt;<br>
