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

    public static int Main () => Execute<Build>(x => x.Test);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;

    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";

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
            UpdateConan("./kth-bch/build/Common.targets","BCH", "4fZKi37a595hP");
            UpdateConan("./kth-btc/build/Common.targets","BTC", "4fZKi37a595hP");
        });


    Target Compile => _ => _
        .DependsOn(Clean)
        // .DependsOn(Version)
        .DependsOn(KnuthVersion)
        .DependsOn(Restore)
        .Executes(() => {

            Info("************************* Compile **************************************************");
            Info("************************* Compile **************************************************");

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

            Info("************************* TESTS **************************************************");
            Info("************************* TESTS **************************************************");

            // Info($"Solution: {Solution.GetProject("tests.bch")}");

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



// Task("Test")
//     .IsDependentOn("Build")
//     .Does(() => {

//          var settings = new DotNetCoreTestSettings
//             {
//                 ArgumentCustomization = args=> args.Append(platform + " -f netcoreapp2.0"),
//                 Configuration = configuration
//             };

//         DotNetCoreTest("./tests/bch", settings);
//         DotNetCoreTest("./tests/btc", settings);
//         DotNetCoreTest("./tests/bch.keoken", settings);
//     });

}




    // string version = null;
    // Target Version => _ => _
    //     .Executes(() => {
    //         Info("Running python get_version.py");

    //         // var process = StartProcess("python", "xunit " +
    //         //                             "-nobuild " +
    //         //                             $"-xml {testFile.DoubleQuoteIfNeeded()}",
    //         //         workingDirectory: projectDirectory)
    //         //     .AssertWaitForExit();


    //         // using(var process = StartProcess("python", new ToolSettings {Arguments = "get_version.py", RedirectOutput = true })
    //         //         .AssertWaitForExit()) {

    //         using(var process = StartProcess("python", "get_version.py", null, null, null, true, false, null, null)
    //                 .AssertWaitForExit()) {

    //             Info($"python get_version.py exit code: {process.ExitCode}");

    //             var output = process.Output;
    //             foreach (var o in output) {
    //                 Info($"o: {o.Text}");
    //                 version = o.Text;
    //                 break;
    //             }
    //         }
    //         Info($"Version calculated: {version}");

    //         // Info("Version calculated: " + versionInfo.MajorMinorPatch);
    //         // Info("Version Nuget calculated: " + versionInfo.NuGetVersion);
    //     });

