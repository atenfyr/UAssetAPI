using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using UAssetAPI.CustomVersions;
using UAssetAPI.ExportTypes;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.Benchmark
{
    public class Program
    {
        private static double BenchmarkAsset(string path, EngineVersion ver, Usmap mappings = null)
        {
            var timer = new Stopwatch();
            timer.Start();
            var loaded = new UAsset(path, ver, mappings);
            timer.Stop();

            bool passBinaryEq = false;
            try
            {
                passBinaryEq = loaded.VerifyBinaryEquality();
            }
            catch { passBinaryEq = false; }

            if (!passBinaryEq)
            {
                try
                {
                    loaded.Write("test.uasset");
                }
                catch { }
            }

            int numPassedExportsTotal = 0;
            int numExportsTotal = 0;
            try
            {
                foreach (Export testExport in loaded.Exports)
                {
                    if (testExport is not RawExport)
                    {
                        numPassedExportsTotal += 1;
                    }
                    numExportsTotal += 1;
                }
            }
            catch { }

            Console.WriteLine(Path.GetFileName(path) + " parsed in " + timer.Elapsed.TotalMilliseconds + " ms");
            Console.WriteLine("Binary equality: " + (passBinaryEq ? "PASS" : "FAIL"));
            Console.WriteLine(numPassedExportsTotal + "/" + numExportsTotal + " exports (" + NumberToTwoDecimalPlaces(100 * numPassedExportsTotal / (double)numExportsTotal) + "%) passed");
            return timer.Elapsed.TotalMilliseconds;
        }

        private static string NumberToTwoDecimalPlaces(double num)
        {
            // avoid any other formatting (e.g. commas), just round decimal places
            int secondPart = (int)(num * 100 % 100);
            return (int)num + "." + (secondPart < 10 ? "0" : "") + secondPart;
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
                            mappings = new Usmap();
                            mappings.SkipBlueprintSchemas = true;
                            mappings.Read(mappings.PathToReader(assetPath));
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
                    int numPassedExportsTotal = 0;
                    timer.Restart();

                    int thresholdToForceStatusUpdate = numTotal / 4;
                    int numAtLastStatusUpdate = 0;
                    double lastMsGaveStatusUpdate = double.MinValue;
                    ISet<string> problemAssets = new HashSet<string>();
                    ISet<string> notEqualAssets = new HashSet<string>();
                    ISet<string> notParsed = new HashSet<string>();
                    Dictionary<string, Dictionary<int, string>> rawExports = new();
                    foreach (string assetPath in allTestingAssets2)
                    {
                        if (!allowedExtensions.Contains(Path.GetExtension(assetPath))) continue;

                        // give status update every once in a while
                        if (timer.Elapsed.TotalMilliseconds - lastMsGaveStatusUpdate >= 3000 || (timer.Elapsed.TotalMilliseconds - lastMsGaveStatusUpdate >= 500 && (num - numAtLastStatusUpdate) > thresholdToForceStatusUpdate))
                        {
                            lastMsGaveStatusUpdate = timer.Elapsed.TotalMilliseconds;
                            numAtLastStatusUpdate = num;
                            Console.WriteLine("[" + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds / 1000) + " s] " + num + "/" + numTotal + " assets parsed" + new string(' ', 15));
                            Console.Write("[" + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds / 1000) + " s] " + numPassedExportsTotal + "/" + numExportsTotal + " exports (" + NumberToTwoDecimalPlaces(numExportsTotal < 1 ? 0 : (numPassedExportsTotal / (double)numExportsTotal * 100)) + "%) passing" + new string(' ', 15));
                            Console.CursorTop -= 1;
                            Console.CursorLeft = 0;
                        }

                        timer.Start();
                        UAsset loaded = null;
                        try
                        {
                            loaded = new UAsset(assetPath, ver, mappings);
                        }
                        catch
                        {
                            timer.Stop();
                            numExportsTotal += loaded?.Exports?.Count ?? 0;
                            num += 1;
                            loaded = null;
                            notParsed.Add(assetPath);
                            continue;
                        }
                        timer.Stop();

                        bool isProblemAsset = false;
                        bool isNotEqual = false;
                        try
                        {
                            if (loaded.VerifyBinaryEquality())
                            {
                                numPassedBinaryEq += 1;
                            }
                            else
                            {
                                isProblemAsset = true;
                                isNotEqual = true;
                            }
                        }
                        catch
                        {
                            isProblemAsset = true;
                            isNotEqual = true;
                        }

                        bool passedAllExports = true;
                        try
                        {
                            for (int i = 0; i < loaded.Exports.Count; i++)
                            {
                                Export testExport = loaded.Exports[i];
                                if (testExport is RawExport)
                                {
                                    passedAllExports = false;
                                    if (!rawExports.ContainsKey(assetPath)) rawExports[assetPath] = [];
                                    var clas = testExport.ClassIndex.IsImport() ? testExport.ClassIndex.ToImport(loaded).ObjectName.ToString() : testExport.ClassIndex.ToExport(loaded).ObjectName.ToString();
                                    rawExports[assetPath].Add(i, clas);
                                    Console.WriteLine("Raw export found for " + assetPath + " at index " + i + " with class " + clas);
                                }
                                else
                                {
                                    numPassedExportsTotal += 1;
                                }
                                numExportsTotal += 1;
                            }
                        }
                        catch
                        {
                            passedAllExports = false;
                        }

                        if (passedAllExports)
                        {
                            numPassedAllExports += 1;
                        }
                        else
                        {
                            isProblemAsset = true;
                        }

                        if (isProblemAsset) problemAssets.Add(assetPath);
                        if (isNotEqual) notEqualAssets.Add(assetPath);
                        num += 1;
                        loaded = null;
                    }

                    Console.WriteLine();
                    Console.WriteLine(num + " assets parsed in " + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds) + " ms combined (" + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds / num) + " ms/asset, on average)");
                    Console.WriteLine(numExportsTotal + " exports were parsed (" + NumberToTwoDecimalPlaces(timer.Elapsed.TotalMilliseconds / numExportsTotal * 100) + " ms per 100 exports, on average)");
                    Console.WriteLine(numPassedExportsTotal + "/" + numExportsTotal + " exports (" + NumberToTwoDecimalPlaces(numPassedExportsTotal / (double)numExportsTotal * 100) + "%) passed");
                    Console.WriteLine(numPassedBinaryEq + "/" + num + " assets (" + NumberToTwoDecimalPlaces(numPassedBinaryEq / (double)num * 100) + "%) passed binary equality");
                    Console.WriteLine(numPassedAllExports + "/" + num + " assets (" + NumberToTwoDecimalPlaces(numPassedAllExports / (double)num * 100) + "%) passed on all exports");

                    if (notEqualAssets.Count > 0)
                    {
                        Console.WriteLine("\nList of non equal assets:");
                        File.WriteAllText("noneuqal_assets.txt", string.Join('\n', notEqualAssets));
                        Console.WriteLine("Written to noneuqal_assets.txt");
                    }

                    if (notParsed.Count > 0)
                    {
                        Console.WriteLine("\nList of not parsed assets:");
                        File.WriteAllText("nonparsed_assets.txt", string.Join('\n', notParsed));
                        Console.WriteLine("Written to nonparsed_assets.txt");
                    }

                    if (problemAssets.Count > 0)
                    {
                        int i = 0;
                        Console.WriteLine("\nList of problematic assets:");
                        foreach (string problemAsset in problemAssets)
                        {
                            Console.WriteLine(problemAsset);
                            i++;
                            if (i >= 50) break;
                        }
                        if (problemAssets.Count > 50) Console.WriteLine("...");

                        File.WriteAllText("problematic_assets.txt", string.Join('\n', problemAssets));
                        Console.WriteLine("Written to problematic_assets.txt");
                    }

                    if (rawExports.Count > 0)
                    {
                        Console.WriteLine("\nList of raw export assets:");
                        File.WriteAllText("raw_export.txt", JsonConvert.SerializeObject(rawExports, Formatting.Indented));
                        Console.WriteLine("Written to raw_export.txt");
                    }
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
                    // replace "!" for " " as stupid hack lol...
                    Usmap singleMappings = null;
                    if (args.Length >= 4) singleMappings = new Usmap(args[3].Replace("!", " "));
                    BenchmarkAsset(args[1].Replace("!", " "), (EngineVersion)Enum.Parse(typeof(EngineVersion), args[2].Replace("!", " ")), singleMappings);
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
                case "mappings":
                    //Usmap with_skip = new Usmap(@"C:\Dumper-7\with_skip.usmap");
                    Usmap no_skip = new Usmap(@"C:\Users\Alexandros\AppData\Local\UAssetGUI\Mappings\ReadyOrNot-D7-PPTH.usmap");
                    break;
            }
        }

        public static void Main(string[] args)
        {
#if DEBUG || DEBUG_VERBOSE
            Run(new string[] { "abcd" });

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
