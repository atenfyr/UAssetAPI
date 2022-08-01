using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI
{
    public static class UAPUtils
    {
        public static string CurrentCommit = string.Empty;
        
        public static string SerializeJson(object obj, Formatting jsonFormatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(obj, jsonFormatting, UAsset.jsonSettings);
        }

        public static List<T> FindAllInstances<T>(object parent) where T : class
        {
            HashSet<object> yaExplorado = new HashSet<object>();
            List<T> res = new List<T>();

            FindAllInstances(parent, yaExplorado, res);

            return res;
        }

        private static void FindAllInstances<T>(object value, HashSet<object> yaExplorado, List<T> res) where T : class
        {
            if (value == null) return;
            if (yaExplorado.Contains(value)) return;

            yaExplorado.Add(value);

            if (value is IEnumerable enumerable)
            {
                foreach (object item in enumerable)
                {
                    FindAllInstances<T>(item, yaExplorado, res);
                }
            }
            else
            {
                if (value is T match) res.Add(match);

                PropertyInfo[] properties = value.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty);
                foreach (PropertyInfo property in properties)
                {
                    if (property.GetIndexParameters().Length > 0) continue;
                    if (value is PropertyData && (property.Name == "PropertyType" || property.Name == "HasCustomStructSerialization")) continue;
                    FindAllInstances<T>(property.GetValue(value, null), yaExplorado, res);
                }

                FieldInfo[] fields = value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (FieldInfo field in fields)
                {
                    FindAllInstances<T>(field.GetValue(value), yaExplorado, res);
                }
            }
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static FieldInfo[] GetOrderedFields<T>()
        {
            return typeof(T).GetFields().Where(fld => fld.IsDefined(typeof(DisplayIndexOrderAttribute), true)).OrderBy(fld => ((DisplayIndexOrderAttribute[])fld.GetCustomAttributes(typeof(DisplayIndexOrderAttribute), true))[0].DisplayingIndex).ToArray();
        }

        public static FieldInfo[] GetOrderedFields(Type t)
        {
            return t.GetFields().Where(fld => fld.IsDefined(typeof(DisplayIndexOrderAttribute), true)).OrderBy(fld => ((DisplayIndexOrderAttribute[])fld.GetCustomAttributes(typeof(DisplayIndexOrderAttribute), true))[0].DisplayingIndex).ToArray();
        }

        public static FString GetImportNameReferenceWithoutZero(int j, UAsset asset)
        {
            FString refer = new FPackageIndex(j).ToImport(asset).ObjectName.Value;
            if (!asset.ContainsNameReference(refer)) return refer;
            return asset.GetNameReferenceWithoutZero(asset.SearchNameReference(refer));
        }

        public static uint[] InterpretAsGuidAndConvertToUnsignedInts(this string value)
        {
            Guid res = value.Trim().ConvertToGUID();
            return res.ToUnsignedInts();
        }

        public static uint[] ToUnsignedInts(this Guid value)
        {
            byte[] vals = value.ToByteArray();
            uint[] res = new uint[4];
            res[0] = BitConverter.ToUInt32(vals, 0);
            res[1] = BitConverter.ToUInt32(vals, sizeof(uint));
            res[2] = BitConverter.ToUInt32(vals, sizeof(uint) * 2);
            res[3] = BitConverter.ToUInt32(vals, sizeof(uint) * 3);
            return res;
        }

        public static Guid GUID(uint value1, uint value2, uint value3, uint value4)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value1).CopyTo(bytes, 0);
            BitConverter.GetBytes(value2).CopyTo(bytes, sizeof(uint));
            BitConverter.GetBytes(value3).CopyTo(bytes, sizeof(uint) * 2);
            BitConverter.GetBytes(value4).CopyTo(bytes, sizeof(uint) * 3);
            return new Guid(bytes);
        }

        public static Guid ConvertToGUID(this string GuidString)
        {
            if (GuidString == null ||
            (GuidString[0] != '{') ||
            (GuidString[9] != '-') ||
            (GuidString[14] != '-') ||
            (GuidString[19] != '-') ||
            (GuidString[24] != '-') ||
            (GuidString[37] != '}'))
            {
                if (Guid.TryParse(GuidString, out Guid res1)) return res1;
                return Guid.Empty;
            }

            string byteText = GuidString.Substring(29, 8) + GuidString.Substring(20, 4) + GuidString.Substring(25, 4) + GuidString.Substring(10, 4) + GuidString.Substring(15, 4) + GuidString.Substring(1, 8);
            byte[] byteArr = ConvertHexStringToByteArray(byteText);
            Array.Reverse(byteArr);
            return new Guid(byteArr);
        }

        public static string ConvertToString(this Guid val)
        {
            byte[] byteArr = val.ToByteArray();
            Array.Reverse(byteArr);
            string bytes = BitConverter.ToString(byteArr).Replace("-", "");
            return ("{" + bytes.Substring(24, 8) + "-" + bytes.Substring(16, 4) + "-" + bytes.Substring(20, 4) + "-" + bytes.Substring(8, 4) + "-" + bytes.Substring(12, 4) + bytes.Substring(0, 8) + "}").ToUpperInvariant();
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            hexString = hexString.Replace(" ", string.Empty).Replace("-", string.Empty);

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
    }
}
