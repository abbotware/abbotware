$Projects        = @{"Abbotware.Nugets" = @{ 
						Name           = "Abbotware.UnitTests";
						Type           = "build"; 
						Action         = "build"
						Source         = "";
						Runtime        = "";
						Framework      = "";
						Configuration  = "Release"
						}

					"Abbotware.UnitTests" = @{ 
						Name           = "Abbotware.UnitTests";
						Type           = "unittest"; 
						Action         = "publish";
						Source         = "Test\Abbotware.UnitTests\" 
						Runtime        = "win-x64;linux-x64;linux-arm"
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						} 

					"Abbotware.IntegrationTests" = @{ 
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