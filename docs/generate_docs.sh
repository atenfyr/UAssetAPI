#!/bin/bash

# cd to script dir https://stackoverflow.com/a/17744637
cd "$(cd -P -- "$(dirname -- "$0")" && pwd -P)"

startdir="$(pwd)"
(
  cd ../UAssetAPI/bin/Debug/net8.0
  cp -r "$HOME/.nuget/packages/newtonsoft.json/13.0.3/lib/netstandard2.0/"* .
  rm -r "$startdir/src/api"
  "$startdir/XMLDoc2Markdown/XMLDoc2Markdown" UAssetAPI.dll "$startdir/src/api"
)
mv src/api/index.md .
python correct_summary.py
python correct_pages.py
rm index.md
