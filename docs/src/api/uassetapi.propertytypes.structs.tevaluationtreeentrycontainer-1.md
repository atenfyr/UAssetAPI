# TEvaluationTreeEntryContainer&lt;T&gt;

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct TEvaluationTreeEntryContainer<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [TEvaluationTreeEntryContainer&lt;T&gt;](./uassetapi.propertytypes.structs.tevaluationtreeentrycontainer-1.md)

## Fields

### **Entries**

List of allocated entries for each allocated entry. Should only ever grow, never shrink. Shrinking would cause previously established handles to become invalid. */

```csharp
public FEntry[] Entries;
```

### **Items**

Linear array of allocated entry contents. Once allocated, indices are never invalidated until Compact is called. Entries needing more capacity are re-allocated on the end of the array.

```csharp
public T[] Items;
```
