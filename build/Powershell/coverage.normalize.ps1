. $PSScriptRoot/common.ps1

if (!$?) {
	$EXIT_CODE=1
	echo "EXIT_CODE=$EXIT_CODE"
}
 	
if ($ActiveRuntime -ne "Windows_NT") 
{
	$searchPath = $TestResultsPath + "/*linux*coverage.xml"
	$toFind = "/var/lib/go-agent/pipelines/"
	$toReplace = "G:\go\pipelines\"
	$slashFind = '/'
	$slashReplace = '\'
} 
else 
{
	$searchPath = $TestResultsPath + "*win-x64*coverage.xml"
	$toFind = "G:\go\pipelines\"
	$toReplace = "/var/lib/go-agent/pipelines/"
	$slashFind = '\'
	$slashReplace = '/'
}

echo $searchPath
echo $toFind 
echo $toReplace

$files = Get-ChildItem $searchPath

foreach($file in $files) 
{
	$xmlDoc = [XML](gc $file) 
	
	$nodes = $xmlDoc.SelectNodes("//class")
	
	foreach($node in $nodes) 
	{
		$original = $node.attributes['filename'].value
		$original = $original.Replace($toFind ,"");
		$original = $original.Replace($slashFind ,$slashReplace);
				
		$normalize = [System.IO.Path]::Combine($toReplace, $original);
		echo $normalize 

	
		$node.attributes['filename'].value = $normalize 
	}

	$xmldoc.Save($file )
}



echo "Exit coverage.normalize.ps1 with $EXIT_CODE"

Exit $EXIT_CODE