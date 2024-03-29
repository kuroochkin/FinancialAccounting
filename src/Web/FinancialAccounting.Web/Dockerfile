FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web/FinancialAccounting.Web/FinancialAccounting.Web.csproj", "Web/FinancialAccounting.Web/"]
RUN dotnet restore "Web/FinancialAccounting.Web/FinancialAccounting.Web.csproj"
COPY . .
WORKDIR "/src/Web/FinancialAccounting.Web"
RUN dotnet build "FinancialAccounting.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FinancialAccounting.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialAccounting.Web.dll"]
