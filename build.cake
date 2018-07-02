///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
.Does(() => {
    Information("Hello Cake!");
});

Task("Extract-Setup")
.Does(() => {
    // Path to dark.exe from the WIX toolset
    var darkPath = @"tmp\wix311-binaries\dark.exe";
    // Extract
    StartProcess(darkPath, @"-x .\installer-extract tmp\EtsInsideSetup.exe");
});

Task("Extract-Setup2")
.Does(() => {
    // Path to lessmsi.exe
    var lessMsiPath = @"tmp\lessmsi-v1.6.1\lessmsi.exe";
    // Extract
    //StartProcess(lessMsiPath, @"x .\installer-extract\AttachedContainer\EtsInsideSetupx64.msi installer-extract2\");
    StartProcess(lessMsiPath, @"o .\installer-extract\AttachedContainer\EtsInsideSetupx64.msi");
    StartProcess(lessMsiPath, @"o .\installer-extract\AttachedContainer\DiscoveryServerSetupx64.msi");
});

Task("Build-Docker")
.Does(() => {
    StartProcess("docker", @"build -t ets -f Dockerfile installer-extract2\SourceDir");
});

Task("Build-Run")
.Does(() => {
    //StartProcess("docker", @"run --rm --name ets-tmp -p 8091:8081 -v C:\Users\rbl\Desktop\ets\iislog:C:\inetpub\logs\LogFiles ets");
    StartProcess("docker", @"run --rm --name ets-tmp -p 8080:80 -p 8081:8081 ets");
});

RunTarget(target);