using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if DEBUGTRACING
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;
#endif

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("178417ec-1177-413e-be85-c83aecd64279")]

#if DEBUGTRACING
[assembly: UAssetAPI.Trace.LoggingAspect]
[assembly: UAssetAPI.Trace.LoggingAspect(AttributeTargetTypes="UAssetAPI.Trace.*", AttributeExclude = true)]
#endif