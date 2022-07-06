#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateway.Web/Gateway.Web.csproj", "Gateway.Web/"]
RUN dotnet restore "Gateway.Web/Gateway.Web.csproj"
COPY . .
WORKDIR "/src/Gateway.Web"
RUN dotnet build "Gateway.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.Web.dll"]