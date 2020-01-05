$Projects        = @{"Namespace.Build" = @{ 
						Name           = "Namespace.UnitTests";
						Type           = "build"; 
						Action         = "build"
						Source         = "Namespace.UnitTests\";
						Runtime       =  "win-x64";
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						}
					"Namespace.UnitTests" = @{ 
						Name           = "Namespace.UnitTests";
						Type           = "unittest"; 
						Action         = "publish";
						Source         = "Namespace.UnitTests\" 
						Runtime        = "win-x64;linux-x64;linux-arm"
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						}
					"Namespace.IntegrationTests" = @{ 
						Name           = "Namespace.IntegrationTests";
						Type           = "unittest"; 
						Action         = "publish";
						Source         = "Namespace.IntegrationTests\" 
						Runtime        = "win-x64;linux-x64;linux-arm"
						Framework      = "netcoreapp3.1";
						Configuration  = "Release"
						}						
					}

$NugetPublishUrl = "http://10.10.10.50:8081/repository/zyronet-sdk/"