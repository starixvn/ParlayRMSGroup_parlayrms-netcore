FROM hub.nexdev.net/base/dotnet/core/sdk:7.0 AS builder
WORKDIR /src

COPY ./Sources/ParlayRMS.Application ./ParlayRMS.Application
COPY ./Sources/ParlayRMS.LeagueEventControl ./ParlayRMS.LeagueEventControl
COPY ./Sources/ParlayRMS.WinLossRiskControl ./ParlayRMS.WinLossRiskControl
COPY ./Sources/ParlayRMS.ManageAlert ./ParlayRMS.ManageAlert
COPY ./Sources/ParlayRMS.Site/ParlayRMS.Site.csproj ./ParlayRMS.Site/ParlayRMS.Site.csproj

RUN ls /src -l
WORKDIR /src/ParlayRMS.Site
RUN dotnet nuget add source http://nuget.nexdev.net/api/v2
RUN --mount=type=cache,target=/root/.nuget/packages \
	dotnet restore -v n

COPY ./Sources/ParlayRMS.Site .
RUN --mount=type=cache,target=/root/.nuget/packages \
	dotnet publish -r linux-x64 --self-contained false -c Release -o /app 

FROM hub.nexdev.net/rebuild-baseimage/dotnetcore/aspnet:7.0-nap-mail-core

# Don’t run as root
RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app
USER dotnetuser
#

ENV TZ="America/La_Paz" 


WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet", "ParlayRMS.Site.dll"]
