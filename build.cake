#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var solutionName = "kth.sln";

var kthProjectBCH = "./kth-bch/kth-bch.csproj";
var kthProjectBTC = "./kth-btc/kth-btc.csproj";
var outputDir = "./build/";

var platform = "/property:Platform=x64";

var publishToNuget = EnvironmentVariable("PUBLISH_TO_NUGET") ?? "false";

var skipNuget = EnvironmentVariable("SKIP_NUGET") ?? "false";

var conanChannel = System.IO.File.ReadAllText("./kth/conan/conan_channel").Trim();
var conanVersion = System.IO.File.ReadAllText("./kth/conan/conan_version").Trim();

void UpdateConan(string pathTarget, string currency, bool keoken)
{
    var fileTarget = System.IO.File.ReadAllText("./kth/build/Common.targets");
    fileTarget = fileTarget.Replace("$(CONAN_CHANNEL)", conanChannel);
    fileTarget = fileTarget.Replace("$(CONAN_VERSION)", conanVersion);
    fileTarget = fileTarget.Replace("$(CONAN_CURRENCY)", currency);
    fileTarget = fileTarget.Replace("$(CONAN_KEOKEN)", keoken ? "True" : "False");
    System.IO.File.WriteAllText(pathTarget,fileTarget);
}

Task("Clean")
    .Does(() => {
        
        Information("Cleaning... ");

        CleanDirectory("./kth-bch/bin");
        CleanDirectory("./kth-btc/bin");
        CleanDirectory("./console/bin");
        CleanDirectory("./tests/bch/bin");
        CleanDirectory("./tests/btc/bin");
        CleanDirectory("./tests/bch.keoken/bin");
       
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


Task("ConanVersion")
    .Does(() => {
        
        Information("Conan Channel " + conanChannel);
        Information("Conan Version " + conanVersion);

        UpdateConan("./kth-bch/build/Common.targets","BCH",true);
        UpdateConan("./kth-btc/build/Common.targets","BTC",false);
    });


Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Version")
    .IsDependentOn("ConanVersion")
    .IsDependentOn("Restore")
    .Does(() => {

        MSBuild(solutionName, new MSBuildSettings {
            ArgumentCustomization = args => args.Append(platform 
                                            + " /p:AssemblyVersion=" + versionInfo.MajorMinorPatch
                                            )
            ,Configuration = configuration
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

        DotNetCoreTest("./tests/bch", settings);
        DotNetCoreTest("./tests/btc", settings);
        DotNetCoreTest("./tests/bch.keoken", settings);
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

        DotNetCorePack(kthProjectBCH, settings);
        DotNetCorePack(kthProjectBTC, settings);

        System.IO.File.WriteAllLines(outputDir + "artifacts", new[]{
            "nuget:kth-bch." + versionInfo.NuGetVersion + ".nupkg",
            "nuget:kth-btc." + versionInfo.NuGetVersion + ".nupkg"
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
    .WithCriteria(AppVeyor.IsRunningOnAppVeyor)
    .Does(() =>
    {
        var branchName = AppVeyor.Environment.Repository.Branch;
        if (branchName != "master")
        {
            skipNuget = "true";
        }

        Information("Publish to nuget:" + publishToNuget);
        Information("Skip nuget:" + skipNuget); 
        Information("Commit message:" + AppVeyor.Environment.Repository.Commit.Message);
        Information("Branch name:" + branchName);
         
        if (publishToNuget == "true" && !AppVeyor.Environment.Repository.Commit.Message.Contains("[skip nuget]") && skipNuget == "false")
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
        }
        
    });

Task("Default")
    .IsDependentOn("DeployNuget");

RunTarget(target);