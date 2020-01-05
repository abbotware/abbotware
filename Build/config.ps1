$Projects        = @{"Abbotware.Core.Nugets" = @{ 
						Name           = "Abbotware.Core.UnitTests;
						Type           = "build"; 
						Action         = "build"
						Source         = "";
						Runtime        = "";
						Framework      = "";
						Configuration  = "Release"
						}

					"Abbotware.Core.UnitTests" = @{ 
						Name           = "Abbotware.Core.UnitTests";
						Type           = "unittest"; 
						Action         = "publish";
						Source         = "Test\Abbotware.Core.UnitTests\" 
						Runtime        = "win-x64;linux-x64;linux-arm"
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						} 

					"Abbotware.Core.IntegrationTests" = @{ 
						Name           = "Abbotware.IntegrationTests";
						Type           = "integrationtest"; 
						Action         = "publish";
						Source         = "Test\Abbotware.IntegrationTests\"
						Runtime         = "win-x64;linux-x64;linux-arm"
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						} 
					}

$CodeCoverageExcludeAssemblies = "-Abbotware.Cleanup;-Abbotware.Contrib.Roslyn;-Abbotware.Unknown"
$NugetPublishUrl = "http://10.10.10.50:8081/repository/abbotware/"					