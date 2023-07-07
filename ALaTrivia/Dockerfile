FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ALaTrivia/ALaTrivia.csproj", "ALaTrivia/"]
RUN dotnet restore "ALaTrivia/ALaTrivia.csproj"
COPY . .
WORKDIR "/src/ALaTrivia"
RUN dotnet build "ALaTrivia.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ALaTrivia.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ALaTrivia.dll"]
