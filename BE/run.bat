cd FPro
cd FPro
dotnet restore && dotnet build
cd ..
cd ..

cd FleetManagementLibrary
nuget restore && dotnet build
cd ..

cd FleetManagementAPI
cd FleetManagementAPI
dotnet restore && dotnet run