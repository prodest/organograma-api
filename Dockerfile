FROM microsoft/dotnet:1.1.0-runtime-deps

COPY OrganogramaWebAPI/src/WebAPI/publish /home/bin
WORKDIR /home/bin

EXPOSE 8935/tcp

CMD ["./WebAPI"]
