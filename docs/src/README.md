# UAssetAPI Documentation

UAssetAPI is a .NET library for reading and writing Unreal Engine game assets.

## Features
- Low-level read/write capability for a wide variety of cooked and uncooked .uasset files from ~4.13 to 5.7
- Support for more than 100 property types and 12 export types
- Support for JSON export and import to a proprietary format that maintains binary equality
- Support for reading and writing raw Kismet (blueprint) bytecode
- Reading capability for the unofficial .usmap format to parse ambiguous and unversioned properties
- Robust fail-safes for many properties and exports that fail serialization
- Automatic reflection for new property types in other loaded assemblies

## Usage
To get started using UAssetAPI, visit the [Basic Usage guide](guide/basic.md) to get started with performing basic operations on your .uasset files.

Major releases of UAssetAPI are available on NuGet: https://www.nuget.org/packages/UAssetAPI/

UAssetGUI, a graphical wrapper around UAssetAPI which allows you to directly view and modify game assets by hand, is also available and can be downloaded for free on GitHub at [https://github.com/atenfyr/UAssetGUI/releases](https://github.com/atenfyr/UAssetGUI/releases).

## Support
The source code of UAssetAPI is publicly available on GitHub at [https://github.com/atenfyr/UAssetAPI](https://github.com/atenfyr/UAssetAPI), and all contributions are greatly appreciated.

Any bugs or feature requests that you may have can be submitted on the GitHub page as an issue. You can also direct any questions you may have to the folks on the Unreal Engine Modding Discord server, which you can join with this invite link: [https://discord.gg/zVvsE9mEEa](https://discord.gg/zVvsE9mEEa).

## Source
Source code for UAssetAPI is available on GitHub: https://github.com/atenfyr/UAssetAPI 

## License
UAssetAPI and UAssetGUI are distributed under the MIT license, which you can view in detail in the [LICENSE file](https://github.com/atenfyr/UAssetAPI/blob/master/LICENSE) on GitHub.
