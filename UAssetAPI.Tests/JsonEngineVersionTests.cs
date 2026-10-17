using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UAssetAPI.ExportTypes;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.Tests
{
    [TestClass]
    public class JsonEngineVersionTests
    {
        private static UAsset MakeAsset()
        {
            var asset = new UAsset(EngineVersion.VER_UE5_8);
            asset.ObjectVersion = ObjectVersion.VER_UE4_NON_OUTER_PACKAGE_IMPORT;
            asset.ObjectVersionUE5 = ObjectVersionUE5.OPTIONAL_RESOURCES;
            asset.CustomVersionContainer = new List<CustomVersion>
            {
                new CustomVersion(new Guid("11335577-2244-6688-aacc-ee0011223344"), 73)
            };
            asset.Exports = new List<Export>();
            asset.Imports = new List<Import>();
            return asset;
        }

        private static string Hint(UAsset asset) =>
            typeof(UAsset).GetProperty("SpecifiedEngineVersion", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(asset).ToString();

        private static void AssertVersions(UAsset expected, UAsset actual)
        {
            Assert.AreEqual(expected.ObjectVersion, actual.ObjectVersion);
            Assert.AreEqual(expected.ObjectVersionUE5, actual.ObjectVersionUE5);
            Assert.AreEqual(1, actual.CustomVersionContainer.Count);
            Assert.AreEqual(expected.CustomVersionContainer[0].Key, actual.CustomVersionContainer[0].Key);
            Assert.AreEqual(73, actual.CustomVersionContainer[0].Version);
        }

        [TestMethod]
        public void StringAndStreamJsonOmitRuntimeHintAndPreserveOriginalVersions()
        {
            var original = MakeAsset();
            var json = original.SerializeJson();
            Assert.AreEqual("VER_UE5_8", Hint(original));
            Assert.IsNull(JObject.Parse(json)["SpecifiedEngineVersion"]);
            var fromString = UAsset.DeserializeJson(json);
            Assert.AreEqual("UNKNOWN", Hint(fromString));
            AssertVersions(original, fromString);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var fromStream = UAsset.DeserializeJson(stream);
            Assert.AreEqual("UNKNOWN", Hint(fromStream));
            AssertVersions(original, fromStream);
        }

        [TestMethod]
        public void LegacyJsonHintCanBeRestoredWithoutNormalizingHeaderVersions()
        {
            var original = MakeAsset();
            var json = JObject.Parse(original.SerializeJson());
            json.Remove("SpecifiedEngineVersion");
            var legacy = UAsset.DeserializeJson(json.ToString());
            Assert.AreEqual("UNKNOWN", Hint(legacy));
            var customVersions = legacy.CustomVersionContainer;
            legacy.SetSerializationEngineVersion(EngineVersion.VER_UE5_8);
            Assert.AreEqual("VER_UE5_8", Hint(legacy));
            Assert.AreSame(customVersions, legacy.CustomVersionContainer);
            AssertVersions(original, legacy);
            legacy.SetSerializationEngineVersion(EngineVersion.UNKNOWN);
            Assert.AreEqual("VER_UE5_8", Hint(legacy));
            AssertVersions(original, legacy);
        }

        [TestMethod]
        public void InvalidHintDoesNotMutateAsset()
        {
            var asset = MakeAsset();
            var before = asset.SerializeJson();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                asset.SetSerializationEngineVersion((EngineVersion)int.MaxValue));
            Assert.AreEqual(before, asset.SerializeJson());
        }

        [TestMethod]
        public void ObsoleteJsonHintIsIgnoredForStringAndStreamLoads()
        {
            var asset = MakeAsset();
            asset.SetSerializationEngineVersion(EngineVersion.VER_UE5_7);
            var json = JObject.Parse(asset.SerializeJson());
            json["SpecifiedEngineVersion"] = "VER_UE5_8";
            var copy = UAsset.DeserializeJson(json.ToString());
            Assert.AreEqual("UNKNOWN", Hint(copy));
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(json.ToString()));
            Assert.AreEqual("UNKNOWN", Hint(UAsset.DeserializeJson(stream)));
            AssertVersions(asset, copy);
        }

        [TestMethod]
        public void SaveSelectionCanChangeWithoutResettingCustomVersions()
        {
            var asset = MakeAsset();
            asset.SetSerializationEngineVersion(EngineVersion.VER_UE5_7);
            var copy = UAsset.DeserializeJson(asset.SerializeJson());
            copy.SetSerializationEngineVersion(EngineVersion.VER_UE5_7);
            Assert.AreEqual("VER_UE5_7", Hint(copy));
            copy.SetSerializationEngineVersion(EngineVersion.VER_UE5_8);
            Assert.AreEqual("VER_UE5_8", Hint(copy));
            AssertVersions(asset, copy);
        }
    }
}
