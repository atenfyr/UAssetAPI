# EExprToken

Namespace: UAssetAPI.Kismet.Bytecode

Evaluatable expression item types.

```csharp
public enum EExprToken
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [EExprToken](./uassetapi.kismet.bytecode.eexprtoken.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| EX_LocalVariable | 0 | A local variable. |
| EX_InstanceVariable | 1 | An object variable. |
| EX_DefaultVariable | 2 | Default variable for a class context. |
| EX_Return | 4 | Return from function. |
| EX_Jump | 6 | Goto a local address in code. |
| EX_JumpIfNot | 7 | Goto if not expression. |
| EX_Assert | 9 | Assertion. |
| EX_Nothing | 11 | No operation. |
| EX_Let | 15 | Assign an arbitrary size value to a variable. |
| EX_ClassContext | 18 | Class default object context. |
| EX_MetaCast | 19 | Metaclass cast. |
| EX_LetBool | 20 | Let boolean variable. |
| EX_EndParmValue | 21 | end of default value for optional function parameter |
| EX_EndFunctionParms | 22 | End of function call parameters. |
| EX_Self | 23 | Self object. |
| EX_Skip | 24 | Skippable expression. |
| EX_Context | 25 | Call a function through an object context. |
| EX_Context_FailSilent | 26 | Call a function through an object context (can fail silently if the context is NULL; only generated for functions that don't have output or return values). |
| EX_VirtualFunction | 27 | A function call with parameters. |
| EX_FinalFunction | 28 | A prebound function call with parameters. |
| EX_IntConst | 29 | Int constant. |
| EX_FloatConst | 30 | Floating point constant. |
| EX_StringConst | 31 | String constant. |
| EX_ObjectConst | 32 | An object constant. |
| EX_NameConst | 33 | A name constant. |
| EX_RotationConst | 34 | A rotation constant. |
| EX_VectorConst | 35 | A vector constant. |
| EX_ByteConst | 36 | A byte constant. |
| EX_IntZero | 37 | Zero. |
| EX_IntOne | 38 | One. |
| EX_True | 39 | Bool True. |
| EX_False | 40 | Bool False. |
| EX_TextConst | 41 | FText constant |
| EX_NoObject | 42 | NoObject. |
| EX_TransformConst | 43 | A transform constant |
| EX_IntConstByte | 44 | Int constant that requires 1 byte. |
| EX_NoInterface | 45 | A null interface (similar to EX_NoObject, but for interfaces) |
| EX_DynamicCast | 46 | Safe dynamic class casting. |
| EX_StructConst | 47 | An arbitrary UStruct constant |
| EX_EndStructConst | 48 | End of UStruct constant |
| EX_SetArray | 49 | Set the value of arbitrary array |
| EX_PropertyConst | 51 | FProperty constant. |
| EX_UnicodeStringConst | 52 | Unicode string constant. |
| EX_Int64Const | 53 | 64-bit integer constant. |
| EX_UInt64Const | 54 | 64-bit unsigned integer constant. |
| EX_DoubleConst | 55 | Double-precision floating point constant. |
| EX_PrimitiveCast | 56 | A casting operator for primitives which reads the type as the subsequent byte |
| EX_StructMemberContext | 66 | Context expression to address a property within a struct |
| EX_LetMulticastDelegate | 67 | Assignment to a multi-cast delegate |
| EX_LetDelegate | 68 | Assignment to a delegate |
| EX_LocalVirtualFunction | 69 | Special instructions to quickly call a virtual function that we know is going to run only locally |
| EX_LocalFinalFunction | 70 | Special instructions to quickly call a final function that we know is going to run only locally |
| EX_LocalOutVariable | 72 | local out (pass by reference) function parameter |
| EX_InstanceDelegate | 75 | const reference to a delegate or normal function object |
| EX_PushExecutionFlow | 76 | push an address on to the execution flow stack for future execution when a EX_PopExecutionFlow is executed. Execution continues on normally and doesn't change to the pushed address. |
| EX_PopExecutionFlow | 77 | continue execution at the last address previously pushed onto the execution flow stack. |
| EX_ComputedJump | 78 | Goto a local address in code, specified by an integer value. |
| EX_PopExecutionFlowIfNot | 79 | continue execution at the last address previously pushed onto the execution flow stack, if the condition is not true. |
| EX_Breakpoint | 80 | Breakpoint. Only observed in the editor, otherwise it behaves like EX_Nothing. |
| EX_InterfaceContext | 81 | Call a function through a native interface variable |
| EX_ObjToInterfaceCast | 82 | Converting an object reference to native interface variable |
| EX_EndOfScript | 83 | Last byte in script code |
| EX_CrossInterfaceCast | 84 | Converting an interface variable reference to native interface variable |
| EX_InterfaceToObjCast | 85 | Converting an interface variable reference to an object |
| EX_WireTracepoint | 90 | Trace point. Only observed in the editor, otherwise it behaves like EX_Nothing. |
| EX_SkipOffsetConst | 91 | A CodeSizeSkipOffset constant |
| EX_AddMulticastDelegate | 92 | Adds a delegate to a multicast delegate's targets |
| EX_ClearMulticastDelegate | 93 | Clears all delegates in a multicast target |
| EX_Tracepoint | 94 | Trace point. Only observed in the editor, otherwise it behaves like EX_Nothing. |
| EX_LetObj | 95 | assign to any object ref pointer |
| EX_LetWeakObjPtr | 96 | assign to a weak object pointer |
| EX_BindDelegate | 97 | bind object and name to delegate |
| EX_RemoveMulticastDelegate | 98 | Remove a delegate from a multicast delegate's targets |
| EX_CallMulticastDelegate | 99 | Call multicast delegate |
| EX_CallMath | 104 | static pure function from on local call space |
| EX_InstrumentationEvent | 106 | Instrumentation event |
| EX_ClassSparseDataVariable | 108 | Sparse data variable |
| EX_AutoRtfmTransact | 112 | AutoRTFM: run following code in a transaction |
| EX_AutoRtfmStopTransact | 113 | AutoRTFM: if in a transaction, abort or break, otherwise no operation |
| EX_AutoRtfmAbortIfNot | 114 | AutoRTFM: evaluate bool condition, abort transaction on false |
