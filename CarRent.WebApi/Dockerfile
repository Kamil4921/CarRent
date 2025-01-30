FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarRent/CarRent.WebApi/CarRent.WebApi.csproj", "CarRent/CarRent.WebApi/"]
COPY ["CarRent/CarRent.Domain/CarRent.Domain.csproj", "CarRent/CarRent.Domain/"]
COPY ["CarRent/CarRent.Application/CarRent.Application.csproj", "CarRent/CarRent.Application/"]
COPY ["CarRent/CarRent.Infrastructure/CarRent.Infrastructure.csproj", "CarRent/CarRent.Infrastructure/"]
RUN dotnet restore "CarRent/CarRent.WebApi/CarRent.WebApi.csproj"
COPY . .
WORKDIR "/src/CarRent.WebApi"
RUN dotnet build "CarRent.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CarRent.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRent.WebApi.dll"]
