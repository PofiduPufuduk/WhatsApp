using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsAppClient.Network
{

    public enum ServerPackets
    {
        SHosgeldinMesaji = 1,
    }

    class DataReceiver
    {


        public static void HandleHosGeldinMesaji(byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            string mesaj = buffer.String_Oku();
            buffer.Dispose();
            MessageBox.Show(mesaj);
        }

    }
}
