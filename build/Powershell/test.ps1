 param (
    [Parameter(Mandatory=$true)][string]$testtype
 )

. $PSScriptRoot/common.ps1

foreach ($P in $Projects.Keys ) {
	$Project = $Projects[$P]
    $Type = $Project.Type
	
	if ($Type -ne $testtype) {continue}

	if (-Not $Project.Runtime.Split(";").Contains($ActiveRuntime)) {continue}

	$Runtime = $ActiveRuntime

    $Name = $Project.Name
    $Source = $Project.Source
    $Framework = $Project.Framework
    $Configuration = $Project.Configuration
	$NetCoreSdk = $Project.NetCoreSdk
	$Tuple = "$Name.$Configuration.$Runtime.$Framework"

    $TrxFile =  $Tuple + ".trx"
    $XmlFile =  $Tuple + ".xml"
    $CoverageDataFile = $Tuple + "coverage.xml"

    Write-Host "$testtype : $Tuple"
    #dotnet vstest _Target\Release\win-x64\netcoreapp2.2\Abbotware.UnitTests\Abbotware.UnitTests.dll --logger:"trx;LogFileName=Abbotware.UnitTests.trx" --ResultsDirectory:_Target/TestResults
    #/p:ExcludeByFile="$cwd/FOLDER/Connected Services/**/*.cs%2c$cwd/FOLDER/Connected Services/**/*.cs"

	if ($NetCoreSdk) {
		dotnet new globaljson --sdk-version $NetCoreSdk --force 

		$env:NetCoreSdk = $NetCoreSdk

		if (!$?) {
			$EXIT_CODE=1
			echo "EXIT_CODE=$EXIT_CODE"
		}	
	}

    dotnet test $Source --filter $TestFilter -f $Framework -c $Configuration /p:NetCoreSdk=$NetCoreSdk --logger:"trx;LogFileName=$TrxFile" --results-directory:$TestResultsPath /p:CoverletOutputFormat=cobertura /p:CoverletOutput=$TestResultsPath$CoverageDataFile /p:CollectCoverage=true /p:ExcludeByAttribute="Obsolete%2cGeneratedCodeAttribute%2cCompilerGeneratedAttribute" -nodeReuse:false

    if (!$?) {
        $EXIT_CODE=1
		echo "EXIT_CODE=$EXIT_CODE"
    }

    $xslt = New-Object System.Xml.Xsl.XslCompiledTransform;
    $xslt.load( "$BuildCommonPath/Xslt/trx-to-junit.xslt" )
    $xslt.Transform( $TestResultsPath + $TrxFile, $TestResultsPath + $XmlFile )

    cd Build\Common\DotNetCliTools

    dotnet restore

    if (!$?) {
        $EXIT_CODE=1
		echo "EXIT_CODE=$EXIT_CODE"
    }

    dotnet reportgenerator -reports:$TestResultsPath$CoverageDataFile -targetdir:$CodeCoveragePath$Name\

    # disable for now - there is a bug in the code coverage tool which means the output file is sometimes never generated
	#if (!$?) {
    #    $EXIT_CODE=1
	#	echo "EXIT_CODE=$EXIT_CODE"
    #}

    cd $CWD 
}

echo "Exit test.ps1 with $EXIT_CODE"

Exit $EXIT_CODE