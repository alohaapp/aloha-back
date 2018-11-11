FROM microsoft/dotnet:2.1-sdk as build

WORKDIR /app

COPY Aloha.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet build

EXPOSE 80
EXPOSE 443

CMD [ "dotnet", "./bin/Debug/netcoreapp2.1/Aloha.dll", "--server.urls", "https://0.0.0.0:443" ]
