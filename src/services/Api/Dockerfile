FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY . /app
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "Service.dll"]