cd FPro
cd FPro
dotnet restore && dotnet build
cd ..
cd ..

cd FleetManagementLibrary
cd FleetManagementLibrary
dotnet restore && msbuild
cd ..
cd ..

cd FleetManagementAPI
cd FleetManagementAPI
dotnet restore && dotnet run