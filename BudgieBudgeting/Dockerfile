# Use the official .NET image as a build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BudgieBudgeting.csproj", "."]
RUN dotnet restore "BudgieBudgeting.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "BudgieBudgeting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BudgieBudgeting.csproj" -c Release -o /app/publish

# Use the base image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BudgieBudgeting.dll"]

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base_debug
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build_debug
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BudgieBudgeting.csproj", "."]
RUN dotnet restore "./BudgieBudgeting.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "./BudgieBudgeting.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build_debug AS publish_debug
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BudgieBudgeting.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base_debug AS final_debug
WORKDIR /app
COPY --from=publish_debug /app/publish .
ENTRYPOINT ["dotnet", "BudgieBudgeting.dll"]
