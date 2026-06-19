FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY Aurora.SupplyWok.Platform/*.csproj Aurora.SupplyWok.Platform/
RUN dotnet restore ./Aurora.SupplyWok.Platform
COPY . .
RUN dotnet publish ./Aurora.SupplyWok.Platform -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "Aurora.SupplyWok.Platform.dll"]