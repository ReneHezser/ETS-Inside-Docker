Import-Module IISAdministration;

$siteName = "ETS Inside Website"
$port = 8081
New-IISSite -Name $siteName -PhysicalPath "C:\Program Files\ETS Inside\Client" -BindingInformation "*:$($port):"
New-WebApplication -Site $siteName -Name "WebServices" -PhysicalPath "C:\Program Files\ETS Inside\Server"
Start-IISSite -Name $siteName
