#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Customer.API/Customer.API.csproj", "Customer.API/"]
COPY ["Framework.ASP.Extensions/Framework.ASP.Extensions.csproj", "Framework.ASP.Extensions/"]
COPY ["Framework.Base/Framework.Base.csproj", "Framework.Base/"]
COPY ["Framework.ASP.Middlewares/Framework.ASP.Middlewares.csproj", "Framework.ASP.Middlewares/"]
RUN dotnet restore "Customer.API/Customer.API.csproj"
COPY . .
WORKDIR "/src/Customer.API"
RUN dotnet build "Customer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.API.dll"]