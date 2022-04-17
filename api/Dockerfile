FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Synonym.Api/Synonym.Api.csproj", "Synonym.Api/"]
COPY ["Synonym.Core/Synonym.Core.csproj", "Synonym.Core/"]
COPY ["Synonym.Infra/Synonym.Infra.csproj", "Synonym.Infra/"]
RUN dotnet restore "Synonym.Api/Synonym.Api.csproj"
RUN dotnet restore "Synonym.Core/Synonym.Core.csproj"
RUN dotnet restore "Synonym.Infra/Synonym.Infra.csproj"
COPY . .
WORKDIR "/src/Synonym.Api"
RUN dotnet restore "Synonym.Api.csproj"
RUN dotnet build "Synonym.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Synonym.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Synonym.Api.dll"]
