#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskFlow.csproj", "."]
RUN dotnet restore "./TaskFlow.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./TaskFlow.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskFlow.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ✅ Etapa 3: Imagem de runtime (base real definida aqui)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
COPY --from=publish /app/publish .

USER app
ENTRYPOINT ["dotnet", "TaskFlow.dll"]
