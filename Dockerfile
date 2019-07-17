FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Loterias.API/Loterias.API.csproj Loterias.API/
COPY Loterias.Domain/Loterias.Domain.csproj Loterias.Domain/
COPY Loterias.Common/Loterias.Common.csproj Loterias.Common/
COPY Loterias.Application/Loterias.Application.csproj Loterias.Application/
COPY Loterias.Data/Loterias.Data.csproj Loterias.Data/
RUN dotnet restore Loterias.API/Loterias.API.csproj
COPY . .
WORKDIR /src/Loterias.API
RUN dotnet build Loterias.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Loterias.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Loterias.API.dll"]
