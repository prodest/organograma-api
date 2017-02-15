FROM microsoft/dotnet:1.1-sdk-projectjson

COPY OrganogramaWebAPI/src /home/src/
WORKDIR /home/src/WebAPI

RUN dotnet restore

EXPOSE 8935/tcp

CMD ["dotnet", "run"]
