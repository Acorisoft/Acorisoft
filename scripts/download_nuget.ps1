Code:[PowerShell]
$Url = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
$Output = "nuget.exe"
$start_time = Get-Date

Write-Output "开始: $start_time"
Invoke-WebRequest -Uri $Url -OutFile $Output
$start_time = Get-Date

Write-Output "获取Nuget完成"
Write-Output "结束: $start_time"