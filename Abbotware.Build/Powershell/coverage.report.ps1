. $PSScriptRoot/common.ps1

cd Build\Common\DotNetCliTools

dotnet restore

dotnet reportgenerator "-reports:$TestResultsPath/*coverage.xml" -targetdir:$CodeCoveragePath$Name\ "-sourcedirs:$SourcePath" -assemblyfilters:$CodeCoverageExcludeAssemblies

if (!$?) {
	$EXIT_CODE=1
	echo "EXIT_CODE=$EXIT_CODE"
}

cd $CWD 

echo "Exit coverage.report.ps1 with $EXIT_CODE"

Exit $EXIT_CODE