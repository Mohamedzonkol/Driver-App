#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DriverApi/Driver.Api.csproj", "DriverApi/"]
COPY ["Driver.DataServicers/Driver.DataServices.csproj", "Driver.DataServicers/"]
COPY ["Driver.Entites/Driver.Entites.csproj", "Driver.Entites/"]
RUN dotnet restore "./DriverApi/./Driver.Api.csproj"
COPY . .
WORKDIR "/src/DriverApi"
RUN dotnet build "./Driver.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Driver.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Driver.Api.dll"]