# More Examples

This page contains several more examples of UAssetAPI usage for completing specific tasks.

### A simple, complete example
```cs
UAsset myAsset = new UAsset("C:\\plwp_6aam_a0.uasset", EngineVersion.VER_UE4_18);

// Find the export with the name "Default__plwp_6aam_a0_C"
NormalExport cdoExport = (NormalExport)myAsset["Default__plwp_6aam_a0_C"];
// Add/replace a property called SpeedMaximum
cdoExport["SpeedMaximum"] = new FloatPropertyData() { Value = 999999 };
// or, modify it directly
FloatPropertyData SpeedMaximum = (FloatPropertyData)cdoExport["SpeedMaximum"];
SpeedMaximum.Value = 999999;

myAsset.Write("C:\\NEW.uasset");
```

### Finding specific exports
```cs
UAsset myAsset = new UAsset("C:\\plwp_6aam_a0.uasset", EngineVersion.VER_UE4_18);

// We can find specific exports by index:
Export cdo = myAsset.Exports[1]; // Export 2; here, indexing from 0
// or like this:
cdo = new FPackageIndex(2).ToExport(myAsset); // also Export 2; FPackageIndex uses negative numbers for imports, 0 for null, and positive numbers for exports
// or, by ObjectName:
cdo = myAsset["Default__plwp_6aam_a0_C"]; // you can find this string value e.g. in UAssetGUI
cdo = myAsset[new FName(myAsset, "Default__plwp_6aam_a0_C")];
// or, to locate the ClassDefaultObject:
foreach (Export exp in myAsset.Exports)
{
    if (exp.ObjectFlags.HasFlag(EObjectFlags.RF_ClassDefaultObject))
    {
        cdo = exp;
        break;
    }
}
// or, based on any property; maybe by SerialSize (length on disk):
long maxSerialSize = -1;
foreach (Export exp in myAsset.Exports)
{
    if (exp.SerialSize > maxSerialSize)
    {
        maxSerialSize = exp.SerialSize;
        cdo = exp;
    }
}
```

### Accessing basic export types and property types
```cs
UAsset myAsset = new UAsset("C:\\plwp_6aam_a0.uasset", EngineVersion.VER_UE4_18);
Export exp = myAsset["Default__plwp_6aam_a0_C"];

// Export contains all fields contained within UAssetGUI's "Export Information"
// to manipulate data under "Export Data," you generally need to cast it to a child type

// NormalExport contains most all "normal" data, i.e. standard tagged properties
if (exp is NormalExport normalExport)
{
    for (int i = 0; i < normalExport.Data.Count; i++)
    {
        PropertyData prop = normalExport.Data[i];
        Console.WriteLine(prop.Name.ToString() + ": " + prop.PropertyType.ToString());

        // you can access prop.Value for many types, but for other types, you can cast to a child type and access other fields
        if (prop is FloatPropertyData floatProp) floatProp.Value = 60; // change all floats to = 60

        // ArrayPropertyData.Value is a PropertyData[] array, entries referenced by index
        // StructPropertyData.Value is a List<PropertyData>, or you can index StructPropertyData directly
        if (prop is ArrayPropertyData arrProp)
        {
            for (int j = 0; j < arrProp.Value.Length; j++)
            {
                PropertyData prop2 = arrProp.Value[j];
                Console.WriteLine(prop2.Name.ToString() + ": " + prop2.PropertyType.ToString());
                // etc.
                // note that arrays and structs can contain arrays and structs too...
            }
        }

        if (prop is StructPropertyData structProp)
        {
            for (int j = 0; j < structProp.Value.Count; j++)
            {
                PropertyData prop2 = structProp.Value[j];
                Console.WriteLine(prop2.Name.ToString() + ": " + prop2.PropertyType.ToString());
                // etc.
                // note that arrays and structs can contain arrays and structs too...
            }

            // or:
            // PropertyData prop2 = structProp["PropertyNameHere"];
        }
    }
}

// DataTableExport is a NormalExport, but also contains entries in DataTables
if (exp is DataTableExport dtExport)
{
    // dtExport.Data exists, but it typically only contains struct type information
    // to access other entries, use:
    List<StructPropertyData> entries = dtExport.Table.Data;

    // etc.
}

// RawExport is an export that failed to parse for some reason, but you can still access and modify its binary data
if (exp is RawExport rawExport)
{
    byte[] rawData = rawExport.Data;

    // etc.
}

// see other examples for more advanced export types!
```

