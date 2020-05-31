using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public class GuiContServer
    {
        internal void StartForm(Button btnStart, Button btnStop)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        internal void StartServer(Server s, Button btnStop, Button btnStart,Thread thr)
        {

            if (s.StartServer())
            {
                s.kraj = false;
                thr = new Thread(s.BeOnTheLookout);
                thr.IsBackground = true;
                thr.Start();
                MessageBox.Show("The server has started working");
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            else
            {
                MessageBox.Show("There has been a problem with the server");
            }
        }

        internal void StopServer(Server s, Button btnStop, Button btnStart, Thread thr)
        {
            s.kraj = true;
            s.listeningSocket.Close();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            MessageBox.Show("The server has stopped working");
        }
    }
}
