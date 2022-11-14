namespace UAssetAPI.Kismet.Bytecode
{
    /// <summary>
    /// Evaluatable expression item types.
    /// </summary>
    public enum EExprToken
	{
		/// <summary>A local variable.</summary>
		EX_LocalVariable = 0x00,
		/// <summary>An object variable.</summary>
		EX_InstanceVariable = 0x01,
		/// <summary>Default variable for a class context.</summary>
		EX_DefaultVariable = 0x02,
		/// <summary>Return from function.</summary>
		EX_Return = 0x04,
		/// <summary>Goto a local address in code.</summary>
		EX_Jump = 0x06,
		/// <summary>Goto if not expression.</summary>
		EX_JumpIfNot = 0x07,
		/// <summary>Assertion.</summary>
		EX_Assert = 0x09,
		/// <summary>No operation.</summary>
		EX_Nothing = 0x0B,
		/// <summary>Assign an arbitrary size value to a variable.</summary>
		EX_Let = 0x0F,
		/// <summary>Class default object context.</summary>
		EX_ClassContext = 0x12,
		/// <summary>Metaclass cast.</summary>
		EX_MetaCast = 0x13,
		/// <summary>Let boolean variable.</summary>
		EX_LetBool = 0x14,
		/// <summary>end of default value for optional function parameter</summary>
		EX_EndParmValue = 0x15,
		/// <summary>End of function call parameters.</summary>
		EX_EndFunctionParms = 0x16,
		/// <summary>Self object.</summary>
		EX_Self = 0x17,
		/// <summary>Skippable expression.</summary>
		EX_Skip = 0x18,
		/// <summary>Call a function through an object context.</summary>
		EX_Context = 0x19,
		/// <summary>Call a function through an object context (can fail silently if the context is NULL; only generated for functions that don't have output or return values).</summary>
		EX_Context_FailSilent = 0x1A,
		/// <summary>A function call with parameters.</summary>
		EX_VirtualFunction = 0x1B,
		/// <summary>A prebound function call with parameters.</summary>
		EX_FinalFunction = 0x1C,
		/// <summary>Int constant.</summary>
		EX_IntConst = 0x1D,
		/// <summary>Floating point constant.</summary>
		EX_FloatConst = 0x1E,
		/// <summary>String constant.</summary>
		EX_StringConst = 0x1F,
		/// <summary>An object constant.</summary>
		EX_ObjectConst = 0x20,
		/// <summary>A name constant.</summary>
		EX_NameConst = 0x21,
		/// <summary>A rotation constant.</summary>
		EX_RotationConst = 0x22,
		/// <summary>A vector constant.</summary>
		EX_VectorConst = 0x23,
		/// <summary>A byte constant.</summary>
		EX_ByteConst = 0x24,
		/// <summary>Zero.</summary>
		EX_IntZero = 0x25,
		/// <summary>One.</summary>
		EX_IntOne = 0x26,
		/// <summary>Bool True.</summary>
		EX_True = 0x27,
		/// <summary>Bool False.</summary>
		EX_False = 0x28,
		/// <summary>FText constant</summary>
		EX_TextConst = 0x29,
		/// <summary>NoObject.</summary>
		EX_NoObject = 0x2A,
		/// <summary>A transform constant</summary>
		EX_TransformConst = 0x2B,
		/// <summary>Int constant that requires 1 byte.</summary>
		EX_IntConstByte = 0x2C,
		/// <summary>A null interface (similar to EX_NoObject, but for interfaces)</summary>
		EX_NoInterface = 0x2D,
		/// <summary>Safe dynamic class casting.</summary>
		EX_DynamicCast = 0x2E,
		/// <summary>An arbitrary UStruct constant</summary>
		EX_StructConst = 0x2F,
		/// <summary>End of UStruct constant</summary>
		EX_EndStructConst = 0x30,
		/// <summary>Set the value of arbitrary array</summary>
		EX_SetArray = 0x31,
		EX_EndArray = 0x32,
		/// <summary>FProperty constant.</summary>
		EX_PropertyConst = 0x33,
		/// <summary>Unicode string constant.</summary>
		EX_UnicodeStringConst = 0x34,
		/// <summary>64-bit integer constant.</summary>
		EX_Int64Const = 0x35,
		/// <summary>64-bit unsigned integer constant.</summary>
		EX_UInt64Const = 0x36,
		/// <summary>A casting operator for primitives which reads the type as the subsequent byte</summary>
		EX_PrimitiveCast = 0x38,
		EX_SetSet = 0x39,
		EX_EndSet = 0x3A,
		EX_SetMap = 0x3B,
		EX_EndMap = 0x3C,
		EX_SetConst = 0x3D,
		EX_EndSetConst = 0x3E,
		EX_MapConst = 0x3F,
		EX_EndMapConst = 0x40,
		/// <summary>Context expression to address a property within a struct</summary>
		EX_StructMemberContext = 0x42,
		/// <summary>Assignment to a multi-cast delegate</summary>
		EX_LetMulticastDelegate = 0x43,
		/// <summary>Assignment to a delegate</summary>
		EX_LetDelegate = 0x44,
		/// <summary>Special instructions to quickly call a virtual function that we know is going to run only locally</summary>
		EX_LocalVirtualFunction = 0x45,
		/// <summary>Special instructions to quickly call a final function that we know is going to run only locally</summary>
		EX_LocalFinalFunction = 0x46,
		/// <summary>local out (pass by reference) function parameter</summary>
		EX_LocalOutVariable = 0x48,
		EX_DeprecatedOp4A = 0x4A,
		/// <summary>const reference to a delegate or normal function object</summary>
		EX_InstanceDelegate = 0x4B,
		/// <summary>push an address on to the execution flow stack for future execution when a EX_PopExecutionFlow is executed. Execution continues on normally and doesn't change to the pushed address.</summary>
		EX_PushExecutionFlow = 0x4C,
		/// <summary>continue execution at the last address previously pushed onto the execution flow stack.</summary>
		EX_PopExecutionFlow = 0x4D,
		/// <summary>Goto a local address in code, specified by an integer value.</summary>
		EX_ComputedJump = 0x4E,
		/// <summary>continue execution at the last address previously pushed onto the execution flow stack, if the condition is not true.</summary>
		EX_PopExecutionFlowIfNot = 0x4F,
		/// <summary>Breakpoint. Only observed in the editor, otherwise it behaves like EX_Nothing.</summary>
		EX_Breakpoint = 0x50,
		/// <summary>Call a function through a native interface variable</summary>
		EX_InterfaceContext = 0x51,
		/// <summary>Converting an object reference to native interface variable</summary>
		EX_ObjToInterfaceCast = 0x52,
		/// <summary>Last byte in script code</summary>
		EX_EndOfScript = 0x53,
		/// <summary>Converting an interface variable reference to native interface variable</summary>
		EX_CrossInterfaceCast = 0x54,
		/// <summary>Converting an interface variable reference to an object</summary>
		EX_InterfaceToObjCast = 0x55,
		/// <summary>Trace point.  Only observed in the editor, otherwise it behaves like EX_Nothing.</summary>
		EX_WireTracepoint = 0x5A,
		/// <summary>A CodeSizeSkipOffset constant</summary>
		EX_SkipOffsetConst = 0x5B,
		/// <summary>Adds a delegate to a multicast delegate's targets</summary>
		EX_AddMulticastDelegate = 0x5C,
		/// <summary>Clears all delegates in a multicast target</summary>
		EX_ClearMulticastDelegate = 0x5D,
		/// <summary>Trace point.  Only observed in the editor, otherwise it behaves like EX_Nothing.</summary>
		EX_Tracepoint = 0x5E,
		/// <summary>assign to any object ref pointer</summary>
		EX_LetObj = 0x5F,
		/// <summary>assign to a weak object pointer</summary>
		EX_LetWeakObjPtr = 0x60,
		/// <summary>bind object and name to delegate</summary>
		EX_BindDelegate = 0x61,
		/// <summary>Remove a delegate from a multicast delegate's targets</summary>
		EX_RemoveMulticastDelegate = 0x62,
		/// <summary>Call multicast delegate</summary>
		EX_CallMulticastDelegate = 0x63,
		EX_LetValueOnPersistentFrame = 0x64,
		EX_ArrayConst = 0x65,
		EX_EndArrayConst = 0x66,
		EX_SoftObjectConst = 0x67,
		/// <summary>static pure function from on local call space</summary>
		EX_CallMath = 0x68,
		EX_SwitchValue = 0x69,
		/// <summary>Instrumentation event</summary>
		EX_InstrumentationEvent = 0x6A,
		EX_ArrayGetByRef = 0x6B,
		/// <summary>Sparse data variable</summary>
		EX_ClassSparseDataVariable = 0x6C,
		EX_FieldPathConst = 0x6D,
		EX_Max = 0x100,
	};

	public enum ECastToken {
		ObjectToInterface = 0x46,
		ObjectToBool = 0x47,
		InterfaceToBool = 0x49,
		Max = 0xFF,
	};
}
