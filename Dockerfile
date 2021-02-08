FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy of csproj and restore  as distinct layers
COPY *.sln .
COPY ./src/SeoulAir.Command.Api/*.csproj ./src/SeoulAir.Command.Api/
COPY ./src/SeoulAir.Command.Domain/*.csproj ./src/SeoulAir.Command.Domain/
COPY ./src/SeoulAir.Command.Domain.Services/*.csproj ./src/SeoulAir.Command.Domain.Services/
COPY ./src/SeoulAir.Command.Repositories/*.csproj ./src/SeoulAir.Command.Repositories/

RUN dotnet restore

# copy everything else and build app
COPY *.sln .
COPY ./src/SeoulAir.Command.Api/. ./src/SeoulAir.Command.Api/
COPY ./src/SeoulAir.Command.Domain/. ./src/SeoulAir.Command.Domain/
COPY ./src/SeoulAir.Command.Domain.Services/. ./src/SeoulAir.Command.Domain.Services/
COPY ./src/SeoulAir.Command.Repositories/. ./src/SeoulAir.Command.Repositories/

WORKDIR /app/src/SeoulAir.Command.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build /app/src/SeoulAir.Command.Api/out ./
ENV ASPNETCORE_URLS=http://+:5800
ENTRYPOINT ["dotnet","SeoulAir.Command.Api.dll"]
