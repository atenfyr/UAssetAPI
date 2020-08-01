# UAsset API
A .uasset API written in C# to facilitate reading and modifying game assets.

## A small example
```cs
var y = new AssetWriter(@"OLD.uasset");
for (int i = 0; i < y.data.headerIndexList.Count; i++)
{
    if (y.data.headerIndexList[i].Item1.Equals(@"/Game/UI/Textures/Icons/Resources/Nuggets/ui_icon_nug_hydrogen"))
    {
        y.data.headerIndexList[i] = Tuple.Create(@"/Game/UI/Textures/Icons/Resources/Nuggets/ui_icon_nug_argon", 3942602863); // GUID is 6F 58 FF EA
    }
    else if (y.data.headerIndexList[i].Item1.Equals(@"ui_icon_nug_hydrogen"))
    {
        y.data.headerIndexList[i] = Tuple.Create(@"ui_icon_nug_argon", 2795021587); // GUID is 13 A5 98 A6
    }
}
y.Write(@"NEW.uasset");
```

## Disclaimer
UAssetAPI is still very much in development, so there is no guarantee of backwards compatibility at this time.