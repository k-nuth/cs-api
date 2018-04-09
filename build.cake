#tool "nuget:?package=GitVersion.CommandLine"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solutionName = "bitprim.sln";

var bitprimProjectBCH = "./bitprim-bch/bitprim-bch.csproj";
var bitprimProjectBTC = "./bitprim-btc/bitprim-btc.csproj";
var outputDir = "./build/";

var platform = "/property:Platform=x64";

Task("Clean")
    .Does(() => {
        
        Information("Cleaning... ");

        CleanDirectory("./bitprim-bch/bin");
        CleanDirectory("./bitprim.console/bin");
        CleanDirectory("./bitprim.tests/bin");
        CleanDirectory("./bitprim-btc/bin");
    
        if (DirectoryExists(outputDir))
        {
            DeleteDirectory(outputDir, new DeleteDirectorySettings {
                            Recursive = true,
                            Force = true
            });
        }
    });

Task("Restore")
    .Does(() => {
        DotNetCoreRestore(solutionName);
        NuGetRestore(solutionName);
    });

GitVersion versionInfo = null;
Task("Version")
    .Does(() => {
        
        GitVersion(new GitVersionSettings{
            UpdateAssemblyInfo = false,
            OutputType = GitVersionOutput.BuildServer
        });
        
        versionInfo = GitVersion(new GitVersionSettings{ OutputType = GitVersionOutput.Json });        

        Information("Version calculated: " + versionInfo.MajorMinorPatch);

        Information("Version Nuget calculated: " + versionInfo.NuGetVersion);

    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Version")
    .IsDependentOn("Restore")
    .Does(() => {

        MSBuild(solutionName, new MSBuildSettings {
            ArgumentCustomization = args => args.Append(platform + " /p:AssemblyVersion=" + versionInfo.MajorMinorPatch),        
            Configuration = configuration
        });

    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        
         var settings = new DotNetCoreTestSettings
            {
                ArgumentCustomization = args=> args.Append(platform + " -f netcoreapp2.0"),
                Configuration = configuration
            };
        
        DotNetCoreTest("./bitprim.tests",settings);
    });

Task("Package")
    .IsDependentOn("Test")
    .Does(() => {

        var settings = new DotNetCorePackSettings
        {
            ArgumentCustomization = args=> args.Append(platform + " /p:PackageVersion=" + versionInfo.NuGetVersion),
            OutputDirectory = outputDir,
            NoBuild = true,
            NoRestore = true,
            Configuration = configuration
        };

        DotNetCorePack(bitprimProjectBCH, settings);
        DotNetCorePack(bitprimProjectBTC, settings);

        System.IO.File.WriteAllLines(outputDir + "artifacts", new[]{
            "nuget:bitprim-bch." + versionInfo.NuGetVersion + ".nupkg",
            "nuget:bitprim-btc." + versionInfo.NuGetVersion + ".nupkg"
        });

        if (AppVeyor.IsRunningOnAppVeyor)
        {
            foreach (var file in GetFiles(outputDir + "**/*"))
                AppVeyor.UploadArtifact(file.FullPath);
        }
    });

Task("Default")
    .IsDependentOn("Package");

RunTarget(target);