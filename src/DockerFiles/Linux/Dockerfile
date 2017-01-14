FROM microsoft/dotnet:1.1.0-runtime

COPY . /app

WORKDIR /app

EXPOSE 5050/tcp

CMD ["dotnet", "RoboBank.Account.Service.NetCore.dll", "5050"]
