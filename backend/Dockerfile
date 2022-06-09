FROM mcr.microsoft.com/dotnet/sdk:6.0-focal as build
WORKDIR /app
COPY . .
RUN dotnet --version
RUN dotnet restore
RUN dotnet publish -c Release -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal as runtime
WORKDIR /app
COPY --from=build /app/published-app /app

ENV ASPNETCORE_URLS="http://+:5000"

EXPOSE 5000

ENTRYPOINT [ "dotnet", "Toro.Accounting.Api.dll" ]