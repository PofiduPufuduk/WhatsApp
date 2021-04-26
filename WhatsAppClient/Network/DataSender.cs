using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WhatsAppClient.Network
{

    class DataSender
    {
        public static void SH(byte[] data)
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
