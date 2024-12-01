# Etapa 1: Base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa 2: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar arquivos de projeto
COPY ../DestifyMovie.API/DestifyMovie.API.csproj .
COPY ../DestifyMovie.Data/DestifyMovie.Data.csproj ../DestifyMovie.Data/
COPY ../DestifyMovie.Repositories/DestifyMovie.Repositories.csproj ../DestifyMovie.Repositories/
COPY ../DestifyMovie.Services/DestifyMovie.Services.csproj ../DestifyMovie.Services/

# Restaurar dependências
RUN dotnet restore DestifyMovie.API.csproj

# Copiar o restante do código
COPY .. .
WORKDIR "/src/DestifyMovie.API"
RUN dotnet build DestifyMovie.API.csproj -c Release -o /app/build

# Etapa 3: Publicação
FROM build AS publish
RUN dotnet publish DestifyMovie.API.csproj -c Release -o /app/publish

# Etapa 4: Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DestifyMovie.API.dll"]
