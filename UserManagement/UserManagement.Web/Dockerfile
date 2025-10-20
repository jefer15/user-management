FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY UserManagement.Web/UserManagement.Web.csproj UserManagement.Web/

# Restaurar dependencias
RUN dotnet restore "UserManagement.Web/UserManagement.Web.csproj"

COPY . .

WORKDIR /src/UserManagement.Web
RUN dotnet publish "UserManagement.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8081
EXPOSE 8081

ENTRYPOINT ["dotnet", "UserManagement.Web.dll"]
