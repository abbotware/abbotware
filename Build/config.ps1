$Projects        = @{"Abbotware.Core.Nugets" = @{ 
						Name           = "Abbotware.Core.UnitTests";
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

					"Abbotware.Interop.UnitTests" = @{ 
						Name           = "Abbotware.Interop.UnitTests";
						Type           = "unittest"; 
						Action         = "publish";
						Source         = "Test\Abbotware.Interop.UnitTests\" 
						Runtime        = "win-x64;linux-x64;linux-arm"
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						} 

					"Abbotware.Interop.IntegrationTests" = @{ 
						Name           = "Abbotware.Interop.IntegrationTests";
						Type           = "integrationtest"; 
						Action         = "publish";
						Source         = "Test\Abbotware.Interop.IntegrationTests\" 
						Runtime        = "win-x64;linux-x64;linux-arm"
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						} 
					}

$ReportGeneratorAssemblyFilters="+Abbotware.*"