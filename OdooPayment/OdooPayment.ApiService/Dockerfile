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
COPY ["OdooPayment.ApiService/OdooPayment.ApiService.csproj", "OdooPayment.ApiService/"]
COPY ["ApiService/ApiService.csproj", "ApiService/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Aplication/Application.csproj", "Aplication/"]
COPY ["Contract/Contract.csproj", "Contract/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["OdooPayment.Background/OdooPayment.Background.csproj", "OdooPayment.Background/"]
COPY ["BackgroundJob/Background.csproj", "BackgroundJob/"]
COPY ["Integration/Integration.csproj", "Integration/"]
COPY ["Odoo.Persistence/Odoo.Persistence.csproj", "Odoo.Persistence/"]
COPY ["Odoo.Domain/Odoo.Domain.csproj", "Odoo.Domain/"]
COPY ["OdooPayment.Application/OdooPayment.Application.csproj", "OdooPayment.Application/"]
COPY ["OdooPayment.Contract/OdooPayment.Contract.csproj", "OdooPayment.Contract/"]
COPY ["Payment.Persistence/Payment.Persistence.csproj", "Payment.Persistence/"]
COPY ["OdooPayment.Infrastructure/OdooPayment.Infrastructure.csproj", "OdooPayment.Infrastructure/"]
COPY ["Odoo.Infrastructure/Odoo.Infrastructure.csproj", "Odoo.Infrastructure/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Odoo.Application/Odoo.Application.csproj", "Odoo.Application/"]
COPY ["Odoo.Contract/Odoo.Contract.csproj", "Odoo.Contract/"]
COPY ["ServiceDefaults/ServiceDefaults.csproj", "ServiceDefaults/"]
RUN dotnet restore "./OdooPayment.ApiService/OdooPayment.ApiService.csproj"
COPY . .
WORKDIR "/src/OdooPayment.ApiService"
RUN dotnet build "./OdooPayment.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OdooPayment.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OdooPayment.ApiService.dll"]