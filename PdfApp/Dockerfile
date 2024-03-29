#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PdfApp/PdfApp.csproj", "PdfApp/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "PdfApp/PdfApp.csproj"
COPY . .
WORKDIR "/src/PdfApp"
RUN dotnet build "PdfApp.csproj" -c Release -o /app/build

# Download and install wkhtmltopdf
RUN apt-get install -y build-essential xorg libssl-dev libxrender-dev wget gdebi
RUN wget http://downloads.sourceforge.net/project/wkhtmltopdf/0.12.2.1/wkhtmltox-0.12.2.1_linux-trusty-amd64.deb
RUN gdebi --n wkhtmltox-0.12.2.1_linux-trusty-amd64.deb
ENTRYPOINT ["wkhtmltopdf"]

FROM build AS publish
RUN dotnet publish "PdfApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfApp.dll"]
