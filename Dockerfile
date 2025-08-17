FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6000
ENV ASPNETCORE_URLS=http://+:6000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app

COPY ["src/KSProject.WebApi/KSProject.WebApi.csproj", "src/KSProject.WebApi/"]
COPY ["src/KSProject.Application/KSProject.Application.csproj", "src/KSProject.Application/"]
COPY ["src/KSProject.Domain/KSProject.Domain.csproj", "src/KSProject.Domain/"]
COPY ["src/KSProject.Infrastructure/KSProject.Infrastructure.csproj", "src/KSProject.Infrastructure/"]
COPY ["src/KSProject.Common/KSProject.Common.csproj", "src/KSProject.Common/"]
COPY ["src/KSProject.Presentation/KSProject.Presentation.csproj", "src/KSProject.Presentation/"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages\
    dotnet restore "./src/KSProject.WebApi/KSProject.WebApi.csproj"

COPY . .
WORKDIR "/app/src/KSProject.WebApi"

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages\
    dotnet build "./KSProject.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish

ARG BUILD_CONFIGURATION=Release
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages\
    dotnet publish "./KSProject.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KSProject.WebApi.dll"]