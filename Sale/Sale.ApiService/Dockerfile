FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sale.ApiService/Sale.ApiService.csproj", "Sale.ApiService/"]
COPY ["ApiService/ApiService.csproj", "ApiService/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Aplication/Application.csproj", "Aplication/"]
COPY ["Contract/Contract.csproj", "Contract/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Odoo.Persistence/Odoo.Persistence.csproj", "Odoo.Persistence/"]
COPY ["Odoo.Domain/Odoo.Domain.csproj", "Odoo.Domain/"]
COPY ["Sale.BackgroundJob.cs/Sale.Background.csproj", "Sale.BackgroundJob.cs/"]
COPY ["BackgroundJob/Background.csproj", "BackgroundJob/"]
COPY ["Sale.Application/Sale.Application.csproj", "Sale.Application/"]
COPY ["Sale.Contract/Sale.Contract.csproj", "Sale.Contract/"]
COPY ["Sale.Domain/Sale.Domain.csproj", "Sale.Domain/"]
COPY ["Sale.Infrastructure/Sale.Infrastructure.csproj", "Sale.Infrastructure/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Sale.Persistence/Sale.Persistence.csproj", "Sale.Persistence/"]
COPY ["Sale.ServiceDefaults/Sale.ServiceDefaults.csproj", "Sale.ServiceDefaults/"]
RUN dotnet restore "Sale.ApiService/Sale.ApiService.csproj"
COPY . .
WORKDIR "/src/Sale.ApiService"
RUN dotnet build "Sale.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sale.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sale.ApiService.dll"]
