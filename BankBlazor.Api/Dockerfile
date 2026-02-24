# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY BankBlazor.sln ./
COPY BankBlazor.Api/BankBlazor.Api.csproj BankBlazor.Api/
COPY BankBlazor.Shared/BankBlazor.Shared.csproj BankBlazor.Shared/

RUN dotnet restore BankBlazor.Api/BankBlazor.Api.csproj

COPY . ./
WORKDIR /src/BankBlazor.Api
RUN dotnet publish -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "BankBlazor.Api.dll"]
