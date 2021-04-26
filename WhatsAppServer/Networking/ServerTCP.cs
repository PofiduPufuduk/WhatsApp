using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppMe.Networking
{
    class ServerTCP
    {
        static TcpListener serverSocket = new TcpListener(IPAddress.Any,6060); // 6060 port
        public static void InitializeNetwork()
        {
            Sabitler.WhatsAppPanel1.listBox1.Items.Add("Paketleriniz başlatılıyor.");
            ServerHandleData.InitializePackes();
            serverSocket.Start();
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null);
        }

        private static void OnClientConnect(IAsyncResult result)
        {
            TcpClient client = serverSocket.EndAcceptTcpClient(result);
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), null);
            ClientManager.CreateNewConnection(client);
        }
    }
}
