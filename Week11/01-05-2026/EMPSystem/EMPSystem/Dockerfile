FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["EMPSystem.csproj", "./"]
RUN dotnet restore "EMPSystem.csproj"

COPY . .
RUN dotnet build "EMPSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "EMPSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EMPSystem.dll"]

# docker build -t empsystem .
#docker network create emp-network
#docker run -d -p 5000:8080 -e ASPNETCORE_ENVIRONMENT=Development --network emp-network --name empsystem-container empsystem
#5000	Host port (your browser)
#8089	Container port (your app)

#docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Strong@1234" -p 1433:1433 --network emp-network --name mssql-container -d mcr.microsoft.com/mssql/server:2022-latest
#docker rm -f empsystem-container
#docker rm -f mssql-container

#docker logs empsystem-container
#docker logs mssql-container
#docker ps