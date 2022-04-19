FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Synonyms.Api/Synonyms.Api.csproj", "Synonyms.Api/"]
COPY ["Synonyms.Core/Synonyms.Core.csproj", "Synonyms.Core/"]
COPY ["Synonyms.Infrastructure/Synonyms.Infrastructure.csproj", "Synonyms.Infrastructure/"]
RUN dotnet restore "Synonyms.Api/Synonyms.Api.csproj"
RUN dotnet restore "Synonyms.Core/Synonyms.Core.csproj"
RUN dotnet restore "Synonyms.Infrastructure/Synonyms.Infrastructure.csproj"
COPY . .
WORKDIR "/src/Synonyms.Api"
RUN dotnet restore "Synonyms.Api.csproj"
RUN dotnet build "Synonyms.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Synonyms.Api.csproj" -c Release -o /app/publish

FROM base AS final
RUN mkdir /logs
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Synonyms.Api.dll"]
