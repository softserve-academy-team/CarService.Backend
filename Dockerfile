FROM microsoft/dotnet:2.0.5-sdk-2.1.4-jessie AS builder
WORKDIR /sln

COPY ./CarService.Api ./CarService.Api  
COPY ./CarService.DbAccess ./CarService.DbAccess  

RUN dotnet restore "./CarService.Api/CarService.Api.csproj"
RUN dotnet restore "./CarService.DbAccess/CarService.DbAccess.csproj"

RUN dotnet build "./CarService.DbAccess/CarService.DbAccess.csproj" -c Release --no-restore
RUN dotnet build "./CarService.Api/CarService.Api.csproj" -c Release --no-restore

RUN dotnet publish "./CarService.Api/CarService.Api.csproj" -c Release -o "../dist" --no-restore


FROM microsoft/aspnetcore:2.0.5-jessie
WORKDIR /app  
ENTRYPOINT ["dotnet", "CarService.Api.dll"]  
COPY --from=builder /sln/dist .  


