using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI.Tests
{
    [TestClass]
    public class AssetUnitTests
    {
        /// <summary>
        /// Determines whether or not all exports in an asset have parsed correctly.
        /// </summary>
        /// <param name="tester">The asset to test.</param>
        /// <returns>true if all the exports in the asset have parsed correctly, otherwise false.</returns>
        public bool CheckAllExportsParsedCorrectly(UAsset tester)
        {
            foreach (Export testExport in tester.Exports)
            {
                if (testExport is RawExport) return false;
            }
            return true;
        }

        /// <summary>
        /// Retrieves all the test assets in a particular folder.
        /// </summary>
        /// <param name="folder">The folder to check for test assets.</param>
        /// <returns>An array of paths to assets that should be tested.</returns>
        public string[] GetAllTestAssets(string folder)
        {
            List<string> allFilesToTest = Directory.GetFiles(folder, "*.uasset").ToList();
            allFilesToTest.AddRange(Directory.GetFiles(folder, "*.umap"));
            return allFilesToTest.ToArray();
        }

        /// <summary>
        /// MapProperties contain no easy way to determine the type of structs within them.
        /// For C++ classes, it is impossible without access to the headers, but for blueprint classes, the correct serialization is contained within the UClass.
        /// In this test, we take an asset with custom struct serialization in a map and extract data from the ClassExport in order to determine the correct serialization for the structs.
        /// Binary equality is expected.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestCustomSerializationStructsInMap/wtf.uasset", "TestCustomSerializationStructsInMap")]
        [DeploymentItem(@"TestAssets/TestCustomSerializationStructsInMap/wtf.uexp", "TestCustomSerializationStructsInMap")]
        public void TestCustomSerializationStructsInMap()
        {
            var tester = new UAsset(Path.Combine("TestCustomSerializationStructsInMap", "wtf.uasset"), UE4Version.VER_UE4_25);
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
        /// As a result of external modification, the asset now has new name map entries whose hashes were left empty.
        /// Binary equality is expected. Expected behavior is for UAssetAPI to detect this and override its normal hash algorithm.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestImproperNameMapHashes/OC_Gatling_DamageB_B.uasset", "TestImproperNameMapHashes")]
        [DeploymentItem(@"TestAssets/TestImproperNameMapHashes/OC_Gatling_DamageB_B.uexp", "TestImproperNameMapHashes")]
        public void TestImproperNameMapHashes()
        {
            var tester = new UAsset(Path.Combine("TestImproperNameMapHashes", "OC_Gatling_DamageB_B.uasset"), UE4Version.VER_UE4_25);
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

        /// <summary>
        /// In this test, we examine a cooked asset that has been modified by an external tool.
        /// As a result of external modification, two identical entries now exist in the name map, which never occurs in assets cooked by the Unreal Engine.
        /// Binary equality is not expected, but the asset must successfully parse anyways.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestDuplicateNameMapEntries/BIOME_AzureWeald.uasset", "TestDuplicateNameMapEntries")]
        [DeploymentItem(@"TestAssets/TestDuplicateNameMapEntries/BIOME_AzureWeald.uexp", "TestDuplicateNameMapEntries")]
        public void TestDuplicateNameMapEntries()
        {
            var tester = new UAsset(Path.Combine("TestDuplicateNameMapEntries", "BIOME_AzureWeald.uasset"), UE4Version.VER_UE4_25);

            // Make sure a duplicate entry actually exists
            bool duplicatesExist = false;
            Dictionary<string, bool> enumeratedEntries = new Dictionary<string, bool>();
            foreach (FString entry in tester.GetNameMapIndexList())
            {
                if (enumeratedEntries.ContainsKey(entry.Value) && enumeratedEntries[entry.Value])
                {
                    duplicatesExist = true;
                    break;
                }
                enumeratedEntries[entry.Value] = true;
            }
            Assert.IsTrue(duplicatesExist);

            // Make sure all exports parsed correctly
            Assert.IsTrue(CheckAllExportsParsedCorrectly(tester));
        }

        /// <summary>
        /// In this test, we have an asset with a few properties that UAssetAPI has no serialization for. (The properties do not actually exist in the engine itself, so this is expected behavior.)
        /// UAssetAPI must fallback to UnknownPropertyType to parse the asset correctly and maintain binary equality.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestUnknownProperties/BP_DetPack_Charge.uasset", "TestUnknownProperties")]
        [DeploymentItem(@"TestAssets/TestUnknownProperties/BP_DetPack_Charge.uexp", "TestUnknownProperties")]
        public void TestUnknownProperties()
        {
            var tester = new UAsset(Path.Combine("TestUnknownProperties", "BP_DetPack_Charge.uasset"), UE4Version.VER_UE4_25);
            Assert.IsTrue(tester.VerifyParsing());
            Assert.IsTrue(CheckAllExportsParsedCorrectly(tester));

            // Check that only the expected unknown properties are present
            Dictionary<string, bool> newUnknownProperties = new Dictionary<string, bool>();
            newUnknownProperties.Add("GarbagePropty", false);
            newUnknownProperties.Add("EvenMoreGarbageTestingPropertyy", false);

            foreach (Export testExport in tester.Exports)
            {
                if (testExport is NormalExport normalTestExport)
                {
                    foreach (PropertyData prop in normalTestExport.Data)
                    {
                        if (prop is UnknownPropertyData unknownProp)
                        {
                            string serializingType = unknownProp?.SerializingPropertyType?.Value?.Value;
                            Assert.AreNotEqual(serializingType, null);
                            Assert.IsTrue(newUnknownProperties.ContainsKey(serializingType));
                            newUnknownProperties[serializingType] = true;
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, bool> entry in newUnknownProperties)
            {
                Assert.IsTrue(entry.Value);
            }
        }

        /// <summary>
        /// In this test, we examine a few assets from Astroneer (4.23) and ensure that they parse correctly and maintain binary equality.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestAstroneerAssets/DebugMenu.uasset", "TestAstroneerAssets")]
        [DeploymentItem(@"TestAssets/TestAstroneerAssets/Staging_T2.umap", "TestAstroneerAssets")]
        [DeploymentItem(@"TestAssets/TestAstroneerAssets/Augment_BroadBrush.uasset", "TestAstroneerAssets")]
        [DeploymentItem(@"TestAssets/TestAstroneerAssets/LargeResourceCanister_IT.uasset", "TestAstroneerAssets")]
        [DeploymentItem(@"TestAssets/TestAstroneerAssets/ResourceProgressCurve.uasset", "TestAstroneerAssets")]
        public void TestAstroneerAssets()
        {
            string[] allTestingAssets = GetAllTestAssets("TestAstroneerAssets");
            foreach (string assetPath in allTestingAssets)
            {
                Debug.WriteLine(assetPath);
                var tester = new UAsset(assetPath, UE4Version.VER_UE4_23);
                Assert.IsTrue(tester.VerifyParsing());
                Assert.IsTrue(CheckAllExportsParsedCorrectly(tester));
            }
        }

        /// <summary>
        /// In this test, we examine a few assets from Bloodstained (4.18) and ensure that they parse correctly and maintain binary equality.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestBloodstainedAssets/m01SIP_000_BG.umap", "TestBloodstainedAssets")]
        [DeploymentItem(@"TestAssets/TestBloodstainedAssets/m01SIP_000_Gimmick.umap", "TestBloodstainedAssets")]
        [DeploymentItem(@"TestAssets/TestBloodstainedAssets/m02VIL_004_Gimmick.umap", "TestBloodstainedAssets")]
        [DeploymentItem(@"TestAssets/TestBloodstainedAssets/PB_DT_RandomizerRoomCheck.uasset", "TestBloodstainedAssets")]
        public void TestBloodstainedAssets()
        {
            string[] allTestingAssets = GetAllTestAssets("TestBloodstainedAssets");
            foreach (string assetPath in allTestingAssets)
            {
                Debug.WriteLine(assetPath);
                var tester = new UAsset(assetPath, UE4Version.VER_UE4_18);
                Assert.IsTrue(tester.VerifyParsing());
                Assert.IsTrue(CheckAllExportsParsedCorrectly(tester));
            }
        }
    }
}
