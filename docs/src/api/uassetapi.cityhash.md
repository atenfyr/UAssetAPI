# CityHash

Namespace: UAssetAPI

```csharp
public class CityHash
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CityHash](./uassetapi.cityhash.md)

## Constructors

### **CityHash()**

```csharp
public CityHash()
```

## Methods

### **CityHash32(Byte*, UInt32)**

```csharp
public static uint CityHash32(Byte* s, uint len)
```

#### Parameters

`s` [Byte*](https://docs.microsoft.com/en-us/dotnet/api/system.byte*)<br>

`len` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **CityHash64(Byte*, UInt32)**

```csharp
public static ulong CityHash64(Byte* s, uint len)
```

#### Parameters

`s` [Byte*](https://docs.microsoft.com/en-us/dotnet/api/system.byte*)<br>

`len` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **CityHash64WithSeed(Byte*, UInt32, UInt64)**

```csharp
public static ulong CityHash64WithSeed(Byte* s, uint len, ulong seed)
```

#### Parameters

`s` [Byte*](https://docs.microsoft.com/en-us/dotnet/api/system.byte*)<br>

`len` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`seed` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **CityHash64WithSeeds(Byte*, UInt32, UInt64, UInt64)**

```csharp
public static ulong CityHash64WithSeeds(Byte* s, uint len, ulong seed0, ulong seed1)
```

#### Parameters

`s` [Byte*](https://docs.microsoft.com/en-us/dotnet/api/system.byte*)<br>

`len` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`seed0` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`seed1` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **CityHash128to64(Uint128_64)**

```csharp
public static ulong CityHash128to64(Uint128_64 x)
```

#### Parameters

`x` [Uint128_64](./uassetapi.cityhash.uint128_64.md)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>
