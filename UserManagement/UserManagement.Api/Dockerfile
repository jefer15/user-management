# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar los archivos .csproj de ambos proyectos
COPY UserManagement.Api/UserManagement.Api.csproj UserManagement.Api/
COPY UserManagement.Infrastructure/UserManagement.Infrastructure.csproj UserManagement.Infrastructure/

# Restaurar dependencias
RUN dotnet restore "UserManagement.Api/UserManagement.Api.csproj"

# Copiar todo el código
COPY . .

# Publicar
WORKDIR /src/UserManagement.Api

COPY UserManagement.Api/appsettings.json /src/UserManagement.Api/appsettings.json

RUN dotnet publish "UserManagement.Api.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
# 🔹 Indicar el puerto interno correcto
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "UserManagement.Api.dll"]
