
docker pull mcr.microsoft.com/dotnet/aspnet
FROM mcr.microsoft.com/dotnet/core/aspnet:6.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:6.0-buster AS build
WORKDIR /src
COPY ["QA.Server/QA.Server.csproj", "QA.Server/"]
COPY ["QA.Infra/QA.Infra.csproj", "QA.Infra/"]
COPY ["DocumentStore.Core/DocumentStore.csproj", "DocumentStore.Core/"]
COPY ["QA.Core/QA.Core.csproj", "QA.Core/"]
RUN dotnet restore "QA.Server/QA.Server.csproj"
COPY . .
WORKDIR "/src/QA.Server"
RUN dotnet build "QA.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QA.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QA.Server.dll"]