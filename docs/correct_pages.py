import glob, re

SUBSTRINGS_TO_CHANGE = {
    re.escape("""### **Item**

```csharp
public PropertyData Item { get; set; }
```

#### Property Value

[PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>

"""): "",

    re.escape("""### **Item**

```csharp
public Export Item { get; set; }
```

#### Property Value

[Export](./uassetapi.exporttypes.export.md)<br>

"""): "",

    r", [\S]+?, Version=[\d\.]+, Culture=.+?, PublicKeyToken=.+?\]": "]",


}

for file in glob.glob("src/api/*.md"):
    dat = ""
    with open(file, "r") as f:
        dat = f.read()
    for entry in SUBSTRINGS_TO_CHANGE:
        dat = re.sub(entry, SUBSTRINGS_TO_CHANGE[entry], dat)
    with open(file, "w") as f:
        f.write(dat)