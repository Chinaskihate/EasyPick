#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Cloud/ModelPredictionRedirector/ModelPredictionRedirector.csproj", "Cloud/ModelPredictionRedirector/"]
RUN dotnet restore "Cloud/ModelPredictionRedirector/ModelPredictionRedirector.csproj"
COPY . .
WORKDIR "/src/Cloud/ModelPredictionRedirector"
RUN dotnet build "ModelPredictionRedirector.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ModelPredictionRedirector.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModelPredictionRedirector.dll"]