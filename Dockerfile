FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY CowsAndBulls.csproj ./
RUN dotnet restore /CowsAndBulls.csproj
COPY . .
WORKDIR /src/
RUN dotnet build CowsAndBulls.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CowsAndBulls.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CowsAndBulls.dll"]
