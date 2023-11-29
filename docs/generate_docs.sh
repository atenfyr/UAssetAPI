#!/bin/bash

# cd to script dir https://stackoverflow.com/a/17744637
cd "$(cd -P -- "$(dirname -- "$0")" && pwd -P)"

startdir="$(pwd)"
(
  cd ../UAssetAPI/bin/Debug/netstandard2.0
  cp -r "$HOME/.nuget/packages/newtonsoft.json/13.0.2/lib/netstandard2.0/"* .
  rm -r "$startdir/src/api"
  xmldoc2md UAssetAPI.dll "$startdir/src/api"
)
mv src/api/index.md .
python correct_summary.py
rm index.md
