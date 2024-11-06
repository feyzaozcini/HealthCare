FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /App

COPY bin/Release/net8.0/publish/ .

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Pusula.Training.HealthCare.DbMigrator.dll"]
