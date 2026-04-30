# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MeuPlantao/MeuPlantao.csproj", "MeuPlantao/"]
RUN dotnet restore "MeuPlantao/MeuPlantao.csproj"
COPY . .
WORKDIR "/src/MeuPlantao"
RUN dotnet build "MeuPlantao.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MeuPlantao.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime (final)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeuPlantao.dll"]