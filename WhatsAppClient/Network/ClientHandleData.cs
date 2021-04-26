using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppClient.Network
{
    static class ClientHandleData
    {
        public delegate void Packet(byte[] data);
        public static Dictionary<int, Packet> packets = new Dictionary<int, Packet>();

        public static void PaketlemeyiBaslat()
        {
            packets.Add((int)ServerPackets.SHosgeldinMesaji, DataReceiver.HandleHosGeldinMesaji);
        }

        private static ByteBuffer clientBuffer;
        public static void HandleData(byte[] data)
        {
            byte[] buffer = (byte[])data.Clone();
            int pLenght = 0;

            if (clientBuffer == null)
                clientBuffer = new ByteBuffer();

            clientBuffer.Bytes_Yaz(buffer);
            if (clientBuffer.Count() == 0)
            {
                clientBuffer.Clear();
                return;
            }
            if (clientBuffer.Length() >= 4)
            {
                pLenght = clientBuffer.Int_Oku(false);
                if (pLenght <= 0)
                {
                    clientBuffer.Clear();
                    return;
                }
            }

            while (pLenght > 0 & pLenght <= clientBuffer.Length() - 4)
            {
                if (pLenght <= clientBuffer.Length() - 4)
                {
                    clientBuffer.Int_Oku();
                    data = clientBuffer.Bytes_Oku(pLenght);
                    HandleDataPackets(data);
                }
                pLenght = 0;

                if (clientBuffer.Length() >= 4)
                {
                    pLenght = clientBuffer.Int_Oku(false);

                    if (pLenght <= 0)
                    {
                        clientBuffer.Clear();
                        return;
                    }
                }

                if (pLenght <= 1)
                    clientBuffer.Clear();
            }

        }

        public static void HandleDataPackets(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            buffer.Dispose();
            if (packets.TryGetValue(packetID, out Packet packet))
            {
                packet.Invoke(data);
            }
        }

    }
}
