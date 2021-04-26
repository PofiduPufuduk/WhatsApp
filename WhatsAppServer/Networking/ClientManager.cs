using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsAppMe.Networking
{
    static class ClientManager
    {
        public static void CreateNewConnection(TcpClient tempClient)
        {
            Client newClient = new Client();
            newClient.socket = tempClient;
            newClient.ConnectionID = ((IPEndPoint)tempClient.Client.RemoteEndPoint).Port;
            newClient.Start();
            Sabitler.bagli_clients.Add(newClient.ConnectionID, newClient);
            Control.CheckForIllegalCrossThreadCalls = false;
            Sabitler.WhatsAppPanel1.listBox1.Items.Add(newClient.ConnectionID + "'si bağlandı.");
            DataSender.SHosGeldinMesaji(newClient.ConnectionID);
        }

        public static void SendDataTo(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.Bytes_Yaz(data);
            Sabitler.bagli_clients[connectionID].stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
            buffer.Dispose();
        }

        public static void SendDataToAll(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.Bytes_Yaz(data);

            foreach(Client kullanıcı in Sabitler.bagli_clients.Values)
            {
                if (kullanıcı != null && kullanıcı.ConnectionID != connectionID)
                    Sabitler.bagli_clients[kullanıcı.ConnectionID].stream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
            }

            buffer.Dispose();
        }
    }
}
