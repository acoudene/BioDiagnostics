# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BioDiagnostics.WebApp/BioDiagnostics.WebApp.csproj", "BioDiagnostics.WebApp/"]
COPY ["BioDiagnostics.Api.BackendForFrontend/BioDiagnostics.Api.BackendForFrontend.csproj", "BioDiagnostics.Api.BackendForFrontend/"]
COPY ["BioDiagnostics.Proxies/BioDiagnostics.Proxies.csproj", "BioDiagnostics.Proxies/"]
COPY ["Core.Proxying/Core.Proxying.csproj", "Core.Proxying/"]
COPY ["Core.Dtos/Core.Dtos.csproj", "Core.Dtos/"]
COPY ["BioDiagnostics.Dtos/BioDiagnostics.Dtos.csproj", "BioDiagnostics.Dtos/"]
COPY ["BioDiagnostics.ViewObjects/BioDiagnostics.ViewObjects.csproj", "BioDiagnostics.ViewObjects/"]
COPY ["Core.ViewObjects/Core.ViewObjects.csproj", "Core.ViewObjects/"]
COPY ["BioDiagnostics.ServiceDefaults/BioDiagnostics.ServiceDefaults.csproj", "BioDiagnostics.ServiceDefaults/"]
COPY ["BioDiagnostics.WebApp.Client/BioDiagnostics.WebApp.Client.csproj", "BioDiagnostics.WebApp.Client/"]
COPY ["Core.RazorComponents.Mud/Core.RazorComponents.Mud.csproj", "Core.RazorComponents.Mud/"]
COPY ["Core.RazorComponents/Core.RazorComponents.csproj", "Core.RazorComponents/"]
COPY ["BioDiagnostics.Localization/BioDiagnostics.Localization.csproj", "BioDiagnostics.Localization/"]
COPY ["BioDiagnostics.RazorComponents/BioDiagnostics.RazorComponents.csproj", "BioDiagnostics.RazorComponents/"]
COPY ["BioDiagnostics.ViewObjects.Validation/BioDiagnostics.ViewObjects.Validation.csproj", "BioDiagnostics.ViewObjects.Validation/"]
COPY ["BioDiagnostics.ViewModels/BioDiagnostics.ViewModels.csproj", "BioDiagnostics.ViewModels/"]
COPY ["Core.ViewModels/Core.ViewModels.csproj", "Core.ViewModels/"]
RUN dotnet restore "./BioDiagnostics.WebApp/BioDiagnostics.WebApp.csproj"
COPY . .
WORKDIR "/src/BioDiagnostics.WebApp"
RUN dotnet build "./BioDiagnostics.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BioDiagnostics.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BioDiagnostics.WebApp.dll"]