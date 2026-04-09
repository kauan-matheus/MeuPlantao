# Stage 1: Build Environment
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution e arquivos de projeto
COPY ["MeuPlantao.sln", "."]
COPY ["MeuPlantao/MeuPlantao.csproj", "MeuPlantao/"]
COPY ["MeuPlantao.Application/MeuPlantao.Application.csproj", "MeuPlantao.Application/"]
COPY ["MeuPlantao.Communication/MeuPlantao.Communication.csproj", "MeuPlantao.Communication/"]
COPY ["MeuPlantao.Domain/MeuPlantao.Domain.csproj", "MeuPlantao.Domain/"]
COPY ["MeuPlantao.Infrastructure/MeuPlantao.Infrastructure.csproj", "MeuPlantao.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "MeuPlantao.sln"

# Copy source code completo
COPY . .

# Publish
RUN dotnet publish "MeuPlantao/MeuPlantao.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore

# Stage 2: Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

# Instalar dependências do Alpine
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add --no-cache icu-libs tzdata curl

WORKDIR /app

EXPOSE 8082
EXPOSE 8443

# Copy published output
COPY --from=build /app/publish .

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=40s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "MeuPlantao.dll"]   