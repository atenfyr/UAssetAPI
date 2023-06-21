# IntroducedAttribute

Namespace: UAssetAPI.CustomVersions

Represents the engine version at the time that a custom version was implemented.

```csharp
public class IntroducedAttribute : System.Attribute
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Attribute](https://docs.microsoft.com/en-us/dotnet/api/system.attribute) → [IntroducedAttribute](./uassetapi.customversions.introducedattribute.md)

## Fields

### **IntroducedVersion**

```csharp
public EngineVersion IntroducedVersion;
```

## Properties

### **TypeId**

```csharp
public object TypeId { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **IntroducedAttribute(EngineVersion)**

```csharp
public IntroducedAttribute(EngineVersion introducedVersion)
```

#### Parameters

`introducedVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
