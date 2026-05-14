# Build stage (uses SDK only inside container)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY "WebApplication2/Vehicle Service Center System.csproj" "WebApplication2/"
RUN dotnet restore "WebApplication2/Vehicle Service Center System.csproj"

COPY . .
RUN dotnet publish "WebApplication2/Vehicle Service Center System.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage (no SDK required)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Vehicle Service Center System.dll"]
