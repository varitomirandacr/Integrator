#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1803 AS build
WORKDIR /src
COPY ["Integrator/Integrator.csproj", "Integrator/"]
COPY ["NetworkService/NetworkService.csproj", "NetworkService/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "Integrator/Integrator.csproj"
COPY . .
WORKDIR "/src/Integrator"
RUN dotnet build "Integrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Integrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Integrator.dll"]