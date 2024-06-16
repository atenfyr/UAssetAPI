using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using UAssetAPI.CustomVersions;
using UAssetAPI.ExportTypes;
using UAssetAPI.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.Benchmark
{
    public class Program
    {
        private static double BenchmarkAsset(string path, EngineVersion ver)
        {
            var timer = new Stopwatch();
            timer.Start();
            new UAsset(path, ver);
            timer.Stop();

            Console.WriteLine(Path.GetFileName(path) + " parsed in " + timer.Elapsed.TotalMilliseconds + " ms");
            return timer.Elapsed.TotalMilliseconds;
        }

        private static string NumberToTwoDecimalPlaces(double num)
        {
            // avoid any other formatting (e.g. commas), just round decimal places
            return (int)num + "." + (int)(num * 100 % 100);
        }

        /// <summary>
        /// Determines whether or not all exports in an asset have parsed correctly.
        /// </summary>
        /// <param name="tester">The asset to test.</param>
        /// <returns>true if all the exports in the asset have parsed correctly, otherwise false.</returns>
        private static bool CheckAllExportsParsedCorrectly(UAsset tester)
        {
            foreach (Export testExport in tester.Exports)
            {
                if (testExport is RawExport) return false;
            }
            return true;
        }

        private static HashSet<string> allowedExtensions = new HashSet<string>()
        {
            ".umap",
            ".uasset"
        };

        public static void Run(string[] args)
        {
            Dictionary<string, string> allTestAssetVersions = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Path.Combine("TestAssets", "_asset_versions.json")));

            // initialize a uasset a few times for good measure
            for (int i = 0; i < 3; i++)
            {
                new UAsset(Path.Combine("TestAssets", "Staging_T2.umap"), EngineVersion.VER_UE4_23);
            }

            var timer = new Stopwatch();

            switch (args[0])
            {
                case "testset":
                    string[] allTestingAssets = Directory.GetFiles("TestAssets");
                    int num1 = 0;
                    double totalTime = 0;
                    foreach (string assetPath in allTestingAssets)
                    {
                        if (!allowedExtensions.Contains(Path.GetExtension(assetPath))) continue;
                        if (!allTestAssetVersions.ContainsKey(Path.GetFileNameWithoutExtension(assetPath))) continue;
                        totalTime += BenchmarkAsset(assetPath, (EngineVersion)Enum.Parse(typeof(EngineVersion), allTestAssetVersions[Path.GetFileNameWithoutExtension(assetPath)]));
                        num1 += 1;
                    }
                    Console.WriteLine("\n" + num1 + " assets parsed in " + NumberToTwoDecimalPlaces(totalTime) + " ms");
                    break;
                case "testall":
                    string[] allRelevantArgs = args.Skip(1).Take(args.Length - 2).ToArray();
                    string[] allTestingAssets2 = Directory.GetFiles(string.Join(" ", allRelevantArgs), "*.*", SearchOption.AllDirectories);
                    EngineVersion ver = (EngineVersion)Enum.Parse(typeof(EngineVersion), args[args.Length - 1]);

                    // load mappings
                    Usmap mappings = null;
                    timer.Restart();
                    foreach (string assetPath in allTestingAssets2)
                    {
                        if (Path.GetExtension(assetPath) == ".usmap")
                        {
                            timer.Start();
                            mappings = new Usmap(assetPath);
                            timer.Stop();
                            Console.WriteLine("Mappings parsed in " + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds) + " ms");
                            break;
                        }
                    }

                    // get num assets in total for status update
                    int numTotal = 0;
                    foreach (string assetPath in allTestingAssets2)
                    {
                        if (!allowedExtensions.Contains(Path.GetExtension(assetPath))) continue;
                        numTotal += 1;
                    }

                    int num = 0;
                    int numPassedBinaryEq = 0;
                    int numPassedAllExports = 0;
                    int numExportsTotal = 0;
                    timer.Restart();
                    double lastMsGaveStatusUpdate = double.MinValue;
                    foreach (string assetPath in allTestingAssets2)
                    {
                        if (!allowedExtensions.Contains(Path.GetExtension(assetPath))) continue;

                        // give status update every once in a while
                        if (timer.Elapsed.TotalMilliseconds - lastMsGaveStatusUpdate >= 500)
                        {
                            lastMsGaveStatusUpdate = timer.Elapsed.TotalMilliseconds;
                            Console.WriteLine("[" + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds) + " ms] " + num + "/" + numTotal + " assets parsed");
                        }

                        timer.Start();
                        var loaded = new UAsset(assetPath, ver, mappings);
                        timer.Stop();

                        if (loaded.VerifyBinaryEquality()) numPassedBinaryEq += 1;
                        if (CheckAllExportsParsedCorrectly(loaded)) numPassedAllExports += 1;
                        numExportsTotal += loaded.Exports.Count;
                        num += 1;
                    }

                    Console.WriteLine();
                    Console.WriteLine(num + " assets parsed in " + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds) + " ms combined (" + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds / num) + " ms/asset, on average)");
                    Console.WriteLine(numExportsTotal + " exports were parsed (" + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds / numExportsTotal * 100) + " ms per 100 exports, on average)");
                    Console.WriteLine(numPassedBinaryEq + "/" + num + " assets (" + NumberToTwoDecimalPlaces(numPassedBinaryEq / (double)num * 100) + "%) passed binary equality");
                    Console.WriteLine(numPassedAllExports + "/" + num + " assets (" + NumberToTwoDecimalPlaces(numPassedAllExports / (double)num * 100) + "%) passed on all exports");
                    break;
                case "testcpu":
                    int numCpuTrials = 5;
                    double trialSum = 0;
                    for (int i = 0; i < numCpuTrials; i++)
                    {
                        // load each time to avoid any weird stream optimizations
                        MemoryStream oneBigAsset = new UAsset().PathToStream(Path.Combine("TestAssets", "PlayerBase01.umap"));
                        var binReader = new AssetBinaryReader(oneBigAsset, null);

                        timer.Restart();
                        timer.Start();
                        new UAsset(binReader, EngineVersion.VER_UE4_22);
                        timer.Stop();

                        oneBigAsset.Dispose();
                        trialSum += timer.Elapsed.TotalMilliseconds;
                        Console.WriteLine("CPU trial " + (i + 1) + " completed in " + timer.Elapsed.TotalMilliseconds + " ms");
                    }
                    Console.WriteLine("\n" + numCpuTrials + " CPU trials completed in " + trialSum + " ms (" + (trialSum / numCpuTrials) + " ms/trial)");
                    break;
                case "test":
                    BenchmarkAsset(args[1], (EngineVersion)Enum.Parse(typeof(EngineVersion), args[2]));
                    break;
                case "guesscustomversion":
                    timer.Restart();
                    timer.Start();
                    UAsset.GuessCustomVersionFromTypeAndEngineVersion(EngineVersion.VER_UE4_AUTOMATIC_VERSION, typeof(FReleaseObjectVersion));
                    timer.Stop();
                    Console.WriteLine("Custom version first retrieved in " + timer.Elapsed.TotalMilliseconds + " ms");

                    int numCustomVersionTrials = 20000;
                    EngineVersion testingEngineVersion = EngineVersion.VER_UE4_16;
                    timer.Restart();
                    timer.Start();
                    for (int i = 0; i < numCustomVersionTrials; i++)
                    {
                        UAsset.GuessCustomVersionFromTypeAndEngineVersion(testingEngineVersion, typeof(FReleaseObjectVersion));

                        testingEngineVersion += 1;
                        if (testingEngineVersion > EngineVersion.VER_UE4_27) testingEngineVersion = EngineVersion.VER_UE4_16;
                    }
                    timer.Stop();
                    Console.WriteLine("Custom version then retrieved " + numCustomVersionTrials + " times in " + timer.Elapsed.TotalMilliseconds + " ms (" + (timer.Elapsed.TotalMilliseconds / numCustomVersionTrials) + " ms/trial)");
                    break;
                case "longestnames":
                    timer.Restart();
                    timer.Start();
                    Assembly relevantAssembly = typeof(UAsset).Assembly;
                    List<string> lineas = new List<string>();
                    foreach (Type type in relevantAssembly.GetTypes())
                    {
                        if (type.IsEnum) continue;
                        lineas.Add(type.Name);
                        foreach (MemberInfo memb in type.GetMembers()) lineas.Add(memb.Name);
                    }

                    Console.WriteLine(string.Join('\n', lineas.OrderByDescending(x => x.Length).Distinct().Take(20)));
                    Console.WriteLine("Operation completed in " + timer.Elapsed.TotalMilliseconds + " ms");
                    timer.Stop();
                    break;
                case "zen":
                    IOStoreContainer test1 = new IOStoreContainer(@"C:\Program Files (x86)\Steam\steamapps\common\Garten Of Banban\Clay\Content\Paks\global.utoc");
                    ZenAsset test = new ZenAsset(EngineVersion.VER_UE5_1, new Usmap(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UAssetGUI", "Mappings", "Clay.usmap")));
                    test.GlobalData = new IOGlobalData(test1, EngineVersion.VER_UE5_1);
                    test.Read(test.PathToReader(Path.Combine("TestAssets", "B_Gamemode.uasset")));
                    Console.WriteLine(test.Name);

                    MemoryStream testStrm = new MemoryStream();
                    new AssetBinaryWriter(testStrm, test).WriteNameBatch(test.HashVersion, (IList<FString>)test.GetNameMapIndexList());
                    Console.WriteLine(BitConverter.ToString(testStrm.ToArray()));
                    break;
            }
        }

        public static void Main(string[] args)
        {
#if DEBUG || DEBUG_VERBOSE
            while (true)
            {
                Console.Write("Input: ");
                string inp = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inp)) return;
                Run(inp.Split(' '));
                Console.WriteLine();
            }
#else
            Run(args);
#endif
        }
    }
}
