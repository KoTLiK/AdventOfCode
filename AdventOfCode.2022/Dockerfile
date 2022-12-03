FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AdventOfCode.2022/AdventOfCode.2022.csproj", "AdventOfCode.2022/"]
RUN dotnet restore "AdventOfCode.2022/AdventOfCode.2022.csproj"
COPY . .
WORKDIR "/src/AdventOfCode.2022"
RUN dotnet build "AdventOfCode.2022.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdventOfCode.2022.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdventOfCode.2022.dll"]
