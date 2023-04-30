# IOrderedDictionary&lt;TKey, TValue&gt;

Namespace: UAssetAPI.UnrealTypes

```csharp
public interface IOrderedDictionary<TKey, TValue> : , , , System.Collections.IEnumerable, System.Collections.Specialized.IOrderedDictionary, System.Collections.IDictionary, System.Collections.ICollection
```

#### Type Parameters

`TKey`<br>

`TValue`<br>

Implements IDictionary&lt;TKey, TValue&gt;, ICollection&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;, IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable), IOrderedDictionary, [IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary), [ICollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection)

## Properties

### **Item**

```csharp
public abstract TValue Item { get; set; }
```

#### Property Value

TValue<br>

### **Item**

```csharp
public abstract TValue Item { get; set; }
```

#### Property Value

TValue<br>

### **Count**

```csharp
public abstract int Count { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Keys**

```csharp
public abstract ICollection<TKey> Keys { get; }
```

#### Property Value

ICollection&lt;TKey&gt;<br>

### **Values**

```csharp
public abstract ICollection<TValue> Values { get; }
```

#### Property Value

ICollection&lt;TValue&gt;<br>

## Methods

### **Add(TKey, TValue)**

```csharp
void Add(TKey key, TValue value)
```

#### Parameters

`key` TKey<br>

`value` TValue<br>

### **Clear()**

```csharp
void Clear()
```

### **Insert(Int32, TKey, TValue)**

```csharp
void Insert(int index, TKey key, TValue value)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`key` TKey<br>

`value` TValue<br>

### **IndexOf(TKey)**

```csharp
int IndexOf(TKey key)
```

#### Parameters

`key` TKey<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ContainsValue(TValue)**

```csharp
bool ContainsValue(TValue value)
```

#### Parameters

`value` TValue<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ContainsValue(TValue, IEqualityComparer&lt;TValue&gt;)**

```csharp
bool ContainsValue(TValue value, IEqualityComparer<TValue> comparer)
```

#### Parameters

`value` TValue<br>

`comparer` IEqualityComparer&lt;TValue&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ContainsKey(TKey)**

```csharp
bool ContainsKey(TKey key)
```

#### Parameters

`key` TKey<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetEnumerator()**

```csharp
IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
```

#### Returns

IEnumerator&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;<br>

### **Remove(TKey)**

```csharp
bool Remove(TKey key)
```

#### Parameters

`key` TKey<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **RemoveAt(Int32)**

```csharp
void RemoveAt(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **TryGetValue(TKey, TValue&)**

```csharp
bool TryGetValue(TKey key, TValue& value)
```

#### Parameters

`key` TKey<br>

`value` TValue&<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetValue(TKey)**

```csharp
TValue GetValue(TKey key)
```

#### Parameters

`key` TKey<br>

#### Returns

TValue<br>

### **SetValue(TKey, TValue)**

```csharp
void SetValue(TKey key, TValue value)
```

#### Parameters

`key` TKey<br>

`value` TValue<br>

### **GetItem(Int32)**

```csharp
KeyValuePair<TKey, TValue> GetItem(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

KeyValuePair&lt;TKey, TValue&gt;<br>

### **SetItem(Int32, TValue)**

```csharp
void SetItem(int index, TValue value)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`value` TValue<br>
