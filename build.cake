#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solutionName = "bitprim.sln";

var bitprimProjectBCH = "./bitprim-bch/bitprim-bch.csproj";
var bitprimProjectBTC = "./bitprim-btc/bitprim-btc.csproj";
var outputDir = "./build/";

var platform = "/property:Platform=x64";

var publishToNuget = EnvironmentVariable("PUBLISH_TO_NUGET") ?? "false";

Task("Clean")
    .Does(() => {
        
        Information("Cleaning... ");

        CleanDirectory("./bitprim-bch/bin");
        CleanDirectory("./bitprim-btc/bin");
        CleanDirectory("./bitprim.console/bin");
        CleanDirectory("./bitprim.tests.bch/bin");
        CleanDirectory("./bitprim.tests.btc/bin");
       
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
        //NuGetRestore(solutionName);
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
        
        DotNetCoreTest("./bitprim.tests.bch",settings);
        DotNetCoreTest("./bitprim.tests.btc",settings);
    });

Task("Package")
    .IsDependentOn("Test")
    .WithCriteria(AppVeyor.IsRunningOnAppVeyor)
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
        
    });

Task("UpdateVersionInfo")
    .IsDependentOn("Package")
    .WithCriteria(AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
    {
        var isTag = AppVeyor.Environment.Repository.Tag.IsTag && !string.IsNullOrWhiteSpace(AppVeyor.Environment.Repository.Tag.Name);
        if (isTag) 
        {
            AppVeyor.UpdateBuildVersion(AppVeyor.Environment.Repository.Tag.Name);
        }
    });

Task("DeployNuget")
    .IsDependentOn("UpdateVersionInfo")
    .WithCriteria(publishToNuget == "true")
    .Does(() =>
    {
        var files = System.IO.File
            .ReadAllLines(outputDir + "artifacts")
            .Select(l => l.Split(':'))
            .Select(l => l[1])
            .ToList();

        foreach (string f in files)
        {
            Information("Pushing to nuget " + f);
            NuGetPush(
                outputDir + f,
                new NuGetPushSettings {
                    ApiKey = EnvironmentVariable("NUGET_API_KEY"),
                    Source = "https://www.nuget.org/api/v2/package"
                });
        }
        
    });

Task("Default")
    .IsDependentOn("DeployNuget");

RunTarget(target);