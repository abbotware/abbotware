. $PSScriptRoot/common.ps1


#dotnet tool install dotnet-outdated --tool-path build/common/tools

if (!$?) {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}

if ([string]::IsNullOrWhiteSpace($NugetUpgradeFilter)) {
	Write-Host "Upgrade Nuget Pattern not set"
	$EXIT_CODE=1
	Exit $EXIT_CODE
}

Write-Host "Upgrade Nugets $NugetUpgradeFilter"

try {
Invoke-Expression "build/common/tools/dotnet-outdated/dotnet-outdated.exe -u $NugetUpgradeFilter"
} catch {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}

if (!$?) {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}

svn commit --username $env:SVN_COMMIT_USERNAME --password $env:SVN_COMMIT_PASSWORD -m "nuget upgrade $NugetUpgradeFilter $env:GO_PIPELINE_LABEL"

if (!$?) {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}