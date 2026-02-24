# Build Blazor WebAssembly
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY BankBlazor.sln ./
COPY BankBlazor.Client/BankBlazor.Client.csproj BankBlazor.Client/
COPY BankBlazor.Shared/BankBlazor.Shared.csproj BankBlazor.Shared/

RUN dotnet restore BankBlazor.Client/BankBlazor.Client.csproj

COPY . ./
WORKDIR /src/BankBlazor.Client
RUN dotnet publish -c Release -o /app/publish

# Serve static files + reverse proxy /api -> API container
FROM nginx:1.27-alpine AS final

COPY BankBlazor.Client/nginx/default.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80
