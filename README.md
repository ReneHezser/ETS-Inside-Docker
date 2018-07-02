# ETS-Inside-Docker
ETS Inside Server inside a Docker container

## Manual
* Extract the `WiX Toolset` into `tmp`
* Extract `lessmsi` into `tmp`
* Extract `ETS Inside server v1.3.0.zip` into `tmp`
* Run the cake task `Extract-Setup`
* Run the cake task `Extract-Setup2`
  * Note: Automatic extraction does somehow not work so the UI is opened (for two files), just select all and extract them into `installer-extract2` folder
* Run the cake task `Build-Docker`
* Run the cake task `Build-Run`

You should now be able to connect to the ETS Inside Server with the ETS Inside client by using a manual connection with <hostname> (not localhost or 127.0.0.1)
