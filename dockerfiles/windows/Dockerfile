# escape=`

# TODO: This basically works but it is unable to see the KNX IP router, probably because of networking of Docker in Windows.
# Also this might be simplified to look like the linux container.

FROM mcr.microsoft.com/windows/servercore:1809

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ENV DOTNET_VERSION 2.1.9

RUN Invoke-WebRequest -OutFile dotnet.zip https://dotnetcli.blob.core.windows.net/dotnet/Runtime/$Env:DOTNET_VERSION/dotnet-runtime-$Env:DOTNET_VERSION-win-x64.zip; `
    Expand-Archive dotnet.zip -DestinationPath $Env:ProgramFiles\dotnet -Force; `
    Remove-Item -Force dotnet.zip

# Add dotnet to the PATH
RUN setx /M PATH $($Env:PATH + ';' + $Env:ProgramFiles + '\dotnet')

ENV `
	# Opt-out from the telemetry
	DOTNET_CLI_TELEMETRY_OPTOUT=true `
	# Configure web servers to bind to port 80 when present
	ASPNETCORE_URLS=http://+:80 `
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true `
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true `
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

COPY ["ETS Inside", "C:\\Program Files\\ETS Inside"]

RUN New-Service -Name 'ETSInsideServer' -BinaryPathName 'C:\Program Files\ETS Inside\Knx.Ets.Osprey.exe'
RUN Start-Service 'ETSInsideServer'

# Expose the ETS Inside port
EXPOSE 8081

ENTRYPOINT ["ping", "-t", "localhost"]
