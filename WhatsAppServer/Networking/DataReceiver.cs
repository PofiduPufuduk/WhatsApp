using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppMe.Networking
{
    public enum ClientPackets
    {
            SHosgeldinMesaji = 1,
    }

    class DataReceiver
    {

        public static void SHosGeldinMesaji(int connectionID )
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Int_Yaz((int)ServerPackets.SHosgeldinMesaji);
            buffer.String_Yaz("Hoş Geldin");
            ClientManager.SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }

    }
}
