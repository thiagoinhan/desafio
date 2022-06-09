FROM mcr.microsoft.com/dotnet/sdk:6.0-focal-arm64v8 as build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal-arm64v8 as runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ENV ASPNETCORE_URLS="http://+:5000"

EXPOSE 5000

ENTRYPOINT [ "dotnet", "Toro.Accounting.Api.dll" ]