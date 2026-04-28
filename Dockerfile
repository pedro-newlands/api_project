FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY . .

<<<<<<< HEAD
RUN dotnet tool install --global dotnet-ef
=======
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

COPY --from=build /app .

<<<<<<< HEAD
ENTRYPOINT ["dotnet", "ProjetoPokeShop.dll"]
=======
ENTRYPOINT ["dotnet", "ProjetoPokeShop.dll"]
>>>>>>> 354d50e5ecccea0eeae8ee7fa0c7838699225379
