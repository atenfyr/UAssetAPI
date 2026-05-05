# AC7XorKey

Namespace: UAssetAPI

XOR key for decrypting a particular Ace Combat 7 asset.

```csharp
public class AC7XorKey
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AC7XorKey](./uassetapi.ac7xorkey.md)

## Fields

### **NameKey**

```csharp
public int NameKey;
```

### **Offset**

```csharp
public int Offset;
```

### **pk1**

```csharp
public int pk1;
```

### **pk2**

```csharp
public int pk2;
```

## Constructors

### **AC7XorKey(String)**

Generates an encryption key for a particular asset on disk.

```csharp
public AC7XorKey(string fname)
```

#### Parameters

`fname` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the asset being encrypted on disk without the extension.

## Methods

### **SkipCount(Int32)**

```csharp
public void SkipCount(int count)
```

#### Parameters

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
