FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src

# Create this "restore-solution" section by running ./Create-DockerfileSolutionRestore.ps1, to optimize build cache reuse
COPY ["HomeCooking.Api/HomeCooking.Api.csproj", "./HomeCooking.Api/"]
COPY ["HomeCooking.Application/HomeCooking.Application.csproj", "./HomeCooking.Application/"]
COPY ["HomeCooking.Data/HomeCooking.Data.csproj", "./HomeCooking.Data/"]
COPY ["HomeCooking.Domain/HomeCooking.Domain.csproj", "./HomeCooking.Domain/"]
COPY ["HomeCooking.Logging/HomeCooking.Logging.csproj", "./HomeCooking.Logging/"]
RUN dotnet restore "./HomeCooking.Api/HomeCooking.Api.csproj"

# Copy everything else and build
COPY . ./
WORKDIR /src/HomeCooking.Api
RUN dotnet publish HomeCooking.Api.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /src/HomeCooking.Api/out .
ENTRYPOINT ["dotnet", "HomeCooking.Api.dll"]

# from solution dir
# docker build -f HomeCooking.Api/Dockerfile -t homecooking-app:dev .

# docker run -d -p 5000:80 --name homecooking-app homecooking-app:dev

# to run without docker:
# dapr run --app-id homecooking-app --app-port 80 --dapr-http-port 3500 dotnet run

# docker build -f HomeCooking.Logging/Dockerfile -t logging-app:dev . & docker build -t homecooking-app:dev . & docker-compose up