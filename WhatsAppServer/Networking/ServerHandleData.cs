using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppMe.Networking
{
    static class ServerHandleData
    {
        public delegate void Packet(int connetionID, byte[] data);
        public static Dictionary<int, Packet> packets = new Dictionary<int, Packet>();


        public static void InitializePackes()
        {

        }

        public static void HandleData(int connectionID, byte[] data)
        {
            byte[] buffer = (byte[])data.Clone();
            int pLenght = 0;

            if (Sabitler.bagli_clients[connectionID].buffer == null)
                Sabitler.bagli_clients[connectionID].buffer = new ByteBuffer();

            Sabitler.bagli_clients[connectionID].buffer.Bytes_Yaz(buffer);
            if (Sabitler.bagli_clients[connectionID].buffer.Count() == 0)
            {
                Sabitler.bagli_clients[connectionID].buffer.Clear();
                return;
            }
            if (Sabitler.bagli_clients[connectionID].buffer.Length() >= 4)
            {
                pLenght = Sabitler.bagli_clients[connectionID].buffer.Int_Oku(false);
                if (pLenght <= 0)
                {
                    Sabitler.bagli_clients[connectionID].buffer.Clear();
                    return;
                }
            }

            while (pLenght > 0 & pLenght <= Sabitler.bagli_clients[connectionID].buffer.Length() -4)
            {
                if(pLenght <= Sabitler.bagli_clients[connectionID].buffer.Length() - 4)
                {
                    Sabitler.bagli_clients[connectionID].buffer.Int_Oku();
                    data = Sabitler.bagli_clients[connectionID].buffer.Bytes_Oku(pLenght);
                    HandleDataPackets(connectionID, data);
                }
                pLenght = 0;

                if (Sabitler.bagli_clients[connectionID].buffer.Length() >=4)
                {
                    pLenght = Sabitler.bagli_clients[connectionID].buffer.Int_Oku(false);

                    if(pLenght <= 0)
                    {
                        Sabitler.bagli_clients[connectionID].buffer.Clear();
                        return;
                    }
                }

                if (pLenght <= 1)
                    Sabitler.bagli_clients[connectionID].buffer.Clear();
            }

        }

        private static void HandleDataPackets(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            buffer.Dispose();
            if(packets.TryGetValue(packetID,out Packet packet))
            {
                packet.Invoke(connectionID, data);
            }
        }


    }
}
