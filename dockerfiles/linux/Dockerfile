FROM mcr.microsoft.com/dotnet/core/aspnet:2.1.9-bionic

ENV ETS_PORT=

COPY ["ETS Inside", "/ets"]

WORKDIR /ets

RUN apt update
RUN apt install libuv1 libuv1-dev

ENTRYPOINT dotnet Knx.Ets.Osprey.dll --console --port=$ETS_PORT
