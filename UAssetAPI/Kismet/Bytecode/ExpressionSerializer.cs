using System;
using UAssetAPI.Kismet.Bytecode.Expressions;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode
{
    public static class ExpressionSerializer
    {
        public static KismetExpression ReadExpression(AssetBinaryReader reader)
        {
            KismetExpression res = null;
            EExprToken token = (EExprToken)reader.ReadByte();
            switch (token)
            {
                case EExprToken.EX_LocalVariable:
                    res = new EX_LocalVariable();
                    break;
                case EExprToken.EX_InstanceVariable:
                    res = new EX_InstanceVariable();
                    break;
                case EExprToken.EX_DefaultVariable:
                    res = new EX_DefaultVariable();
                    break;
                case EExprToken.EX_Return:
                    res = new EX_Return();
                    break;
                case EExprToken.EX_Jump:
                    res = new EX_Jump();
                    break;
                case EExprToken.EX_JumpIfNot:
                    res = new EX_JumpIfNot();
                    break;
                case EExprToken.EX_Assert:
                    res = new EX_Assert();
                    break;
                case EExprToken.EX_Nothing:
                    res = new EX_Nothing();
                    break;
                case EExprToken.EX_Let:
                    res = new EX_Let();
                    break;
                case EExprToken.EX_ClassContext:
                    res = new EX_ClassContext();
                    break;
                case EExprToken.EX_MetaCast:
                    res = new EX_MetaCast();
                    break;
                case EExprToken.EX_LetBool:
                    res = new EX_LetBool();
                    break;
                case EExprToken.EX_EndParmValue:
                    res = new EX_EndParmValue();
                    break;
                case EExprToken.EX_EndFunctionParms:
                    res = new EX_EndFunctionParms();
                    break;
                case EExprToken.EX_Self:
                    res = new EX_Self();
                    break;
                case EExprToken.EX_Skip:
                    res = new EX_Skip();
                    break;
                case EExprToken.EX_Context:
                    res = new EX_Context();
                    break;
                case EExprToken.EX_Context_FailSilent:
                    res = new EX_Context_FailSilent();
                    break;
                case EExprToken.EX_VirtualFunction:
                    res = new EX_VirtualFunction();
                    break;
                case EExprToken.EX_FinalFunction:
                    res = new EX_FinalFunction();
                    break;
                case EExprToken.EX_IntConst:
                    res = new EX_IntConst();
                    break;
                case EExprToken.EX_FloatConst:
                    res = new EX_FloatConst();
                    break;
                case EExprToken.EX_DoubleConst:
                    res = new EX_DoubleConst();
                    break;
                case EExprToken.EX_StringConst:
                    res = new EX_StringConst();
                    break;
                case EExprToken.EX_ObjectConst:
                    res = new EX_ObjectConst();
                    break;
                case EExprToken.EX_NameConst:
                    res = new EX_NameConst();
                    break;
                case EExprToken.EX_RotationConst:
                    res = new EX_RotationConst();
                    break;
                case EExprToken.EX_VectorConst:
                    res = new EX_VectorConst();
                    break;
                case EExprToken.EX_ByteConst:
                    res = new EX_ByteConst();
                    break;
                case EExprToken.EX_IntZero:
                    res = new EX_IntZero();
                    break;
                case EExprToken.EX_IntOne:
                    res = new EX_IntOne();
                    break;
                case EExprToken.EX_True:
                    res = new EX_True();
                    break;
                case EExprToken.EX_False:
                    res = new EX_False();
                    break;
                case EExprToken.EX_TextConst:
                    res = new EX_TextConst();
                    break;
                case EExprToken.EX_NoObject:
                    res = new EX_NoObject();
                    break;
                case EExprToken.EX_TransformConst:
                    res = new EX_TransformConst();
                    break;
                case EExprToken.EX_IntConstByte:
                    res = new EX_IntConstByte();
                    break;
                case EExprToken.EX_NoInterface:
                    res = new EX_NoInterface();
                    break;
                case EExprToken.EX_DynamicCast:
                    res = new EX_DynamicCast();
                    break;
                case EExprToken.EX_StructConst:
                    res = new EX_StructConst();
                    break;
                case EExprToken.EX_EndStructConst:
                    res = new EX_EndStructConst();
                    break;
                case EExprToken.EX_SetArray:
                    res = new EX_SetArray();
                    break;
                case EExprToken.EX_EndArray:
                    res = new EX_EndArray();
                    break;
                case EExprToken.EX_PropertyConst:
                    res = new EX_PropertyConst();
                    break;
                case EExprToken.EX_UnicodeStringConst:
                    res = new EX_UnicodeStringConst();
                    break;
                case EExprToken.EX_Int64Const:
                    res = new EX_Int64Const();
                    break;
                case EExprToken.EX_UInt64Const:
                    res = new EX_UInt64Const();
                    break;
                case EExprToken.EX_PrimitiveCast:
                    res = new EX_PrimitiveCast();
                    break;
                case EExprToken.EX_SetSet:
                    res = new EX_SetSet();
                    break;
                case EExprToken.EX_EndSet:
                    res = new EX_EndSet();
                    break;
                case EExprToken.EX_SetMap:
                    res = new EX_SetMap();
                    break;
                case EExprToken.EX_EndMap:
                    res = new EX_EndMap();
                    break;
                case EExprToken.EX_SetConst:
                    res = new EX_SetConst();
                    break;
                case EExprToken.EX_EndSetConst:
                    res = new EX_EndSetConst();
                    break;
                case EExprToken.EX_MapConst:
                    res = new EX_MapConst();
                    break;
                case EExprToken.EX_EndMapConst:
                    res = new EX_EndMapConst();
                    break;
                case EExprToken.EX_StructMemberContext:
                    res = new EX_StructMemberContext();
                    break;
                case EExprToken.EX_LetMulticastDelegate:
                    res = new EX_LetMulticastDelegate();
                    break;
                case EExprToken.EX_LetDelegate:
                    res = new EX_LetDelegate();
                    break;
                case EExprToken.EX_LocalVirtualFunction:
                    res = new EX_LocalVirtualFunction();
                    break;
                case EExprToken.EX_LocalFinalFunction:
                    res = new EX_LocalFinalFunction();
                    break;
                case EExprToken.EX_LocalOutVariable:
                    res = new EX_LocalOutVariable();
                    break;
                case EExprToken.EX_DeprecatedOp4A:
                    res = new EX_DeprecatedOp4A();
                    break;
                case EExprToken.EX_InstanceDelegate:
                    res = new EX_InstanceDelegate();
                    break;
                case EExprToken.EX_PushExecutionFlow:
                    res = new EX_PushExecutionFlow();
                    break;
                case EExprToken.EX_PopExecutionFlow:
                    res = new EX_PopExecutionFlow();
                    break;
                case EExprToken.EX_ComputedJump:
                    res = new EX_ComputedJump();
                    break;
                case EExprToken.EX_PopExecutionFlowIfNot:
                    res = new EX_PopExecutionFlowIfNot();
                    break;
                case EExprToken.EX_Breakpoint:
                    res = new EX_Breakpoint();
                    break;
                case EExprToken.EX_InterfaceContext:
                    res = new EX_InterfaceContext();
                    break;
                case EExprToken.EX_ObjToInterfaceCast:
                    res = new EX_ObjToInterfaceCast();
                    break;
                case EExprToken.EX_EndOfScript:
                    res = new EX_EndOfScript();
                    break;
                case EExprToken.EX_CrossInterfaceCast:
                    res = new EX_CrossInterfaceCast();
                    break;
                case EExprToken.EX_InterfaceToObjCast:
                    res = new EX_InterfaceToObjCast();
                    break;
                case EExprToken.EX_WireTracepoint:
                    res = new EX_WireTracepoint();
                    break;
                case EExprToken.EX_SkipOffsetConst:
                    res = new EX_SkipOffsetConst();
                    break;
                case EExprToken.EX_AddMulticastDelegate:
                    res = new EX_AddMulticastDelegate();
                    break;
                case EExprToken.EX_ClearMulticastDelegate:
                    res = new EX_ClearMulticastDelegate();
                    break;
                case EExprToken.EX_Tracepoint:
                    res = new EX_Tracepoint();
                    break;
                case EExprToken.EX_LetObj:
                    res = new EX_LetObj();
                    break;
                case EExprToken.EX_LetWeakObjPtr:
                    res = new EX_LetWeakObjPtr();
                    break;
                case EExprToken.EX_BindDelegate:
                    res = new EX_BindDelegate();
                    break;
                case EExprToken.EX_RemoveMulticastDelegate:
                    res = new EX_RemoveMulticastDelegate();
                    break;
                case EExprToken.EX_CallMulticastDelegate:
                    res = new EX_CallMulticastDelegate();
                    break;
                case EExprToken.EX_LetValueOnPersistentFrame:
                    res = new EX_LetValueOnPersistentFrame();
                    break;
                case EExprToken.EX_ArrayConst:
                    res = new EX_ArrayConst();
                    break;
                case EExprToken.EX_EndArrayConst:
                    res = new EX_EndArrayConst();
                    break;
                case EExprToken.EX_SoftObjectConst:
                    res = new EX_SoftObjectConst();
                    break;
                case EExprToken.EX_CallMath:
                    res = new EX_CallMath();
                    break;
                case EExprToken.EX_SwitchValue:
                    res = new EX_SwitchValue();
                    break;
                case EExprToken.EX_InstrumentationEvent:
                    res = new EX_InstrumentationEvent();
                    break;
                case EExprToken.EX_ArrayGetByRef:
                    res = new EX_ArrayGetByRef();
                    break;
                case EExprToken.EX_ClassSparseDataVariable:
                    res = new EX_ClassSparseDataVariable();
                    break;
                case EExprToken.EX_FieldPathConst:
                    res = new EX_FieldPathConst();
                    break;
                default:
                    throw new NotImplementedException("Unimplemented token " + token);
            }

            if (res != null)
            {
                res.Read(reader);
            }
            return res;
        }

        public static int WriteExpression(KismetExpression expr, AssetBinaryWriter writer)
        {
            writer.Write((byte)expr.Token);
            return expr.Write(writer) + sizeof(byte);
        }
    }
}
