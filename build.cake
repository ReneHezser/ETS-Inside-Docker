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
    var darkPath = @"C:\Users\rbl\Downloads\wix311-binaries\dark.exe";
    // Extract
    StartProcess(darkPath, @"-x .\installer-extract EtsInsideSetup.exe");
});

Task("Extract-Setup2")
.Does(() => {
    // Path to lessmsi.exe
    var lessMsiPath = @"C:\Users\rbl\Downloads\lessmsi-v1.6.1\lessmsi.exe";
    // Extract
    //StartProcess(lessMsiPath, @"x .\installer-extract\AttachedContainer\EtsInsideSetupx64.msi installer-extract2\");
    StartProcess(lessMsiPath, @"o .\installer-extract\AttachedContainer\EtsInsideSetupx64.msi");
});

Task("Build-Docker")
.Does(() => {
    StartProcess("docker build -t ets .");
});

Task("Build-Run")
.Does(() => {
    StartProcess(@"docker run --rm --name ets-tmp -p 8091:8081 -v C:\Users\rbl\Desktop\ets\iislog:C:\inetpub\logs\LogFiles ets");
});

RunTarget(target);