FROM microsoft/dotnet

COPY OrganogramaWebAPI/src /home/src/
WORKDIR /home/src/WebAPI

RUN dotnet restore

EXPOSE 8935/tcp

CMD ["dotnet", "run"]
