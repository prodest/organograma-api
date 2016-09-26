FROM microsoft/dotnet:latest

COPY OrganogramaWebAPI/src /home/src/
WORKDIR /home/src/WebAPI.Restrito

RUN dotnet restore

EXPOSE 8935/tcp

CMD ["dotnet", "run"]
