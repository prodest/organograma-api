FROM microsoft/dotnet:latest

COPY ProcessoEletronicoWebAPI/src /home/src/
WORKDIR /home/src/WebAPI.Restrito

RUN dotnet restore

EXPOSE 3308/tcp

CMD ["dotnet", "run"]
