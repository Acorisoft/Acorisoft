$Nuget_URL = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$Nuget_Output = "nuget.exe"
$Cake_URL = "nuget install Cake -version 1.1.0"

$Time = Get-Date
Write-Output("$Time : starting download nuget.exe")

Invoke-WebRequest -Uri $Nuget_URL -OutFile $Nuget_Output

$Time = Get-Date
Write-Output("$Time : download nuget.exe completed")

$Time = Get-Date
Write-Output("$Time : install package Cake 1.1.0")
Invoke-WebRequest -Uri "http://cakebuild.net/download/bootstrapper/windows" -OutFile "build.ps1"
$Time = Get-Date
Write-Output("$Time : install package Cake 1.1.0")

Start-Process -FilePath "nuget.exe" -ArgumentList $Cake_URL 

$Time = Get-Date
Write-Output("$Time : install completed")