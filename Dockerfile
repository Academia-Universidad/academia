# ============================================================
# ETAPA 1: CONSTRUCCIÓN
# En esta etapa utilizamos el SDK porque necesitamos
# restaurar dependencias y compilar la aplicación.
# ============================================================

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build


# ------------------------------------------------------------
# Definimos /src como directorio de trabajo dentro de Docker.
# Las siguientes instrucciones se ejecutarán desde esta carpeta.
# ------------------------------------------------------------

WORKDIR /src


# ------------------------------------------------------------
# Copiamos primero únicamente el archivo .csproj.
# Esto permite que Docker aproveche su sistema de caché.
# ------------------------------------------------------------

COPY ["ProgramacionV.csproj", "./"]


# ------------------------------------------------------------
# Descargamos las dependencias NuGet definidas en el .csproj.
# ------------------------------------------------------------

RUN dotnet restore "ProgramacionV.csproj"


# ------------------------------------------------------------
# Ahora copiamos todo el código fuente del proyecto.
#
# Primer punto  = carpeta actual de nuestro computador.
# Segundo punto = /src dentro de Docker.
# ------------------------------------------------------------

COPY . .


# ------------------------------------------------------------
# Publicamos la aplicación usando configuración Release.
#
# -c Release       -> compilar para despliegue.
# -o /app/publish  -> carpeta donde quedará el resultado.
# --no-restore     -> no repetir el restore anterior.
# ------------------------------------------------------------

RUN dotnet publish "ProgramacionV.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore



# ============================================================
# ETAPA 2: EJECUCIÓN
#
# Ya no necesitamos el SDK completo.
# Utilizamos solamente ASP.NET Runtime.
# ============================================================

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final


# ------------------------------------------------------------
# Carpeta donde estará nuestra aplicación final.
# ------------------------------------------------------------

WORKDIR /app


# ------------------------------------------------------------
# Copiamos solamente el resultado generado en la etapa build.
#
# No copiamos compiladores ni herramientas de desarrollo.
# ------------------------------------------------------------

COPY --from=build /app/publish .


# ------------------------------------------------------------
# Indicamos a ASP.NET Core que escuche en el puerto 8080.
# ------------------------------------------------------------

ENV ASPNETCORE_URLS=http://+:8080


# ------------------------------------------------------------
# Documentamos que nuestra aplicación utiliza el puerto 8080.
#
# EXPOSE no publica el puerto hacia Windows.
# Eso se hará después con docker run -p.
# ------------------------------------------------------------

EXPOSE 8080


# ------------------------------------------------------------
# Comando que Docker ejecutará cuando arranque el contenedor.
#
# Equivale a ejecutar:
#
# dotnet ProgramacionV.dll
# ------------------------------------------------------------

ENTRYPOINT ["dotnet", "ProgramacionV.dll"]