# TMap&lt;TKey, TValue&gt;

Namespace: UAssetAPI.UnrealTypes

A dictionary object that allows rapid hash lookups using keys, but also
 maintains the key insertion order so that values can be retrieved by
 key index.

```csharp
public class TMap<TKey, TValue> : IOrderedDictionary`2, , , , System.Collections.IEnumerable, System.Collections.Specialized.IOrderedDictionary, System.Collections.IDictionary, System.Collections.ICollection
```

#### Type Parameters

`TKey`<br>

`TValue`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TMap&lt;TKey, TValue&gt;](./uassetapi.unrealtypes.tmap-2.md)<br>
Implements IOrderedDictionary&lt;TKey, TValue&gt;, IDictionary&lt;TKey, TValue&gt;, ICollection&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;, IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable), IOrderedDictionary, [IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary), [ICollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection)

## Properties

### **Item**

```csharp
public TValue Item { get; set; }
```

#### Property Value

TValue<br>

### **Item**

```csharp
public TValue Item { get; set; }
```

#### Property Value

TValue<br>

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
public ICollection<TKey> Keys { get; }
```

#### Property Value

ICollection&lt;TKey&gt;<br>

### **Values**

Gets all the values in the ordered dictionary in their proper order.

```csharp
public ICollection<TValue> Values { get; }
```

#### Property Value

ICollection&lt;TValue&gt;<br>

### **Comparer**

Gets the key comparer for this dictionary

```csharp
public IEqualityComparer<TKey> Comparer { get; private set; }
```

#### Property Value

IEqualityComparer&lt;TKey&gt;<br>

## Constructors

### **TMap()**

```csharp
public TMap()
```

### **TMap(IEqualityComparer&lt;TKey&gt;)**

```csharp
public TMap(IEqualityComparer<TKey> comparer)
```

#### Parameters

`comparer` IEqualityComparer&lt;TKey&gt;<br>

### **TMap(IOrderedDictionary&lt;TKey, TValue&gt;)**

```csharp
public TMap(IOrderedDictionary<TKey, TValue> dictionary)
```

#### Parameters

`dictionary` IOrderedDictionary&lt;TKey, TValue&gt;<br>

### **TMap(IOrderedDictionary&lt;TKey, TValue&gt;, IEqualityComparer&lt;TKey&gt;)**

```csharp
public TMap(IOrderedDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
```

#### Parameters

`dictionary` IOrderedDictionary&lt;TKey, TValue&gt;<br>

`comparer` IEqualityComparer&lt;TKey&gt;<br>

### **TMap(IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;)**

```csharp
public TMap(IEnumerable<KeyValuePair<TKey, TValue>> items)
```

#### Parameters

`items` IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;<br>

### **TMap(IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;, IEqualityComparer&lt;TKey&gt;)**

```csharp
public TMap(IEnumerable<KeyValuePair<TKey, TValue>> items, IEqualityComparer<TKey> comparer)
```

#### Parameters

`items` IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;<br>

`comparer` IEqualityComparer&lt;TKey&gt;<br>

## Methods

### **Add(TKey, TValue)**

Adds the specified key and value to the dictionary.

```csharp
public void Add(TKey key, TValue value)
```

#### Parameters

`key` TKey<br>
The key of the element to add.

`value` TValue<br>
The value of the element to add. The value can be null for reference types.

### **Clear()**

Removes all keys and values from this object.

```csharp
public void Clear()
```

### **Insert(Int32, TKey, TValue)**

Inserts a new key-value pair at the index specified.

