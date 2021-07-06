import struct
import threading
import traceback
import wave
import socket
from time import sleep

import lz4.block
import pyaudio

HOST = "0.0.0.0"
PORT = 13900

CHUNK = 1024 * 4
FORMAT = pyaudio.paFloat32


class Worker(threading.Thread):
    def __init__(self, conn):
        super().__init__()
        self.conn: socket.socket = conn

    def run(self):
        f = self.conn.makefile(mode='b')
        channel = self.conn.recv(1)[0]
        rate = struct.unpack('H', self.conn.recv(2))[0]

        stream = p.open(format=FORMAT,
                        channels=channel,
                        rate=rate,
                        output=True,
                        frames_per_buffer=CHUNK)

        comData = " "
        while comData != "":
            try:
                comData = f.read(4)
                unCompressLength = int.from_bytes(comData, "little")
                comData = f.read(4)
                length = int.from_bytes(comData, "little")
                comData = f.read(length)
                #  print(f'{length} == {len(comData)}')
                data = lz4.block.decompress(comData, uncompressed_size=unCompressLength)
                stream.write(data)
            except socket.error:
                print("Client Disconnected")
                break
            except Exception:
                traceback.print_exc()
                stream.close()
                self.conn.close()
                break


if __name__ == '__main__':
    p = pyaudio.PyAudio()

    with socket.socket() as server_socket:
        server_socket.bind((HOST, PORT))
        server_socket.listen(1)
        while True:
            conn, address = server_socket.accept()
            print(f"Connection from {address[0]}:{address[1]}")

            Worker(conn).start()
