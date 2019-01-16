set path=%path%;%cd%
cd .\..\Walt.Freamwork.Service
dotnet restore
set versionNumber=1.0.5
nuget pack . -Build -Prop Configuration=Release   -Version %versionNumber% -OutputDirectory .\..\output

cd .\..\output
nuget  push  .\Walt.Freamwork.Service.%versionNumber%.nupkg -ApiKey 123456 -Source http://localhost/nuget


cd .\..\publish



nuget  config -set repositoryPath=../packages/