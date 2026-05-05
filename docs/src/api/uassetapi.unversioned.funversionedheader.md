# FUnversionedHeader

Namespace: UAssetAPI.Unversioned

List of serialized property indices and which of them are non-zero.
 Serialized as a stream of 16-bit skip-x keep-y fragments and a zero bitmask.

```csharp
public class FUnversionedHeader
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FUnversionedHeader](./uassetapi.unversioned.funversionedheader.md)

## Fields

### **Fragments**

```csharp
public LinkedList<FFragment> Fragments;
```

### **CurrentFragment**

```csharp
public LinkedListNode<FFragment> CurrentFragment;
```

### **UnversionedPropertyIndex**

```csharp
public int UnversionedPropertyIndex;
```

### **ZeroMaskIndex**

```csharp
public int ZeroMaskIndex;
```

### **ZeroMaskNum**

```csharp
public uint ZeroMaskNum;
```

### **ZeroMask**

```csharp
public BitArray ZeroMask;
```

### **bHasNonZeroValues**

```csharp
public bool bHasNonZeroValues;
```

## Constructors

### **FUnversionedHeader(AssetBinaryReader)**

```csharp
public FUnversionedHeader(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **FUnversionedHeader()**

```csharp
public FUnversionedHeader()
```

## Methods

### **Read(AssetBinaryReader)**

```csharp
public void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **LoadZeroMaskData(AssetBinaryReader, UInt32)**

```csharp
public void LoadZeroMaskData(AssetBinaryReader reader, uint NumBits)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`NumBits` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **SaveZeroMaskData()**

```csharp
public Byte[] SaveZeroMaskData()
```

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **CheckIfZeroMaskIsAllOnes()**

```csharp
public bool CheckIfZeroMaskIsAllOnes()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

### **HasValues()**

```csharp
public bool HasValues()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **HasNonZeroValues()**

```csharp
public bool HasNonZeroValues()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
