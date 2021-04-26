using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsAppMe.Networking;

namespace WhatsAppMe
{
    public partial class WhatsAppServerPanel : Form
    {
        public WhatsAppServerPanel()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void başlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            General.Sunucuyu_Baslat();
        }
    }
}
