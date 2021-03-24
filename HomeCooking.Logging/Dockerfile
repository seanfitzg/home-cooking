FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Create this "restore-solution" section by running ./Create-DockerfileSolutionRestore.ps1, to optimize build cache reuse
COPY ["HomeCooking.Logging/HomeCooking.Logging.csproj", "./"]
COPY ["HomeCooking.Domain/HomeCooking.Domain.csproj", "./HomeCooking.Domain"]
RUN dotnet restore "HomeCooking.Logging.csproj"

# Copy everything else and build
COPY ./HomeCooking.Logging/ ./
COPY ./HomeCooking.Domain /HomeCooking.Domain
RUN dotnet publish HomeCooking.Logging.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HomeCooking.Logging.dll"]

# docker build -f HomeCooking.Logging/Dockerfile -t logging-app:dev .
# docker run -d -p 5000:80 --name logging-app logging-app:dev

# to run without docker:
# dapr run --app-id logging-app --app-port 80 --dapr-http-port 3500 dotnet run