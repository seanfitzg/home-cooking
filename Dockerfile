FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
#COPY *.csproj ./
COPY . ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HomeCooking.Api.dll"]

# docker build -t recipe-app:dev .
# docker run -d -p 5000:80 --name recipe-app recipe-app:dev


# to run without docker:
# dapr run --app-id recipe-app --app-port 80 dotnet run