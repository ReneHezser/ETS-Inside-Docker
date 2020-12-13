///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

var etsVersion = "1.4.0";
var toolsDir = Directory("./tools2");
var wixDir = toolsDir + Directory("wix");
var lessmsiDir = toolsDir + Directory("lessmsi");
var part1ExtractDir = Directory("./extract-part1");
var part2ExtractDir = Directory("./extract-part2");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Download-Tools")
.Does(() => {
    CleanDirectory(toolsDir);

    // Download WixToolset
    var outputPath = toolsDir + File("wix311-binaries.zip");
    DownloadFile("https://github.com/wixtoolset/wix3/releases/download/wix3111rtm/wix311-binaries.zip", outputPath);
    Unzip(outputPath, wixDir);

    // Download LessMSI
    outputPath = toolsDir + File("lessmsi-v1.6.3.zip");
    DownloadFile("https://github.com/activescott/lessmsi/releases/download/v1.6.3/lessmsi-v1.6.3.zip", outputPath);
    Unzip(outputPath, lessmsiDir);

    // Download ETS Inside Installer
    outputPath = toolsDir + File("ETS Inside server v1.4.0.zip");
    DownloadFile("https://knxcloud.org/index.php/s/GVwEAx95RZMUORK/download", outputPath);
    Unzip(outputPath, toolsDir);
});

Task("Extract-Part1")
.Does(() => {
    CleanDirectory(part1ExtractDir);

    // Path to dark.exe from the WIX toolset
    var darkPath = wixDir + File("dark.exe");
    // Extract
    StartProcess(darkPath, $@"-x {part1ExtractDir.Path.FullPath} {toolsDir.Path.FullPath}\EtsInsideSetup.exe");
});

Task("Extract-Part2")
.Does(() => {
    CleanDirectory(part2ExtractDir);

    // Path to lessmsi.exe
    var lessMsiPath = lessmsiDir + File("lessmsi.exe");
    // Extract
    StartProcess(lessMsiPath, $@"x {part1ExtractDir.Path.FullPath}\AttachedContainer\EtsInsideSetupx64.msi {part2ExtractDir.Path.FullPath}\");
});

Task("Docker-Linux-Build")
.Does(() => {
    // Copy the runtime config file to run the programm as standalone
    var configfileTarget = part2ExtractDir + Directory("SourceDir") + Directory("ETS Inside") + File("Knx.Ets.Osprey.runtimeconfig.json");
    CopyFile("./additionalfiles/Knx.Ets.Osprey.runtimeconfig.json", configfileTarget);

    var ruleTarget = part2ExtractDir + Directory("SourceDir") + Directory("ETS Inside") + File("10-ets-inside.rules");
    CopyFile("./additionalfiles/10-ets-inside.rules", ruleTarget);

    // Build the image
    StartProcess("docker", $@"build -t ets-inside-server -f ./dockerfiles/linux/Dockerfile {part2ExtractDir.Path.FullPath}\SourceDir");
});

Task("Docker-Linux-Run")
.Does(() => {
    StartProcess("docker", @"run --rm --network host ets-inside-server");
});

Task("Docker-Windows-Run")
.Does(() => {
    StartProcess("docker", @"run --rm --network host --isolation=process ets-inside-server");
});

RunTarget(target);
