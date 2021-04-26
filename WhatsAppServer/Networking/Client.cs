using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppMe.Networking
{
    class Client
    {
        public int ConnectionID;
        public TcpClient socket;
        public NetworkStream stream;
        private byte[] recBuffer;
        public ByteBuffer buffer;

        public void Start()
        {
            socket.SendBufferSize = 4096;
            socket.ReceiveBufferSize = 4096;
            stream = socket.GetStream();
            recBuffer = new byte[4096];
            stream.BeginRead(recBuffer,0,socket.ReceiveBufferSize,OnReceiveData, null);
        }

        private void OnReceiveData(IAsyncResult result)
        {
            try
            {
                int lenght = stream.EndRead(result);
                if(lenght <= 0)
                {
                    CloseConnection();
                    return;
                }

                byte[] newBytes = new byte[lenght];
                Array.Copy(recBuffer, newBytes, lenght);

                ServerHandleData.HandleData(ConnectionID, newBytes);
                stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnReceiveData, null);
            }
            catch(Exception)
            {
                CloseConnection();
            }
        }
        private void CloseConnection()
        {
            Sabitler.WhatsAppPanel1.listBox1.Items.Add(ConnectionID + "'in bağlantısı koptu.");
            socket.Close();
        }
    }
}
