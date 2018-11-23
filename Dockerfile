FROM microsoft/dotnet:2.1-sdk as build
WORKDIR /app

COPY Aloha.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o build

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app

COPY --from=build /app/build .

EXPOSE 80
EXPOSE 443

ENTRYPOINT [ "dotnet", "./Aloha.dll" ]
