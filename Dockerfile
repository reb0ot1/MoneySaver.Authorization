#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MoneySaver.Authorization/MoneySaver.Identity.Api/MoneySaver.Identity.Api.csproj", "MoneySaver.Authorization/MoneySaver.Identity.Api/"]
RUN dotnet restore "MoneySaver.Authorization/MoneySaver.Identity.Api/MoneySaver.Identity.Api.csproj"
COPY ["MoneySaver.Authorization/MoneySaver.Identity.Api/.", "MoneySaver.Authorization/MoneySaver.Identity.Api/"]
WORKDIR "/src/MoneySaver.Authorization/MoneySaver.Identity.Api"
RUN dotnet build "MoneySaver.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoneySaver.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY ["testcert/localhost.pfx", "/https/localhost.pfx"]
COPY ["testcert/RootCA.crt", "/usr/local/share/ca-certificates/RootCA.crt"]
RUN update-ca-certificates
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneySaver.Identity.Api.dll"]
