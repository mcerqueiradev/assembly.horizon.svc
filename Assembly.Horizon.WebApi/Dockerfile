FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Assembly.Horizon.WebApi/Assembly.Horizon.WebApi.csproj", "Assembly.Horizon.WebApi/"]
COPY ["Assembly.Horizon.Application/Assembly.Horizon.Application.csproj", "Assembly.Horizon.Application/"]
COPY ["Assembly.Horizon.Domain.Core/Assembly.Horizon.Domain.Core.csproj", "Assembly.Horizon.Domain.Core/"]
COPY ["Assembly.Horizon.Domain/Assembly.Horizon.Domain.csproj", "Assembly.Horizon.Domain/"]
COPY ["Assembly.Horizon.Security/Assembly.Horizon.Security.csproj", "Assembly.Horizon.Security/"]
COPY ["Assembly.Horizon.Infra.Data/Assembly.Horizon.Infra.Data.csproj", "Assembly.Horizon.Infra.Data/"]
COPY ["Assembly.Horizon.Infra.IoC/Assembly.Horizon.Infra.IoC.csproj", "Assembly.Horizon.Infra.IoC/"]
RUN dotnet restore "./Assembly.Horizon.WebApi/Assembly.Horizon.WebApi.csproj"
COPY . .
WORKDIR "/src/Assembly.Horizon.WebApi"
RUN dotnet build "./Assembly.Horizon.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Assembly.Horizon.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/Uploads
RUN mkdir -p /app/wwwroot
ENV ASPNETCORE_URLS="http://+:8080"
ENTRYPOINT ["dotnet", "Assembly.Horizon.WebApi.dll"]