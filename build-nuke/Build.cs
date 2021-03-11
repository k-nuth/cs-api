using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.CI.AppVeyor;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Logger;
using static Nuke.Common.Tooling.ProcessTasks;
// using static Nuke.Common.Tooling.ToolSettings;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    string knuthChannel = System.IO.File.ReadAllText("./kth/conan/conan_channel").Trim();
    string knuthVersion = System.IO.File.ReadAllText("./kth/conan/conan_version").Trim();

    public void UpdateConan(string pathTarget, string currency, string marchId) {
        Info($"Channel    {knuthChannel}");
        Info($"Version    {knuthVersion}");
        Info($"Currency   {currency}");
        Info($"MarchId    {marchId}");

        var content = System.IO.File.ReadAllText("./kth/build/Common.targets");
        content = content.Replace("$(KNUTH_CHANNEL)", knuthChannel);
        content = content.Replace("$(KNUTH_VERSION)", knuthVersion);
        content = content.Replace("$(KNUTH_CURRENCY)", currency);
        content = content.Replace("$(KNUTH_MARCH_ID)", marchId);
        System.IO.File.WriteAllText(pathTarget, content);
    }

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath NugetDirectory => OutputDirectory / "nuget";

    [Parameter] string NugetApiUrl = "https://api.nuget.org/v3/index.json"; //default
    [Parameter] string NugetApiKey;

    // var publishToNuget = EnvironmentVariable("PUBLISH_TO_NUGET") ?? "false";
    // var skipNuget = EnvironmentVariable("SKIP_NUGET") ?? "false";
    string publishToNuget = Environment.GetEnvironmentVariable("PUBLISH_TO_NUGET") ?? "false";
    string skipNuget = Environment.GetEnvironmentVariable("SKIP_NUGET") ?? "false";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() => {
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target KnuthVersion => _ => _
        .Executes(() => {
            //TODO(fernando): march_id is hardcoded, see what to do.
            UpdateConan("./kth-bch/build/Common.targets", "BCH", "4fZKi37a595hP");
            // UpdateConan("./kth-btc/build/Common.targets", "BTC", "4fZKi37a595hP");
        });


    Target Compile => _ => _
        .DependsOn(Clean)
        // .DependsOn(Version)
        .DependsOn(KnuthVersion)
        .DependsOn(Restore)
        .Executes(() => {

            // Info($"GitVersion.AssemblySemVer:       {GitVersion.AssemblySemVer}");
            // Info($"GitVersion.AssemblySemFileVer:   {GitVersion.AssemblySemFileVer}");
            // Info($"GitVersion.InformationalVersion: {GitVersion.InformationalVersion}");

            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoRestore());
        });


    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() => {

            DotNetTest(s => s
                // get the test project
                .SetProjectFile(Solution.GetProject("tests.bch"))
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                // write the trx files for azure pipelines
                .SetLogger("trx")
                .SetLogOutput(true)
                // collect code coverage to use services like codecov or coveralls
                // .SetArgumentConfigurator(arguments => arguments.Add("/p:CollectCoverage={0}", true)
                //     .Add("/p:CoverletOutput={0}/", OutputDirectory / "coverage")
                //     .Add("/p:UseSourceLink={0}", "true")
                //     .Add("/p:CoverletOutputFormat={0}", "cobertura"))
                // .SetResultsDirectory(OutputDirectory / "tests")
                );
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Executes(() => {
            DotNetPack(s => s
                .SetProject(Solution.GetProject("kth-bch"))
                .SetConfiguration(Configuration)
                .EnableNoBuild()
                .EnableNoRestore()
                .SetDescription("Bitcoin full node as a C# library")
                .SetPackageTags("bitcoin cash bch btc knuth kth blockchain c# cs dotnet .net")
                .SetVersion(GitVersion.NuGetVersionV2)
                .SetNoDependencies(true)
                .SetOutputDirectory(NugetDirectory)
                .SetAuthors("Knuth")
                .SetPackageProjectUrl("https://github.com/k-nuth/cs-api")
            );
        });

    Target Push => _ => _
        .DependsOn(Pack)
        .Requires(() => NugetApiUrl)
        .Requires(() => NugetApiKey)
        .Requires(() => Configuration.Equals(Configuration.Release))

        // .Requires(() => AppVeyor.IsRunningAppVeyor)
        // .Requires(() => !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPVEYOR")))
        .Requires(() => !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("GHA_BRANCH")))

        .Executes(() => {

            // var branchName = AppVeyor.Environment.Repository.Branch;
            // var branchName = Environment.GetEnvironmentVariable("APPVEYOR_REPO_BRANCH");
            // var commitMessage = Environment.GetEnvironmentVariable("APPVEYOR_REPO_COMMIT_MESSAGE");

            var branchName = Environment.GetEnvironmentVariable("GHA_BRANCH");
            var commitMessage = Environment.GetEnvironmentVariable("GHA_COMMIT_MSG");

            Info($"Publish to nuget: {publishToNuget}");
            Info($"Commit message: {commitMessage}");
            // Info($"Commit message:" + AppVeyor.Environment.Repository.Commit.Message);
            Info($"Branch name:" + branchName);
            Info($"Skip nuget: {skipNuget}"); 

            // if (branchName != "master") {
            //     skipNuget = "true";
            // }

            if (branchName == "dev" && branchName.StartsWith("release-") && branchName.StartsWith("feature")) {
                skipNuget = "true";
            }

            Info($"Skip nuget: {skipNuget}"); 

            // if (publishToNuget == "true" && !AppVeyor.Environment.Repository.Commit.Message.Contains("[skip nuget]") && skipNuget == "false") {
            if (publishToNuget == "true" && skipNuget == "false" && ! commitMessage.Contains("[skip nuget]")) {
                GlobFiles(NugetDirectory, "*.nupkg")
                    .NotEmpty()
                    .Where(x => !x.EndsWith("symbols.nupkg"))
                    .ForEach(x => {
                        DotNetNuGetPush(s => s
                            .SetTargetPath(x)
                            .SetSource(NugetApiUrl)
                            .SetApiKey(NugetApiKey)
                        );
                    });
            }
        });
}
