# Build Instructions

### Prerequisites
* Visual Studio 2022 or later, with .NET 8.0 SDK
* Git

### Initial Setup
1. Clone the UAssetAPI repository:

```sh
git clone https://github.com/atenfyr/UAssetAPI.git
```

2. Open the `UAssetAPI.sln` solution file within the newly-created UAssetAPI directory in Visual Studio, right-click on the solution name in the Solution Explorer, and press "Restore Nuget Packages."

3. Press F6 or right-click the solution name in the Solution Explorer and press "Build Solution" to compile UAssetAPI, which will be written as a .dll file to the `bin` directory. Note that this solution does not include UAssetGUI.