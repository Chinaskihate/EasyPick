#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Cloud/DraftPrediction/DraftPrediction.csproj", "Cloud/DraftPrediction/"]
RUN dotnet restore "Cloud/DraftPrediction/DraftPrediction.csproj"
COPY . .
WORKDIR "/src/Cloud/DraftPrediction"
RUN dotnet build "DraftPrediction.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DraftPrediction.csproj" -c Release -o /app/DraftPrediction/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/DraftPrediction
COPY --from=publish /app/DraftPrediction/publish .
ENTRYPOINT ["dotnet", "DraftPrediction.dll"]