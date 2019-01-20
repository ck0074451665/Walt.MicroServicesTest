FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 876

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Walt.TestMcroservices.OrderService.csproj", "./"]
RUN dotnet restore "./Walt.TestMcroservices.OrderService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Walt.TestMcroservices.OrderService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Walt.TestMcroservices.OrderService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Walt.TestMcroservices.OrderService.dll"]
