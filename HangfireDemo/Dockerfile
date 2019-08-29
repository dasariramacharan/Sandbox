FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["HangfireDemo/Playground.Web.csproj", "HangfireDemo/"]
COPY ["ServiceLayer/ServiceLayer.csproj", "ServiceLayer/"]
RUN dotnet restore "HangfireDemo/Playground.Web.csproj"
COPY . .
WORKDIR "/src/HangfireDemo"
RUN dotnet build "Playground.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Playground.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Playground.Web.dll"]