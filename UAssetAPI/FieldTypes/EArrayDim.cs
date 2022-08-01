using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.FieldTypes
{
    /// <summary>
    /// The type of array that this property represents. This is represented an integer in the engine.
    /// </summary>
    public enum EArrayDim : int
    {
        NotAnArray = 0,
        TArray = 1,
        CArray = 2
    }
}
