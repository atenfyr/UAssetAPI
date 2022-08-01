# UAssetAPI
A .NET library for reading and writing Unreal Engine 4 game assets.

<img src="https://i.imgur.com/GZbr93m.png" align="center">

## Compilation
If you'd like to compile UAssetAPI for yourself, read on:

### Prerequisites
* Visual Studio 2017 or later
* Git

### Initial Setup
1. Clone the UAssetAPI repository:

```sh
git clone https://github.com/atenfyr/UAssetAPI.git
```

2. Open the `UAssetAPI.sln` solution file within the newly-created UAssetAPI directory in Visual Studio, right-click on the solution name in the Solution Explorer, and press "Restore Nuget Packages."

3. Press F6 and right-click the solution name in the Solution Explorer and press "Build Solution" to compile UAssetAPI.

## Contributing
Any contributions, whether through pull requests or issues, that you make are greatly appreciated.

If you find an Unreal Engine 4 .uasset that has its `VerifyBinaryEquality()` method return false (or display "failed to maintain binary equality" within [UAssetGUI](https://github.com/atenfyr/UAssetGUI)), feel free to submit an issue here with a copy of the asset in question along with the name of the game and the Unreal version that it was cooked with and I will try to push a commit to make it verify parsing.

## License
UAssetAPI and UAssetGUI are distributed under the MIT license, which you can view in detail in the [LICENSE file](LICENSE).
