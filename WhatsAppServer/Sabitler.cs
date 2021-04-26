using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppMe.Networking;

namespace WhatsAppMe
{
    class Sabitler
    {
        public static Dictionary<int, Client> bagli_clients = new Dictionary<int, Client>();
        public static WhatsAppServerPanel WhatsAppPanel1 = ((WhatsAppServerPanel)Application.OpenForms.OfType<WhatsAppServerPanel>().SingleOrDefault());
    }
}
