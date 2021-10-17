FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HomeCooking.Queries/HomeCooking.Queries.csproj", "HomeCooking.Queries/"]
RUN dotnet restore "HomeCooking.Queries/HomeCooking.Queries.csproj"
COPY . .
WORKDIR "/src/HomeCooking.Queries"
RUN dotnet build "HomeCooking.Queries.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeCooking.Queries.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeCooking.Queries.dll"]
