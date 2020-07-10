#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#WORKDIR ...
#COPY NuGet.Config /

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AspNetCoreFFmpeg/AspNetCoreFFmpeg.csproj", "AspNetCoreFFmpeg/"]
RUN dotnet restore "AspNetCoreFFmpeg/AspNetCoreFFmpeg.csproj" -s https://api.nuget.org/v3/index.json -s http://skysoftware.ddns.net:7000/nuget
COPY . .
WORKDIR "/src/AspNetCoreFFmpeg"
RUN dotnet build "AspNetCoreFFmpeg.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreFFmpeg.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreFFmpeg.dll"]