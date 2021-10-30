FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TopCourseWorkBl/TopCourseWorkBl.csproj", "TopCourseWorkBl/"]
RUN dotnet restore "TopCourseWorkBl/TopCourseWorkBl.csproj"
COPY . .
WORKDIR "/src/TopCourseWorkBl"
RUN dotnet build "TopCourseWorkBl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TopCourseWorkBl.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TopCourseWorkBl.dll"]
