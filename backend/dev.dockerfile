FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app


ENTRYPOINT [ "dotnet", "watch", "run", "--urls", "http://*:5000", "--project", "src/WebApi/WebApi.csproj" ]