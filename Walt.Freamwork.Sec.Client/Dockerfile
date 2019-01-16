FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8755

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Walt.Freamwork.Sec.Client.csproj", "./"]
RUN dotnet restore "./Walt.Freamwork.Sec.Client.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Walt.Freamwork.Sec.Client.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Walt.Freamwork.Sec.Client.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Walt.Freamwork.Sec.Client.dll"]
