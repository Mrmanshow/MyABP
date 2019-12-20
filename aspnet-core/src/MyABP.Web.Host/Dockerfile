FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/MyABP.Web.Host/MyABP.Web.Host.csproj src/MyABP.Web.Host/
COPY src/MyABP.Web.Core/MyABP.Web.Core.csproj src/MyABP.Web.Core/
COPY src/MyABP.EntityFrameworkCore/MyABP.EntityFrameworkCore.csproj src/MyABP.EntityFrameworkCore/
COPY src/MyABP.Core/MyABP.Core.csproj src/MyABP.Core/
COPY src/MyABP.Application/MyABP.Application.csproj src/MyABP.Application/
RUN dotnet restore src/MyABP.Web.Host/MyABP.Web.Host.csproj
COPY . .
WORKDIR /src/src/MyABP.Web.Host
RUN dotnet build MyABP.Web.Host.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MyABP.Web.Host.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyABP.Web.Host.dll"]
