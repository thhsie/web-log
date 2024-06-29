FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false
ENV DOTNET_NOLOGO=1
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src
COPY ["Weblog.Api/Weblog.Api.csproj", "Weblog.Api/"]
RUN dotnet restore "Weblog.Api/Weblog.Api.csproj"
COPY . .
WORKDIR "/src/Weblog.Api/."
RUN dotnet build "Weblog.Api.csproj" --configuration Release --no-restore --nologo --output /app/build 

FROM build AS publish
RUN dotnet publish "Weblog.Api.csproj" --configuration Release --no-restore --nologo --output /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weblog.Api.dll"]