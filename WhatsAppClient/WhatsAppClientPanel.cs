using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppClient.Network;

namespace WhatsAppClient
{
    public partial class WhatsAppClientPanel : Form
    {
        public WhatsAppClientPanel()
        {
            InitializeComponent();
        }

        private void WhatsAppClientPanel_Load(object sender, EventArgs e)
        {
            NetworkManager.Start();
        }
    }
}
