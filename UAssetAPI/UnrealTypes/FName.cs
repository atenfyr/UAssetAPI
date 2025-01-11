using Newtonsoft.Json;
using System;
using UAssetAPI.JSON;

namespace UAssetAPI.UnrealTypes
{
    public enum EMappedNameType
    {
        Package,
        Container,
        Global
    }

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
                // TODO: if type = Global, retrieve value from global name map (= script objects from usmap apparently?)
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

        internal const int IndexBits = 30;
        internal const uint IndexMask = (1u << IndexBits) - 1u;
        internal const uint TypeMask = ~IndexMask;
        internal const int TypeShift = IndexBits;

        /// <summary>
        /// The type of this FName; i.e. whether it points to a package-level name table, container-level name table, or global name table. This value is always <see cref="EMappedNameType.Package"/> for non-Zen assets.
        /// </summary>
        public EMappedNameType Type = EMappedNameType.Package;

        /// <summary>
        /// Does this FName point into the global name table? This value is always false for non-Zen assets.
        /// </summary>
        public bool IsGlobal => Type != EMappedNameType.Package;

        /// <summary>
        /// The asset that this FName is bound to.
        /// </summary>
        public INameMap Asset;

        private int _index;
        /// <summary>
        /// Index into the name map of <see cref="Asset"/> that this FName points to.
        /// </summary>
        internal int Index
        {
            get
            {
                if (IsDummy) throw new InvalidOperationException("Attempt to retrieve index of dummy FName");
                return _index;
            }
            set
            {
                _index = value;
                DummyValue = null;
            }
        }

        /// <summary>
        /// Dummy value. If defined, this FName does not actually point to a value in any name map, but will still act as if it does.
        /// </summary>
        internal FString DummyValue = null;

        /// <summary>
        /// Converts this FName instance into a human-readable string. This is the inverse of <see cref="FromString(INameMap, string)"/>.
        /// </summary>
        /// <returns>The human-readable string that represents this FName.</returns>
        public override string ToString()
        {
            if (Value == null) return FString.NullCase;
            if (Number > 0) return Value.ToString() + "_" + (Number - 1);
            return Value.ToString();
        }

        internal static void FromStringFragments(INameMap asset, string val, out string str, out int num)
        {
            str = val; num = 0;

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
                        if (int.TryParse(endSegment, out int endSegmentVal))
                        {
                            str = startSegment;
                            num = endSegmentVal + 1;
                            return;
                        }
                    }
                }
            }
        }

        public static bool IsFromStringValid(INameMap asset, string val)
        {
            if (val == null || val == FString.NullCase) return true;
            if (val.Length == 0) return true;

            FromStringFragments(asset, val, out string value, out _);
            return asset.ContainsNameReference(FString.FromString(value));
        }

        /// <summary>
        /// Converts a human-readable string into an FName instance. This is the inverse of <see cref="ToString"/>.
        /// </summary>
        /// <param name="asset">The asset that the new FName will be bound to.</param>
        /// <param name="val">The human-readable string to convert into an FName instance.</param>
        /// <returns>An FName instance that this string represents.</returns>
        public static FName FromString(INameMap asset, string val)
        {
            if (val == null || val == FString.NullCase) return null;
            if (val.Length == 0) return new FName(asset, val, 0);

            FromStringFragments(asset, val, out string value, out int number);
            return new FName(asset, value, number);
        }

        /// <summary>
        /// Creates a new FName with the same string value and number as the current instance but is bound to a different asset.
        /// </summary>
        /// <param name="newAsset">The asset to bound the new FName to.</param>
        /// <returns>An equivalent FName bound to a different asset.</returns>
        public FName Transfer(INameMap newAsset)
        {
            return new FName(newAsset, Value, Number);
        }

        /// <summary>
        /// Creates a new dummy FName.
        /// This can be used for cases where a valid FName must be produced without referencing a specific asset's name map.
        /// <para />
        /// USE WITH CAUTION! UAssetAPI must never attempt to serialize a dummy FName to disk.
        /// </summary>
        /// <param name="asset">The asset that this FName is bound to.</param>
        /// <param name="val">The FString that the FName's value will be, verbatim.</param>
        /// <param name="number">The instance number of the new FName.</param>
        /// <returns>A dummy FName instance that represents the string.</returns>
        public static FName DefineDummy(INameMap asset, FString val, int number = 0)
        {
            if (asset != null && !asset.CanCreateDummies())
            {
                return new FName(asset, val, number);
            }

            var res = new FName();
            res.Asset = asset;
            res.DummyValue = val;
            res.Number = number;
            return res;
        }

        /// <summary>
        /// Creates a new dummy FName.
        /// This can be used for cases where a valid FName must be produced without referencing a specific asset's name map.
        /// <para />
        /// USE WITH CAUTION! UAssetAPI must never attempt to serialize a dummy FName to disk.
        /// </summary>
        /// <param name="asset">The asset that this FName is bound to.</param>
        /// <param name="val">The string literal that the FName's value will be, verbatim.</param>
        /// <param name="number">The instance number of the new FName.</param>
        /// <returns>A dummy FName instance that represents the string.</returns>
        public static FName DefineDummy(INameMap asset, string val, int number = 0)
        {
            if (asset != null && !asset.CanCreateDummies())
            {
                return new FName(asset, val, number);
            }

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
            if (this.Asset != name.Asset) return this.Value.ToString().Equals(name.Value.ToString()); // if assets aren't the same, compare string values
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
            if (this.IsDummy) return DefineDummy(Asset, (FString)Value.Clone(), Number);
            return new FName(Asset, (FString)Value.Clone(), Number);
        }

        /// <summary>
        /// Creates a new FName instance.
        /// </summary>
        /// <param name="asset">The asset that this FName is bound to.</param>
        /// <param name="value">The string literal that the new FName's value will be, verbatim.</param>
        /// <param name="number">The instance number of the new FName.</param>
        public FName(INameMap asset, string value, int number = 0)
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
        public FName(INameMap asset, FString value, int number = 0)
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
        public FName(INameMap asset, int index, int number = 0)
        {
            Asset = asset;
            Index = index;
            Number = number;
        }

        /// <summary>
        /// Creates a new blank FName instance.
        /// </summary>
        /// <param name="asset">The asset that this FName is bound to.</param>
        public FName(INameMap asset)
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
