using Newtonsoft.Json;
using System;
using System.Text;
using UAssetAPI.JSON;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// Unreal name - consists of an FString (which is serialized as an index in the name map) and an instance number
    /// </summary>
    [JsonConverter(typeof(FNameJsonConverter))]
    public class FName : ICloneable
    {
        public FString Value
        {
            get
            {
                if (DummyValue != null) return DummyValue;
                if (Asset == null) throw new InvalidOperationException("Attempt to get Value with no Asset defined");
                if (Index < 0) return null;
                return Asset.GetNameReference(Index);
            }
            set
            {
                DummyValue = null;
                if (Asset == null) throw new InvalidOperationException("Attempt to set Value with no Asset defined");
                Index = value?.Value == null ? -1 : Asset.AddNameReference(value);
            }
        }

        public bool IsDummy
        {
            get
            {
                return DummyValue != null;
            }
        }

        /// <summary>Instance number.</summary>
        public int Number;

        /// <summary>
        /// The asset that this FName is bound to.
        /// </summary>
        public UAsset Asset;

        /// <summary>
        /// Index into the name map of <see cref="Asset"/> that this FName points to.
        /// </summary>
        internal int Index;

        /// <summary>
        /// Dummy value. If defined, this FName does not actually point to a value in the name map, but will still act as if it does. Used for debugging and display only.
        /// </summary>
        internal FString DummyValue = null;

        /// <summary>
        /// Converts this FName instance into a human-readable string. This is the inverse of <see cref="FromString(UAsset, string)"/>.
        /// </summary>
        /// <returns>The human-readable string that represents this FName.</returns>
        public override string ToString()
        {
            //if (Value == null) return FString.NullCase;
            if (Number > 0) return Value.ToString() + "_" + (Number - 1);
            return Value.ToString();
        }

        /// <summary>
        /// Converts a human-readable string into an FName instance. This is the inverse of <see cref="ToString"/>.
        /// </summary>
        /// <param name="asset">The asset that the new FName will be bound to.</param>
        /// <param name="val">The human-readable string to convert into an FName instance.</param>
        /// <returns>An FName instance that this string represents.</returns>
        public static FName FromString(UAsset asset, string val)
        {
            if (val == null || val == FString.NullCase) return null;
            if (val.Length == 0) return new FName(asset, val, 0);

            if (val[val.Length - 1] >= '0' && val[val.Length - 1] <= '9')
            {
                int i = val.Length - 1;
                while (i > 1 && (val[i] >= '0' && val[i] <= '9'))
                {
                    i--;
                }

                if (val[i] == '_')
                {
                    string startSegment = val.Substring(0, i);
                    string endSegment = val.Substring(i + 1, val.Length - i - 1);
                    if (endSegment.Length == 1 || endSegment[0] != '0')
                    {
                        if (int.TryParse(endSegment, out int endSegmentVal)) return new FName(asset, startSegment, endSegmentVal + 1);
                    }
                }
            }
            
            return new FName(asset, val, 0);
        }

        /// <summary>
        /// Creates a new FName with the same string value and number as the current instance but is bound to a different asset.
        /// </summary>
        /// <param name="newAsset">The asset to bound the new FName to.</param>
        /// <returns>An equivalent FName bound to a different asset.</returns>
        public FName Transfer(UAsset newAsset)
        {
            return new FName(newAsset, Value, Number);
        }

        public static FName DefineDummy(UAsset asset, FString val, int number = 0)
        {
            var res = new FName();
            res.Asset = asset;
            res.DummyValue = val;
            res.Number = number;
            return res;
        }

        public static FName DefineDummy(UAsset asset, string val, int number = 0)
        {
            var res = new FName();
            res.Asset = asset;
            res.DummyValue = FString.FromString(val);
            res.Number = number;
            return res;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FName name)) return false;
            if (this is null || obj is null) return this is null && obj is null;
            return (this.Value == name.Value || this.Value.Value == name.Value.Value) && this.Number == name.Number;
        }

        public static bool operator ==(FName one, FName two)
        {
            if (one is null || two is null) return one is null && two is null;
            return one.Equals(two);
        }

        public static bool operator !=(FName one, FName two)
        {
            if (one is null || two is null) return !(one is null && two is null);
            return !one.Equals(two);
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : (Value.GetHashCode() ^ Number.GetHashCode());
        }

        public object Clone()
        {
            return new FName(Asset, (FString)Value.Clone(), Number);
        }

        /// <summary>
        /// Creates a new FName instance.
        /// </summary>
        /// <param name="asset">The asset that this FName is bound to.</param>
        /// <param name="value">The string literal that the new FName's value will be, verbatim.</param>
        /// <param name="number">The instance number of the new FName.</param>
        public FName(UAsset asset, string value, int number = 0)
        {
            Asset = asset;
            if (value == null)
            {
                Value = new FString(null);
            }
            else
            {
                Value = new FString(value);
            }
            Number = number;
        }

        /// <summary>
        /// Creates a new FName instance.
        /// </summary>
        /// <param name="asset">The asset that this FName is bound to.</param>
        /// <param name="value">The FString that the FName's value will be, verbatim.</param>
        /// <param name="number">The instance number of the new FName.</param>
        public FName(UAsset asset, FString value, int number = 0)
        {
            Asset = asset;
            Value = value;
            Number = number;
        }

        /// <summary>
        /// Creates a new FName instance.
        /// </summary>
        /// <param name="asset">The asset that this FName is bound to.</param>
        /// <param name="index">The index that this FName's value will be.</param>
        /// <param name="number">The instance number of the new FName.</param>
        public FName(UAsset asset, int index, int number = 0)
        {
            Asset = asset;
            Index = index;
            Number = number;
        }

        /// <summary>
        /// Creates a new blank FName instance.
        /// </summary>
        /// <param name="asset">The asset that this FName is bound to.</param>
        public FName(UAsset asset)
        {
            Asset = asset;
            Value = new FString(string.Empty);
            Number = 0;
        }

        /// <summary>
        /// Creates a new blank FName instance, with no asset bound to it. An asset must be bound to this FName before setting its value.
        /// </summary>
        public FName()
        {
            Number = 0;
        }
    }
}
