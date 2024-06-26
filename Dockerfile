﻿# Utilizar la imagen base de .NET Core SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Establecer el directorio de trabajo dentro del contenedor
WORKDIR /source

# Copiar los archivos del proyecto al contenedor
COPY . .
# Restaurar las dependencias y compilar la aplicación
RUN dotnet restore "WebApi/WebApi.csproj" --disable-parallel
RUN dotnet publish "WebApi/WebApi.csproj" -c release -o /app --no-restore -f net6.0


# Etapa de producción
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
# Copiar los archivos publicados de la etapa anterior
COPY --from=build /app ./

# El puerto que expondrá la aplicación
EXPOSE 7034

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "WebApi.dll"]
