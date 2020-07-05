# UAsset API
Somewhat messy UAsset API which probably doesn't work half the time. Intended for Astroneer, but might work for other stuff, who knows?

## An example
```cs
var y = new AssetWriter(@"OLD.uasset");
for (int i = 0; i < y.data.headerIndexList.Count; i++)
{
    if (y.data.headerIndexList[i].Item1.Equals(@"/Game/UI/Textures/Icons/Resources/Nuggets/ui_icon_nug_hydrogen"))
    {
        y.data.headerIndexList[i] = Tuple.Create(@"/Game/UI/Textures/Icons/Resources/Nuggets/ui_icon_nug_argon", -352364433); // GUID is 6F 58 FF EA
    }
    else if (y.data.headerIndexList[i].Item1.Equals(@"ui_icon_nug_hydrogen"))
    {
        y.data.headerIndexList[i] = Tuple.Create(@"ui_icon_nug_argon", -1499945709); // GUID is 13 A5 98 A6
    }
}
y.Write(@"NEW.uasset");
```