FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Designly.API/Designly.API.csproj", "Designly.API/"]
COPY ["Designly.Application/Designly.Application.csproj", "Designly.Application/"]
COPY ["Designly.Domain/Designly.Domain.csproj", "Designly.Domain/"]
COPY ["Designly.Infrastructure/Designly.Infrastructure.csproj", "Designly.Infrastructure/"]
RUN dotnet restore "./Designly.API/Designly.API.csproj"
COPY . .
WORKDIR "/src/Designly.API"
RUN dotnet build "./Designly.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Designly.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Designly.API.dll"]
