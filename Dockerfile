FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

 COPY messaging.sln ./  
 COPY Messaging.Api/*.csproj ./Messaging.Api/  
 COPY Messaging.Core/*.csproj ./Messaging.Core/  
 COPY Messaging.Infrastructure/*.csproj ./Messaging.Infrastructure/ 
 COPY Messaging.Test/*.csproj ./Messaging.Test/  
 COPY Messaging/*.csproj ./Messaging/   

RUN dotnet restore 

COPY . .

RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "Messaging.Api.dll"]  