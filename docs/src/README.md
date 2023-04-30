# UAssetAPI Documentation

UAssetAPI is a .NET library for reading and writing Unreal Engine 4 game assets.

## Major features

- Lua scripting API: Write lua mods based on the UE object system
- Blueprint Modloading: Spawn blueprint mods automatically without editing/replacing game files.
- Live property editor: Search, view, edit & watch the properties of every loaded object, great for debugging mods or figuring out how values are changed during runtime
- Generate Unreal Header Tool compatible C++ headers for creating a mirror .uproject for your game
- Generate standard C++ headers from reflected classes and blueprints, with offsets
- Unlock the game console
- Generate .usmap mapping files for unversioned properties
- Dump all loaded actors to file to generate .umaps in-editor

## Targeting UE Versions: From 4.12 To 5.1

The goal of UE4SS is not to be a plug-n-play solution that always works with every game.  
The goal is to have an underlying system that works for most games.  
You may need to update AOBs on your own, and there's a guide for that below.  