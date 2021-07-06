using K4os.Compression.LZ4;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WindowsClient
{
    public class Server
    {
        private Thread thread;

        private TcpClient tcpClient;
        private NetworkStream stream;

        private string host;
        private WasapiLoopbackCapture capture;

        private byte[] compress = new byte[1024 * 1024];

        public Server(string host)
        {
            this.host = host;

            tcpClient = new TcpClient("192.168.1.1", 13900);
            stream = tcpClient.GetStream();

            capture = new WasapiLoopbackCapture();
            stream.WriteByte((byte)capture.WaveFormat.Channels);
            stream.Write(BitConverter.GetBytes((short)capture.WaveFormat.SampleRate));
            
            capture.DataAvailable += Capture_DataAvailable;
            capture.StartRecording();
        }

        private void Capture_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (e.BytesRecorded == 0) return;
            var encodedLength = LZ4Codec.Encode(e.Buffer, 0, e.BytesRecorded, compress, 0, compress.Length);
            // Debug.WriteLine($"Sent {encodedLength}");
            stream.Write(BitConverter.GetBytes(e.BytesRecorded));
            stream.Write(BitConverter.GetBytes(encodedLength));
            stream.Write(compress, 0, encodedLength);
        }
    }
}
