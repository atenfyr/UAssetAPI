# KeyedCollection2&lt;TKey, TItem&gt;

Namespace: UAssetAPI.UnrealTypes

A concrete implementation of the abstract KeyedCollection class using lambdas for the
 implementation.

```csharp
public class KeyedCollection2<TKey, TItem> : , , , , System.Collections.IEnumerable, System.Collections.IList, System.Collections.ICollection, , 
```

#### Type Parameters

`TKey`<br>

`TItem`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → Collection&lt;TItem&gt; → KeyedCollection&lt;TKey, TItem&gt; → [KeyedCollection2&lt;TKey, TItem&gt;](./uassetapi.unrealtypes.keyedcollection2-2.md)<br>
Implements IList&lt;TItem&gt;, ICollection&lt;TItem&gt;, IEnumerable&lt;TItem&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable), [IList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ilist), [ICollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection), IReadOnlyList&lt;TItem&gt;, IReadOnlyCollection&lt;TItem&gt;<br>
Attributes [DefaultMemberAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.defaultmemberattribute), [DebuggerTypeProxyAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.debuggertypeproxyattribute), [DebuggerDisplayAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.debuggerdisplayattribute), [DebuggerTypeProxyAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.debuggertypeproxyattribute), [DebuggerDisplayAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.debuggerdisplayattribute)

## Properties

### **Comparer**

```csharp
public IEqualityComparer<TKey> Comparer { get; }
```

#### Property Value

IEqualityComparer&lt;TKey&gt;<br>

### **Item**

```csharp
public TItem Item { get; }
```

#### Property Value

TItem<br>

### **Dictionary**

```csharp
protected IDictionary<TKey, TItem> Dictionary { get; }
```

#### Property Value

IDictionary&lt;TKey, TItem&gt;<br>

### **Count**

```csharp
public int Count { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Items**

```csharp
protected IList<TItem> Items { get; }
```

#### Property Value

IList&lt;TItem&gt;<br>

### **Item**

```csharp
public TItem Item { get; set; }
```

#### Property Value

TItem<br>

## Constructors

### **KeyedCollection2(Func&lt;TItem, TKey&gt;)**

```csharp
public KeyedCollection2(Func<TItem, TKey> getKeyForItemFunction)
```

#### Parameters

`getKeyForItemFunction` Func&lt;TItem, TKey&gt;<br>

### **KeyedCollection2(Func&lt;TItem, TKey&gt;, IEqualityComparer&lt;TKey&gt;)**

```csharp
public KeyedCollection2(Func<TItem, TKey> getKeyForItemDelegate, IEqualityComparer<TKey> comparer)
```

#### Parameters

`getKeyForItemDelegate` Func&lt;TItem, TKey&gt;<br>

`comparer` IEqualityComparer&lt;TKey&gt;<br>

## Methods

### **GetKeyForItem(TItem)**

```csharp
protected TKey GetKeyForItem(TItem item)
```

#### Parameters

`item` TItem<br>

#### Returns

TKey<br>

### **SortByKeys()**

```csharp
public void SortByKeys()
```

### **SortByKeys(IComparer&lt;TKey&gt;)**

```csharp
public void SortByKeys(IComparer<TKey> keyComparer)
```

#### Parameters

`keyComparer` IComparer&lt;TKey&gt;<br>

### **SortByKeys(Comparison&lt;TKey&gt;)**

```csharp
public void SortByKeys(Comparison<TKey> keyComparison)
```

#### Parameters

`keyComparison` Comparison&lt;TKey&gt;<br>

### **Sort()**

```csharp
public void Sort()
```

### **Sort(Comparison&lt;TItem&gt;)**

```csharp
public void Sort(Comparison<TItem> comparison)
```

#### Parameters

`comparison` Comparison&lt;TItem&gt;<br>

### **Sort(IComparer&lt;TItem&gt;)**

```csharp
public void Sort(IComparer<TItem> comparer)
```

#### Parameters

`comparer` IComparer&lt;TItem&gt;<br>
