FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ControleFinanceiroSimplificado.sln", "./"]
COPY ["src/Financeiro.Domain/Financeiro.Domain.csproj", "src/Financeiro.Domain/"]
COPY ["src/Financeiro.Application/Financeiro.Application.csproj", "src/Financeiro.Application/"]
COPY ["src/Financeiro.Infrastructure/Financeiro.Infrastructure.csproj", "src/Financeiro.Infrastructure/"]
COPY ["src/Financeiro.API/Financeiro.API.csproj", "src/Financeiro.API/"]
RUN dotnet restore "ControleFinanceiroSimplificado.sln"

COPY . .
RUN dotnet publish "src/Financeiro.API/Financeiro.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

FROM runtime AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Financeiro.API.dll"]
