FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MOT.ApiService/MOT.ApiService.csproj", "MOT.ApiService/"]
COPY ["ApiService/ApiService.csproj", "ApiService/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Aplication/Application.csproj", "Aplication/"]
COPY ["Contract/Contract.csproj", "Contract/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Sale.ServiceDefaults/Sale.ServiceDefaults.csproj", "Sale.ServiceDefaults/"]
RUN dotnet restore "MOT.ApiService/MOT.ApiService.csproj"
COPY . .
WORKDIR "/src/MOT.ApiService"
RUN dotnet build "MOT.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MOT.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MOT.ApiService.dll"]
