FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sms.ApiService/Sms.ApiService.csproj", "Sms.ApiService/"]
COPY ["ApiService/ApiService.csproj", "ApiService/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Aplication/Application.csproj", "Aplication/"]
COPY ["Contract/Contract.csproj", "Contract/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["ServiceDefaults/ServiceDefaults.csproj", "ServiceDefaults/"]
COPY ["Sms.Application/Sms.Application.csproj", "Sms.Application/"]
COPY ["Sms.Domain/Sms.Domain.csproj", "Sms.Domain/"]
COPY ["Sms.Contract/Sms.Contract.csproj", "Sms.Contract/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Sms.Persistence/Sms.Persistence.csproj", "Sms.Persistence/"]
RUN dotnet restore "Sms.ApiService/Sms.ApiService.csproj"
COPY . .
WORKDIR "/src/Sms.ApiService"
RUN dotnet build "Sms.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sms.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sms.ApiService.dll"]
