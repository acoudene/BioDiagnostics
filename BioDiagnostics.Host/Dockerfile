# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BioDiagnostics.Host/BioDiagnostics.Host.csproj", "BioDiagnostics.Host/"]
COPY ["BioDiagnostics.Api/BioDiagnostics.Api.csproj", "BioDiagnostics.Api/"]
COPY ["Core.Api/Core.Api.csproj", "Core.Api/"]
COPY ["Core.Data/Core.Data.csproj", "Core.Data/"]
COPY ["Core.Dtos/Core.Dtos.csproj", "Core.Dtos/"]
COPY ["BioDiagnostics.Data/BioDiagnostics.Data.csproj", "BioDiagnostics.Data/"]
COPY ["BioDiagnostics.Dtos/BioDiagnostics.Dtos.csproj", "BioDiagnostics.Dtos/"]
COPY ["BioDiagnostics.Data.MongoDb/BioDiagnostics.Data.MongoDb.csproj", "BioDiagnostics.Data.MongoDb/"]
COPY ["Core.Data.MongoDb/Core.Data.MongoDb.csproj", "Core.Data.MongoDb/"]
COPY ["BioDiagnostics.ServiceDefaults/BioDiagnostics.ServiceDefaults.csproj", "BioDiagnostics.ServiceDefaults/"]
RUN dotnet restore "./BioDiagnostics.Host/BioDiagnostics.Host.csproj"
COPY . .
WORKDIR "/src/BioDiagnostics.Host"
RUN dotnet build "./BioDiagnostics.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BioDiagnostics.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BioDiagnostics.Host.dll"]