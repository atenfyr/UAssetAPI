using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public enum ViewTargetBlendFunction
    {
        /** Camera does a simple linear interpolation. */
        VTBlend_Linear,
        /** Camera has a slight ease in and ease out, but amount of ease cannot be tweaked. */
        VTBlend_Cubic,
        /** Camera immediately accelerates, but smoothly decelerates into the target.  Ease amount controlled by BlendExp. */
        VTBlend_EaseIn,
        /** Camera smoothly accelerates, but does not decelerate into the target.  Ease amount controlled by BlendExp. */
        VTBlend_EaseOut,
        /** Camera smoothly accelerates and decelerates.  Ease amount controlled by BlendExp. */
        VTBlend_EaseInOut,
        VTBlend_MAX,
    }

    // Referred to as FViewTargetTransitionParams in the UE4 src
    public class ViewTargetBlendParamsPropertyData : PropertyData
    {
        public float BlendTime;
        public ViewTargetBlendFunction BlendFunction;
        public float BlendExp;
        public bool bLockOutgoing;

        public ViewTargetBlendParamsPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public ViewTargetBlendParamsPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("ViewTargetBlendParams");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            BlendTime = reader.ReadSingle();
            BlendFunction = (ViewTargetBlendFunction)reader.ReadByte();
            BlendExp = reader.ReadSingle();
            bLockOutgoing = reader.ReadInt32() != 0;
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(BlendTime);
            writer.Write((byte)BlendFunction);
            writer.Write(BlendExp);
            writer.Write(bLockOutgoing ? 1 : 0);
            return sizeof(float) * 2 + sizeof(byte) + sizeof(int);
        }

        public override void FromString(string[] d)
        {
            if (float.TryParse(d[0], out float res1)) BlendTime = res1;
            if (Enum.TryParse(d[1], out ViewTargetBlendFunction res2)) BlendFunction = res2;
            if (float.TryParse(d[2], out float res3)) BlendExp = res3;
            if (bool.TryParse(d[3], out bool res4)) bLockOutgoing = res4;
        }

        public override string ToString()
        {
            string oup = "(";
            oup += BlendTime + ", ";
            oup += BlendFunction + ", ";
            oup += BlendExp + ", ";
            oup += bLockOutgoing + ", ";
            return oup.Remove(oup.Length - 2) + ")";
        }
    }
}
