. $PSScriptRoot/common.ps1

Write-Host "Setting Label:" "$BuildCommonPath/Msbuild.props/Abbotware.Metadata.props"

$props	= "$BuildCommonPath/Msbuild.props/Abbotware.Metadata.props"
$xmlDoc = [XML](gc $props) 
$xmldoc.Project.PropertyGroup.AssemblyVersion =  "$env:GO_PIPELINE_LABEL"
$xmldoc.Project.PropertyGroup.FileVersion = "$env:GO_PIPELINE_LABEL"
$xmldoc.Project.PropertyGroup.Version =  "$env:GO_PIPELINE_LABEL"
$xmldoc.Project.PropertyGroup.AssemblyInformationalVersion =  "$env:GO_PIPELINE_LABEL"
$xmldoc.Save($props)
