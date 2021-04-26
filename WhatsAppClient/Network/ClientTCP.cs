using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppClient.Network
{
   static class ClientTCP
    {
        private static TcpClient clientSocket;
        private static NetworkStream myStream;
        private static byte[] recBuffer;

        public static void NetworkBaslat()
        {
            clientSocket = new TcpClient();
            clientSocket.ReceiveBufferSize = 4096;
            clientSocket.SendBufferSize = 4096;
            recBuffer = new byte[4096];
            clientSocket.BeginConnect("192.168.2.100", 6060, new AsyncCallback(ClientConnectCallBack), clientSocket); // Ipadresi girilecek.
        }

        private static void ClientConnectCallBack(IAsyncResult result)
        {
            clientSocket.EndConnect(result);

        if(clientSocket.Connected == false)
            {
                return;
            }
            else
            {
                clientSocket.NoDelay = true;
                myStream = clientSocket.GetStream();
                myStream.BeginRead(recBuffer, 0, 4096, ReceiveCallBack, null);
            }
        }

        private static void ReceiveCallBack(IAsyncResult result)
        {
           try
            {
                int lenght = myStream.EndRead(result);
                if (lenght <= 0)
                    return;

                byte[] newBytes = new byte[lenght];
                Array.Copy(recBuffer, newBytes, lenght);

                ClientHandleData.HandleData(newBytes);

                myStream.BeginRead(recBuffer, 0, 4096, ReceiveCallBack, null);
            }
            catch (Exception)
            {
                Disconnect();
            }
        }

        public static void SendData(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.Bytes_Yaz(data);
            myStream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
            buffer.Dispose();
        }

        public static void Disconnect()
        {
            clientSocket.Close();
        }
    }
}
