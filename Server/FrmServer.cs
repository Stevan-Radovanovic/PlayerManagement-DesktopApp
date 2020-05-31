using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FrmServer : Form
    {
        private Server s = new Server();
        public GuiContServer cont = new GuiContServer();
        public Thread thr;

        public FrmServer()
        {
            InitializeComponent();
            cont.StartForm(btnStart,btnStop);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            cont.StartServer(s,btnStop,btnStart,thr);          
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cont.StopServer(s, btnStop, btnStart, thr);
        }
    }
}
