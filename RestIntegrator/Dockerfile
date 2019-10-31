#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1803 AS build
WORKDIR /src
COPY ["RestIntegrator/RestIntegrator.csproj", "RestIntegrator/"]
COPY ["LocationService/LocationService.csproj", "LocationService/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["VirusServices/VirusServices.csproj", "VirusServices/"]
COPY ["NetworkService/NetworkService.csproj", "NetworkService/"]
COPY ["Integrator/Integrator.csproj", "Integrator/"]
RUN dotnet restore "RestIntegrator/RestIntegrator.csproj"
COPY . .
WORKDIR "/src/RestIntegrator"
RUN dotnet build "RestIntegrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestIntegrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestIntegrator.dll"]