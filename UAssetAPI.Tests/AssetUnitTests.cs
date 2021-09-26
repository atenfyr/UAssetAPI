using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI.Tests
{
    [TestClass]
    public class AssetUnitTests
    {
        /// <summary>
        /// MapProperties contain no easy way to determine the type of structs within them.
        /// For C++ classes, it is impossible without access to the headers, but for blueprint classes, the correct serialization is contained within the UClass.
        /// In this test, we take an asset with custom struct serialization in a map and extract data from the ClassExport in order to determine the correct serialization for the structs.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestCustomSerializationStructsInMap/wtf.uasset")]
        [DeploymentItem(@"TestAssets/TestCustomSerializationStructsInMap/wtf.uexp")]
        public void TestCustomSerializationStructsInMap()
        {
            var tester = new UAsset("wtf.uasset", UE4Version.VER_UE4_24);
            Assert.IsTrue(tester.VerifyParsing());

            // Get the first property in export 2, which should be a map
            Export exportTwo = FPackageIndex.FromRawIndex(2).ToExport(tester);
            Assert.IsTrue(exportTwo is NormalExport);

            NormalExport exportTwoNormal = (NormalExport)exportTwo;
            Assert.IsTrue(exportTwoNormal.Data[0] is MapPropertyData);

            MapPropertyData testMap = (MapPropertyData)exportTwoNormal.Data[0];

            // Get the first entry of the map
            DictionaryEntry firstEntry = testMap.Value.Cast<DictionaryEntry>().ElementAt(0);
            StructPropertyData entryKey = (StructPropertyData)firstEntry.Key;
            StructPropertyData entryValue = (StructPropertyData)firstEntry.Value;

            // Check that the properties are correct
            Assert.IsTrue(entryKey.Value[0] is VectorPropertyData);
            Assert.IsTrue(entryValue.Value[0] is LinearColorPropertyData);
        }

        /// <summary>
        /// In this test, we examine a cooked asset that has been modified by an external tool.
        /// It has new name map entries whose hashes were left empty. Expected behavior is for UAssetAPI to detect this and override its normal hash algorithm.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestImproperNameMapHashes/OC_Gatling_DamageB_B.uasset")]
        [DeploymentItem(@"TestAssets/TestImproperNameMapHashes/OC_Gatling_DamageB_B.uexp")]
        public void TestImproperNameMapHashes()
        {
            var tester = new UAsset("OC_Gatling_DamageB_B.uasset", UE4Version.VER_UE4_24);
            Assert.IsTrue(tester.VerifyParsing());

            Dictionary<string, bool> testingEntries = new Dictionary<string, bool>();
            testingEntries["/Game/WeaponsNTools/GatlingGun/Overclocks/OC_BonusesAndPenalties/OC_Bonus_MovmentBonus_150p"] = false;
            testingEntries["/Game/WeaponsNTools/GatlingGun/Overclocks/OC_BonusesAndPenalties/OC_Bonus_MovmentBonus_150p.OC_Bonus_MovmentBonus_150p"] = false;

            foreach (KeyValuePair<FString, uint> overrideHashes in tester.OverrideNameMapHashes)
            {
                if (testingEntries.ContainsKey(overrideHashes.Key.Value))
                {
                    Assert.IsTrue(overrideHashes.Value == 0);
                    testingEntries[overrideHashes.Key.Value] = true;
                }
            }

            foreach (KeyValuePair<string, bool> testingEntry in testingEntries)
            {
                Assert.IsTrue(testingEntry.Value);
            }
        }
    }
}
