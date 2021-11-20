# UAssetAPI
This is a work-in-progress .NET API to facilitate reading and modifying Unreal Engine 4 game assets.

## Getting Started
If you'd like to compile UAssetAPI for yourself, read on:

### Prerequisites
* Visual Studio 2017 or later
* Git

### Compilation
1. Clone the UAssetAPI repository:

```sh
git clone https://github.com/atenfyr/UAssetAPI.git
```

4. Open the `UAssetAPI.sln` solution file within the newly-created UAssetAPI directory in Visual Studio, right-click on the solution name in the Solution Explorer, and press "Restore Nuget Packages."

6. Press F6 and right-click the solution name in the Solution Explorer and press "Build Solution" to compile UAssetAPI.

## Contributing
Contributions are always welcome to this repository, and they're what make the open source community so great. Any contributions, whether through pull requests or issues, that you make are greatly appreciated.

If you find an Unreal Engine 4 .uasset that has its `VerifyBinaryEquality()` method return false (or display "failed to maintain binary equality" within [UAssetGUI](https://github.com/atenfyr/UAssetGUI)), feel free to submit an issue here with a copy of the asset in question along with the name of the game and the Unreal version that it was cooked with and I will try to push a commit to make it verify parsing.

## A quick example
```cs
// The goal is to modify a ChildSlotComponent in the Seat3_Large asset in order to rotate and translate the slots so that the seat faces forward
UAsset y = new UAsset(@"C:\Users\Alexandros\Desktop\astroneer_tinkering\pak\Astro\Content\Components_Large\Seat3_Large.uasset");
Console.WriteLine("Data preserved: " + (y.VerifyParsing() ? "YES" : "NO"));
{
    Export baseUs = y.Exports[17]; // Export 18 is the ChildSlotComponent we want to modify
    if (baseUs is NormalExport us)
    {
        // Here we make a new StructProperty that we'll insert in the ChildSlotComponent category to translate it
        var newStruc = new StructPropertyData("RelativeLocation", y)
        {
            StructType = "Vector",
            Value = new List<PropertyData>
            {
                new VectorPropertyData("Vector", y)
                {
                    Value = new float[] { 0, 175, 100 } // Translation in unreal units (X, Y, Z)
                }
            }
        };

        for (int j = 0; j < us.Data.Count; j++)
        {
            PropertyData me = us.Data[j];
            if (me.Name == "RelativeRotation" && me is StructPropertyData struc)
            {
                IList<PropertyData> cosa = struc.Value;
                for (int m = 0; m < cosa.Count; m++)
                {
                    PropertyData algo = cosa[m];
                    if (algo is RotatorPropertyData)
                    {
                        float[] newRot = ((RotatorPropertyData)algo).Value;
                        newRot[0] += 90; // Rotate 90 degrees pitch
                        newRot[2] += 180; // Rotate 180 degrees roll
                        break;
                    }
                }

                us.Data.Insert(j, newStruc); // The position of the new StructProperty in the category doesn't actually matter
                break;
            }
        }
    }

    // Save to a separate directory for packing
    Directory.CreateDirectory(Path.GetDirectoryName(y.FilePath.Replace("pak", "mod")));
    y.Write(y.FilePath.Replace("pak", "mod"));
}
```
