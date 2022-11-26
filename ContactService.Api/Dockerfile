FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactService.Api/ContactService.Api.csproj", "ContactService.Api/"]
RUN dotnet restore "ContactService.Api/ContactService.Api.csproj"
COPY . .
WORKDIR "/src/ContactService.Api"
RUN dotnet build "ContactService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactService.Api.dll"]
