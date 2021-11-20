using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
            Assert.IsTrue(tester.VerifyBinaryEquality());

            // Get the map property in export 2
            Export exportTwo = FPackageIndex.FromRawIndex(2).ToExport(tester);
            Assert.IsTrue(exportTwo is NormalExport);

            NormalExport exportTwoNormal = (NormalExport)exportTwo;

            var mapPropertyName = FName.FromString("KekWait");
            MapPropertyData testMap = exportTwoNormal[mapPropertyName] as MapPropertyData;
            Assert.IsNotNull(testMap);
            Assert.IsTrue(testMap == exportTwoNormal[mapPropertyName.Value.Value]);

            // Get the first entry of the map
            StructPropertyData entryKey = testMap?.Value?.Keys?.ElementAt(0) as StructPropertyData;
            StructPropertyData entryValue = testMap?.Value?[0] as StructPropertyData;
            Assert.IsNotNull(entryKey?.Value?[0]);
            Assert.IsNotNull(entryValue?.Value?[0]);

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
            Assert.IsTrue(tester.VerifyBinaryEquality());

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
            Assert.IsTrue(tester.VerifyBinaryEquality());
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

        private void TestManyAssetsSubsection(string game, UE4Version version)
        {
            string[] allTestingAssets = GetAllTestAssets(Path.Combine("TestManyAssets", game));
            foreach (string assetPath in allTestingAssets)
            {
                Debug.WriteLine(assetPath);
                var tester = new UAsset(assetPath, version);
                Assert.IsTrue(tester.VerifyBinaryEquality());
                Assert.IsTrue(CheckAllExportsParsedCorrectly(tester));
            }
        }

        /// <summary>
        /// In this test, we examine a variety of assets from different games and ensure that they parse correctly and maintain binary equality.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestManyAssets/Astroneer/DebugMenu.uasset", "TestManyAssets/Astroneer")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Astroneer/Staging_T2.umap", "TestManyAssets/Astroneer")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Astroneer/Augment_BroadBrush.uasset", "TestManyAssets/Astroneer")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Astroneer/LargeResourceCanister_IT.uasset", "TestManyAssets/Astroneer")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Astroneer/ResourceProgressCurve.uasset", "TestManyAssets/Astroneer")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/m01SIP_000_BG.umap", "TestManyAssets/Bloodstained")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/m01SIP_000_Gimmick.umap", "TestManyAssets/Bloodstained")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/m02VIL_004_Gimmick.umap", "TestManyAssets/Bloodstained")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/PB_DT_RandomizerRoomCheck.uasset", "TestManyAssets/Bloodstained")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/PB_DT_ItemMaster.uasset", "TestManyAssets/Bloodstained")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/m05SAN_000_Gimmick.uasset", "TestManyAssets/Bloodstained")]
        public void TestManyAssets()
        {
            TestManyAssetsSubsection("Astroneer", UE4Version.VER_UE4_23);
            TestManyAssetsSubsection("Bloodstained", UE4Version.VER_UE4_18);
        }

        /// <summary>
        /// In this test, we examine and modify a DataTable to ensure that it parses correctly and maintains binary equality.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/PB_DT_RandomizerRoomCheck.uasset", "TestDataTables")]
        public void TestDataTables()
        {
            var tester = new UAsset(Path.Combine("TestDatatables", "PB_DT_RandomizerRoomCheck.uasset"), UE4Version.VER_UE4_18);
            Assert.IsTrue(tester.VerifyBinaryEquality());
            Assert.IsTrue(CheckAllExportsParsedCorrectly(tester));
            Assert.IsTrue(tester.Exports.Count == 1);

            var ourDataTableExport = tester.Exports[0] as DataTableExport;
            var ourTable = ourDataTableExport?.Table;
            Assert.IsNotNull(ourTable);

            // Check out the first entry to make sure it's parsing alright, and flip all the flags for later testing
            StructPropertyData firstEntry = ourTable.Data[0];

            bool didFindTestName = false;
            for (int i = 0; i < firstEntry.Value.Count; i++)
            {
                var propData = firstEntry.Value[i];
                Debug.WriteLine(i + ": " + propData.Name + ", " + propData.PropertyType);
                if (propData.Name == new FName("AcceleratorANDDoubleJump")) didFindTestName = true;
                if (propData is BoolPropertyData boolProp) boolProp.Value = !boolProp.Value;
            }
            Assert.IsTrue(didFindTestName);

            // Save the modified table
            tester.Write(Path.Combine("TestDatatables", "MODIFIED.uasset"));

            // Load the modified table back in and make sure we're good
            var tester2 = new UAsset(Path.Combine("TestDatatables", "MODIFIED.uasset"), UE4Version.VER_UE4_18);
            Assert.IsTrue(tester2.VerifyBinaryEquality());
            Assert.IsTrue(CheckAllExportsParsedCorrectly(tester2));
            Assert.IsTrue(tester2.Exports.Count == 1);

            // Flip the flags back to what they originally were
            firstEntry = (tester2.Exports[0] as DataTableExport)?.Table?.Data?[0];
            Assert.IsNotNull(firstEntry);
            for (int i = 0; i < firstEntry.Value.Count; i++)
            {
                if (firstEntry.Value[i] is BoolPropertyData boolProp) boolProp.Value = !boolProp.Value;
            }

            // Save and check that it's binary equal to what we originally had
            tester2.Write(tester2.FilePath);
            Assert.IsTrue(File.ReadAllBytes(Path.Combine("TestDatatables", "PB_DT_RandomizerRoomCheck.uasset")).SequenceEqual(File.ReadAllBytes(Path.Combine("TestDatatables", "MODIFIED.uasset"))));
        }

        private void TestJsonOnFile(string file, UE4Version version)
        {
            Debug.WriteLine(file);
            var tester = new UAsset(Path.Combine("TestJson", file), version);
            Assert.IsTrue(tester.VerifyBinaryEquality());
            Assert.IsTrue(CheckAllExportsParsedCorrectly(tester));

            string jsonSerializedAsset = tester.SerializeJson();
            File.WriteAllText(Path.Combine("TestJson", "raw.json"), jsonSerializedAsset);

            var tester2 = UAsset.DeserializeJson(File.ReadAllText(Path.Combine("TestJson", "raw.json")));
            tester2.Write(Path.Combine("TestJson", "MODIFIED.uasset"));

            // For the assets we're testing binary equality is maintained and can be used as a metric of success, but binary equality is not guaranteed for most assets
            Assert.IsTrue(File.ReadAllBytes(Path.Combine("TestJson", file)).SequenceEqual(File.ReadAllBytes(Path.Combine("TestJson", "MODIFIED.uasset"))));
        }

        /// <summary>
        /// In this test, we serialize some assets to JSON and back to test if the JSON serialization system is functional.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/PB_DT_RandomizerRoomCheck.uasset", "TestJson")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/m02VIL_004_Gimmick.umap", "TestJson")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Bloodstained/m05SAN_000_Gimmick.uasset", "TestJson")]
        [DeploymentItem(@"TestAssets/TestManyAssets/Astroneer/Staging_T2.umap", "TestJson")]
        [DeploymentItem(@"TestAssets/TestJson/ABP_SMG_A.uasset", "TestJson")]
        [DeploymentItem(@"TestAssets/TestJson/ABP_SMG_A.uexp", "TestJson")]
        public void TestJson()
        {
            TestJsonOnFile("PB_DT_RandomizerRoomCheck.uasset", UE4Version.VER_UE4_18);
            TestJsonOnFile("m02VIL_004_Gimmick.umap", UE4Version.VER_UE4_18);
            TestJsonOnFile("Staging_T2.umap", UE4Version.VER_UE4_23);
            TestJsonOnFile("ABP_SMG_A.uasset", UE4Version.VER_UE4_25);
        }
    }
}
