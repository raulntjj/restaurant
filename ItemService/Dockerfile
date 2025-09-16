FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Deps stage
COPY *.csproj ./
RUN dotnet restore

# Build stage
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ItemService.dll"]