### Duplicating properties
```cs
UAsset myAsset = new UAsset("C:\\plwp_6aam_a0.uasset", EngineVersion.VER_UE4_18);
NormalExport cdoExport = (NormalExport)myAsset["Default__plwp_6aam_a0_C"];

FloatPropertyData targetProp = (FloatPropertyData)cdoExport["SpeedMaximum"];

// if we try something like:

/*
FloatPropertyData newProp = targetProp;
newProp.Value = 999999;
*/

// we'll accidentally change the value of targetProp too!
// we can duplicate this property using .Clone() instead:

FloatPropertyData newProp = (FloatPropertyData)targetProp.Clone();
newProp.Value = 999999;
cdoExport["SpeedMaximum2"] = newProp;

// .Clone() performs a deep copy, so you can e.g. clone a StructProperty and modify child properties freely
// .Clone() on an Export directly, however, is not implemented properly for child export types (e.g. the .Data list of a NormalExport is not cloned)
```

### Read assets that use unversioned properties
```cs
// to read an asset that uses unversioned properties, you must first source a .usmap mappings file for the game the asset is from, e.g. with UE4SS
// you can read a mappings file with the Usmap class, and pass it into the UAsset constructor
Usmap mappings = new Usmap("C:\\MyGame.usmap");
UAsset myAsset = new UAsset("C:\\my_asset.uasset", EngineVersion.VER_UE5_3, mappings);

// then, read and write data as normal
// myAsset.HasUnversionedProperties will return true

// notes for the curious:
// * using the FName constructor adds new entries to the name map, which is often frivolous with unversioned properties; if you care, use FName.DefineDummy instead, but if UAssetAPI tries to write a dummy FName to disk it will throw an exception
// * UAssetAPI only supports reading .usmap files, not writing
// * UAssetAPI supports .usmap versions 0 through 3, uncompressed and zstandard-compressed files, and PPTH/EATR/ENVP extensions
```

### Interface with JSON
```cs
UAsset myAsset = new UAsset("C:\\plwp_6aam_a0.uasset", EngineVersion.VER_UE4_18);

// write asset to JSON
string jsonSerializedAsset = tester.SerializeJson();
File.WriteAllText("C:\\plwp_6aam_a0.json", jsonSerializedAsset);

// read asset back from JSON
UAsset myAsset2 = UAsset.DeserializeJson("C:\\plwp_6aam_a0.json");
// myAsset2 should contain the same information as myAsset

// write asset to binary format
myAsset2.Write("C:\\plwp_6aam_a0_NEW.uasset");
```

### Read and modify blueprint bytecode
```cs
UAsset myAsset = new UAsset("C:\\my_asset.uasset", EngineVersion.VER_UE4_18);

// all StructExport exports can contain blueprint bytecode, let's pretend Export 1 is a StructExport
StructExport myStructExport = (StructExport)myAsset.Exports[0];

KismetExpression[] bytecode = myStructExport.ScriptBytecode;
if (bytecode != null) // bytecode may fail to parse, in which case it will be null and stored raw in ScriptBytecodeRaw
{
    // KismetExpression has many child classes, one child class for each type of instruction
    // as with PropertyData, you can access .RawValue for many instruction types, but you'll need to cast for other kinds of instructions to access specific fields
    foreach (KismetExpression instruction in bytecode)
    {
        Console.WriteLine(instruction.Token.ToString() + ": " + instruction.RawValue.ToString());
    }
}
```