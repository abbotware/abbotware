. $PSScriptRoot/common.ps1

dotnet nuget locals http-cache --clear

if (!$?) {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}

dotnet nuget locals global-packages --clear

if (!$?) {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}

dotnet nuget locals temp --clear

if (!$?) {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}

dotnet nuget locals plugins-cache --clear

if (!$?) {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}

dotnet nuget locals all --clear

if (!$?) {
    $EXIT_CODE=1
	Exit $EXIT_CODE
}