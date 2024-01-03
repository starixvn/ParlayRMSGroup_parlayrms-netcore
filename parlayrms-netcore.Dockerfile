FROM hub.nexdev.net/base/dotnet/core/sdk:8.0-alpine AS builder

WORKDIR /src
COPY ./Sources/ParlayRMS/*.csproj ./Sources/ParlayRMS/
COPY ./Sources/ParlayRMS.Application/*.csproj ./Sources/ParlayRMS.Application/
COPY ./Sources/ParlayRMS.LeagueEventControl/*.csproj ./Sources/ParlayRMS.LeagueEventControl/
COPY ./Sources/ParlayRMS.ManageAlert/*.csproj ./ParlayRMS.ManageAlert/
COPY ./Sources/ParlayRMS.WinLossRiskControl/*.csproj ./Sources/ParlayRMS.WinLossRiskControl/
COPY ./Sources/ParlayRMS.Legacy/*.csproj ./Sources/ParlayRMS.Legacy/
COPY ./ParlayRMS.Site/*.csproj  ./ParlayRMS.Site/

RUN dotnet nuget add source https://nuget.nexdev.net/api/v2
RUN --mount=type=cache,target=/root/.nuget/packages \
 dotnet restore ParlayRMS.Site/ParlayRMS.Site.csproj

# publish
COPY ./Sources/. ./Sources
COPY ./ParlayRMS.Site/. ./ParlayRMS.Site

WORKDIR /src/ParlayRMS.Site
RUN --mount=type=cache,target=/root/.nuget/packages \
 dotnet publish -c release -o /app
 
FROM hub.nexdev.net/base/dotnet/core/aspnet:8.0-alpine AS base

# Don’t run as root
RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app && chown -R dotnetuser /tmp
USER dotnetuser
#

WORKDIR /app
COPY --from=builder /app .
ENV TZ="America/La_Paz"
ENV COMPlus_EnableDiagnostics=0

ENTRYPOINT ["dotnet", "ParlayRMS.Site.dll"]
