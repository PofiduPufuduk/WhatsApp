using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppMe.Networking
{
    public static class General
    {
        public static void Sunucuyu_Baslat()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ServerTCP.InitializeNetwork();
            Sabitler.WhatsAppPanel1.listBox1.Items.Add("Sunucu başlatıldı" + "Başlatılma süresi"  + sw.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}
