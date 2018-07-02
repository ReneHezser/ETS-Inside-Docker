# escape=`

FROM microsoft/dotnet-framework:3.5-runtime-windowsservercore-1803

# Add the default IIS
RUN powershell -Command `
    Add-WindowsFeature Web-Server; `
    Invoke-WebRequest -UseBasicParsing -Uri "https://dotnetbinaries.blob.core.windows.net/servicemonitor/2.0.1.3/ServiceMonitor.exe" -OutFile "C:\ServiceMonitor.exe"

# Add additional IIS features
RUN Add-WindowsFeature NET-Framework-45-ASPNET; `
    Add-WindowsFeature Web-Asp-Net45; `
	Add-WindowsFeature NET-WCF-HTTP-Activation45;

COPY ETS_Inside_1.3_portable.zip .
RUN powershell -Command Expand-Archive -Path 'ETS_Inside_1.3_portable.zip' -DestinationPath 'C:\\Program Files\\' -Force;

COPY SetupIIS.ps1 .
RUN powershell ".\\SetupIIS.ps1"

# Expose the default IIS port (for testing)
EXPOSE 80

# Expose the ETS Inside port
EXPOSE 8081

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]


# The code below is the trial/error to install to full installer in Docker which currently
# fails when it tries to setup the firewall (there is no in Docker).


#FROM microsoft/iis:windowsservercore-1803

#RUN powershell -Command Add-WindowsFeature NetFx3;
#RUN powershell -Command Add-WindowsFeature Net-Framework-Core
#RUN powershell -Command Add-WindowsCapability –Online -Name NetFx3~~~~

#RUN powershell -Command Add-WindowsFeature NetFx3ServerFeatures;
#RUN powershell -Command Add-WindowsFeature NetFx4Extended-ASPNET45;
#RUN powershell -Command Add-WindowsFeature WCF-HTTP-Activation45 ;

# Set the default shell to powershell
#SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Download and install ETS Inside 1.3
#RUN (New-Object System.Net.WebClient).DownloadFile('https://knxcloud.org/index.php/s/P0hQRe25evAkClz/download', \"$(Convert-Path .)\ETS Inside server v1.3.0.zip\"); `
#	Expand-Archive -Path 'ETS Inside server v1.3.0.zip' -DestinationPath '.' -Force; `
#	Start-Process -FilePath 'EtsInsideSetup.exe' -Wait -ArgumentList '-quiet' ; `
#	Remove-Item -Force 'ETS Inside server v1.3.0.zip' ; `
#	Remove-Item -Force 'EtsInsideSetup.exe' ;

#COPY EtsInsideSetup.exe .

#RUN Start-Process -FilePath 'EtsInsideSetup.exe' -Wait -ArgumentList '-quiet'

#RUN EtsInsideSetup.exe -quiet -log C:\install.log

#RUN Remove-Item -Force 'EtsInsideSetup.exe'

# Expose the ETS Inside port
#EXPOSE 8081

# Keep the default IIS entrypoint