```csharp
public void Insert(int index, TKey key, TValue value)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The insertion index. This value must be between 0 and the count of items in this object.

`key` TKey<br>
A unique key for the element to add

`value` TValue<br>
The value of the element to add. Can be null for reference types.

### **IndexOf(TKey)**

Gets the index of the key specified.

```csharp
public int IndexOf(TKey key)
```

#### Parameters

`key` TKey<br>
The key whose index will be located

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Returns the index of the key specified if found. Returns -1 if the key could not be located.

### **ContainsValue(TValue)**

Determines whether this object contains the specified value.

```csharp
public bool ContainsValue(TValue value)
```

#### Parameters

`value` TValue<br>
The value to locate in this object.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the value is found. False otherwise.

### **ContainsValue(TValue, IEqualityComparer&lt;TValue&gt;)**

Determines whether this object contains the specified value.

```csharp
public bool ContainsValue(TValue value, IEqualityComparer<TValue> comparer)
```

#### Parameters

`value` TValue<br>
The value to locate in this object.

`comparer` IEqualityComparer&lt;TValue&gt;<br>
The equality comparer used to locate the specified value in this object.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the value is found. False otherwise.

### **ContainsKey(TKey)**

Determines whether this object contains the specified key.

```csharp
public bool ContainsKey(TKey key)
```

#### Parameters

`key` TKey<br>
The key to locate in this object.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the key is found. False otherwise.

### **GetItem(Int32)**

Returns the KeyValuePair at the index specified.

```csharp
public KeyValuePair<TKey, TValue> GetItem(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the KeyValuePair desired

#### Returns

KeyValuePair&lt;TKey, TValue&gt;<br>

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>
Thrown when the index specified does not refer to a KeyValuePair in this object

### **SetItem(Int32, TValue)**

Sets the value at the index specified.

```csharp
public void SetItem(int index, TValue value)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the value desired

`value` TValue<br>
The value to set

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>
Thrown when the index specified does not refer to a KeyValuePair in this object

### **GetEnumerator()**

Returns an enumerator that iterates through all the KeyValuePairs in this object.

```csharp
public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
```

#### Returns

IEnumerator&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;<br>

### **Remove(TKey)**

Removes the key-value pair for the specified key.

```csharp
public bool Remove(TKey key)
```

#### Parameters

`key` TKey<br>
The key to remove from the dictionary.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the item specified existed and the removal was successful. False otherwise.

### **RemoveAt(Int32)**

Removes the key-value pair at the specified index.

```csharp
public void RemoveAt(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the key-value pair to remove from the dictionary.

### **GetValue(TKey)**

Gets the value associated with the specified key.

```csharp
public TValue GetValue(TKey key)
```

#### Parameters

`key` TKey<br>
The key associated with the value to get.

#### Returns

TValue<br>

### **SetValue(TKey, TValue)**

Sets the value associated with the specified key.

```csharp
public void SetValue(TKey key, TValue value)
```

#### Parameters

`key` TKey<br>
The key associated with the value to set.

`value` TValue<br>
The the value to set.

### **TryGetValue(TKey, TValue&)**

Tries to get the value associated with the specified key.

```csharp
public bool TryGetValue(TKey key, TValue& value)
```

#### Parameters

`key` TKey<br>
The key of the desired element.

`value` TValue&<br>
When this method returns, contains the value associated with the specified key if
 that key was found. Otherwise it will contain the default value for parameter's type.
 This parameter should be provided uninitialized.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the value was found. False otherwise.

**Remarks:**



### **SortKeys()**

```csharp
public void SortKeys()
```

### **SortKeys(IComparer&lt;TKey&gt;)**

```csharp
public void SortKeys(IComparer<TKey> comparer)
```

#### Parameters

`comparer` IComparer&lt;TKey&gt;<br>

### **SortKeys(Comparison&lt;TKey&gt;)**

```csharp
public void SortKeys(Comparison<TKey> comparison)
```

#### Parameters

`comparison` Comparison&lt;TKey&gt;<br>

### **SortValues()**

```csharp
public void SortValues()
```

### **SortValues(IComparer&lt;TValue&gt;)**

```csharp
public void SortValues(IComparer<TValue> comparer)
```

#### Parameters

`comparer` IComparer&lt;TValue&gt;<br>

### **SortValues(Comparison&lt;TValue&gt;)**

```csharp
public void SortValues(Comparison<TValue> comparison)
```

#### Parameters

`comparison` Comparison&lt;TValue&gt;<br>
