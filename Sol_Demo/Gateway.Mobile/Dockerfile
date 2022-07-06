#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateway.Mobile/Gateway.Mobile.csproj", "Gateway.Mobile/"]
RUN dotnet restore "Gateway.Mobile/Gateway.Mobile.csproj"
COPY . .
WORKDIR "/src/Gateway.Mobile"
RUN dotnet build "Gateway.Mobile.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.Mobile.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.Mobile.dll"]