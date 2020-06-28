#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat


#FROM mcr.microsoft.com/windows/servercore:ltsc2019
#
#SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
#
#ENV chocolateyUseWindowsCompression false
#
#RUN iex ((new-object net.webclient).DownloadString('https://chocolatey.org/install.ps1')); \
    #choco feature disable --name showDownloadProgress; \
    #choco install -y mysql --version=8.0.17;
#
#CMD C:\tools\mysql\current\run.ps1


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["PaymentMicroservices.Api/PaymentMicroservices.Api.csproj", "PaymentMicroservices.Api/"]
RUN dotnet restore "PaymentMicroservices.Api/PaymentMicroservices.Api.csproj"
COPY . .
WORKDIR "/src/PaymentMicroservices.Api"
RUN dotnet build "PaymentMicroservices.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentMicroservices.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentMicroservices.Api.dll"]