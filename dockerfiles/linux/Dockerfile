FROM microsoft/dotnet:2.1-aspnetcore-runtime

ENV ETS_PORT=

RUN useradd -ms /bin/bash knx
RUN usermod -u 996 knx

COPY ["ETS Inside", "/ets"]
RUN chmod 755 /ets/Knx.Ets.Osprey.dll
COPY ["ETS Inside/10-ets-inside.rules", "/etc/udev/rules.d/10-ets-inside.rules"]

WORKDIR /ets

RUN apt-get update \
 && apt-get upgrade -y \
 && apt-get install libc6-dev libunwind8 libuv1 libuv1-dev -y \
 && apt-get autoclean -y

# RUN udevadm control --reload-rules

# ENTRYPOINT udevadm control --reload-rules && dotnet Knx.Ets.Osprey --console --port=$ETS_PORT
ENTRYPOINT dotnet exec Knx.Ets.Osprey.dll --console --port=$ETS_PORT
# ENTRYPOINT [ "/bin/bash" ]