# DictionaryEnumerator&lt;TKey, TValue&gt;

Namespace: UAssetAPI.UnrealTypes

```csharp
public class DictionaryEnumerator<TKey, TValue> : System.Collections.IDictionaryEnumerator, System.Collections.IEnumerator, System.IDisposable
```

#### Type Parameters

`TKey`<br>

`TValue`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DictionaryEnumerator&lt;TKey, TValue&gt;](./uassetapi.unrealtypes.dictionaryenumerator-2.md)<br>
Implements [IDictionaryEnumerator](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionaryenumerator), [IEnumerator](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator), [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **Entry**

```csharp
public DictionaryEntry Entry { get; }
```

#### Property Value

[DictionaryEntry](https://docs.microsoft.com/en-us/dotnet/api/system.collections.dictionaryentry)<br>

### **Key**

```csharp
public object Key { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **Value**

```csharp
public object Value { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **Current**

```csharp
public object Current { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **DictionaryEnumerator(IDictionary&lt;TKey, TValue&gt;)**

```csharp
public DictionaryEnumerator(IDictionary<TKey, TValue> value)
```

#### Parameters

`value` IDictionary&lt;TKey, TValue&gt;<br>

## Methods

### **Dispose()**

```csharp
public void Dispose()
```

### **Reset()**

```csharp
public void Reset()
```

### **MoveNext()**

```csharp
public bool MoveNext()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
