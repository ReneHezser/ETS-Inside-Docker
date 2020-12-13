# ETS-Inside-Docker
ETS Inside Server inside a Docker container

## Using the image
Run the image `https://hub.docker.com/r/roemer/ets-inside-server` with `--network host` parameter and specify the userid of the user who can access the dongle via udev rule (see KNX installation guide within the ETS Inside Server linux tar).
You can override the port of the ETS Inside Server by using the environment variable `ETS_PORT`, so for example add `-e ETS_PORT=9090`.

## Manually building the image
Note: this currently only works on a Windows machine.

The build.ps1 script is a wrapper to execute the cake tasks. Run ```powershell ./build.ps1 -target="Download-Tools"``` for the first task and so on.

* Make sure to have Docker Desktop installed and Linux Containers enabled.
* Run the cake task `Download-Tools` which downloads the needed tools
* Run the cake task `Extract-Part1` which extracts the installers from the meta-installer
* Run the cake task `Extract-Part2` which extracts the files from the installer
* Run the cake task `Docker-Linux-Build` which builds the linux image
* Run the cake task `Docker-Linux-Run` which runs the image in a container

Next tag the image so it can be uploaded to Docker Hub with e.g. ```docker tag ets-inside-server:latest roemer/ets-inside-server:latest``` and ```docker tag ets-inside-server:latest roemer/ets-inside-server:1.4.0-85``` (replace with the current version of the ETS Inside server).

Finally upload it to Docker Hub with ```docker push roemer/ets-inside-server:latest```.

## TODO:
* Find out where to set the volume to
* Find out how usb-passthru for the dongle works
