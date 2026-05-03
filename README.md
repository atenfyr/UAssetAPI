# UAssetAPI
[![CI Status](https://img.shields.io/github/actions/workflow/status/atenfyr/UAssetAPI/build.yml?label=CI)](https://github.com/atenfyr/UAssetAPI/actions)
[![Issues](https://img.shields.io/github/issues/atenfyr/UAssetAPI.svg?style=flat-square)](https://github.com/atenfyr/UAssetAPI/issues)
[![License](https://img.shields.io/github/license/atenfyr/UAssetAPI.svg?style=flat-square)](https://github.com/atenfyr/UAssetAPI/blob/master/LICENSE.md)

UAssetAPI is a low-level .NET library for reading and writing Unreal Engine game assets.

![Example image of C# source code using UAssetAPI](https://i.imgur.com/GZbr93m.png)

## Features
- Low-level read/write capability for a wide variety of cooked and uncooked .uasset files from ~4.13 to 5.7
- Support for more than 100 property types and 12 export types
- Support for JSON export and import to a proprietary format that maintains binary equality
- Support for reading and writing raw Kismet (blueprint) bytecode
- Reading capability for the unofficial .usmap format to parse ambiguous and unversioned properties
- Robust fail-safes for many properties and exports that fail serialization
- Automatic reflection for new property types in other loaded assemblies

## Usage
To get started with UAssetAPI, visit the [Basic Usage guide](https://atenfyr.github.io/UAssetAPI/guide/basic.html) to get started with performing basic operations on your .uasset files.

Major releases of UAssetAPI are available on NuGet: [https://www.nuget.org/packages/UAssetAPI/](https://www.nuget.org/packages/UAssetAPI/)

UAssetGUI, a graphical wrapper around UAssetAPI which allows you to directly view and modify game assets by hand, is also available and can be downloaded for free on GitHub at [https://github.com/atenfyr/UAssetGUI/releases](https://github.com/atenfyr/UAssetGUI/releases).

## Contributing
Any contributions, whether through pull requests or issues, that you may make are greatly appreciated.

If you have an Unreal Engine .uasset file that displays "failed to maintain binary equality" in UAssetGUI or has the `VerifyBinaryEquality()` method return false, feel free to submit an issue on [the UAssetAPI issues page](https://github.com/atenfyr/UAssetAPI/issues) with a copy of the asset in question along with the name of the game, the Unreal Engine version that it was cooked with, and a mappings file for the game, if needed.

Please note: Your issue will NOT be reviewed if your issue cannot be replicated due to no test asset being provided.

We currently do not accept AI-generated code on the UAssetAPI or UAssetGUI repositories. UAssetAPI is mature, stable software, so all changes must be thoroughly tested and reviewed by a human. Pull requests containing AI-generated code, text, documentation, or other AI-generated assets will not be reviewed.

## Source
Source code for UAssetAPI is available on GitHub: https://github.com/atenfyr/UAssetAPI

## License
UAssetAPI and UAssetGUI are distributed under the MIT license, which you can view in detail in the [LICENSE file](https://github.com/atenfyr/UAssetAPI/blob/master/LICENSE) on GitHub.
