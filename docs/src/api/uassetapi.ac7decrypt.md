# AC7Decrypt

Namespace: UAssetAPI

Decryptor for Ace Combat 7 assets.

```csharp
public class AC7Decrypt
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AC7Decrypt](./uassetapi.ac7decrypt.md)

## Constructors

### **AC7Decrypt()**

```csharp
public AC7Decrypt()
```

## Methods

### **Decrypt(String, String)**

Decrypts an Ace Combat 7 encrypted asset on disk.

```csharp
public void Decrypt(string input, string output)
```

#### Parameters

`input` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to an encrypted asset on disk.

`output` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path that the decrypted asset should be saved to.

### **Encrypt(String, String)**

Encrypts an Ace Combat 7 encrypted asset on disk.

```csharp
public void Encrypt(string input, string output)
```

#### Parameters

`input` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to a decrypted asset on disk.

`output` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path that the encrypted asset should be saved to.

### **DecryptUAssetBytes(Byte[], AC7XorKey)**

```csharp
public Byte[] DecryptUAssetBytes(Byte[] uasset, AC7XorKey xorkey)
```

#### Parameters

`uasset` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`xorkey` [AC7XorKey](./uassetapi.ac7xorkey.md)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **EncryptUAssetBytes(Byte[], AC7XorKey)**

```csharp
public Byte[] EncryptUAssetBytes(Byte[] uasset, AC7XorKey xorkey)
```

#### Parameters

`uasset` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`xorkey` [AC7XorKey](./uassetapi.ac7xorkey.md)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **DecryptUexpBytes(Byte[], AC7XorKey)**

```csharp
public Byte[] DecryptUexpBytes(Byte[] uexp, AC7XorKey xorkey)
```

#### Parameters

`uexp` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`xorkey` [AC7XorKey](./uassetapi.ac7xorkey.md)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **EncryptUexpBytes(Byte[], AC7XorKey)**

```csharp
public Byte[] EncryptUexpBytes(Byte[] uexp, AC7XorKey xorkey)
```

#### Parameters

`uexp` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`xorkey` [AC7XorKey](./uassetapi.ac7xorkey.md)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>
