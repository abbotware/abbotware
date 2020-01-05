. $PSScriptRoot/common.ps1

Write-Host "Push Nugets to $NugetPublishUrl"

if (-Not (Test-Path env:\NUGET_API_KEY))
{
	Write-Host "ERRRO NUGET_API_KEY NOT SET"
	Exit 1
}

dotnet nuget push _Target\Release\nuget\*.nupkg -k $env:NUGET_API_KEY -s $NugetPublishUrl

if (!$?) {
    $EXIT_CODE=1
}

Exit $EXIT_CODE