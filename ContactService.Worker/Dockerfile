FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactService.Worker/ContactService.Worker.csproj", "ContactService.Worker/"]
RUN dotnet restore "ContactService.Worker/ContactService.Worker.csproj"
COPY . .
WORKDIR "/src/ContactService.Worker"
RUN dotnet build "ContactService.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactService.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactService.Worker.dll"]
