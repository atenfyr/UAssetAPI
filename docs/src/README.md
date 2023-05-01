# UAssetAPI Documentation

UAssetAPI is a .NET library for reading and writing Unreal Engine 4 game assets.

## Features
- Low-level read/write capability for a wide variety of cooked .uasset files from ~4.13 to 4.27
- Support for more than 80 property types and 10 export types
- Support for JSON export and import to a proprietary format that maintains binary equality
- Support for reading and writing raw Kismet (blueprint) bytecode
- Reading capability for the unofficial .usmap format to parse ambiguous and unversioned properties
- Robust fail-safes for many properties and exports that fail serialization
- Automatic reflection for new property types in other loaded assemblies
- Continual updates to support games with custom or obfuscated serialization

## Usage
To get started using UAssetAPI, first build the API using the [Build Instructions guide](guide/build.md) and learn how to perform basic operations on your cooked .uasset files using the [Basic Usage guide](guide/basic.md).

UAssetGUI, a graphical wrapper around UAssetAPI which allows you to directly view and modify game assets by hand, is also available and can be downloaded for free on GitHub at [https://github.com/atenfyr/UAssetGUI/releases](https://github.com/atenfyr/UAssetGUI/releases).

## Support
The source code of UAssetAPI is publicly available on GitHub at [https://github.com/atenfyr/UAssetAPI](https://github.com/atenfyr/UAssetAPI), and all contributions are greatly appreciated.

Any bugs or feature requests that you may have can be submitted on the GitHub page as an issue. I am also active on the Unreal Engine Modding Discord server and will often answer questions about UAssetAPI usage and functionality, which you can join with this invite link: [https://discord.gg/zVvsE9mEEa](https://discord.gg/zVvsE9mEEa).