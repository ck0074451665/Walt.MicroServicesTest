FROM microsoft/dotnet:2.2-sdk AS base
WORKDIR /app
EXPOSE 801

FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /src
COPY . Walt.TestMcroServices.Webapi
RUN dotnet restore "Walt.TestMcroServices.Webapi/Walt.TestMcroServices.Webapi.csproj"
COPY . .
WORKDIR "/src/Walt.TestMcroServices.Webapi"
RUN dotnet build "Walt.TestMcroServices.Webapi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Walt.TestMcroServices.Webapi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Walt.TestMcroServices.Webapi.dll"]