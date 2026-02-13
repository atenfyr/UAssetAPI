using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode.Expressions
{
    /// <summary>
    /// A single Kismet bytecode instruction, corresponding to the <see cref="EExprToken.EX_ObjToInterfaceCast"/> instruction.
    /// A conversion from an object or interface variable to a native interface variable.
    /// </summary>
    public class EX_ObjToInterfaceCast : EX_CastBase
    {
        /// <summary>
        /// The token of this expression.
        /// </summary>
        public override EExprToken Token { get { return EExprToken.EX_ObjToInterfaceCast; } }
    }
}
