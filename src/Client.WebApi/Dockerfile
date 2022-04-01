#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Client.WebApi/Client.WebApi.csproj", "Client.WebApi/"]
RUN dotnet restore "Client.WebApi/Client.WebApi.csproj"
COPY . .
WORKDIR "/src/Client.WebApi"
RUN dotnet build "Client.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Client.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.WebApi.dll"]