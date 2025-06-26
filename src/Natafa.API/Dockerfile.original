FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY src/Natafa.Api/Natafa.Api.csproj Natafa.Api/
COPY src/Natafa.Domain/Natafa.Domain.csproj Natafa.Domain/
COPY src/Natafa.Repository/Natafa.Repository.csproj Natafa.Repository/
RUN dotnet restore Natafa.Api/Natafa.Api.csproj
COPY src/ .
WORKDIR /src/Natafa.Api
RUN dotnet build Natafa.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Natafa.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Natafa.Api.dll"]
