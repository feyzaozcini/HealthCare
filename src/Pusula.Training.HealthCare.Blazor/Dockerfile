FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /App

COPY bin/Release/net8.0/publish/ .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Pusula.Training.HealthCare.Blazor.dll"]
