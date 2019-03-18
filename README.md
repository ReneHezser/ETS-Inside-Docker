# ETS-Inside-Docker
ETS Inside Server inside a Docker container

## Using the image
Run the image `https://hub.docker.com/r/roemer/ets-inside-server` with `--network host` parameter.
You can override the port of the ETS Inside Server by using the environment variable `ETS_PORT`, so for example add `-e ETS_PORT=9090`.

## Manually building the image
Note: this currently only works on a Windows machine.
* Make sure to have Docker Desktop installed and Linux Containers enabled.
* Run the cake task `Download-Tools` which downloads the needed tools
* Run the cake task `Extract-Part1` which extracts the installers from the meta-installer
* Run the cake task `Extract-Part2` which extracts the files from the installer
* Run the cake task `Docker-Linux-Build` which builds the linux image
* Run the cake task `Docker-Linux-Run` which runs the image in a container

## TODO:
* Find out where to set the volume to
* Find out how usb-passthru for the dongle works
