#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MissionSystem.Main/MissionSystem.Main.csproj", "MissionSystem.Main/"]
COPY ["TheLightingController.Lib/TheLightingController.Lib.csproj", "TheLightingController.Lib/"]
COPY ["MissionSystem.Util/MissionSystem.Util.csproj", "MissionSystem.Util/"]
COPY ["MissionSystem.Data/MissionSystem.Data.csproj", "MissionSystem.Data/"]
COPY ["MissionSystem.Interface/MissionSystem.Interface.csproj", "MissionSystem.Interface/"]
COPY ["MissionSystem.Factory/MissionSystem.Factory.csproj", "MissionSystem.Factory/"]
COPY ["MissionSystem.Logic.CTF/MissionSystem.Logic.CTF.csproj", "MissionSystem.Logic.CTF/"]
COPY ["MissionSystem.Logic/MissionSystem.Logic.csproj", "MissionSystem.Logic/"]
COPY ["MissionSystem.MqttBroker/MissionSystem.MqttBroker.csproj", "MissionSystem.MqttBroker/"]
RUN dotnet restore "MissionSystem.Main/MissionSystem.Main.csproj"
COPY . .
WORKDIR "/src/MissionSystem.Main"
RUN dotnet build "MissionSystem.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MissionSystem.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MissionSystem.Main.dll"]