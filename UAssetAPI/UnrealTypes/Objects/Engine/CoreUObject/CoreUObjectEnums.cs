namespace UAssetAPI.UnrealTypes;

public enum EInterpCurveMode : byte {
	CIM_Linear = 0,
	CIM_CurveAuto = 1,
	CIM_Constant = 2,
	CIM_CurveUser = 3,
	CIM_CurveBreak = 4,
	CIM_CurveAutoClamped = 5,
    CIM_Unknown = 6
};

public enum ERangeBoundTypes : byte {
	Exclusive = 0,
	Inclusive = 1,
	Open = 2,
};

public enum EAxis : byte {
	None = 0,
	X = 1,
	Y = 2,
	Z = 3,
};