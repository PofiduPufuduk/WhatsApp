using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppMe.Networking
{

    public enum ServerPackets
    {
        SHosgeldinMesaji = 1,
    }
    class DataSender
    {
        public static void SHosGeldinMesaji(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SHosgeldinMesaji);
            buffer.String_Yaz("Hoşgeldin");
            ClientManager.SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }
    }
}
