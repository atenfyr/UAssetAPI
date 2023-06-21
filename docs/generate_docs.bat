@echo off
set "startdir=%cd%"
cd ..\UAssetAPI\bin\Debug\netstandard2.0
copy /Y "%UserProfile%\.nuget\packages\newtonsoft.json\13.0.2\lib\netstandard2.0" .
rd /S /Q "%startdir%\src\api"
xmldoc2md UAssetAPI.dll "%startdir%\src\api"
cd %startdir%
move ".\src\api\index.md" .
python correct_summary.py
del index.md
