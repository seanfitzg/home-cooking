FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Create this "restore-solution" section by running ./Create-DockerfileSolutionRestore.ps1, to optimize build cache reuse
COPY ["HomeCooking.Api.Tests/HomeCooking.Api.Tests.csproj", "HomeCooking.Api.Tests/"]
COPY ["HomeCooking.Api/HomeCooking.Api.csproj", "HomeCooking.Api/"]
COPY ["HomeCooking.Application/HomeCooking.Application.csproj", "HomeCooking.Application/"]
COPY ["HomeCooking.Data/HomeCooking.Data.csproj", "HomeCooking.Data/"]
COPY ["HomeCooking.Domain.Tests/HomeCooking.Domain.Tests.csproj", "HomeCooking.Domain.Tests/"]
COPY ["HomeCooking.Domain/HomeCooking.Domain.csproj", "HomeCooking.Domain/"]
COPY ["HomeCooking.Logging/HomeCooking.Logging.csproj", "HomeCooking.Logging/"]
COPY ["docker-compose.dcproj", "./"]
#COPY ["NuGet.config", "./"]
COPY ["HomeCookingApi.sln", "./"]
RUN dotnet restore "HomeCookingApi.sln"

# Copy everything else and build
COPY . ./
RUN dotnet publish HomeCookingApi.sln -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HomeCooking.Api.dll"]

# docker build -t homecooking-app:dev .
# docker run -d -p 5000:80 --name homecooking-app homecooking-app:dev

# to run without docker:
# dapr run --app-id homecooking-app --app-port 80 --dapr-http-port 3500 dotnet run

# docker build -f HomeCooking.Logging/Dockerfile -t logging-app:dev . & docker build -t homecooking-app:dev . & docker-compose up