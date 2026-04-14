FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["MeuPlantao.sln", "."]
COPY ["MeuPlantao/MeuPlantao.csproj", "MeuPlantao/"]
COPY ["MeuPlantao.Application/MeuPlantao.Application.csproj", "MeuPlantao.Application/"]
COPY ["MeuPlantao.Communication/MeuPlantao.Communication.csproj", "MeuPlantao.Communication/"]
COPY ["MeuPlantao.Domain/MeuPlantao.Domain.csproj", "MeuPlantao.Domain/"]
COPY ["MeuPlantao.Infrastructure/MeuPlantao.Infrastructure.csproj", "MeuPlantao.Infrastructure/"]

RUN dotnet restore "MeuPlantao.sln"

COPY . .

RUN dotnet publish "MeuPlantao/MeuPlantao.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add --no-cache icu-libs tzdata curl

WORKDIR /app

EXPOSE 8082
EXPOSE 8443

COPY --from=build /app/publish .

HEALTHCHECK --interval=30s --timeout=10s --start-period=40s --retries=3 \
    CMD curl -f http://localhost:8082/swagger/index.html || exit 1

ENTRYPOINT ["dotnet", "MeuPlantao.dll"]   