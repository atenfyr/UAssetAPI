#!/bin/bash

# cd to script dir
cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd

startdir="$(pwd)"
(
  cd ../UAssetAPI/bin/Release/net10.0
  cp -r "$HOME/.nuget/packages/newtonsoft.json/13.0.4/lib/netstandard2.0/"* .
  rm -r "$startdir/src/api"
  dotnet tool exec XMLDoc2Markdown --allow-roll-forward ./UAssetAPI.dll -o "$startdir/src/api"
)
mv src/api/index.md .
python3 correct_summary.py
python3 correct_pages.py
rm index.md
