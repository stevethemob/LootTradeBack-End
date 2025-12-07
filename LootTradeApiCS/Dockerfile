# =============================
# Build stage
# =============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy ONLY the actual project
COPY LootTradeApiCS/LootTradeApiCS.csproj LootTradeApiCS/

# Restore
RUN dotnet restore LootTradeApiCS/LootTradeApiCS.csproj

# Copy everything else
COPY . .

WORKDIR /src/LootTradeApiCS
RUN dotnet publish -c Release -o /app/publish

# =============================
# Runtime stage
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LootTradeApiCS.dll"]
