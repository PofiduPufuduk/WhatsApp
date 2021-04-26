using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppClient.Network
{
    public static class NetworkManager
    {
        public static void Start()
        {
            ClientHandleData.PaketlemeyiBaslat();
            ClientTCP.NetworkBaslat();
        }
    }
}
