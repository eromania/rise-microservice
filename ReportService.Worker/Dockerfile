FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReportService.Worker/ReportService.Worker.csproj", "ReportService.Worker/"]
RUN dotnet restore "ReportService.Worker/ReportService.Worker.csproj"
COPY . .
WORKDIR "/src/ReportService.Worker"
RUN dotnet build "ReportService.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.Worker.dll"]
