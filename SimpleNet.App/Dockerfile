FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SimpleNet/SimpleNet.csproj", "SimpleNet/"]
RUN dotnet restore "SimpleNet/SimpleNet.csproj"
COPY . .
WORKDIR "/src/SimpleNet"
RUN dotnet build "SimpleNet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleNet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleNet.dll"]
