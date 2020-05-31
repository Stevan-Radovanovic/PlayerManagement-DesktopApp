using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public class GuiContStart
    {
        internal void Connect(FrmStart frmStart)
        {
            bool flag = Communication.Instance.Connect();
            if (!flag)
            {
                MessageBox.Show("The server is down!"); frmStart.Close();
            }
        }

        internal void clickRegister()
        {
            FrmRegister register = new FrmRegister();
            register.ShowDialog();
        }

        internal void clickLogin()
        {
            FrmLogin login = new FrmLogin();
            login.ShowDialog();
        }
    }
}
