# Use official .NET 8 SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY Bakery2048.API/Bakery2048.API.csproj Bakery2048.API/
RUN dotnet restore Bakery2048.API/Bakery2048.API.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/Bakery2048.API
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose port (Railway will set PORT env variable)
EXPOSE 8080

# Create entrypoint script
RUN echo '#!/bin/bash\nset -e\necho "Waiting for database..."\nsleep 5\necho "Database ready"\nexec dotnet Bakery2048.API.dll' > /app/entrypoint.sh && chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
