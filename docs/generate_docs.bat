@echo off
set "startdir=%cd%"
cd ..\UAssetAPI\bin\Debug\net8.0
copy /Y "%UserProfile%\.nuget\packages\newtonsoft.json\13.0.3\lib\netstandard2.0" .
rd /S /Q "%startdir%\src\api"
"%startdir%\XMLDoc2Markdown\XMLDoc2Markdown.exe" UAssetAPI.dll "%startdir%\src\api"
cd %startdir%
move ".\src\api\index.md" .
python correct_summary.py
python correct_pages.py
del index.md
