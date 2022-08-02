using Newtonsoft.Json.Linq;
using UAssetAPI.FieldTypes;
using UAssetAPI.Kismet.Bytecode.Expressions;
using UAssetAPI.Kismet.Bytecode;
using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet
{
    public static class KismetSerializer
    {
        public static UAsset asset;
        public struct FSimpleMemberReference
        {
            public string MemberParent;
            public string MemberName;
            public Guid MemberGuid;
        }

        public struct FEdGraphTerminalType
        {
            public string TerminalCategory;
            public string TerminalSubCategory;
            public string TerminalSubCategoryObject;
            public bool bTerminalIsConst;
            public bool bTerminalIsWeakPointer;
            public bool bTerminalIsUObjectWrapper;
        }

        public struct FEdGraphPinType
        {
            public string PinCategory;
            public string PinSubCategory;
            public string PinSubCategoryObject;
            public FSimpleMemberReference PinSubCategoryMemberReference;
            public FEdGraphTerminalType PinValueType;
            public EPinContainerType ContainerType;

            public bool bIsReference;
            public bool bIsConst;
            public bool bIsWeakPointer;
            public bool bIsUObjectWrapper;
        }

        public enum EPinContainerType : byte
        {
            None,
            Array,
            Set,
            Map
        };

        const string PC_Boolean = "Bool";
        const string PC_Byte = "Byte";
        const string PC_Class = "Class";
        const string PC_Int = "Int";
        const string PC_Int64 = "Int64";
        const string PC_Float = "Float";
        const string PC_Name = "Name";
        const string PC_Delegate = "Delegate";
        const string PC_MCDelegate = "mcdelegate";
        const string PC_Object = "Object";
        const string PC_Interface = "Interface";
        const string PC_String = "String";
        const string PC_Text = "Text";
        const string PC_Struct = "Struct";
        const string PC_Enum = "Enum";
        const string PC_SoftObject = "Softobject";
        const string PC_SoftClass = "Softclass";
        const string PC_None = "None";

        public static JArray SerializeScript(KismetExpression[] code)
        {
            JArray jscript = new JArray();
            int index = 0;
            foreach (KismetExpression instruction in code)
            {
                jscript.Add(SerializeExpression(instruction, ref index, true));
            }

            return jscript;
        }

        public static string GetName(int index)
        {
            if (index > 0)
            {
                return asset.Exports[index - 1].ObjectName.ToString();
            }
            else if (index < 0)
            {
                return asset.Imports[-index - 1].ObjectName.ToString();
            }
            else
            {
                return "";
            }
        }

        public static int GetClassIndex()
        {
            for (int i = 1; i <= asset.Exports.Count; i++)
            {
                if (asset.Exports[i - 1] is ClassExport)
                {
                    return i;
                }
            }
            return 0;
        }

        public static string GetFullName(int index, bool alt = false)
        {

            if (index > 0)
            {
                if (asset.Exports[index - 1].OuterIndex.Index != 0)
                {
                    string parent = GetFullName(asset.Exports[index - 1].OuterIndex.Index);
                    return parent + "." + asset.Exports[index - 1].ObjectName.ToString();
                }
                else
                {
                    return asset.Exports[index - 1].ObjectName.ToString();
                }

            }
            else if (index < 0)
            {

                if (asset.Imports[-index - 1].OuterIndex.Index != 0)
                {
                    string parent = GetFullName(asset.Imports[-index - 1].OuterIndex.Index);
                    return parent + "." + asset.Imports[-index - 1].ObjectName.ToString();
                }
                else
                {
                    return asset.Imports[-index - 1].ObjectName.ToString();
                }

            }
            else
            {
                return "";
            }
        }

        public static string GetParentName(int index)
        {
            if (index > 0)
            {
                if (asset.Exports[index - 1].OuterIndex.Index != 0)
                {
                    string parent = GetFullName(asset.Exports[index - 1].OuterIndex.Index);
                    return parent;
                }
                else
                {
                    return "";
                }

            }
            else if (index < 0)
            {

                if (asset.Imports[-index - 1].OuterIndex.Index != 0)
                {
                    string parent = GetFullName(asset.Imports[-index - 1].OuterIndex.Index);
                    return parent;
                }
                else
                {
                    return "";
                }

            }
            else
            {
                return "";
            }
        }

        public static bool FindProperty(int index, FName propname, out FProperty property)
        {
            if (index < 0)
            {

                property = new FObjectProperty();
                return false;

            }
            Export export = asset.Exports[index - 1];
            if (export is StructExport)
            {
                foreach (FProperty prop in (export as StructExport).LoadedProperties)
                {
                    if (prop.Name == propname)
                    {
                        property = prop;
                        return true;
                    }
                }
            }
            property = new FObjectProperty();
            return false;
        }

        public static FEdGraphPinType GetPropertyCategoryInfo(FProperty prop)
        {
            FEdGraphPinType pin = new FEdGraphPinType();
            switch (prop)
            {
                case FInterfaceProperty finterface:
                    {
                        pin.PinCategory = PC_Interface;
                        pin.PinSubCategoryObject = GetFullName(finterface.InterfaceClass.Index);
                        break;
                    };
                case FClassProperty fclassprop:
                    {
                        pin.PinCategory = PC_Class;
                        pin.PinSubCategoryObject = GetFullName(fclassprop.MetaClass.Index);
                        break;
                    };
                case FSoftClassProperty fsoftclassprop:
                    {
                        pin.PinCategory = PC_SoftClass;
                        pin.PinSubCategoryObject = GetFullName(fsoftclassprop.MetaClass.Index);
                        break;
                    };
                case FSoftObjectProperty fsoftobjprop:
                    {
                        pin.PinCategory = PC_SoftObject;
                        pin.PinSubCategoryObject = GetFullName(fsoftobjprop.PropertyClass.Index);
                        break;
                    };
                case FObjectProperty fobjprop:
                    {
                        pin.PinCategory = PC_Object;
                        pin.PinSubCategoryObject = GetFullName(fobjprop.PropertyClass.Index);
                        if (fobjprop.PropertyFlags.HasFlag(EPropertyFlags.CPF_AutoWeak))
                        {
                            pin.bIsWeakPointer = true;
                        }
                        break;
                    };
                case FStructProperty fstruct:
                    {
                        pin.PinCategory = PC_Struct;
                        pin.PinSubCategoryObject = GetFullName(fstruct.Struct.Index);
                        break;
                    };
                case FByteProperty fbyte:
                    {
                        pin.PinCategory = PC_Byte;
                        pin.PinSubCategoryObject = GetFullName(fbyte.Enum.Index);
                        break;
                    };
                case FEnumProperty fenum:
                    {
                        if (!(fenum.UnderlyingProp is FByteProperty))
                        {
                            break;
                        }
                        pin.PinCategory = PC_Byte;
                        pin.PinSubCategoryObject = GetFullName(fenum.Enum.Index);
                        break;
                    }
                case FBoolProperty fbool:
                    {
                        pin.PinCategory = PC_Boolean;
                        break;
                    };
                case FGenericProperty fgeneric:
                    {

                        switch (fgeneric.SerializedType.ToString())
                        {
                            case "FloatProperty":
                                {
                                    pin.PinCategory = PC_Float;
                                    break;
                                }
                            case "Int64Property":
                                {
                                    pin.PinCategory = PC_Int64;
                                    break;
                                }
                            case "IntProperty":
                                {
                                    pin.PinCategory = PC_Int;
                                    break;
                                }
                            case "NameProperty":
                                {
                                    pin.PinCategory = PC_Name;
                                    break;
                                }
                            case "StrProperty":
                                {
                                    pin.PinCategory = PC_String;
                                    break;
                                }
                            case "TextProperty":
                                {
                                    pin.PinCategory = PC_Text;
                                    break;
                                }
                            default: break;
                        };
                        break;
                    }

                default: break;
            }

            return pin;

        }

        public static FSimpleMemberReference FillSimpleMemberReference(int index)
        {
            FSimpleMemberReference member = new FSimpleMemberReference();
            if (index > 0)
            {
                member.MemberName = asset.Exports[index - 1].ObjectName.ToString();
                member.MemberParent = GetName(asset.Exports[index - 1].OuterIndex.Index);
                member.MemberGuid = asset.Exports[index - 1].PackageGuid;
            }
            else if (index < 0)
            {
                member.MemberName = asset.Imports[-index - 1].ObjectName.ToString();
                member.MemberParent = asset.Imports[-index - 1].ClassPackage.ToString();
                member.MemberGuid = new Guid("00000000000000000000000000000000");
            }

            return member;

        }

        public static JObject SerializeGraphPinType(FEdGraphPinType pin)
        {

            JObject jpin = new JObject();
            jpin.Add("PinCategory", pin.PinCategory);
            jpin.Add("PinSubCategory", pin.PinCategory);
            if (pin.PinSubCategoryObject == "" || pin.PinSubCategoryObject == null)
            {

            }
            else { jpin.Add("PinSubCategoryObject", pin.PinSubCategoryObject); }

            if (pin.PinSubCategoryMemberReference.MemberName != null)
            {
                FSimpleMemberReference member = pin.PinSubCategoryMemberReference;
                if (member.MemberGuid.Equals(new Guid("00000000000000000000000000000000")))
                {
                }
                else
                {
                    JObject jmember = new JObject();
                    if (member.MemberParent != "" || member.MemberParent != null)
                    {
                        jmember.Add("MemberParent", member.MemberParent);
                    }
                    jmember.Add("MemberName", member.MemberName);
                    jmember.Add("MemberGuid", member.MemberGuid);
                    jpin.Add("PinSubCategoryMemberReference", jmember);
                }
            }

            if (pin.ContainerType == EPinContainerType.Map)
            {
                FEdGraphTerminalType valuetype = pin.PinValueType;
                JObject jvaluetype = new JObject();

                jvaluetype.Add("TerminalCategory", valuetype.TerminalCategory);
                if (valuetype.TerminalSubCategory == null || valuetype.TerminalSubCategory == "")
                {
                    jvaluetype.Add("TerminalSubCategory", "None");
                }
                else
                {
                    jvaluetype.Add("TerminalSubCategory", valuetype.TerminalSubCategory);
                }
                if (valuetype.TerminalSubCategoryObject != "" && valuetype.TerminalSubCategoryObject != null)
                {
                    jvaluetype.Add("TerminalSubCategoryObject", valuetype.TerminalSubCategoryObject);
                }
                jvaluetype.Add("TerminalIsConst", valuetype.bTerminalIsConst);
                jvaluetype.Add("TerminalIsWeakPointer", valuetype.bTerminalIsWeakPointer);
                jpin.Add("PinValueType", jvaluetype);

            }

            if (pin.ContainerType != EPinContainerType.None)
            {
                jpin.Add("ContainerType", (int)pin.ContainerType);
            }

            if (pin.bIsReference)
            {
                jpin.Add("IsReference", pin.bIsReference);
            }
            if (pin.bIsConst)
            {
                jpin.Add("IsConst", pin.bIsConst);
            }
            if (pin.bIsWeakPointer)
            {
                jpin.Add("IsWeakPointer", pin.bIsWeakPointer);
            }
            return jpin;

        }
        public static FEdGraphPinType ConvertPropertyToPinType(FProperty property)
        {
            FEdGraphPinType pin = new FEdGraphPinType();
            FProperty prop = property;

            if (property is FMapProperty)
            {
                prop = (property as FMapProperty).KeyProp;
                pin.ContainerType = EPinContainerType.Map;
                pin.bIsWeakPointer = false;
                FEdGraphPinType temppin = GetPropertyCategoryInfo((property as FMapProperty).ValueProp);
                pin.PinValueType.TerminalCategory = temppin.PinCategory;
                pin.PinValueType.TerminalSubCategory = temppin.PinSubCategory;
                pin.PinValueType.TerminalSubCategoryObject = temppin.PinSubCategoryObject;

                pin.PinValueType.bTerminalIsConst = temppin.bIsConst;
                pin.PinValueType.bTerminalIsWeakPointer = temppin.bIsWeakPointer;

            }
            else if (property is FSetProperty)
            {
                prop = (property as FSetProperty).ElementProp;
                pin.ContainerType = EPinContainerType.Set;
            }
            else if (property is FArrayProperty)
            {
                prop = (property as FArrayProperty).Inner;
                pin.ContainerType = EPinContainerType.Array;
            }
            pin.bIsReference = property.PropertyFlags.HasFlag(EPropertyFlags.CPF_OutParm) && property.PropertyFlags.HasFlag(EPropertyFlags.CPF_ReferenceParm);
            pin.bIsConst = property.PropertyFlags.HasFlag(EPropertyFlags.CPF_ConstParm);


            if (prop is FMulticastDelegateProperty)
            {
                pin.PinCategory = PC_MCDelegate;
                pin.PinSubCategoryMemberReference = FillSimpleMemberReference((prop as FMulticastDelegateProperty).SignatureFunction.Index);

            }
            else if (prop is FDelegateProperty)
            {
                pin.PinCategory = PC_Delegate;
                pin.PinSubCategoryMemberReference = FillSimpleMemberReference((prop as FDelegateProperty).SignatureFunction.Index);
            }
            else
            {
                FEdGraphPinType temppin = GetPropertyCategoryInfo(prop);
                pin.PinCategory = temppin.PinCategory;
                pin.PinSubCategory = temppin.PinSubCategory;
                pin.PinSubCategoryObject = temppin.PinSubCategoryObject;
                pin.bIsWeakPointer = temppin.bIsWeakPointer;

            }
            return pin;
        }

        public static JProperty[] SerializePropertyPointer(KismetPropertyPointer pointer, string[] names)
        {

            JProperty[] jproparray = new JProperty[names.Length];

            FProperty property;
            if (asset.EngineVersion >= KismetPropertyPointer.XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION)
            {
                if (pointer != null && pointer.New.ResolvedOwner.Index != 0)
                {

                    if (FindProperty(pointer.New.ResolvedOwner.Index, pointer.New.Path[0], out property))
                    {
                        FEdGraphPinType PropertyType = ConvertPropertyToPinType(property);
                        jproparray[0] = new JProperty(names[0], SerializeGraphPinType(PropertyType));
                    }
                    else
                    {
                        jproparray[0] = new JProperty(names[0], "##NOT SERIALIZED##");
                    }
                    if (names.Length > 1)
                    {
                        jproparray[1] = new JProperty(names[1], pointer.New.Path[0].ToString());
                    }

                    return jproparray;

                }
            }
            if (pointer != null && pointer.Old.Index != 0)
            {
                if (names.Length > 1)
                {
                    string[] split = GetFullName(pointer.Old.Index).Split('.');
                    jproparray[0] = new JProperty(names[0], split[0]);
                    string path = "";
                    for (int i = 1; i < split.Length; i++)
                    {
                        path += split[i] + ".";
                    }
                    if (path.EndsWith("."))
                    {
                        path = path.Substring(0, path.Length - 1);
                    }
                    jproparray[1] = new JProperty(names[1], path);
                }
                else
                {
                    jproparray[0] = new JProperty(names[0], GetFullName(pointer.Old.Index));
                }
            }
            else
            {
                jproparray[0] = new JProperty(names[0], "#Pointer Error#");
                if (names.Length > 1)
                {
                    jproparray[1] = new JProperty(names[1], "^^^^^");
                }
            }
            return jproparray;

        }

        private static bool FindProperty(int index, FPackageIndex old, out FProperty property)
        {
            throw new NotImplementedException();
        }

        public static JObject SerializeExpression(KismetExpression expression, ref int index, bool addindex = false)
        {

            int savedindex = index;
            JObject jexp = new JObject();
            index++;
            switch (expression)
            {
                case EX_PrimitiveCast exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index++;
                        switch (exp.ConversionType)
                        {

                            case ECastToken.InterfaceToBool:
                                {
                                    jexp.Add("CastType", "InterfaceToBool");
                                    break;
                                }
                            case ECastToken.ObjectToBool:
                                {
                                    jexp.Add("CastType", "ObjectToBool");
                                    break;
                                }
                            case ECastToken.ObjectToInterface:
                                {
                                    jexp.Add("CastType", "ObjectToInterface");
                                    index += 8;
                                    jexp.Add("InterfaceClass", "##NOT SERIALIZED##");
                                    break;
                                }
                            default: break;
                        }
                        jexp.Add("Expression", SerializeExpression(exp.Target, ref index));
                        break;
                    }
                case EX_SetSet exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("LeftSideExpression", SerializeExpression(exp.SetProperty, ref index));
                        JArray jparams = new JArray();

                        index += 4;
                        foreach (KismetExpression param in exp.Elements)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Values", jparams);
                        break;
                    }
                case EX_SetConst exp:
                    {
                        index += 8;
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add(SerializePropertyPointer(exp.InnerProperty, new[] { "InnerProperty" }));

                        index += 4;
                        JArray jparams = new JArray();
                        foreach (KismetExpression param in exp.Elements)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Values", jparams);
                        break;
                    }
                case EX_SetMap exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("LeftSideExpression", SerializeExpression(exp.MapProperty, ref index));

                        index += 4;
                        JArray jparams = new JArray();
                        for (var j = 1; j <= exp.Elements.Length / 2; j++)
                        {
                            JObject jobject = new JObject();
                            jobject.Add("Key", SerializeExpression(exp.Elements[2 * (j - 1)], ref index));
                            jobject.Add("Value", SerializeExpression(exp.Elements[2 * (j - 1) + 1], ref index));
                            jparams.Add(jobject);
                        }
                        index++;
                        jexp.Add("Values", jparams);
                        break;
                    }
                case EX_MapConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.KeyProperty, new[] { "KeyProperty" }));
                        jexp.Add(SerializePropertyPointer(exp.ValueProperty, new[] { "ValueProperty" }));

                        index += 4;
                        JArray jparams = new JArray();
                        for (var j = 1; j <= exp.Elements.Length / 2; j++)
                        {
                            JObject jobject = new JObject();
                            jobject.Add("Key", SerializeExpression(exp.Elements[2 * (j - 1)], ref index));
                            jobject.Add("Value", SerializeExpression(exp.Elements[2 * (j - 1) + 1], ref index));
                            jparams.Add(jobject);
                        }
                        index++;
                        jexp.Add("Values", jparams);
                        break;
                    }
                case EX_ObjToInterfaceCast exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("InterfaceClass", GetFullName(exp.ClassPtr.Index));
                        jexp.Add("Expression", SerializeExpression(exp.Target, ref index));
                        break;
                    }
                case EX_CrossInterfaceCast exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("InterfaceClass", GetFullName(exp.ClassPtr.Index));
                        jexp.Add("Expression", SerializeExpression(exp.Target, ref index));
                        break;
                    }
                case EX_InterfaceToObjCast exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("ObjectClass", GetFullName(exp.ClassPtr.Index));
                        jexp.Add("Expression", SerializeExpression(exp.Target, ref index));
                        break;
                    }
                case EX_Let exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Variable", SerializeExpression(exp.Variable, ref index));
                        jexp.Add("Expression", SerializeExpression(exp.Expression, ref index));
                        break;
                    }
                case EX_LetObj exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Variable", SerializeExpression(exp.VariableExpression, ref index));
                        jexp.Add("Expression", SerializeExpression(exp.AssignmentExpression, ref index));
                        break;
                    }
                case EX_LetWeakObjPtr exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Variable", SerializeExpression(exp.VariableExpression, ref index));
                        jexp.Add("Expression", SerializeExpression(exp.AssignmentExpression, ref index));
                        break;
                    }
                case EX_LetBool exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Variable", SerializeExpression(exp.VariableExpression, ref index));
                        jexp.Add("Expression", SerializeExpression(exp.AssignmentExpression, ref index));
                        break;
                    }
                case EX_LetValueOnPersistentFrame exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.DestinationProperty, new[] { "Property Outer", "Property Name" }));
                        jexp.Add("Expression", SerializeExpression(exp.AssignmentExpression, ref index));
                        break;
                    }
                case EX_StructMemberContext exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.StructMemberExpression, new[] { "Property Outer", "Property Name" }));
                        jexp.Add("StructExpression", SerializeExpression(exp.StructExpression, ref index));
                        break;
                    }
                case EX_LetDelegate exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Variable", SerializeExpression(exp.VariableExpression, ref index));
                        jexp.Add("Expression", SerializeExpression(exp.AssignmentExpression, ref index));
                        break;
                    }
                case EX_LocalVirtualFunction exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 12;
                        jexp.Add("FunctionName", exp.VirtualFunctionName.ToString());
                        JArray jparams = new JArray();
                        foreach (KismetExpression param in exp.Parameters)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Parameters", jparams);
                        break;
                    }
                case EX_LocalFinalFunction exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Function", GetName(exp.StackNode.Index));
                        index += 8;
                        JArray jparams = new JArray();
                        foreach (KismetExpression param in exp.Parameters)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Parameters", jparams);
                        break;
                    }
                case EX_LetMulticastDelegate exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Variable", SerializeExpression(exp.VariableExpression, ref index));
                        jexp.Add("Expression", SerializeExpression(exp.AssignmentExpression, ref index));
                        break;
                    }
                case EX_ComputedJump exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("OffsetExpression", SerializeExpression(exp.CodeOffsetExpression, ref index));
                        break;
                    }
                case EX_Jump exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 4;
                        jexp.Add("Offset", exp.CodeOffset);
                        break;
                    }
                case EX_LocalVariable exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.Variable, new[] { "Variable Outer", "Variable Name" }));
                        break;
                    }
                case EX_DefaultVariable exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.Variable, new[] { "Variable Outer", "Variable Name" }));
                        break;
                    }
                case EX_InstanceVariable exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.Variable, new[] { "Variable Outer", "Variable Name" }));
                        break;
                    }
                case EX_LocalOutVariable exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.Variable, new[] { "Variable Outer", "Variable Name" }));
                        break;
                    }
                case EX_InterfaceContext exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Expression", SerializeExpression(exp.InterfaceValue, ref index));
                        break;
                    }
                case EX_DeprecatedOp4A exp1:
                case EX_Nothing exp2:
                case EX_EndOfScript exp3:
                case EX_IntZero exp4:
                case EX_IntOne exp5:
                case EX_True exp6:
                case EX_False exp7:
                case EX_NoObject exp8:
                case EX_NoInterface exp9:
                case EX_Self exp10:
                    {
                        jexp.Add("Inst", expression.Inst);
                        break;
                    }
                case EX_Return exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Expression", SerializeExpression(exp.ReturnExpression, ref index));
                        break;
                    }
                case EX_CallMath exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Function", GetName(exp.StackNode.Index));
                        jexp.Add("ContextClass", GetParentName(exp.StackNode.Index));
                        JArray jparams = new JArray();
                        foreach (KismetExpression param in exp.Parameters)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Parameters", jparams);
                        break;
                    }
                case EX_CallMulticastDelegate exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        JObject jsign = new JObject();
                        bool bIsSelfContext = GetClassIndex() == exp.StackNode.Index;
                        jsign.Add("IsSelfContext", bIsSelfContext);
                        jsign.Add("MemberParent", GetFullName(exp.StackNode.Index));
                        jsign.Add("MemberName", GetName(exp.StackNode.Index));
                        jexp.Add("DelegateSignatureFunction", jsign);
                        jexp.Add("Delegate", SerializeExpression(exp.Delegate, ref index));

                        JArray jparams = new JArray();
                        foreach (KismetExpression param in exp.Parameters)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Parameters", jparams);
                        break;
                    }
                case EX_FinalFunction exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Function", GetName(exp.StackNode.Index));
                        JArray jparams = new JArray();
                        foreach (KismetExpression param in exp.Parameters)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Parameters", jparams);
                        break;
                    }
                case EX_VirtualFunction exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 12;
                        jexp.Add("Function", exp.VirtualFunctionName.ToString());
                        JArray jparams = new JArray();

                        foreach (KismetExpression param in exp.Parameters)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Parameters", jparams);
                        break;
                    }
                //case EX_ClassContext:
                //case EX_Context_FailSilent: {
                case EX_Context exp:
                    {

                        if (exp is EX_Context_FailSilent)
                        {
                            exp = exp as EX_Context_FailSilent;
                        }
                        else if (exp is EX_ClassContext)
                        {
                            exp = exp as EX_ClassContext;
                        }
                        else
                        {
                        }
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Context", SerializeExpression(exp.ObjectExpression, ref index));
                        index += 4;
                        jexp.Add("SkipOffsetForNull", exp.Offset);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.RValuePointer, new[] { "RValuePropertyOuter", "RValuePropertyName" }));
                        jexp.Add("Expression", SerializeExpression(exp.ContextExpression, ref index));
                        break;
                    }
                case EX_IntConst exp:
                    {
                        index += 4;
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_SkipOffsetConst exp:
                    {
                        index += 4;
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_FloatConst exp:
                    {
                        index += 4;
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_StringConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += exp.Value.Length + 1;
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_UnicodeStringConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 2 * (exp.Value.Length + 1);
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_TextConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index++;
                        switch (exp.Value.TextLiteralType)
                        {
                            case EBlueprintTextLiteralType.Empty:
                                {
                                    jexp.Add("TextLiteralType", "Empty");
                                    break;
                                }
                            case EBlueprintTextLiteralType.LocalizedText:
                                {
                                    jexp.Add("TextLiteralType", "LocalizedText");
                                    jexp.Add("SourceString", ReadString(exp.Value.LocalizedSource, ref index));
                                    jexp.Add("LocalizationKey", ReadString(exp.Value.LocalizedKey, ref index));
                                    jexp.Add("LocalizationNamespace", ReadString(exp.Value.LocalizedNamespace, ref index));
                                    break;
                                }
                            case EBlueprintTextLiteralType.InvariantText:
                                {
                                    jexp.Add("TextLiteralType", "InvariantText");
                                    jexp.Add("SourceString", ReadString(exp.Value.InvariantLiteralString, ref index));

                                    break;
                                }
                            case EBlueprintTextLiteralType.LiteralString:
                                {
                                    jexp.Add("TextLiteralType", "LiteralString");
                                    jexp.Add("SourceString", ReadString(exp.Value.LiteralString, ref index));
                                    break;
                                }
                            case EBlueprintTextLiteralType.StringTableEntry:
                                {
                                    jexp.Add("TextLiteralType", "StringTableEntry");
                                    index += 8;
                                    jexp.Add("TableId", ReadString(exp.Value.StringTableId, ref index));
                                    jexp.Add("TableKey", ReadString(exp.Value.StringTableKey, ref index));
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case EX_ObjectConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Object", GetFullName(exp.Value.Index));
                        break;
                    }
                case EX_SoftObjectConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Value", SerializeExpression(exp.Value, ref index));
                        break;
                    }
                case EX_NameConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 12;
                        jexp.Add("Value", exp.Value.ToString());
                        break;
                    }
                case EX_RotationConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 12;
                        jexp.Add("Pitch", exp.Pitch);
                        jexp.Add("Yaw", exp.Yaw);
                        jexp.Add("Roll", exp.Roll);
                        break;
                    }
                case EX_VectorConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 12;
                        jexp.Add("X", exp.Value.X);
                        jexp.Add("Y", exp.Value.Y);
                        jexp.Add("Z", exp.Value.Z);
                        break;
                    }
                case EX_TransformConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 40;
                        JObject jrot = new JObject();
                        JObject jtrans = new JObject();
                        JObject jscale = new JObject();

                        jrot.Add("X", exp.Value.Rotation.X);
                        jrot.Add("Y", exp.Value.Rotation.Y);
                        jrot.Add("Z", exp.Value.Rotation.Z);
                        jrot.Add("W", exp.Value.Rotation.W);

                        jtrans.Add("X", exp.Value.Translation.X);
                        jtrans.Add("Y", exp.Value.Translation.Y);
                        jtrans.Add("Z", exp.Value.Translation.Z);

                        jscale.Add("X", exp.Value.Scale3D.X);
                        jscale.Add("Y", exp.Value.Scale3D.Y);
                        jscale.Add("Z", exp.Value.Scale3D.Z);

                        jexp.Add("Rotation", jrot);
                        jexp.Add("Translation", jtrans);
                        jexp.Add("Scale", jscale);
                        break;
                    }
                case EX_StructConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Struct", GetFullName(exp.Struct.Index));

                        index += 4;
                        JObject jstruct = new JObject();
                        int tempindex = 0;
                        foreach (KismetExpression param in exp.Value)
                        {
                            JArray jstructpart = new JArray();
                            jstructpart.Add(SerializeExpression(param, ref index));
                            jstruct.Add("Missing property name" + tempindex, jstructpart);
                            tempindex++;
                        }
                        index++;
                        jexp.Add("Properties", jstruct);
                        break;
                    }
                case EX_SetArray exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("LeftSideExpression", SerializeExpression(exp.AssigningProperty, ref index));
                        JArray jparams = new JArray();
                        foreach (KismetExpression param in exp.Elements)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Values", jparams);
                        break;
                    }
                case EX_ArrayConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add(SerializePropertyPointer(exp.InnerProperty, new[] { "Variable Outer" }));

                        index += 4;
                        JArray jparams = new JArray();
                        foreach (KismetExpression param in exp.Elements)
                        {
                            jparams.Add(SerializeExpression(param, ref index));
                        }
                        index++;
                        jexp.Add("Values", jparams);
                        break;
                    }
                case EX_ByteConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index++;
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_IntConstByte exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index++;
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_Int64Const exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_UInt64Const exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Value", exp.Value);
                        break;
                    }
                case EX_FieldPathConst exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Expression", SerializeExpression(exp.Value, ref index));
                        break;
                    }
                case EX_MetaCast exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Class", GetFullName(exp.ClassPtr.Index));
                        jexp.Add("Expression", SerializeExpression(exp.TargetExpression, ref index));
                        break;
                    }
                case EX_DynamicCast exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 8;
                        jexp.Add("Class", GetFullName(exp.ClassPtr.Index));
                        jexp.Add("Expression", SerializeExpression(exp.TargetExpression, ref index));
                        break;
                    }
                case EX_JumpIfNot exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 4;
                        jexp.Add("Offset", exp.CodeOffset);
                        jexp.Add("Condition", SerializeExpression(exp.BooleanExpression, ref index));
                        break;
                    }
                case EX_Assert exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 3;
                        jexp.Add("LineNumber", exp.LineNumber);
                        jexp.Add("Debug", exp.DebugMode);
                        jexp.Add("Expression", SerializeExpression(exp.AssertExpression, ref index));
                        break;
                    }
                case EX_InstanceDelegate exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 12;
                        jexp.Add("FunctionName", exp.FunctionName.ToString());
                        break;
                    }
                case EX_AddMulticastDelegate exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("MulticastDelegate", SerializeExpression(exp.Delegate, ref index));
                        jexp.Add("Delegate", SerializeExpression(exp.DelegateToAdd, ref index));
                        break;
                    }
                case EX_RemoveMulticastDelegate exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("MulticastDelegate", SerializeExpression(exp.Delegate, ref index));
                        jexp.Add("Delegate", SerializeExpression(exp.DelegateToAdd, ref index));
                        break;
                    }
                case EX_ClearMulticastDelegate exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("MulticastDelegate", SerializeExpression(exp.DelegateToClear, ref index));
                        break;
                    }
                case EX_BindDelegate exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 12;
                        jexp.Add("FunctionName", exp.FunctionName.ToString());
                        jexp.Add("Delegate", SerializeExpression(exp.Delegate, ref index));
                        jexp.Add("Object", SerializeExpression(exp.ObjectTerm, ref index));
                        break;
                    }
                case EX_PushExecutionFlow exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 4;
                        jexp.Add("Offset", exp.PushingAddress);
                        break;
                    }
                case EX_PopExecutionFlow exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        break;
                    }
                case EX_PopExecutionFlowIfNot exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("Condition", SerializeExpression(exp.BooleanExpression, ref index));
                        break;
                    }
                case EX_Breakpoint exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        break;
                    }
                case EX_WireTracepoint exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        break;
                    }
                case EX_InstrumentationEvent exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index++;
                        switch (exp.EventType)
                        {
                            case EScriptInstrumentationType.Class:
                                jexp.Add("EventType", "Class");
                                break;
                            case EScriptInstrumentationType.ClassScope:
                                jexp.Add("EventType", "ClassScope");
                                break;
                            case EScriptInstrumentationType.Instance:
                                jexp.Add("EventType", "Instance");
                                break;
                            case EScriptInstrumentationType.Event:
                                jexp.Add("EventType", "Event");
                                break;
                            case EScriptInstrumentationType.InlineEvent:
                                {
                                    index += 12;
                                    jexp.Add("EventType", "InlineEvent");
                                    jexp.Add("EventName", exp.EventName.ToString());
                                    break;
                                }
                            case EScriptInstrumentationType.ResumeEvent:
                                jexp.Add("EventType", "ResumeEvent");
                                break;
                            case EScriptInstrumentationType.PureNodeEntry:
                                jexp.Add("EventType", "PureNodeEntry");
                                break;
                            case EScriptInstrumentationType.NodeDebugSite:
                                jexp.Add("EventType", "NodeDebugSite");
                                break;
                            case EScriptInstrumentationType.NodeEntry:
                                jexp.Add("EventType", "NodeEntry");
                                break;
                            case EScriptInstrumentationType.NodeExit:
                                jexp.Add("EventType", "NodeExit");
                                break;
                            case EScriptInstrumentationType.PushState:
                                jexp.Add("EventType", "PushState");
                                break;
                            case EScriptInstrumentationType.RestoreState:
                                jexp.Add("EventType", "RestoreState");
                                break;
                            case EScriptInstrumentationType.ResetState:
                                jexp.Add("EventType", "ResetState");
                                break;
                            case EScriptInstrumentationType.SuspendState:
                                jexp.Add("EventType", "SuspendState");
                                break;
                            case EScriptInstrumentationType.PopState:
                                jexp.Add("EventType", "PopState");
                                break;
                            case EScriptInstrumentationType.TunnelEndOfThread:
                                jexp.Add("EventType", "TunnelEndOfThread");
                                break;
                            case EScriptInstrumentationType.Stop:
                                jexp.Add("EventType", "Stop");
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                case EX_Tracepoint exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        break;
                    }
                case EX_SwitchValue exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        index += 6;

                        jexp.Add("Expression", SerializeExpression(exp.IndexTerm, ref index));
                        jexp.Add("OffsetToSwitchEnd", exp.EndGotoOffset);
                        JArray jcases = new JArray();

                        for (var j = 0; j < exp.Cases.Length; j++)
                        {
                            JObject jcase = new JObject();
                            jcase.Add("CaseValue", SerializeExpression(exp.Cases[j].CaseIndexValueTerm, ref index));
                            index += 4;
                            jcase.Add("OffsetToNextCase", exp.Cases[j].NextOffset);
                            jcase.Add("CaseResult", SerializeExpression(exp.Cases[j].CaseTerm, ref index));
                            jcases.Add(jcase);
                        }

                        jexp.Add("Cases", jcases);
                        jexp.Add("DefaultResult", SerializeExpression(exp.DefaultTerm, ref index));

                        break;
                    }
                case EX_ArrayGetByRef exp:
                    {
                        jexp.Add("Inst", exp.Inst);
                        jexp.Add("ArrayExpression", SerializeExpression(exp.ArrayVariable, ref index));
                        jexp.Add("IndexExpression", SerializeExpression(exp.ArrayIndex, ref index));
                        break;
                    }
                default:
                    {
                        // This should never occur.
                        //checkf(0, TEXT("Unknown bytecode 0x%02X"), (uint8)Opcode);
                        break;
                    }
            }
            if (addindex) { jexp.Add("StatementIndex", savedindex); }
            return jexp;
        }

        public static string ReadString(KismetExpression expr, ref int index)
        {

            string result = "";
            index++;
            switch (expr)
            {
                case EX_StringConst exp:
                    {
                        result = exp.Value;
                        index += result.Length + 1;
                        break;
                    }
                case EX_UnicodeStringConst exp:
                    {
                        result = exp.Value;
                        index += 2 * (result.Length + 1);
                        break;
                    }
                default:
                    break;
            }
            return result;
        }
    }
}